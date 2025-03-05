using System.ComponentModel;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using SickLanguage.Core.AST;

namespace SickLanguage.Core {
    public class Parser {
        private List<Token> tokens;
        private int position = 0;

        public Parser(List<Token> tokens) {
            this.tokens = tokens;
        }

        private Token Get(int relative_position) {
            int position = this.position + relative_position;
            if (position >= tokens.Count) return new Token(TokenType.EOF);
            return tokens[position];
        }

        private bool Match(TokenType token_type) {
            Token current = Get(0);
            if (token_type != current.type) return false;
            position++;
            return true;
        }

        private bool LookMatch(int position, TokenType token_type) {
            return Get(position).type == token_type;
        }

        private Token Consume(TokenType token_type) {
            Token current = Get(0);
            if (token_type != current.type)
            throw new Exception($"A {token_type} was expected, but a {current.type} was received.");
            position++;
            return current;
        }

        private ValueExpression Value() {
            Token current = Get(0);
            if (Match(TokenType.NUMBER)) return new ValueExpression(float.Parse(current.value));
            else if (Match(TokenType.STRING)) return new ValueExpression(current.value);
            throw new Exception($"Unknown expression {current}.");
        }

        public CompoundStatement Parse() {
            CompoundStatement compound = new();
            while (!Match(TokenType.EOF)) {
                compound.AddStatement(Statement());
                Consume(TokenType.SEMICOLON);
            }
            return compound;
        }

        private Statement Statement() {
            if (Match(TokenType.PRINT)) return new PrintStatement(Expression());
            return AssignmentStatement();
        }

        private Statement AssignmentStatement() {
            if (Match(TokenType.VARIABLE)) {
                string variable = Consume(TokenType.KEYWORD).value;
                Consume(TokenType.ASSIGN);
                return new AssignmentStatement(variable, Expression());
            }
            throw new Exception($"Unknown statement {Get(0)}.");
        }

        private Expression Expression() => LogicalOr();

        private Expression LogicalOr() {
            Expression expression = LogicalAnd();

            while (true) {
                if (Match(TokenType.OR)) {
                    expression = new ConditionalExpression(Operator.OR, expression, LogicalAnd());
                    continue;
                }
                break;
            }

            return expression;
        }

        private Expression LogicalAnd() {
            Expression expression = Equality();

            while (true) {
                if (Match(TokenType.AND)) {
                    expression = new ConditionalExpression(Operator.AND, expression, Equality());
                    continue;
                }
                break;
            }

            return expression;
        }

        private Expression Equality() {
            Expression expression = Conditional();

            if (Match(TokenType.EQUALS))
            return new ConditionalExpression(Operator.EQUALS, expression, Conditional());
            if (Match(TokenType.REJECT_EQUALS))
            return new ConditionalExpression(Operator.REJECT_EQUALS, expression, Conditional());

            return expression;
        }

        private Expression Conditional() {
            Expression expression = Additive();

            while (true) {
                if (Match(TokenType.GREATER)) {
                    expression = new ConditionalExpression(Operator.GREATER, expression, Additive());
                }
                if (Match(TokenType.LESS)) {
                    expression = new ConditionalExpression(Operator.LESS, expression, Additive());
                }
                if (Match(TokenType.GREATER_EQUALS)) {
                    expression = new ConditionalExpression(Operator.GREATER_EQUALS, expression, Additive());
                }
                if (Match(TokenType.LESS_EQUALS)) {
                    expression = new ConditionalExpression(Operator.LESS_EQUALS, expression, Additive());
                }
                break;
            }

            return expression;
        }

        private Expression Additive() {
            Expression expression = Multiplicative();

            while (true) {
                if (Match(TokenType.PLUS)) {
                    expression = new BinaryExpression(Operator.PLUS, expression, Multiplicative());
                    continue;
                }
                if (Match(TokenType.MINUS)) {
                    expression = new BinaryExpression(Operator.MINUS, expression, Multiplicative());
                    continue;
                }
                break;
            }

            return expression;
        }

        private Expression Multiplicative() {
            Expression expression = Unary();

            while (true) {
                if (Match(TokenType.MULTIPLY)) {
                    expression = new BinaryExpression(Operator.MULTIPLY, expression, Unary());
                    continue;
                }
                if (Match(TokenType.DIVIDE)) {
                    expression = new BinaryExpression(Operator.DIVIDE, expression, Unary());
                    continue;
                }
                break;
            }

            return expression;
        }

        private Expression Unary() {
            if (Match(TokenType.PLUS)) {
                return Primary();
            }
            if (Match(TokenType.MINUS)) {
                return new UnaryExpression(Operator.MINUS, Primary());
            }
            return Primary();
        }

        private Expression Primary() {
            if (Match(TokenType.FUNCTION)) {
                Consume(TokenType.LEFT_ROUND_BRACKET);
                List<string> arguments = [];
                while (!Match(TokenType.RIGHT_ROUND_BRACKET)) {
                    arguments.Add(Consume(TokenType.KEYWORD).value);
                    Match(TokenType.COMMA);
                }
            }
            if (Match(TokenType.LEFT_ROUND_BRACKET)) {
                Expression expression = Expression();
                Match(TokenType.RIGHT_ROUND_BRACKET);
                return expression;
            }
            return Variable();
        }

        private Expression Variable() {
            if (LookMatch(0, TokenType.KEYWORD) && LookMatch(1, TokenType.LEFT_ROUND_BRACKET))
            return Function(new ValueExpression(Consume(TokenType.KEYWORD).value));
            Expression? qualified_name = QualifiedName();
            if (qualified_name != null) {
                if (LookMatch(0, TokenType.LEFT_ROUND_BRACKET))
                return Function(qualified_name);
                return qualified_name;
            }
            return Value();
        }

        private Expression? QualifiedName() {
            Token current = Get(0);
            if (Match(TokenType.KEYWORD)) {
                return new VariableExpression(current.value);
            }
            return null;
        }

        private Expression Function(Expression qualified_name) {
            return new ValueExpression(0.0f);
        }
    }
}

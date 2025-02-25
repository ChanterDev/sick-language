using System.Data;
using SickLanguage.Core.AST;

namespace SickLanguage.Core {
    public class Parser {
        private List<Token> tokens;
        private int position = 0;

        public Parser(List<Token> tokens) {
            this.tokens = tokens;
        }

        private Token? match(params TokenType[] expected) {
            if (position < tokens.Count) {
                Token token = tokens[position];
                foreach (TokenType type in expected) {
                    if (token.getType() == type) {
                        position++;
                        return token;
                    }
                }
            }
            return null;
        }

        private Token require(params TokenType[] expected) {
            Token? token = match(expected);
            if (token == null) throw new SyntaxErrorException($"Expected {expected[0]} at {position}");
            return token;
        }

        private ExpressionNode parsePrint() {
            Token? operatorPrint = match(TokenType.PRINT);
            if (operatorPrint != null) {
                return new UnarOperationNode(operatorPrint, parseFormula());
            }
            throw new SyntaxErrorException($"Expected {TokenType.PRINT} at {position}");
        }

        private ExpressionNode parseVariableOrNumber() {
            Token? number = match(TokenType.NUMBER);
            if (number != null) {
                return new NumberNode(number);
            }
            Token? String = match(TokenType.STRING);
            if (String != null) {
                return new StringNode(String);
            }
            Token? variable = match(TokenType.VARIABLE, TokenType.KEYWORD);
            if (variable != null) {
                return new VariableNode(variable);
            }
            throw new SyntaxErrorException($"Expected {TokenType.NUMBER}, {TokenType.VARIABLE}, {TokenType.KEYWORD} or {TokenType.STRING} at {position}");
        }

        private ExpressionNode parseParentheses() {
            if (match(TokenType.LPAREN) != null) {
                ExpressionNode node = parseFormula();
                require(TokenType.RPAREN);
                return node;
            } else {
                return parseVariableOrNumber();
            }
        }

        private ExpressionNode parseFormula() {
            ExpressionNode leftNode = parseParentheses();
            Token? Operator = match(TokenType.PLUS, TokenType.MINUS, TokenType.MULTIPLY, TokenType.DIVIDE);
            while (Operator != null) {
                ExpressionNode rightNode = parseParentheses();
                leftNode = new BinOperationNode(Operator, leftNode, rightNode);
                Operator = match(TokenType.PLUS, TokenType.MINUS, TokenType.MULTIPLY, TokenType.DIVIDE);
            }
            return leftNode;
        }

        private ExpressionNode parseExpression() {
            if (match(TokenType.VARIABLE) == null) {
                return parsePrint();
            }
            position--;
            ExpressionNode variableNode = parseVariableOrNumber();
            Token? assignOperator = match(TokenType.ASSIGN);
            if (assignOperator != null) {
                ExpressionNode rightNode = parseFormula();
                BinOperationNode binaryNode = new(assignOperator, variableNode, rightNode);
                return binaryNode;
            }
            throw new SyntaxErrorException($"Expected {TokenType.ASSIGN} after variable declaration at {position}");
        }

        public StatementsNode parse() {
            StatementsNode root = new();
            while (position < tokens.Count) {
                ExpressionNode node = parseExpression();
                require(TokenType.SEMICOLON);
                root.addNode(node);
            }
            return root;
        }
    }
}

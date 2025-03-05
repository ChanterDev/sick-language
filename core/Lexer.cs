using System.Text;

namespace SickLanguage.Core {
    public class Lexer {
        private List<Token> tokens = [];
        private string code;
        private int position = 0;
        private static string OPERATOR_CHARS = "+-*/(){}[]=><!.,:;";

        public Lexer(string code) {
            this.code = code;
        }

        private char Peek(int relative_position) {
            int position = this.position + relative_position;
            if (position >= code.Length) return '\0';
            return code[position];
        }

        private char Next() {
            position++;
            return Peek(0);
        }

        private void AddToken(TokenType token_type, string value) => tokens.Add(new Token(token_type, value));
        private void AddToken(TokenType token_type) => tokens.Add(new Token(token_type));

        private void TokenizeWord() {
            StringBuilder buffer = new();
            char current = Peek(0);
            while (true) {
                if (!char.IsLetterOrDigit(current)) break;
                buffer.Append(current);
                current = Next();
            }

            switch (buffer.ToString()) {
                case "print":
                    AddToken(TokenType.PRINT);
                    break;
                case "if":
                    AddToken(TokenType.IF);
                    break;
                case "elif":
                    AddToken(TokenType.ELIF);
                    break;
                case "else":
                    AddToken(TokenType.ELSE);
                    break;
                case "for":
                    AddToken(TokenType.FOR);
                    break;
                case "while":
                    AddToken(TokenType.WHILE);
                    break;
                case "break":
                    AddToken(TokenType.BREAK);
                    break;
                case "func":
                    AddToken(TokenType.FUNCTION);
                    break;
                case "var":
                    AddToken(TokenType.VARIABLE);
                    break;
                default:
                    AddToken(TokenType.KEYWORD, buffer.ToString());
                    break;
            }
        }

        private void TokenizeString() {
            StringBuilder buffer = new();
            char current = Next();
            while (true) {
                if (current == '\\') {
                    current = Next();
                    switch (current) {
                        case '"':
                            buffer.Append('"');
                            continue;
                        case 'n':
                            buffer.Append('\n');
                            continue;
                        case 't':
                            buffer.Append('\t');
                            continue;
                    }
                    buffer.Append('\\');
                    continue;
                }
                if (current == '"') break;
                buffer.Append(current);
                current = Next();
            }
            Next();

            AddToken(TokenType.STRING, buffer.ToString());
        }

        private void TokenizeNumber() {
            StringBuilder buffer = new();
            char current = Peek(0);
            while (true) {
                if (!char.IsDigit(current)) break;
                buffer.Append(current);
                current = Next();
            }
            AddToken(TokenType.NUMBER, buffer.ToString());
        }

        private void TokenizeOperator() {
            StringBuilder buffer = new();
            char current = Peek(0);
            while (true) {
                switch (buffer.ToString() + current) {
                    case "+":
                        AddToken(TokenType.PLUS);
                        Next();
                        return;
                    case "-":
                        AddToken(TokenType.MINUS);
                        Next();
                        return;
                    case "*":
                        AddToken(TokenType.MULTIPLY);
                        Next();
                        return;
                    case "/":
                        AddToken(TokenType.DIVIDE);
                        Next();
                        return;
                    case "=":
                        AddToken(TokenType.ASSIGN);
                        Next();
                        return;
                    case ":":
                        AddToken(TokenType.COLON);
                        Next();
                        return;
                    case ";":
                        AddToken(TokenType.SEMICOLON);
                        Next();
                        return;
                    case "(":
                        AddToken(TokenType.LEFT_ROUND_BRACKET);
                        Next();
                        return;
                    case ")":
                        AddToken(TokenType.RIGHT_ROUND_BRACKET);
                        Next();
                        return;
                    case "{":
                        AddToken(TokenType.LEFT_CURLY_BRACKET);
                        Next();
                        return;
                    case "}":
                        AddToken(TokenType.RIGHT_CURLY_BRACKET);
                        Next();
                        return;
                    case "[":
                        AddToken(TokenType.LEFT_SQUARE_BRACKET);
                        Next();
                        return;
                    case "]":
                        AddToken(TokenType.RIGHT_SQUARE_BRACKET);
                        Next();
                        return;
                    case "!":
                        AddToken(TokenType.REJECT);
                        Next();
                        return;
                    case "!=":
                        AddToken(TokenType.REJECT_EQUALS);
                        Next();
                        return;
                    case ">":
                        AddToken(TokenType.GREATER);
                        Next();
                        return;
                    case "<":
                        AddToken(TokenType.LESS);
                        Next();
                        return;
                    case ">=":
                        AddToken(TokenType.GREATER_EQUALS);
                        Next();
                        return;
                    case "<=":
                        AddToken(TokenType.LESS_EQUALS);
                        Next();
                        return;
                    case ".":
                        AddToken(TokenType.DOT);
                        Next();
                        return;
                    case ",":
                        AddToken(TokenType.COMMA);
                        Next();
                        return;
                }

                buffer.Append(current);
                current = Next();
            }
        }

        public List<Token> Tokenize() {
            while (position < code.Length) {
                char current = Peek(0);
                if (char.IsDigit(current)) TokenizeNumber();
                else if (char.IsLetter(current)) TokenizeWord();
                else if (current == '"') TokenizeString();
                else if (OPERATOR_CHARS.Contains(current)) TokenizeOperator();
                else Next();
            }

            return tokens;
        }
    }
}

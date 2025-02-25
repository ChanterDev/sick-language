using System.Text;

namespace SickLanguage.Core {
    public class Lexer {
        private List<Token> tokens = [];
        private string code;
        private int position = 0;
        private char? character;


        public Lexer(string code) {
            this.code = code;
            character = code[position];
        }

        private void nextCharacter() {
            position++;
            try { character = code[position]; } catch (IndexOutOfRangeException) { character = null; }
        }

        private void pushToken(Token token) {
            tokens.Add(token);
            nextCharacter();
        }

        private string variableBuilder() {
            StringBuilder builder = new();

            nextCharacter();
            while (character != null && char.IsLetterOrDigit((char) character)) {
                builder.Append(character);
                nextCharacter();
            }
            return builder.ToString();
        }

        private void keywordBuilder() {
            StringBuilder builder = new();

            while (character != null && char.IsLetterOrDigit((char) character)) {
                builder.Append(character);
                nextCharacter();
            }
            string result = builder.ToString();
            if (result == "var") tokens.Add(new Token(TokenType.VARIABLE, variableBuilder()));
            else if (result == "if") tokens.Add(new Token(TokenType.IF));
            else if (result == "elif") tokens.Add(new Token(TokenType.ELIF));
            else if (result == "else") tokens.Add(new Token(TokenType.ELSE));
            else if (result == "for") tokens.Add(new Token(TokenType.FOR));
            else if (result == "while") tokens.Add(new Token(TokenType.WHILE));
            else if (result == "true") tokens.Add(new Token(TokenType.BOOLEAN, "true"));
            else if (result == "false") tokens.Add(new Token(TokenType.BOOLEAN, "false"));
            else if (result == "not") tokens.Add(new Token(TokenType.NOT));
            else if (result == "print") tokens.Add(new Token(TokenType.PRINT));
            else tokens.Add(new Token(TokenType.KEYWORD, result));
        }

        private string stringBuilder() {
            StringBuilder builder = new();

            nextCharacter();
            while (character != null && character != '"') {
                builder.Append(character);
                nextCharacter();
            }
            nextCharacter();

            return builder.ToString();
        }

        private string numberBuilder() {
            StringBuilder builder = new();

            while (character != null && char.IsDigit((char) character)) {
                builder.Append(character);
                nextCharacter();
            }

            return builder.ToString();
        }

        private void equalsAssignBuilder() {
            nextCharacter();
            if (character == '=') {
                tokens.Add(new Token(TokenType.EQUALS));
                nextCharacter();
            } else tokens.Add(new Token(TokenType.ASSIGN));
        }

        private void moreEqualsBuilder() {
            nextCharacter();
            if (character == '=') {
                tokens.Add(new Token(TokenType.MORE_OR_EQUALS));
                nextCharacter();
            } else tokens.Add(new Token(TokenType.MORE));
        }

        private void lessEqualsBuilder() {
            nextCharacter();
            if (character == '=') {
                tokens.Add(new Token(TokenType.LESS_OR_EQUALS));
                nextCharacter();
            } else tokens.Add(new Token(TokenType.LESS));
        }

        public List<Token> tokenize() {
            while (character != null) {
                if (char.IsLetter((char) character)) keywordBuilder();
                else if (char.IsDigit((char) character)) tokens.Add(new Token(TokenType.NUMBER, numberBuilder()));
                else if (character == '"') tokens.Add(new Token(TokenType.STRING, stringBuilder()));
                else if (character == '=') equalsAssignBuilder();
                else if (character == '+') pushToken(new Token(TokenType.PLUS));
                else if (character == '-') pushToken(new Token(TokenType.MINUS));
                else if (character == '*') pushToken(new Token(TokenType.MULTIPLY));
                else if (character == '/') pushToken(new Token(TokenType.DIVIDE));
                else if (character == '>') moreEqualsBuilder();
                else if (character == '<') lessEqualsBuilder();
                else if (character == '(') pushToken(new Token(TokenType.LPAREN));
                else if (character == ')') pushToken(new Token(TokenType.RPAREN));
                else if (character == ':') pushToken(new Token(TokenType.COLON));
                else if (character == ';') pushToken(new Token(TokenType.SEMICOLON));
                else if (character == ',') pushToken(new Token(TokenType.COMMA));
                else nextCharacter();
            }

            //tokens.Add(new Token(TokenType.EOF));

            return tokens;
        }
    }
}

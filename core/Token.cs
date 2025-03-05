namespace SickLanguage.Core {
    public class Token {
        public TokenType type { get; }
        public string value { get; } = "null";

        public Token(TokenType type, string value) {
            this.type = type;
            this.value = value;
        }

        public Token(TokenType type) {
            this.type = type;
        }

        override public string ToString() {
            return $"{type}: {value};";
        }
    }
}
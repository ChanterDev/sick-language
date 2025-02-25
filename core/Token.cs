namespace SickLanguage.Core {
    public class Token {
        private TokenType type;
        private string value;

        public Token(TokenType type, string value) {
            this.type = type;
            this.value = value;
        }

        public Token(TokenType type) {
            this.type = type;
            value = "null";
        }

        public string parseString() {
            return value;
        }

        public int parseInt() {
            return int.Parse(value);
        }

        public float parseFloat() {
            return float.Parse(value);
        }

        public bool parseBool() {
            return bool.Parse(value);
        }
        
        public TokenType getType() => type;

        override public string ToString() {
            return $"{type}: {value};";
        }
    }
}
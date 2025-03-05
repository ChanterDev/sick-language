namespace SickLanguage.Core {
    public enum TokenType {
        KEYWORD,
        STRING,
        NUMBER,
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        EQUALS,
        GREATER,
        LESS,
        GREATER_EQUALS,
        LESS_EQUALS,
        REJECT,
        REJECT_EQUALS,
        ASSIGN,
        OR,
        AND,
        VARIABLE,
        FUNCTION,
        RETURN,
        LEFT_ROUND_BRACKET,
        RIGHT_ROUND_BRACKET,
        LEFT_CURLY_BRACKET,
        RIGHT_CURLY_BRACKET,
        LEFT_SQUARE_BRACKET,
        RIGHT_SQUARE_BRACKET,
        IF,
        ELIF,
        ELSE,
        FOR,
        WHILE,
        BREAK,
        COLON,
        SEMICOLON,
        COMMA,
        DOT,
        PRINT,
        EOF
    }
}
using SickLanguage.Core;
using SickLanguage.Core.AST;

public class Sick {
    public static void Main(string[] args) {
        string content = File.ReadAllText(args[0]);
        Lexer lexer = new(content);
        List<Token> tokens = lexer.tokenize();
        Parser parser = new(tokens);
        StatementsNode node = parser.parse();
        Interpreter interpreter = new(node);
        interpreter.interpret();
    }
}


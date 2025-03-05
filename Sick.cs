using SickLanguage.Core;
using SickLanguage.Core.AST;

public class Sick {
    public static void Main(string[] args) {
        string content = File.ReadAllText(args[0]);
        Lexer lexer = new(content);
        List<Token> tokens = lexer.Tokenize();
        /*
        foreach (Token token in tokens) {
            Console.WriteLine(token);
        }
        */
        Parser parser = new(tokens);
        CompoundStatement compound = parser.Parse();
        compound.Execute();
    }
}


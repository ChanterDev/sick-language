namespace SickLanguage.Core.AST {
    public class PrintStatement : Statement {
        Expression expression;

        public PrintStatement(Expression expression) {
            this.expression = expression;
        }

        public void Execute() {
            Console.WriteLine(expression.Eval().AsString());
        }
    }
}
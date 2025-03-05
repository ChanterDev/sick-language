using SickLanguage.Core.Lib;

namespace SickLanguage.Core.AST {
    public class AssignmentStatement : Statement {
        private string variable;
        private Expression expression;

        public AssignmentStatement(string variable, Expression expression) {
            this.variable = variable;
            this.expression = expression;
        }

        public void Execute() {
            Stack.Set(variable, expression.Eval());
        }
    }
}
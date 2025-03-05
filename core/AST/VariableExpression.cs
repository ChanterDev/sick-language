using SickLanguage.Core.Lib;

namespace SickLanguage.Core.AST {
    public class VariableExpression : Expression {
        string name;

        public VariableExpression(string name) {
            this.name = name;
        }

        public Value Eval() {
            if (!Stack.IsExists(name))
            throw new Exception($"Unknown variable: {name}");
            return Stack.Get(name);
        }
    }
}
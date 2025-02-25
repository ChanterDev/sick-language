namespace SickLanguage.Core.AST {
    public class VariableNode : ExpressionNode {
        private Token variable;

        public VariableNode(Token variable) {
            this.variable = variable;
        }

        public override string getVariableName() {
            return variable.parseString();
        }
    }
}
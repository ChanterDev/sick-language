namespace SickLanguage.Core.AST {
    public class StringNode : ExpressionNode {
        private Token String;

        public StringNode(Token String) {
            this.String = String;
        }

        public override string getString() {
            return String.parseString();
        }
    }
}
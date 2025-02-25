namespace SickLanguage.Core.AST {
    public class NumberNode : ExpressionNode {
        private Token number;

        public NumberNode(Token number) {
            this.number = number;
        }

        public override int getNumber() {
            return number.parseInt();
        }
    }
}
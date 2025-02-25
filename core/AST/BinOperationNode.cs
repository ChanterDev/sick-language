namespace SickLanguage.Core.AST {
    public class BinOperationNode : ExpressionNode {
        private Token Operator;
        private ExpressionNode leftNode;
        private ExpressionNode rightNode;

        public BinOperationNode(Token Operator, ExpressionNode leftNode, ExpressionNode rightNode) {
            this.Operator = Operator;
            this.leftNode = leftNode;
            this.rightNode = rightNode;
        }

        public override TokenType getOperator() {
            return Operator.getType();
        }
        public override ExpressionNode getLeft() {
            return leftNode;
        }
        public override ExpressionNode getRight() {
            return rightNode;
        }
    }
}
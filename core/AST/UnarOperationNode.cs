namespace SickLanguage.Core.AST {
    public class UnarOperationNode : ExpressionNode {
        private Token Operator;
        private ExpressionNode operand;

        public UnarOperationNode(Token Operator, ExpressionNode operand) {
            this.Operator = Operator;
            this.operand = operand;
        }

        override public TokenType getOperator() {
            return Operator.getType();
        }
        override public ExpressionNode getOperand() {
            return operand;
        }
    }
}
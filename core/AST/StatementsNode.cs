namespace SickLanguage.Core.AST {
    public class StatementsNode : ExpressionNode {
        private List<ExpressionNode> statements = [];

        public void addNode(ExpressionNode node) {
            statements.Add(node);
        }

        public override List<ExpressionNode> getStatements() {
            return statements;
        }
    }
}
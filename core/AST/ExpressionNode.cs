
namespace SickLanguage.Core.AST {
    public class ExpressionNode {
        virtual public TokenType getOperator() {
            return TokenType.KEYWORD;
        }
        virtual public ExpressionNode getLeft() {
            return new ExpressionNode();
        }
        virtual public ExpressionNode getRight() {
            return new ExpressionNode();
        }
        virtual public ExpressionNode getOperand() {
            return new ExpressionNode();
        }
        virtual public int getNumber() {
            return 0;
        }
        virtual public string getString() {
            return "";
        }
        virtual public string getVariableName() {
            return "";
        }
        virtual public List<ExpressionNode> getStatements() {
            return [];
        }
    }
}
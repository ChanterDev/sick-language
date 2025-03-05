using System.IO.Pipelines;
using SickLanguage.Core.Lib;

namespace SickLanguage.Core.AST {
    public class UnaryExpression : Expression {
        Expression expression;
        Operator operation;

        public UnaryExpression(Operator operation, Expression expression) {
            this.expression = expression;
            this.operation = operation;
        }

        public Value Eval() {
            switch (operation) {
                case Operator.PLUS:
                    return expression.Eval();
                case Operator.MINUS:
                    return new NumberValue(-expression.Eval().AsNumber());
            }
            throw new Exception($"Unexpected operator: {operation}");
        }
    }
}
using System.IO.Pipelines;
using SickLanguage.Core.Lib;

namespace SickLanguage.Core.AST {
    public class ConditionalExpression : Expression {
        Expression expression1;
        Expression expression2;
        Operator operation;

        public ConditionalExpression(Operator operation, Expression expression1, Expression expression2) {
            this.expression1 = expression1;
            this.expression2 = expression2;
            this.operation = operation;
        }

        public Value Eval() {
            Value value1 = expression1.Eval();
            Value value2 = expression2.Eval();

            dynamic value3;
            dynamic value4;

            if (value1 is StringValue) {
                value3 = value1.AsString();
                value4 = value2.AsString();
            } else if (value1 is NumberValue) {
                value3 = value1.AsNumber();
                value4 = value2.AsNumber();
            } else {
                value3 = value1.AsBoolean();
                value4 = value2.AsBoolean();
            }

            bool result = true;
            switch (operation) {
                case Operator.EQUALS:
                    result = value3 == value4;
                    break;
                case Operator.GREATER:
                    result = value3 > value4;
                    break;
                case Operator.LESS:
                    result = value3 < value4;
                    break;
                case Operator.GREATER_EQUALS:
                    result = value3 >= value4;
                    break;
                case Operator.LESS_EQUALS:
                    result = value3 <= value4;
                    break;
                case Operator.REJECT_EQUALS:
                    result = value3 != value4;
                    break;
                case Operator.AND:
                    result = value3 && value4;
                    break;
                case Operator.OR:
                    result = value3 || value4;
                    break;
            }
            return new BooleanValue(result);
        }
    }
}
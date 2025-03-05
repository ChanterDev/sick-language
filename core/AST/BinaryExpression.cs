using SickLanguage.Core.Lib;

namespace SickLanguage.Core.AST {
    public class BinaryExpression : Expression {
        Expression expression1;
        Expression expression2;
        Operator operation;

        public BinaryExpression(Operator operation, Expression expression1, Expression expression2) {
            this.expression1 = expression1;
            this.expression2 = expression2;
            this.operation = operation;
        }

        public Value Eval() {
            Value value1 = expression1.Eval();
            Value value2 = expression2.Eval();

            if (value1 is StringValue) {
                string str = value1.AsString();
                switch (operation) {
                    case Operator.PLUS:
                        return new StringValue(str + value2.AsString());
                }
            }

            float number1 = value1.AsNumber();
            float number2 = value2.AsNumber();
            switch (operation) {
                case Operator.PLUS:
                    return new NumberValue(number1 + number2);
                case Operator.MINUS:
                    return new NumberValue(number1 - number2);
                case Operator.MULTIPLY:
                    return new NumberValue(number1 * number2);
                case Operator.DIVIDE:
                    return new NumberValue(number1 / number2);
            }
            return new NumberValue(0.0f);
        }
    }
}
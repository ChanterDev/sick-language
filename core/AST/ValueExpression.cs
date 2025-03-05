using SickLanguage.Core.Lib;

namespace SickLanguage.Core.AST {
    public class ValueExpression : Expression {
        Value value;

        public ValueExpression(dynamic value) {
            if (value is float) this.value = new NumberValue(value);
            else if (value is string) this.value = new StringValue(value);
            else this.value = new NullValue();
        }

        public Value Eval() {
            return value;
        }
    }
}
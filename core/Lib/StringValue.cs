using System.Linq.Expressions;

namespace SickLanguage.Core.Lib {
    public class StringValue : Value {
        private string value = "";

        public StringValue(string value) {
            this.value = value;
        }

        public float AsNumber() {
            try {
                return float.Parse(value);
            } catch {
                throw new Exception("Cannot convert string to number.");
            }
        }

        public string AsString() {
            return value;
        }

        public bool AsBoolean() {
            return value != "";
        }
    }
}
namespace SickLanguage.Core.Lib {
    public class NumberValue : Value {
        private float value;

        public NumberValue(float value) {
            this.value = value;
        }

        public float AsNumber() {
            return value;
        }

        public string AsString() {
            return value.ToString();
        }

        public bool AsBoolean() {
            if (value >= 1.0f) return true;
            else return false;
        }
    }
}
namespace SickLanguage.Core.Lib {
    public class BooleanValue : Value {
        private bool value;

        public BooleanValue(bool value) {
            this.value = value;
        }

        public float AsNumber() {
            return (float)Convert.ToDouble(value);
        }

        public string AsString() {
            return value.ToString();
        }
        
        public bool AsBoolean() {
            return value;
        }
    }
}
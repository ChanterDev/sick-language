namespace SickLanguage.Core.Lib {
    public class NullValue : Value {
        public float AsNumber() {
            return 0.0f;
        }

        public string AsString() {
            return "";
        }

        public bool AsBoolean() {
            return false;
        }
    }
}
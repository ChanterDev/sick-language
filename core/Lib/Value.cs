namespace SickLanguage.Core.Lib {
    public interface Value {
        public float AsNumber();
        public string AsString();
        public bool AsBoolean();
    }
}
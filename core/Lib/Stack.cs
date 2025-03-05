namespace SickLanguage.Core.Lib {
    public static class Stack {
        private static Dictionary<string, Value> stack = [];

        public static bool IsExists(string key) {
            return stack.ContainsKey(key);
        }

        public static Value Get(string key) {
            return stack[key];
        }

        public static void Set(string key, Value value) {
            stack.Add(key, value);
        }
    }
}
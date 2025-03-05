using SickLanguage.Core.Lib;

namespace SickLanguage.Core.AST {
    public interface Expression {
        public Value Eval();
    }
}
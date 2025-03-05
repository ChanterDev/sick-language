namespace SickLanguage.Core.AST {
    public class CompoundStatement : Statement {
        private List<Statement> statements = [];

        public void AddStatement(Statement statement) {
            statements.Add(statement);
        }

        public void Execute() {
            foreach (Statement statement in statements) {
                statement.Execute();
            }
        }
    }
}
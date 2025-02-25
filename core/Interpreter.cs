using SickLanguage.Core.AST;

namespace SickLanguage.Core {
    public class Interpreter {
        StatementsNode node;
        Dictionary<string, dynamic> scope = [];

        public Interpreter(StatementsNode node) {
            this.node = node;
        }

        private dynamic run(ExpressionNode node) {
            if (node is NumberNode) {
                return node.getNumber();
            }
            if (node is StringNode) {
                return node.getString();
            }
            if (node is UnarOperationNode) {
                switch (node.getOperator()) {
                    case TokenType.PRINT:
                        Console.WriteLine(run(node.getOperand()));
                        return 0;
                }
            }
            if (node is BinOperationNode) {
                if (!(node.getLeft() is NumberNode) || !(node.getRight() is NumberNode))
                throw new Exception($"Arithmetic operations can be performed only on a {TokenType.NUMBER}s");
                switch (node.getOperator()) {
                    case TokenType.PLUS:
                        return run(node.getLeft()) + run(node.getRight());
                    case TokenType.MINUS:
                        return run(node.getLeft()) - run(node.getRight());
                    case TokenType.MULTIPLY:
                        return run(node.getLeft()) * run(node.getRight());
                    case TokenType.DIVIDE:
                        return run(node.getLeft()) / run(node.getRight());
                    case TokenType.ASSIGN:
                        dynamic result = run(node.getRight());
                        VariableNode variableNode = (VariableNode) node.getLeft();
                        scope.Add(variableNode.getVariableName(), result);
                        return result;
                }
            }
            if (node is VariableNode) {
                if (scope.TryGetValue(node.getVariableName(), out _)) {
                    return scope[node.getVariableName()];
                } else {
                    throw new Exception($"Variable {node.getVariableName()} does not exist in current scope");
                }
            }
            throw new Exception("Error");
        }

        public void interpret() {
            List<ExpressionNode> statements = node.getStatements();
            foreach (ExpressionNode statement in statements) {
                run(statement);
            }
        }
    }
}
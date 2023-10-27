// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class for a node that performs an operation on two subtrees.
    /// </summary>
    public class OperatorNode : ExpressionTreeNode
    {
        // Operators and their precedence, lower numbers come first, definition must be implemented in Evaluate()
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "It is.")]
        public static readonly Dictionary<char, int> Operators = new Dictionary<char, int>
        {
            { '+', 3 },
            { '-', 3 },
            { '*', 2 },
            { '/', 2 },
            { '%', 2 },
            { '^', 1 },
        };

        private readonly char op;                                                   // Operator type
        private ExpressionTreeNode left;                                            // Left subtree
        private ExpressionTreeNode right;                                           // Right subtree
        private bool evaluated = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="op">The type of operator.</param>
        /// <param name="left">Left subtree.</param>
        /// <param name="right">Right subtree.</param>
        public OperatorNode(char op, ExpressionTreeNode left, ExpressionTreeNode right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }

        /// <summary>
        /// Calculate the value of this node, set it, and return it.
        /// </summary>
        /// <returns>Evaluated value of the left and right subtrees.</returns>
        /// <exception cref="DivideByZeroException">Divide by zero exception.</exception>
        public override double Evaluate()
        {
            // Only perform computation if the tree has not been evaluated yet
            if (this.evaluated == false)
            {
                switch (this.op)
                {
                    case '+':
                        this.value = this.left.Evaluate() + this.right.Evaluate();
                        break;
                    case '-':
                        this.value = this.left.Evaluate() - this.right.Evaluate();
                        break;
                    case '*':
                        this.value = this.left.Evaluate() * this.right.Evaluate();
                        break;
                    case '/':
                        if (this.right.Evaluate() != 0)
                        {
                            this.value = this.left.Evaluate() / this.right.Evaluate();
                            break;
                        }

                        throw new DivideByZeroException();
                    case '%':
                        if (this.right.Evaluate() != 0)
                        {
                            this.value = this.left.Evaluate() % this.right.Evaluate();
                            break;
                        }

                        throw new DivideByZeroException();
                    case '^':
                        this.value = Math.Pow(this.left.Evaluate(), this.right.Evaluate());
                        break;
                }

                // The operation has new been evaluated and does not need to be done again
                this.evaluated = true;
            }

            return this.value;
        }
    }
}
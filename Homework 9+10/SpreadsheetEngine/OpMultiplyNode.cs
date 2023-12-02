// <copyright file="OpMultiplyNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class for a node that performs an operation on two subtrees.
    /// </summary>
    public class OpMultiplyNode : OperatorNode
    {
        static OpMultiplyNode()
        {
            Operator = '*';
            Precedence = 2;
        }

        /// <summary>
        /// Gets or sets the operator character.
        /// </summary>
        public static new char Operator { get; set; }

        /// <summary>
        /// Gets or sets the operator precedence, 1 = highest precedence, 2 = second highest, etc.
        /// </summary>
        public static new int Precedence { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpMultiplyNode"/> class.
        /// </summary>
        /// <param name="left">Left subtree.</param>
        /// <param name="right">Right subtree.</param>
        public OpMultiplyNode(ExpressionTreeNode left, ExpressionTreeNode right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Calculate the value of this node, set it, and return it.
        /// </summary>
        /// <returns>Evaluated value of the left and right subtrees.</returns>
        public override double Evaluate()
        {
            // Only perform computation if the tree has not been evaluated yet
            if (this.evaluated == false)
            {
                this.value = this.left.Evaluate() * this.right.Evaluate();
                this.evaluated = true;
            }

            return this.value;
        }
    }
}
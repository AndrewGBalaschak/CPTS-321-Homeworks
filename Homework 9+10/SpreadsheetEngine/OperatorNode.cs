// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Abstract class for a node that performs an operation on two subtrees.
    /// </summary>
    public abstract class OperatorNode : ExpressionTreeNode
    {
        protected static int precedence;                                            // Precedence level for operator
        protected ExpressionTreeNode left;                                          // Left subtree
        protected ExpressionTreeNode right;                                         // Right subtree
        protected bool evaluated = false;                                           // True if node has been evaluated previously

        /// <summary>
        /// Gets or sets the operator character.
        /// </summary>
        public static char Operator { get; set; }

        /// <summary>
        /// Gets or sets the operator precedence, 1 = highest precedence, 2 = second highest, etc.
        /// </summary>
        public static int Precedence { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="left">Left subtree.</param>
        /// <param name="right">Right subtree.</param>
        public OperatorNode(ExpressionTreeNode left, ExpressionTreeNode right)
        {
            this.left = left;
            this.right = right;
        }
    }
}
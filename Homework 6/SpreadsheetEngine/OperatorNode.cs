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
        protected ExpressionTreeNode left;                                          // Left subtree
        protected ExpressionTreeNode right;                                         // Right subtree
        protected bool evaluated = false;                                           // True if node has been evaluated previously

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
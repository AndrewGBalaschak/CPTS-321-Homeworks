// <copyright file="ExpressionTreeNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Abstract class for a node.
    /// </summary>
    public abstract class ExpressionTreeNode
    {
        internal double value;                          // Value of the node

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTreeNode"/> class.
        /// </summary>
        public ExpressionTreeNode()
        {
        }

        /// <summary>
        /// Returns the value of the node.
        /// </summary>
        /// <returns>The value of the node.</returns>
        public abstract double Evaluate();
    }
}
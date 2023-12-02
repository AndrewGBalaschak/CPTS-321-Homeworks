// <copyright file="ConstantNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class for a node that has a constant value.
    /// </summary>
    public class ConstantNode : ExpressionTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value">The value of the constant.</param>
        public ConstantNode(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Returns the value of the constant.
        /// </summary>
        /// <returns>The value of the constant.</returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
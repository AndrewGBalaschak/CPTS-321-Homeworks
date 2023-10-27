﻿// <copyright file="OpSubtractNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class for a node that performs an operation on two subtrees.
    /// </summary>
    public class OpSubtractNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpSubtractNode"/> class.
        /// </summary>
        /// <param name="left">Left subtree.</param>
        /// <param name="right">Right subtree.</param>
        public OpSubtractNode(ExpressionTreeNode left, ExpressionTreeNode right)
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
                this.value = this.left.Evaluate() - this.right.Evaluate();
                this.evaluated = true;
            }

            return this.value;
        }
    }
}
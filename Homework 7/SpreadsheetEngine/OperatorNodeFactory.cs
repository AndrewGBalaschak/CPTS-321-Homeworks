// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// Factory generator for operator nodes.
    /// </summary>
    public static class OperatorNodeFactory
    {
        // Operators and their precedence, lower numbers come first
        public static readonly Dictionary<char, int> Operators = new Dictionary<char, int>
            {
                { '+', 3 },
                { '-', 3 },
                { '*', 2 },
                { '/', 2 },
                { '%', 2 },
                { '^', 1 },
            };

        /// <summary>
        /// Returns an operator node of the specified type.
        /// </summary>
        /// <param name="type">Operator character.</param>
        /// <param name="left">Left subtree.</param>
        /// <param name="right">Right subtree.</param>
        /// <returns>Returns a new OperatorNode of the type specified.</returns>
        /// <exception cref="NotSupportedException">Thrown when operator is not yet implemented.</exception>
        public static OperatorNode CreateOperatorNode(char type, ExpressionTreeNode left, ExpressionTreeNode right)
        {
            if (type == '+')
            {
                return new OpAddNode(left, right);
            }

            if (type == '-')
            {
                return new OpSubtractNode(left, right);
            }

            if (type == '*')
            {
                return new OpMultiplyNode(left, right);
            }

            if (type == '/')
            {
                return new OpDivideNode(left, right);
            }

            if (type == '%')
            {
                return new OpModulusNode(left, right);
            }

            if (type == '^')
            {
                return new OpPowerNode(left, right);
            }

            throw new NotSupportedException(string.Format("Operator type {0} is not supported yet.", type));
        }
    }
}
// <copyright file="VariableNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class for a node that has a variable value.
    /// </summary>
    public class VariableNode : ExpressionTreeNode
    {
        private readonly string name;                    // Variable name

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="value">Variable value.</param>
        /// <param name="name">Variable name.</param>
        public VariableNode(double value, string name)
        {
            this.value = value;
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name">Varialbe name.</param>
        public VariableNode(string name)
        {
            this.value = 0;
            this.name = name;
        }

        /// <summary>
        /// Gets name property.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Sets value property.
        /// </summary>
        public double Value
        {
            set { this.value = value; }
        }

        /// <summary>
        /// Returns the value of the variable.
        /// </summary>
        /// <returns>The value of the variable.</returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
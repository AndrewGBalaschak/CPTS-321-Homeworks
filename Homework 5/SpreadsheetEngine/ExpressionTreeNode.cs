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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "No.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:Accessible fields should begin with upper-case letter", Justification = "Wrong again, it needs to be that way for the property that inherited classes implement.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Wrong for the third time, shame on you. It is.")]
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
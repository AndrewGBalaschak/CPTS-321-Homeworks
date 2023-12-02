// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Factory generator for operator nodes.
    /// </summary>
    public static class OperatorNodeFactory
    {

        // Operators and their precedence, lower numbers come first
        public static readonly Dictionary<char, Type> Operators = new Dictionary<char, Type>();

        private static int maxPrecedence;

        /// <summary>
        /// Initializes static members of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        static OperatorNodeFactory()
        {
            TraverseAvailableOperators((op, type) => Operators.Add(op, type));

            // Loop through available operators to find max precedence level
            maxPrecedence = 0;
            foreach (var op in Operators.Values)
            {
                // for each subclass, retrieve the Precedence property
                PropertyInfo operatorField = op.GetProperty("Precedence");

                if (operatorField != null)
                {
                    // Get the character of the Operator
                    object value = operatorField.GetValue(op);

                    if (value is int)
                    {
                        int precedenceLevel = (int)value;

                        if (precedenceLevel > maxPrecedence)
                        {
                            maxPrecedence = precedenceLevel;
                        }
                    }
                }
            }
        }

        public static int MaxPrecedence
        {
            get { return maxPrecedence; }
        }

        private delegate void OnOperator(char op, Type type);

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
            if (Operators.ContainsKey(type))
            {
                object operatorNodeObject = System.Activator.CreateInstance(Operators[type], left, right);

                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            throw new NotSupportedException(string.Format("Operator type {0} is not supported yet.", type));
        }

        private static void TraverseAvailableOperators(OnOperator onOperator)
        {
            // get the type declaration of OperatorNode
            Type operatorNodeType = typeof(OperatorNode);

            // Iterate over all loaded assemblies:
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from our OperatorNode class using LINQ
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                // Iterate over those subclasses of OperatorNode
                foreach (var type in operatorTypes)
                {
                    // for each subclass, retrieve the Operator property
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        // Get the character of the Operator
                        object value = operatorField.GetValue(type);

                        // If “Operator” property is not static, you will need to create an instance first and use the following code instead (or similar): bject value = operatorField.GetValue(Activator.CreateInstance(type,
                        // new ConstantNode(0)));
                        if (value is char)
                        {
                            char operatorSymbol = (char)value;

                            // And invoke the function passed as parameter with the operator symbol and the operator class
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
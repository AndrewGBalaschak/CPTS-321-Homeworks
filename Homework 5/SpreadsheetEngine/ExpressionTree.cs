// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// Class for an expression tree compose of ExpressionTreeNodes.
    /// </summary>
    public class ExpressionTree
    {
        private readonly string expression;                         // Input expression
        private List<string> tokens;                                // Tokens parsed from input expression
        private ExpressionTreeNode? root;                           // Root of the expression tree
        private Dictionary<string, double> variables;               // Variable dictionary
        private int maxLevel;                                       // Highest precedence level
        private int parseIndex;                                     // Index along the token list

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class with the given expression.
        /// </summary>
        /// <param name="expression">String representing the expression to be parsed.</param>
        public ExpressionTree(string expression)
        {
            this.expression = expression;
            this.tokens = new List<string>();
            this.variables = new Dictionary<string, double>();
            this.maxLevel = OperatorNode.Operators.Values.Max();
        }

        /// <summary>
        /// Gets expression.
        /// </summary>
        public string Expression
        {
            get { return this.expression; }
        }

        /// <summary>
        /// Sets a variable in the expression tree's variables dictionary.
        /// </summary>
        /// <param name="variableName">Variable name.</param>
        /// <param name="variableValue">Variable value.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables[variableName] = variableValue;
        }

        /// <summary>
        /// Calculates and returns evaluated value of expression tree.
        /// </summary>
        /// <returns>The evaluated value of the expression.</returns>
        public double Evaluate()
        {
            // Parse the expression tree into tokens
            this.Tokenize();

            // Construct the expression tree from the tokens
            this.parseIndex = 0;
            this.root = this.ParseLevel(this.maxLevel);
            return this.root.Evaluate();
        }

        /// <summary>
        /// Parses and list of constants, variables, and operators from the input expression. Populates variables dictionary with placeholder values.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown for unexpected character in expression string.</exception>
        private void Tokenize()
        {
            int index = 0;
            while (index < this.expression.Length)
            {
                char currentChar = this.expression[index];

                // If we are at a number
                if (char.IsDigit(currentChar))
                {
                    int startIndex = index;

                    // Keep reading until we are not at a digit
                    while (index < this.expression.Length && (char.IsDigit(this.expression[index]) || (this.expression[index] == '.')))
                    {
                        index++;
                    }

                    // Now we know the bounds of that number we can add it to the tokens list
                    this.tokens.Add(this.expression.Substring(startIndex, index - startIndex));
                }

                // If we are at a variable
                else if (char.IsLetter(currentChar))
                {
                    int startIndex = index;

                    // Keep reading until we are at the end or we reach an operator
                    while (index < this.expression.Length && !(OperatorNode.Operators.Keys.Contains(this.expression[index]) || this.expression[index] == '(' || this.expression[index] == ')'))
                    {
                        index++;
                    }

                    string tempVariableName = this.expression.Substring(startIndex, index - startIndex);

                    // Now we know the bounds of that variable name we can add it to the tokens list
                    this.tokens.Add(tempVariableName);

                    // We also need to add it to the variable dictionary
                    this.variables.TryAdd(tempVariableName, 0);
                }

                // If we are at an operator or parentheses
                else if (OperatorNode.Operators.Keys.Contains(currentChar) || currentChar == '(' || currentChar == ')')
                {
                    // Add it to the tokens list
                    this.tokens.Add(currentChar.ToString());
                    index++;
                }

                // Something unexpected is in the string
                else
                {
                    throw new InvalidOperationException("Invalid character in expression");
                }
            }

            // Print the tokens (kind of for debugging)
            Console.Write("Tokens: ");
            foreach (string token in this.tokens)
            {
                Console.Write("\"" + token + "\", ");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Parses a given precedence level.
        /// </summary>
        /// <param name="level">Precedence level to parse.</param>
        /// <returns>Returns expression tree subtree.</returns>
        private ExpressionTreeNode ParseLevel(int level)
        {
            // Parse operators one level down since they have higher operator precedence
            ExpressionTreeNode left;

            // Base case
            if (level == 1)
            {
                left = this.ParseBaseCase();
            }

            // Otherwise, parse one level down
            else
            {
                left = this.ParseLevel(level - 1);
            }

            // Get level operators
            List<char> levelOperators = new List<char>();
            foreach (var key in OperatorNode.Operators.Keys)
            {
                if (OperatorNode.Operators[key] == level)
                {
                    levelOperators.Add(key);
                }
            }

            while (this.parseIndex < this.tokens.Count)
            {
                string currentToken = this.tokens[this.parseIndex];

                // If the current token is a level operator
                if (levelOperators.Contains(currentToken[0]))
                {
                    // Parse the indices to the right of the operation
                    this.parseIndex++;
                    ExpressionTreeNode right;

                    // Base case
                    if (level == 1)
                    {
                        right = this.ParseBaseCase();
                    }

                    // Otherwise, parse one level down
                    else
                    {
                        right = this.ParseLevel(level - 1);
                    }

                    // Create a node for the operation
                    left = new OperatorNode(currentToken[0], left, right);
                }

                // Otherwise, we don't have any level three operators to parse
                else
                {
                    break;
                }
            }

            return left;
        }

        private ExpressionTreeNode ParseBaseCase()
        {
            string currentToken = this.tokens[this.parseIndex];

            // If the current token is a constant, make a constant node
            if (double.TryParse(currentToken, out double value))
            {
                this.parseIndex++;
                return new ConstantNode(value);
            }

            // If the current token is a variable, make a variable node
            if (this.variables.ContainsKey(currentToken))
            {
                this.parseIndex++;
                return new VariableNode(this.variables[currentToken], currentToken);
            }

            // If the current token is an open parenthese, we need to parse the inside
            if (currentToken == "(")
            {
                this.parseIndex++;

                // Now we parse the part inside the parentheses
                ExpressionTreeNode inside = this.ParseLevel(this.maxLevel);
                if (this.parseIndex < this.tokens.Count && this.tokens[this.parseIndex] == ")")
                {
                    this.parseIndex++;
                    return inside;
                }
            }

            throw new InvalidOperationException("Invalid expression");
        }
    }
}
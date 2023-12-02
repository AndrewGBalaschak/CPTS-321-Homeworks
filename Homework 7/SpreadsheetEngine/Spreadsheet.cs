// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Spreadsheet class.
    /// </summary>
    public class Spreadsheet
    {
        // 2D array of cells
        private Cell[,] cells;

        // Dictionary of dependecies
        private Dictionary<string, HashSet<string>> dependecies;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numRows">Number of rows in the spreadsheet.</param>
        /// <param name="numColumns">Number of columns in the spreadsheet.</param>
        public Spreadsheet(int numRows, int numColumns)
        {
            // Create 2D array of cells
            this.cells = new Cell[numRows, numColumns];

            // Create new reference dictionary
            this.dependecies = new Dictionary<string, HashSet<string>>();

            // Loop through cells to populate with default values
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    // Initialize the cell, making sure to convert the column
                    // into an ASCII character
                    this.cells[i, j] = new PrivateCell(i, (char)(j + 65));
                    this.cells[i, j].PropertyChanged += this.PropertyChanged;
                }
            }
        }

        public event PropertyChangedEventHandler? CellPropertyChanged;

        /// <summary>
        /// Gets number of rows.
        /// </summary>
        public int NumRows
        {
            get { return this.cells.GetLength(0); }
        }

        /// <summary>
        /// Gets number of columns.
        /// </summary>
        public int NumColumns
        {
            get { return this.cells.GetLength(1); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="value"></param>
        public void SetCell(int row, int col, string value)
        {
            // Validate input
            if (row >= 0 && row <= this.NumRows && col >= 0 && col <= this.NumColumns)
            {
                this.cells[row, col].Text = value;
            }
        }

        /// <summary>
        /// Retrieves the cell at the specified integer row and column.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="col">Column index.</param>
        /// <returns>Cell object, may be null if index is-out of-bounds.</returns>
        public Cell GetCell(int row, int col)
        {
            // Validate input
            if (row >= 0 && row <= this.NumRows && col >= 0 && col <= this.NumColumns)
            {
                return this.cells[row, col];
            }

            throw new IndexOutOfRangeException(string.Format("Cell at {0}{1} is out of bounds", row, col));
        }

        /// <summary>
        /// Retrieves the cell based on the human-readable cell name (for instance, "A1").
        /// </summary>
        /// <param name="cellName">Cell name in the format {char}{integer}</param>
        /// <returns>Cell object, may be null if index is out-of-bounds.</returns>
        public Cell GetCell(string cellName)
        {
            int tempRow, tempCol;

            // Parse out the column index
            tempCol = (int)(cellName[0] - 65);

            // Parse out the row index
            int.TryParse(cellName.Substring(1), out tempRow);

            // Correct for 0-indexed rows
            tempRow--;

            // Try and read the cell's value
            return this.GetCell(tempRow, tempCol);
        }

        /// <summary>
        /// Processes a cell that has changed and notifies subscribers.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event args.</param>
        public void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Cast sender to a cell variable
            if (sender is Cell cell)
            {
                if (e.PropertyName == "Text")
                {
                    this.EvaluateCell(cell);
                }
            }
        }

        /// <summary>
        /// Evaluates a cell, then recalcualtes the evaluation for any cells that depend on that cell.
        /// </summary>
        /// <param name="cell">Cell object to be evaluated.</param>
        public void EvaluateCell(Cell cell)
        {
            // If cell text not an expression:
            if (cell.Text[0] != '=')
            {
                cell.Value = cell.Text;
                this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
            }

            // If cell text is an expression:
            else
            {
                // Generate an expression tree from the expression
                ExpressionTree expression = new ExpressionTree(cell.Text.Substring(1), this);

                // Get the expression's variables from the expression tree
                string[] variables = expression.GetVariables();

                // Set the current cell to reference all the cells required
                foreach (string variable in variables)
                {
                    // Current cell is a dependent of cells in variable list
                    this.AddReference(variable, cell.CellName);
                }

                // Evaluate the expression
                cell.Value = expression.Evaluate().ToString();
                this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
            }

            // Now check if the cell we just recalculated has any dependents
            if (this.dependecies.ContainsKey(cell.CellName))
            {
                // Recalculate each dependent
                HashSet<string> dependentCells = this.dependecies[cell.CellName];

                foreach (string dependentCell in dependentCells)
                {
                    int tempRow, tempCol;

                    // Parse out the column index
                    tempCol = (int)(dependentCell[0] - 65);

                    // Parse out the row index
                    int.TryParse(dependentCell.Substring(1), out tempRow);

                    // Correct for 0-indexed rows
                    tempRow--;
                    this.EvaluateCell(this.GetCell(tempRow, tempCol));
                }

            }
        }

        /// <summary>
        /// Adds a dependent cell that depends on the source cell cell.
        /// </summary>
        /// <param name="source">The source cell.</param>
        /// <param name="dependent">A cell that depends on the value of the source.</param>
        private void AddReference(string source, string dependent)
        {
            // If the dependent is not in the dictionary yet, add it
            if (!this.dependecies.ContainsKey(source))
            {
                this.dependecies[source] = new HashSet<string>();
            }

            // Add the dependent
            this.dependecies[source].Add(dependent);
        }

        /// <summary>
        /// Internal class that extends the abstract Cell.
        /// </summary>
        internal class PrivateCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PrivateCell"/> class.
            /// </summary>
            /// <param name="rowIndex">Row index of cell.</param>
            /// <param name="colIndex">Column index of cell.</param>
            public PrivateCell(int rowIndex, char colIndex)
                : base(rowIndex, colIndex)
            {
            }
        }
    }
}
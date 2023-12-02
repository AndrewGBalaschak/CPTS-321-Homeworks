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

        // Undo Stack
        private Stack<UndoRedo> undos;

        // Redo stack
        private Stack<UndoRedo> redos;

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

            // Create stacks
            this.undos = new Stack<UndoRedo>();
            this.redos = new Stack<UndoRedo>();

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

        /// <summary>
        /// Invokes events when spreadsheet properties change.
        /// </summary>
        public event PropertyChangedEventHandler? SpreadsheetPropertyChanged;

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
        /// Sets a cell to a specific value.
        /// </summary>
        /// <param name="row">Row of cell to change.</param>
        /// <param name="col">Column of cell to change.</param>
        /// <param name="text">New text value of cell.</param>
        public void SetCell(int row, int col, string text)
        {
            // Validate input
            if (row >= 0 && row <= this.NumRows && col >= 0 && col <= this.NumColumns)
            {
                this.AddUndo(new List<Cell> { this.cells[row, col] }, "Text");
                this.cells[row, col].Text = text;
            }
        }

        /// <summary>
        /// Sets a cell to a specific value.
        /// </summary>
        /// <param name="cellName">Name of cell to change.</param>
        /// <param name="text">New text value of cell.</param>
        public void SetCell(string cellName, string text)
        {
            int row, col;

            // Parse out the column index
            col = (int)(cellName[0] - 65);

            // Parse out the row index
            int.TryParse(cellName.Substring(1), out row);

            // Correct for 0-indexed rows
            row--;

            this.SetCell(row, col, text);
        }

        /// <summary>
        /// Retrieves the cell at the specified integer row and column.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="col">Column index.</param>
        /// <returns>Cell object.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if index is out-of-bounds.</exception>
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
        /// <param name="cellName">Cell name in the format {char}{integer}.</param>
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

                if (e.PropertyName == "Color")
                {
                    this.SpreadsheetPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Color"));
                }
            }
        }

        /// <summary>
        /// Evaluates a cell, then recalcualtes the evaluation for any cells that depend on that cell.
        /// </summary>
        /// <param name="cell">Cell object to be evaluated.</param>
        public void EvaluateCell(Cell cell)
        {
            if (cell.Text != string.Empty)
            {
                // If cell text not an expression:
                if (cell.Text[0] != '=')
                {
                    cell.Value = cell.Text;
                    this.SpreadsheetPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
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
                    this.SpreadsheetPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
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
            else
            {
                cell.Value = string.Empty;
                this.SpreadsheetPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
            }
        }

        /// <summary>
        /// Adds an undo operation to the undos stack.
        /// </summary>
        /// <param name="cells">List of cells to perform the undo operation on.</param>
        /// <param name="propertyChanged">The property of the cells that has changed.</param>
        public void AddUndo(List<Cell> cells, string propertyChanged)
        {
            // Add to the undo stack
            UndoRedo temp = UndoRedoFactory.CreateUndoRedo(cells, propertyChanged);
            this.undos.Push(temp);
            this.SpreadsheetPropertyChanged?.Invoke(temp, new PropertyChangedEventArgs("Undo Available"));

            // Clear the redo stack
            this.redos.Clear();
            this.SpreadsheetPropertyChanged?.Invoke(temp, new PropertyChangedEventArgs("Redo Empty"));
        }

        /// <summary>
        /// Adds a redo operation to the redos stack.
        /// </summary>
        /// <param name="cells">List of cells to perfom the redo operation on.</param>
        /// <param name="propertyChanged">The property of the cells that has changed.</param>
        public void AddRedo(List<Cell> cells, string propertyChanged)
        {
            // Add to the redo stack
            UndoRedo temp = UndoRedoFactory.CreateUndoRedo(cells, propertyChanged);
            this.redos.Push(temp);
            this.SpreadsheetPropertyChanged?.Invoke(temp, new PropertyChangedEventArgs("Redo Available"));
        }

        /// <summary>
        /// Executes the top of the undo stack.
        /// </summary>
        public void Undo()
        {
            if (this.undos.Count() > 0)
            {
                UndoRedo temp = this.undos.Pop();
                this.AddRedo(temp.Cells, temp.PropertyChanged);
                temp.Execute();

                // If we are out of undo levels, notifiy subscribers
                if (this.undos.Count() <= 0)
                {
                    this.SpreadsheetPropertyChanged?.Invoke(temp, new PropertyChangedEventArgs("Undo Empty"));
                }
            }
        }

        /// <summary>
        /// Executes the top of the redo stack.
        /// </summary>
        public void Redo()
        {
            if (this.redos.Count() > 0)
            {
                UndoRedo temp = this.redos.Pop();
                this.AddUndo(temp.Cells, temp.PropertyChanged);
                temp.Execute();

                // If we are out of redo levels, notify subscribers
                if (this.redos.Count() <= 0)
                {
                    this.SpreadsheetPropertyChanged?.Invoke(temp, new PropertyChangedEventArgs("Redo Empty"));
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
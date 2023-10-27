using SpreadsheetEngine;
using System.ComponentModel;

namespace SpreadsheetEngine
{
    public class Spreadsheet
    {
        public event PropertyChangedEventHandler? CellPropertyChanged;      // Event handler, idk why the ? fixes the green squiggly but it does

        // 2D array of cells
        private Cell[,] cells;

        /// <summary>
        /// Creates a spreadsheet object with the set parameters
        /// </summary>
        /// <param name="numRows">Number of rows in the spreadsheet</param>
        /// <param name="numColumns">Number of columns in the spreadsheet</param>
        public Spreadsheet(int numRows, int numColumns)
        {
            // Create 2D array of cells
            cells = new Cell[numRows, numColumns];

            // Loop through cells to populate with default values
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    // Initialize the cell, making sure to convert the column
                    // into an ASCII character
                    cells[i, j] = new PrivateCell(i, (char)(j + 65));
                    cells[i, j].PropertyChanged += this.NotifyPropertyChanged;
                }
            }
        }

        // Property for number of rows
        public int NumRows
        { get { return cells.GetLength(0); } }

        // Property for number of columns
        public int NumColumns
        { get { return cells.GetLength(1); } }

        /// <summary>
        /// Retrieves the cell at the specified row and column
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        /// <returns>Cell object, may be null if index is-out of-bounds</returns>
        public Cell? GetCell(int row, int col)
        {
            // Validate input
            if (row >= 0 && row <= NumRows && col >= 0 && col <= NumColumns)
            {
                return cells[row, col];
            }
            return null;
        }

        /// <summary>
        /// Processes a cell that has changed and notifies subscribers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Cast sender to a cell variable
            if (sender is Cell cell)
            {
                // If the text property changed we need to update the value of the cell
                if (e.PropertyName == "Text")
                {
                    // If the Text of the cell does NOT start with ‘=’ then the value is just set to the text
                    if (cell.Text[0] != '=')
                    {
                        cell.Value = cell.Text;
                        CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
                        return;
                    }

                    // Otherwise, the value must be computed based on the formula that comes after the ‘=’

                    // For now, I ONLY support pulling the value from another cell
                    // Parse out the cell for the equivalance
                    int tempRow, tempCol;

                    //Parse out the column index from the char
                    tempCol = (int)(cell.Text[1] - 65);

                    //Parse out the row as an integer
                    int.TryParse(cell.Text.Substring(2), out tempRow);

                    // Correct for 0-indexed rows
                    tempRow--;

                    // Retrieve the referenced cell
                    Cell? tempCell = GetCell(tempRow, tempCol);

                    // Set cell value to referenced cell's value if the other cell exists
                    if (tempCell != null)
                    {
                        cell.Value = tempCell.Value;
                        CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
                    }

                }
            }
        }

        // I *think* this is the solution she wants us to figure out, but I have no idea
        // Never used a private class before so here goes nothing
        internal class PrivateCell : Cell
        {
            public PrivateCell(int rowIndex, char colIndex) : base(rowIndex, colIndex)
            {
            }
        }
    }
}
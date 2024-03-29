// Copyright (C) Andrew Balaschak
// Licensed under the GPL-3.0 License. See License.txt in the project root for license information.

namespace Spreadsheet_Andrew_Balaschak
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Forms;
    using SpreadsheetEngine;

    /// <summary>
    /// Class for main application window.
    /// </summary>
    public partial class Form1 : Form
    {
        // This is the main DataGridView object that displays the data
        private DataGridView dataGridView;

        // This is the main Spreadsheet object that stores the data
        private Spreadsheet mainSpreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Init spreadsheet
            this.mainSpreadsheet = new Spreadsheet(50, 26);
            this.mainSpreadsheet.CellPropertyChanged += this.OnCellPropertyChanged;

            // Init columns
            for (int i = 0; i < 26; i++)
            {
                string columnName = ((char)(i + 65)).ToString();
                this.dataGridView.Columns.Add(columnName, columnName);
            }

            // Init rows
            for (int i = 0; i < 50; i++)
            {
                this.dataGridView.Rows.Add();
                this.dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Cast sender to a cell variable
            if (sender is Cell cell)
            {
                if (e.PropertyName == "Value")
                {
                    this.dataGridView[cell.ColumnIndex - 65, cell.RowIndex].Value = cell.Value;
                }
            }
        }

        private void DemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the text in about 50 random cells to a text string of your choice
            Random random = new Random(); // Create a Random object to generate random numbers
            for (int i = 0; i < 50; i++)
            {
                // Get a random row and column index within the bounds of the spreadsheet
                int rowIndex = random.Next(0, this.mainSpreadsheet.NumRows);
                int columnIndex = random.Next(2, this.mainSpreadsheet.NumColumns);

                // Get the Cell object at that location from the spreadsheet
                Cell? cell = this.mainSpreadsheet.GetCell(rowIndex, columnIndex);

                // Set its Text property to a text string of your choice (e.g., "Hello World!")
                if (cell != null)
                {
                    cell.Text = "Hello World!";
                }
            }

            // Set the text in every cell in column B to "This is cell B#", where # number is the row number for the cell
            for (int i = 0; i < this.mainSpreadsheet.NumRows; i++)
            {
                // Get the Cell object at column B and row i from the spreadsheet
                Cell? cell = this.mainSpreadsheet.GetCell(i, (int)('B' - 65));

                // Set its Text property to "This is cell B#" using string interpolation
                if (cell != null)
                {
                    cell.Text = $"This is cell B{i + 1}"; // Add 1 to i because row numbers start from 1, not 0
                }
            }

            // Set the text in every cell in column A to "=B#", where �#� is the row number of the cell
            for (int i = 0; i < this.mainSpreadsheet.NumRows; i++)
            {
                // Get the Cell object at column A and row i from the spreadsheet
                Cell? cell = this.mainSpreadsheet.GetCell(i, (int)('A' - 65));

                // Set its Text property to "=B#" using string interpolation
                if (cell != null)
                {
                    cell.Text = $"=B{i + 1}"; // Add 1 to i because row numbers start from 1, not 0
                }
            }
        }

        /// <summary>
        /// Saves the current spreadsheet as a .csv file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();
            this.saveFileDialog1.DefaultExt = ".csv";
            this.saveFileDialog1.Filter = "Comma Separated Values|*.csv";
            DialogResult dr = this.saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                for (int i = 0; i < this.mainSpreadsheet.NumRows; i++)
                {
                    for (int j = 0; j < this.mainSpreadsheet.NumColumns; j++)
                    {
                        output.Append(this.mainSpreadsheet.GetCell(i, j).Text + ',');
                    }

                    output.Append("\n");
                }
            }

            File.WriteAllText(this.saveFileDialog1.FileName, output.ToString());
        }

        /// <summary>
        /// TODO: Opens a File.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Check if the row and column indices are valid
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Cell selection = this.mainSpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);
                this.dataGridView[e.ColumnIndex, e.RowIndex].Value = selection.Text;
            }
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the row and column indices are valid
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Cell selection = this.mainSpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

                if (this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    selection.Text = this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
                else
                {
                    selection.Text = string.Empty;
                }

                this.dataGridView[e.ColumnIndex, e.RowIndex].Value = selection.Value;
            }
        }
    }
}
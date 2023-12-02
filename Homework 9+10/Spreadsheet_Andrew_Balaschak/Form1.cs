// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Andrew_Balaschak
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Forms;
    using SpreadsheetEngine;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            this.mainSpreadsheet.SpreadsheetPropertyChanged += this.UpdateGUI;

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

        /// <summary>
        /// Updates the GUI when notified by the spreadsheet object.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void UpdateGUI(object sender, PropertyChangedEventArgs e)
        {
            // If a cell has changed
            if (sender is Cell cell)
            {
                if (e.PropertyName == "Value")
                {
                    this.dataGridView[cell.ColumnIndex - 65, cell.RowIndex].Value = cell.Value;
                }
                else if (e.PropertyName == "Color")
                {
                    this.dataGridView[cell.ColumnIndex - 65, cell.RowIndex].Style.BackColor = Color.FromArgb((int)cell.BGColor);
                }
            }

            // If undo/redo has changed
            if (sender is UndoRedo undoRedo)
            {
                if (e.PropertyName == "Undo Available")
                {
                    this.undoToolStripMenuItem.Enabled = true;
                    this.undoToolStripMenuItem.Text = string.Format("Undo cell {0} change", undoRedo.PropertyChanged.ToLower());
                }
                else if (e.PropertyName == "Undo Empty")
                {
                    this.undoToolStripMenuItem.Enabled = false;
                    this.undoToolStripMenuItem.Text = "Undo";
                }
                else if (e.PropertyName == "Redo Available")
                {
                    this.redoToolStripMenuItem.Enabled = true;
                    this.redoToolStripMenuItem.Text = string.Format("Redo cell {0} change", undoRedo.PropertyChanged.ToLower());
                }
                else if (e.PropertyName == "Redo Empty")
                {
                    this.redoToolStripMenuItem.Enabled = false;
                    this.redoToolStripMenuItem.Text = "Redo";
                }
            }
        }

        /// <summary>
        /// Saves the current spreadsheet as a .csv file.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();
            this.saveFileDialog1.DefaultExt = ".xml";
            this.saveFileDialog1.Filter = "Extensible Markup Language|*.xml";
            DialogResult dr = this.saveFileDialog1.ShowDialog();

            // Saving to an XML file
            if (dr == DialogResult.OK)
            {
                this.mainSpreadsheet.Save(this.saveFileDialog1.FileName);
            }

            // CSV Section
            /*
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

                File.WriteAllText(this.saveFileDialog1.FileName, output.ToString());
            }
            */
        }

        /// <summary>
        /// TODO: Opens a File.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.mainSpreadsheet.Open(this.openFileDialog1.FileName);
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
                    // Add undo level
                    List<Cell> temp = new List<Cell> { selection };
                    this.mainSpreadsheet.AddUndo(temp, "Text");
                    selection.Text = this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
                else
                {
                    selection.Text = string.Empty;
                }

                this.dataGridView[e.ColumnIndex, e.RowIndex].Value = selection.Value;
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mainSpreadsheet.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mainSpreadsheet.Redo();
        }

        private void ChangeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uint newColor = 0x0;

            // Pull up ColorDialog
            ColorDialog myColorDialog = new ColorDialog();

            // Keeps the user from selecting a custom color.
            myColorDialog.AllowFullOpen = false;

            // Set the color in the spreadsheet object, which will then trigger the UI to update
            if (myColorDialog.ShowDialog() == DialogResult.OK)
            {
                newColor = (uint)myColorDialog.Color.ToArgb();
                List<Cell> cells = new List<Cell>();

                // Get the cells
                foreach (DataGridViewCell cell in this.dataGridView.SelectedCells)
                {
                    Cell current = this.mainSpreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex);
                    cells.Add(current);
                }

                // Add undo level
                this.mainSpreadsheet.AddUndo(cells, "Color");

                // Change the color
                foreach (Cell cell in cells)
                {
                    cell.BGColor = newColor;
                }
            }
        }
    }
}
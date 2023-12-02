// <copyright file="UndoRedoText.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// Stores the previous text of a list of cells and allows for changes to be undone.
    /// </summary>
    public class UndoRedoText : UndoRedo
    {
        private List<string> text;                          // Cell text

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoText"/> class.
        /// </summary>
        /// <param name="cells">>List of cells to preserve.</param>
        public UndoRedoText(List<Cell> cells)
            : base(cells, "Text")
        {
            this.text = new List<string>();

            foreach (Cell cell in cells)
            {
                this.text.Add(cell.Text);
            }
        }

        /// <summary>
        /// Returns the cells to their previous state.
        /// </summary>
        public override void Execute()
        {
            for (int i = 0; i < this.cells.Count; i++)
            {
                this.cells[i].Text = this.text[i];
            }
        }
    }
}
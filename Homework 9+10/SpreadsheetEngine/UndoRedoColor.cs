// <copyright file="UndoRedoColor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// Stores the previous colors of a list of cells and allows for changes to be undone.
    /// </summary>
    public class UndoRedoColor : UndoRedo
    {
        private List<uint> colors;                          // Cell colors

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoColor"/> class.
        /// </summary>
        /// <param name="cells">List of cells to preserve.</param>
        public UndoRedoColor(List<Cell> cells)
            : base(cells, "Color")
        {
            this.colors = new List<uint>();

            foreach (Cell cell in cells)
            {
                this.colors.Add(cell.BGColor);
            }
        }

        /// <summary>
        /// Returns the cells to their previous state.
        /// </summary>
        public override void Execute()
        {
            for (int i = 0; i < this.cells.Count; i++)
            {
                this.cells[i].BGColor = this.colors[i];
            }
        }
    }
}
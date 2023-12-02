// <copyright file="UndoRedo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// Stores the previous state of a cell and allows for changes to be undone.
    /// </summary>
    public abstract class UndoRedo
    {
        protected List<Cell> cells;                         // Reference to cells in the spreadsheet
        protected string propertyChanged;                   // Name of what changed, color or text, etc

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedo"/> class.
        /// </summary>
        /// <param name="cells">List of cells to preserve.</param>
        /// <param name="propertyChanged">Type of property undone/redone.</param>
        public UndoRedo(List<Cell> cells, string propertyChanged)
        {
            this.cells = cells;
            this.propertyChanged = propertyChanged;
        }

        /// <summary>
        /// Gets the affected cells.
        /// </summary>
        public List<Cell> Cells
        {
            get { return this.cells; }
        }

        /// <summary>
        /// Gets the property changed.
        /// </summary>
        public string PropertyChanged
        {
            get { return this.propertyChanged; }
        }

        /// <summary>
        /// Returns the cell to the previous state.
        /// </summary>
        public abstract void Execute();
    }
}
// <copyright file="UndoRedoFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// Factory generator for undo/redo nodes.
    /// </summary>
    public static class UndoRedoFactory
    {
        /// <summary>
        /// Returns an undo/redo node of the specified type.
        /// </summary>
        /// <param name="oldCell">Cell to preserve.</param>
        /// <param name="propertyChanged">Type of property changed.</param>
        /// <returns>Returns a new UndoRedoCollection object of the type specified.</returns>
        /// <exception cref="NotSupportedException">Thrown when undo/redo for that property is not yet implemented.</exception>
        public static UndoRedo CreateUndoRedo(Cell oldCell, string propertyChanged)
        {
            return CreateUndoRedo(new List<Cell> { oldCell }, propertyChanged);
        }

        /// <summary>
        /// Returns an undo/redo node of the specified type.
        /// </summary>
        /// <param name="oldCells">List of cells to preserve.</param>
        /// <param name="propertyChanged">Type of property changed.</param>
        /// <returns>Returns a new UndoRedoCollection object of the type specified.</returns>
        /// <exception cref="NotSupportedException">Thrown when undo/redo for that property is not yet implemented.</exception>
        public static UndoRedo CreateUndoRedo(List<Cell> oldCells, string propertyChanged)
        {
            if (propertyChanged == "Text")
            {
                return new UndoRedoText(oldCells);
            }

            if (propertyChanged == "Color")
            {
                return new UndoRedoColor(oldCells);
            }

            throw new NotImplementedException(string.Format("Undo for property {0} not implemented yet", propertyChanged));
        }
    }
}
// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// Abstract cell class. Revolutionary.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "No it should not.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "It is.")]
        protected string text = string.Empty;       // Raw value of cell
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "No it should not.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "It is.")]
        protected string value = string.Empty;      // Computed value of cell
        private readonly int rowIndex;              // Cell y-coordinate
        private readonly char columnIndex;          // Cell x-coordinate

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex">The cell's y-coordinate.</param>
        /// <param name="columnIndex">The cell's x-coordinate.</param>
        public Cell(int rowIndex, char columnIndex)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets rowIndex.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets columnIndex.
        /// </summary>
        public char ColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Gets or sets text property, notifies event subscribers.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value;
                this.NotifyPropertyChanged("Text");
            }
        }

        /// <summary>
        /// Gets value property.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }

            internal set
            {
                this.value = value;
                this.NotifyPropertyChanged("Value");
            }
        }

        // This method is from the MSDN documentation on implementing INotifyPropertyChanged
        private void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
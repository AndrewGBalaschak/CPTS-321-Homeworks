using System.ComponentModel;

namespace SpreadsheetEngine
{
    public abstract class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;      // Event handler, idk why the ? fixes the green squiggly but it does

        private int rowIndex;                       // Cell y-coordinate
        private char columnIndex;                   // Cell x-coordinate
        protected string text = string.Empty;       // Raw value of cell
        protected string value = string.Empty;      // Computed value of cell

        /// <summary>
        /// Cell Constructor
        /// </summary>
        /// <param name="rowIndex">The cell's y-coordinate</param>
        /// <param name="columnIndex">The cell's x-coordinate</param>
        public Cell(int rowIndex, char columnIndex)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        // RowIndex property
        public int RowIndex
        { get { return rowIndex; } }

        // ColumnIndex property
        public char ColumnIndex
        { get { return columnIndex; } }

        // Text property, notifies event subscribers
        public string Text
        {
            get { return text; }
            set
            {
                // If the text is being set to the exact same text then just ignore it.
                // Do not invoke the property change event
                if (this.text == value)
                {
                    return;
                }

                // If the text is actually being changed then update the protected field
                text = value;
                this.NotifyPropertyChanged("Text");
            }
        }

        // Value property, notifies event subscribers
        public string Value
        {
            get { return value; }

            // Only classes within the same assembly can set the value
            internal set
            {
                this.value = value;
                this.NotifyPropertyChanged("Value");
            }
        }

        // This method is from the MSDN documentation on implementing INotifyPropertyChanged
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
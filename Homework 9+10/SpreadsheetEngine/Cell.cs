// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Abstract cell class. Revolutionary.
    /// </summary>
    public class Cell : INotifyPropertyChanged, IXmlSerializable
    {
        protected string text = string.Empty;       // Raw value of cell
        protected string value = string.Empty;      // Computed value of cell
        protected uint cellColor = 0xFFFFFFFF;      // Cell background color
        private int rowIndex;                       // Cell y-coordinate
        private char columnIndex;                   // Cell x-coordinate

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

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// Paremeterless constructor for IXmlSerializable interface.
        /// </summary>
        public Cell()
        {
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
        /// Gets the column index concatenated with the row index.
        /// </summary>
        public string CellName
        {
            get { return this.columnIndex.ToString() + (this.rowIndex + 1).ToString(); }
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
        /// Gets value property, notifies event subscribers.
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

        /// <summary>
        /// Gets or sets cell color, notifies event subscribers.
        /// </summary>
        public uint BGColor
        {
            get
            {
                return this.cellColor;
            }

            set
            {
                this.cellColor = value;
                this.NotifyPropertyChanged("Color");
            }
        }

        // XML Serialization

        /// <summary>
        /// Serialize cell to XML.
        /// </summary>
        /// <param name="writer">XmlWriter object.</param>
        public void WriteXml(XmlWriter writer)
        {
            // Write cell name as attribute
            writer.WriteAttributeString("CellName", this.CellName);

            // Write cell text and color as elements of the cell
            writer.WriteElementString("Text", this.Text);
            writer.WriteElementString("Color", this.BGColor.ToString("X"));
        }

        /// <summary>
        /// Read cell from XML serialization.
        /// </summary>
        /// <param name="reader">XmlReader object.</param>
        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();

            // Read the cell name
            string cellName = reader.GetAttribute("CellName");
            int row;

            // Parse out the row index
            int.TryParse(cellName.Substring(1), out row);

            // Correct for 0-indexed rows
            row--;

            this.rowIndex = row;
            this.columnIndex = cellName[0];

            reader.ReadStartElement();

            // Keep reading until we reach the EndElement for the cell
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Text":
                            this.Text = reader.ReadElementContentAsString();
                            break;
                        case "Color":
                            this.BGColor = uint.Parse(reader.ReadElementContentAsString(), System.Globalization.NumberStyles.HexNumber);
                            break;
                    }
                }

                // Exit the loop when the end of the cell element is reached
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Cell")
                {
                    break;
                }
            }

            reader.ReadEndElement();
        }

        /// <summary>
        /// This does nothing, according to spec.
        /// </summary>
        /// <returns>nothing lmao.</returns>
        public XmlSchema GetSchema()
        {
            #pragma warning disable CS8603 // Possible (and dare I say proable) null reference return.
            return null;
            #pragma warning restore CS8603
        }

        // This method is from the MSDN documentation on implementing INotifyPropertyChanged
        private void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
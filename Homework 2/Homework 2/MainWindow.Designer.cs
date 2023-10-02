namespace Homework_2
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainText = new TextBox();
            SuspendLayout();
            // 
            // MainText
            // 
            MainText.Dock = DockStyle.Fill;
            MainText.Location = new Point(0, 0);
            MainText.Multiline = true;
            MainText.Name = "MainText";
            MainText.ReadOnly = true;
            MainText.Size = new Size(800, 450);
            MainText.TabIndex = 0;
            MainText.TextChanged += MainText_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(MainText);
            Name = "Form1";
            Text = "Andrew Balaschak - 11687126";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MainText;
    }
}
namespace Homework_3
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
            components = new System.ComponentModel.Container();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            fibonacciToolStripMenuItem = new ToolStripMenuItem();
            Fibonacci50 = new ToolStripMenuItem();
            Fibonacci100 = new ToolStripMenuItem();
            richTextBox = new RichTextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Title = "Open";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Title = "Save";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(28, 28);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(28, 28);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, fibonacciToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(776, 38);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, openToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(62, 34);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(182, 40);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(182, 40);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // fibonacciToolStripMenuItem
            // 
            fibonacciToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { Fibonacci50, Fibonacci100 });
            fibonacciToolStripMenuItem.Name = "fibonacciToolStripMenuItem";
            fibonacciToolStripMenuItem.Size = new Size(118, 34);
            fibonacciToolStripMenuItem.Text = "Fibonacci";
            // 
            // Fibonacci50
            // 
            Fibonacci50.Name = "Fibonacci50";
            Fibonacci50.Size = new Size(315, 40);
            Fibonacci50.Text = "50";
            Fibonacci50.Click += fibonacci50_Click;
            // 
            // Fibonacci100
            // 
            Fibonacci100.Name = "Fibonacci100";
            Fibonacci100.Size = new Size(315, 40);
            Fibonacci100.Text = "100";
            Fibonacci100.Click += fibonacci100_Click;
            // 
            // richTextBox
            // 
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox.Location = new Point(0, 38);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new Size(776, 398);
            richTextBox.TabIndex = 3;
            richTextBox.Text = "";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 436);
            Controls.Add(richTextBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainWindow";
            Text = "Notepad";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ContextMenuStrip contextMenuStrip1;
        private RichTextBox richTextBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem fibonacciToolStripMenuItem;
        private ToolStripMenuItem Fibonacci50;
        private ToolStripMenuItem Fibonacci100;
    }
}
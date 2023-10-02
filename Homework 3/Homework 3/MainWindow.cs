using System;
using System.Windows.Forms;

namespace Homework_3
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads text from TextReader object into the richTextBox
        /// </summary>
        /// <param name="sr">TextReader object containing the text you want to load</param>
        private void LoadText(TextReader sr)
        {
            richTextBox.Text = sr.ReadToEnd();
        }

        /// <summary>
        /// Loads text from a file into the richTextBox
        /// </summary>
        /// <param name="filepath">The path to the file that a StreamReader will read from</param>
        private void LoadTextFromFile(string filepath)
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                LoadText(sr);
            }
        }

        /// <summary>
        /// Saves a file as text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = ".txt";
            saveFileDialog1.Filter = "Text File|*.txt";
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox.Text);
            }
        }

        /// <summary>
        /// Loads a file as text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadTextFromFile(openFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Overwrites document with the first 50 Fibonacci numbers (0-49)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fibonacci50_Click(object sender, EventArgs e)
        {
            FibonacciTextReader fib = new FibonacciTextReader(50);
            richTextBox.Text = fib.ReadToEnd();
        }

        /// <summary>
        /// Overwrites document with the first 100 Fibonacci numbers (0-99)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fibonacci100_Click(object sender, EventArgs e)
        {
            FibonacciTextReader fib = new FibonacciTextReader(100);
            richTextBox.Text = fib.ReadToEnd();
        }
    }
}
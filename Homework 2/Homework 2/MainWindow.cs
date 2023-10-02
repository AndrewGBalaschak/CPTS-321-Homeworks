using System.Diagnostics;
using System.Text;

namespace Homework_2
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            List<int> randomNumbers = DistinctNumbers.GenerateRandomInts(0, 20000, 10000);
            string output = DistinctNumbers.RunDistinctIntegers(randomNumbers);
            MainText.Text = output;
        }

        private void MainText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
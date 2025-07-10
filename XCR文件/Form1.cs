namespace XCR文件
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            XCR.BaseFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "所有文件 (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInputFile.Text = openFileDialog.FileName;
                    button5_Click(sender, e);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XCR.cryptFile("decrypt", txtInputFile.Text);
            button5_Click(sender, e);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            XCR.cryptFile("encrypt", txtInputFile.Text);
            button5_Click(sender, e);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = File.ReadAllText(txtInputFile.Text);
        }
    }
}

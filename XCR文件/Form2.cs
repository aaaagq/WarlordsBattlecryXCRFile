using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCR文件
{
    public partial class Form2 : Form
    {


        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "所有文件 (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInputFile.Text = openFileDialog.FileName;
                    XCR.loadXCR(txtInputFile.Text);
                    listBox1.DataSource = null;
                    listBox1.DataSource = XCR.xCRFiles;
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textBox2.Text = ((XCRFile)listBox1.SelectedItem).ToALLString();
            textBox2.Text = XCR.ReadStringDecrypt((XCRFile)listBox1.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = "out\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                XCRFile xCRFile = listBox1.SelectedItem as XCRFile;
                string text = XCR.ReadStringDecrypt((XCRFile)listBox1.SelectedItem);
                File.WriteAllText(path + xCRFile.FileName, text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XCR.saveXCR("in\\");
        }
    }
}

namespace XCR文件
{
    partial class Form1
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
            textBox1 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            txtInputFile = new TextBox();
            button1 = new Button();
            button4 = new Button();
            button5 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(41, 120);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Size = new Size(673, 288);
            textBox1.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(882, 66);
            button2.Name = "button2";
            button2.Size = new Size(75, 46);
            button2.TabIndex = 2;
            button2.Text = "BaseFile";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(41, 12);
            button3.Name = "button3";
            button3.Size = new Size(75, 39);
            button3.TabIndex = 3;
            button3.Text = "选择文件";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // txtInputFile
            // 
            txtInputFile.Location = new Point(136, 13);
            txtInputFile.Name = "txtInputFile";
            txtInputFile.Size = new Size(578, 23);
            txtInputFile.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(639, 57);
            button1.Name = "button1";
            button1.Size = new Size(75, 38);
            button1.TabIndex = 5;
            button1.Text = "加密";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button4
            // 
            button4.Location = new Point(136, 57);
            button4.Name = "button4";
            button4.Size = new Size(75, 38);
            button4.TabIndex = 6;
            button4.Text = "解密";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(41, 57);
            button5.Name = "button5";
            button5.Size = new Size(75, 38);
            button5.TabIndex = 7;
            button5.Text = "读取";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(748, 450);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button1);
            Controls.Add(txtInputFile);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox1;
        private Button button2;
        private Button button3;
        private TextBox txtInputFile;
        private Button button1;
        private Button button4;
        private Button button5;
    }
}

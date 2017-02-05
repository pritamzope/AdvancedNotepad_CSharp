using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdvancedNotepad_CSharp
{
    public partial class Replace_Form : Form
    {
        RichTextBox richtext;
        public Replace_Form(RichTextBox R)
        {
            InitializeComponent();
            richtext = R;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richtext.Text = richtext.Text.Replace(textBox1.Text, textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

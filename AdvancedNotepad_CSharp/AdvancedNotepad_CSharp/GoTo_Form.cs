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
    public partial class GoTo_Form : Form
    {
        RichTextBox richText;
        public GoTo_Form(RichTextBox R)
        {
            InitializeComponent();
            richText = R;
        }

        private void GoTo_Form_Load(object sender, EventArgs e)
        {
            int lines = richText.Lines.Length;
            label1.Text = "Enter Line Number (1-" + lines.ToString() + ") :";

            int sel = richText.GetLineFromCharIndex(richText.SelectionStart) + 1;
            textBox1.Text = sel.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int sel;
            RichTextBox rtb = new RichTextBox();
            rtb.Text = richText.Text;
            int lines = rtb.Lines.Length;

            if (textBox1.Text == "")
            {
                button1.Enabled = false;
            }
            else if (!int.TryParse(textBox1.Text, out sel))
            {
                button1.Enabled = false;
            }
            else if (Int32.Parse(textBox1.Text) > rtb.Lines.Length)
            {
                button1.Enabled = false;
            }
            else if (textBox1.Text == "0")
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int line = Int32.Parse(textBox1.Text);
            richText.SelectionStart = richText.GetFirstCharIndexFromLine(line - 1);
            richText.ScrollToCaret();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

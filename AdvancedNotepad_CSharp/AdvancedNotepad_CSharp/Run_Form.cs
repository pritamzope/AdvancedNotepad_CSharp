using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace AdvancedNotepad_CSharp
{
    public partial class Run_Form : Form
    {
        public Run_Form()
        {
            InitializeComponent();
        }

        private void selectfile_button_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void run_button_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="")
            {
                if(File.Exists(textBox1.Text))
                {
                    Process.Start(textBox1.Text);
                    this.Close();
                }
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

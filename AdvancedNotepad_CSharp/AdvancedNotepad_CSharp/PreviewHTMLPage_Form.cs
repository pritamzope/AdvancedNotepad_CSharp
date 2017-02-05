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
    public partial class PreviewHTMLPage_Form : Form
    {
        String text = "";
        String filename = "";
        public PreviewHTMLPage_Form(String T,String fname)
        {
            InitializeComponent();
            text = T;
            filename = fname;
        }

        private void PreviewHTMLPage_Form_Load(object sender, EventArgs e)
        {
            if (filename.Contains("Untitled"))
            {
                webBrowser1.DocumentText = text;
            }
            else
            {
                webBrowser1.Navigate(filename);
            }
        }

        private void reload_button_Click(object sender, EventArgs e)
        {
            if (filename.Contains("Untitled"))
            {
                webBrowser1.DocumentText = text;
            }
            else
            {
                webBrowser1.Navigate(filename);
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void foreword_button_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

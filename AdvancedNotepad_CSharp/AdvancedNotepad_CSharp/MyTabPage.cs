using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace AdvancedNotepad_CSharp
{
   public class MyTabPage : System.Windows.Forms.TabPage
    {
       public MainForm mainform;
       public MyRichTextBox _myRichTextBox = new MyRichTextBox();

       public MyTabPage(MainForm mf)
       {
           mainform = mf;

           this._myRichTextBox.Dock = DockStyle.Fill;

           this._myRichTextBox.richTextBox1.Text = "";
           _myRichTextBox.richTextBox1.Font = new System.Drawing.Font("Monospaced", 10, FontStyle.Regular);
           this._myRichTextBox.richTextBox1.Select();

           _myRichTextBox.richTextBox1.TextChanged += new EventHandler(this.richTextBox1_TextChanged);
           _myRichTextBox.richTextBox1.SelectionChanged += new EventHandler(this.richTextBox1_SelectionChanged);

           _myRichTextBox.richTextBox1.LinkClicked += new LinkClickedEventHandler(this.richTextBox1_LinkClicked);

           this.Controls.Add(_myRichTextBox);
       }



       private void richTextBox1_TextChanged(object sender, EventArgs e)
       {
           String str = this.Text;
           if (str.Contains("*"))
           {

           }
           else
           {
               this.Text = str + "*";
           }
       }

       private void richTextBox1_SelectionChanged(object sender, EventArgs e)
       {
           int sel = _myRichTextBox.richTextBox1.SelectionStart;
           int line = _myRichTextBox.richTextBox1.GetLineFromCharIndex(sel) + 1;
           int col = sel - _myRichTextBox.richTextBox1.GetFirstCharIndexFromLine(line - 1) + 1;

           mainform.LineToolStripLabel.Text = "Line : " + line.ToString();
           mainform.ColumnToolStripLabel.Text = "Col : " + col.ToString();
       }



       private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
       {
           Process.Start(e.LinkText);
       }

    }
}

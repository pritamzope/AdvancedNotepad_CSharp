using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace AdvancedNotepad_CSharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if (args != null && args.Length > 0)
            {
                String[] files = args;

                MainForm mf = new MainForm();
                mf.IsArgumentNull = false;
                mf.OpenAssociatedFiles_WhenApplicationStarts(files);
                Application.EnableVisualStyles();
                Application.Run(mf);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}

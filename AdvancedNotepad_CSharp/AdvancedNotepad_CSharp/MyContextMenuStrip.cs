using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace AdvancedNotepad_CSharp
{
    public class MyContextMenuStrip : ContextMenuStrip
    {
        public MyContextMenuStrip()
        {
            this.Renderer = new MyMenuRenderer();
        }
    }

}

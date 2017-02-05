using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdvancedNotepad_CSharp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static List<String> OpenedFilesList = new List<String> { };


        //**********************************************************************
        //Custom form operations
        bool isTopPanelDragged = false;
        bool isLeftPanelDragged = false;
        bool isRightPanelDragged = false;
        bool isBottomPanelDragged = false;
        bool isTopBorderPanelDragged = false;
        bool isWindowMaximized = false;
        Point offset;
        Size _normalWindowSize=new Size(new Point(0,0));
        Point _normalWindowLocation = Point.Empty;


        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isTopPanelDragged = true;
                Point pointStartPosition = this.PointToScreen(new Point(e.X, e.Y));
                offset = new Point();
                offset.X = this.Location.X - pointStartPosition.X;
                offset.Y = this.Location.Y - pointStartPosition.Y;
            }
            else
            {
                isTopPanelDragged = false;
            }
            if (e.Clicks == 2)
            {
                isTopPanelDragged = false;
                _MaxButton_Click(sender, e);
            }
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isTopPanelDragged)
            {
                Point newPoint = TopPanel.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(offset);
                this.Location = newPoint;

                if (this.Location.X > 2 || this.Location.Y > 2)
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.Location = _normalWindowLocation;
                        this.Size = _normalWindowSize;
                        toolTip1.SetToolTip(_MaxButton, "Maximize");
                        _MaxButton.CFormState = MinMaxButton.CustomFormState.Normal;
                        isWindowMaximized = false;
                    }
                }
            }
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isTopPanelDragged = false;
            if (this.Location.Y <= 5)
            {
                if (!isWindowMaximized)
                {
                    _normalWindowSize = this.Size;
                    _normalWindowLocation = this.Location;

                    Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                    this.Location = new Point(0, 0);
                    this.Size = new System.Drawing.Size(rect.Width, rect.Height);
                    toolTip1.SetToolTip(_MaxButton, "Restore Down");
                    _MaxButton.CFormState = MinMaxButton.CustomFormState.Maximize;
                    isWindowMaximized = true;
                }
            }
        }

        private void WindowTextLabel_MouseDown(object sender, MouseEventArgs e)
        {
            TopPanel_MouseDown(sender, e);
        }

        private void WindowTextLabel_MouseMove(object sender, MouseEventArgs e)
        {
            TopPanel_MouseMove(sender, e);
        }

        private void WindowTextLabel_MouseUp(object sender, MouseEventArgs e)
        {
            TopPanel_MouseUp(sender, e);
        }



        private void TopBorderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isTopBorderPanelDragged = true;
            }
            else
            {
                isTopBorderPanelDragged = false;
            }
        }

        private void TopBorderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y < this.Location.Y)
            {
                if (isTopBorderPanelDragged)
                {
                    if (this.Height < 50)
                    {
                        this.Height = 50;
                        isTopBorderPanelDragged = false;
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y + e.Y);
                        this.Height = this.Height - e.Y;
                    }
                }
            }
        }

        private void TopBorderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isTopBorderPanelDragged = false;
        }



        private void LeftPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Location.X <= 0 || e.X < 0)
            {
                isLeftPanelDragged = false;
                this.Location = new Point(10, this.Location.Y);
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    isLeftPanelDragged = true;
                }
                else
                {
                    isLeftPanelDragged = false;
                }
            }
        }

        private void LeftPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < this.Location.X)
            {
                if (isLeftPanelDragged)
                {
                    if (this.Width < 100)
                    {
                        this.Width = 100;
                        isLeftPanelDragged = false;
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X + e.X, this.Location.Y);
                        this.Width = this.Width - e.X;
                    }
                }
            }
        }

        private void LeftPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isLeftPanelDragged = false;
        }



        private void RightPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isRightPanelDragged = true;
            }
            else
            {
                isRightPanelDragged = false;
            }
        }

        private void RightPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRightPanelDragged)
            {
                if (this.Width < 100)
                {
                    this.Width = 100;
                    isRightPanelDragged = false;
                }
                else
                {
                    this.Width = this.Width + e.X;
                }
            }
        }

        private void RightPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isRightPanelDragged = false;
        }



        private void BottomPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isBottomPanelDragged = true;
            }
            else
            {
                isBottomPanelDragged = false;
            }
        }

        private void BottomPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isBottomPanelDragged)
            {
                if (this.Height < 50)
                {
                    this.Height = 50;
                    isBottomPanelDragged = false;
                }
                else
                {
                    this.Height = this.Height + e.Y;
                }
            }
        }

        private void BottomPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isBottomPanelDragged = false;
        }


        private void _MinButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void _MaxButton_Click(object sender, EventArgs e)
        {
            if (isWindowMaximized)
            {
                this.Location = _normalWindowLocation;
                this.Size = _normalWindowSize;
                toolTip1.SetToolTip(_MaxButton, "Maximize");
                _MaxButton.CFormState = MinMaxButton.CustomFormState.Normal;
                isWindowMaximized = false;
            }
            else
            {
                _normalWindowSize = this.Size;
                _normalWindowLocation = this.Location;

                Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                this.Location = new Point(0, 0);
                this.Size = new System.Drawing.Size(rect.Width, rect.Height);
                toolTip1.SetToolTip(_MaxButton, "Restore Down");
                _MaxButton.CFormState = MinMaxButton.CustomFormState.Maximize;
                isWindowMaximized = true;
            }
        }

        private void _CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_TextChanged(object sender, EventArgs e)
        {
            WindowTextLabel.Text = this.Text;
        }
        //**********************************************************************





        //***********************************************************************************
        //  ChangeTextOfReadyLabel() function to change text of ReadyLabel
        //***********************************************************************************
        public void ChangeTextOfReadyLabel(ToolStripMenuItem menuitem)
        {
            menuitem.MouseEnter += new EventHandler(this.menuitem_MouseEnter);
            menuitem.MouseLeave += new EventHandler(this.menuitem_MouseLeave);
        }
        private void menuitem_MouseEnter(object sender,EventArgs e)
        {
            Object b = (ToolStripMenuItem)sender;
            String s = b.ToString().Trim();
            switch(s)
            {
                case "File": AboutLabel.Text = "Create New,Open,Save,Close and Print Documents";
                    break;
                case "New": AboutLabel.Text = "Create New document";
                    break;
                case "Open": AboutLabel.Text = "Open New Document";
                    break;
                case "Save": AboutLabel.Text = "Save Current Document";
                    break;
                case "Save As": AboutLabel.Text = "Save As Current Document";
                    break;
                case "Save All": AboutLabel.Text = "Save All opened documents";
                    break;
                case "Close": AboutLabel.Text = "Close Current Document";
                    break;
                case "Close All": AboutLabel.Text = "Close All Opened Documents";
                    break;
                case "Open In System Editor": AboutLabel.Text = "Open current document in its system editor";
                    break;
                case "Print": AboutLabel.Text = "Print Current Document";
                    break;
                case "Print Preview": AboutLabel.Text = "Print Preview Current Document";
                    break;
                case "Exit": AboutLabel.Text = "Exit from Application";
                    break;

                case "Edit": AboutLabel.Text = "Cut,Copy,Paste,Undo,Redo,Find,Replace etc. in current document";
                    break;
                case "Cut": AboutLabel.Text = "Cut the selected text from current document";
                    break;
                case "Copy": AboutLabel.Text = "Copy the selected text from current document";
                    break;
                case "Paste": AboutLabel.Text = "Paste the text into current document";
                    break;
                case "Undo": AboutLabel.Text = "Perform Undo operation in current document";
                    break;
                case "Redo": AboutLabel.Text = "Perform Redo operation in current document";
                    break;
                case "Find": AboutLabel.Text = "Find a text in current document";
                    break;
                case "Replace": AboutLabel.Text = "Replace text in current document";
                    break;
                case "GoTo": AboutLabel.Text = "GoTo the specific line number in current document";
                    break;
                case "Select All": AboutLabel.Text = "Select all text in current document";
                    break;
                case "Change Case": AboutLabel.Text = "Change Upper,Lower and Sentence case of selected text";
                    break;
                case "Upper": AboutLabel.Text = "Change selected text case to Upper case";
                    break;
                case "Lower": AboutLabel.Text = "Change selected text case to Lower case";
                    break;
                case "Sentence": AboutLabel.Text = "Change selected text case to Sentence case";
                    break;
                case "Next Document": AboutLabel.Text = "Go to next document";
                    break;
                case "Previous Document": AboutLabel.Text = "Go to previous document";
                    break;

                case "View": AboutLabel.Text = "Set Font,Fore and Back color";
                    break;
                case "Font": AboutLabel.Text = "Set Font in current document";
                    break;
                case "Fore Color": AboutLabel.Text = "Set Fore Color in current document";
                    break;
                case "Back Color": AboutLabel.Text = "Set Back Color in current document";
                    break;
            }
        }
        private void menuitem_MouseLeave(object sender, EventArgs e)
        {
            AboutLabel.Text = "Ready";
        }

        public void UpdateReadyLabel()
        {
            ChangeTextOfReadyLabel(File_MenuItem);
            ChangeTextOfReadyLabel(File_New_MenuItem);
            ChangeTextOfReadyLabel(File_Open_MenuItem);
            ChangeTextOfReadyLabel(File_Save_MenuItem);
            ChangeTextOfReadyLabel(File_SaveAs_MenuItem);
            ChangeTextOfReadyLabel(File_SaveAll_MenuItem);
            ChangeTextOfReadyLabel(File_Close_MenuItem);
            ChangeTextOfReadyLabel(File_CloseAll_MenuItem);
            ChangeTextOfReadyLabel(File_OpenInSystemEditor_MenuItem);
            ChangeTextOfReadyLabel(File_Print_MenuItem);
            ChangeTextOfReadyLabel(File_PrintPreview_MenuItem);
            ChangeTextOfReadyLabel(File_Exit_MenuItem);

            ChangeTextOfReadyLabel(Edit_MenuItem);
            ChangeTextOfReadyLabel(Edit_Cut_MenuItem);
            ChangeTextOfReadyLabel(Edit_Copy_MenuItem);
            ChangeTextOfReadyLabel(Edit_Paste_MenuItem);
            ChangeTextOfReadyLabel(Edit_Undo_MenuItem);
            ChangeTextOfReadyLabel(Edit_Redo_MenuItem);
            ChangeTextOfReadyLabel(Edit_Find_MenuItem);
            ChangeTextOfReadyLabel(Edit_Replace_MenuItem);
            ChangeTextOfReadyLabel(Edit_GoTo_MenuItem);
            ChangeTextOfReadyLabel(Edit_SelectAll_MenuItem);
            ChangeTextOfReadyLabel(Edit_ChangeCase_MenuItem);
            ChangeTextOfReadyLabel(Edit_ChangeCase_Upper_MenuItem);
            ChangeTextOfReadyLabel(Edit_ChangeCase_Lower_MenuItem);
            ChangeTextOfReadyLabel(Edit_ChangeCase_Sentence_MenuItem);
            ChangeTextOfReadyLabel(Edit_NextDocument_MenuItem);
            ChangeTextOfReadyLabel(Edit_PreviousDocument_MenuItem);

            ChangeTextOfReadyLabel(View_MenuItem);
            ChangeTextOfReadyLabel(View_Font_MenuItem);
            ChangeTextOfReadyLabel(View_ForeColor_MenuItem);
            ChangeTextOfReadyLabel(View_BackColor_MenuItem);
        }




        //***************************************************************************
        //       IsArgumentNull Property  
        //***************************************************************************
        public static Boolean _isArgsNull = true;
        public Boolean IsArgumentNull
        {
            get { return _isArgsNull; }
            set { _isArgsNull = value; Invalidate(); }
        }


        //***************************************************************************
        //         MainForm Load
        //***************************************************************************
        private void MainForm_Load(object sender, EventArgs e)
        {
            if(_isArgsNull)
            {
                File_New_MenuItem_Click(sender, e);
                UpdateReadyLabel();
            }
        }

        //***************************************************************************
        //         MainForm Closing
        //***************************************************************************
        private void MainForm_Closing(object sender, FormClosingEventArgs e) 
        {
            if (myTabControlZ.TabCount > 0) 
            { 
                TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages; 
                foreach (TabPage tabpage in tabcoll)
                { 
                    myTabControlZ.SelectedTab = tabpage; 
                    if (tabpage.Text.Contains("*")) 
                    { 
                        DialogResult dg = MessageBox.Show("Do you want to save file " + tabpage.Text + " before close ?", "Save or Not", MessageBoxButtons.YesNoCancel); 
                        if (dg == DialogResult.Yes) 
                        { 
                            File_Save_MenuItem_Click(sender, e); 
                            myTabControlZ.TabPages.Remove(tabpage); 
                            myTabControlZ_SelectedIndexChanged(sender, e); 
                        } 
                        else if (dg == DialogResult.Cancel) 
                        { 
                            e.Cancel = true;
                            myTabControlZ.Select();
                            break;
                        } 
                    } 
                    else 
                    {
                        myTabControlZ.TabPages.Remove(tabpage); 
                        myTabControlZ_SelectedIndexChanged(sender, e); 
                    } 
                } 
            } 
        }

        //******************************************************************************************
        //         myTabControlZ_SelectedIndexChanged
        //******************************************************************************************
        private void myTabControlZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                if (tabpage.Text.Contains("Untitled"))
                {
                    FilenameToolStripLabel.Text = tabpage.Text;
                    this.Text = "Advanced Notepad in C# [ "+tabpage.Text+" ]";
                    UpdateWindowsList_WindowMenu();
                }
                else
                {
                    foreach (String filename in OpenedFilesList)
                    {
                        if (tabpage != null)
                        {
                            String str = filename.Substring(filename.LastIndexOf("\\") + 1);
                            if (tabpage.Text.Contains("*"))
                            {
                                String str2 = tabpage.Text.Remove(tabpage.Text.Length - 1);
                                if (str == str2)
                                {
                                    FilenameToolStripLabel.Text = filename;
                                    this.Text = "Advanced Notepad in C# [ " + tabpage.Text + " ]";
                                }
                            }

                            else
                            {
                                if (str == tabpage.Text)
                                {
                                    FilenameToolStripLabel.Text = filename;
                                    this.Text = "Advanced Notepad in C# [ " + tabpage.Text + " ]";
                                }
                            }
                        }
                    }

                    UpdateWindowsList_WindowMenu();
                }
            }
            else
            {
                FilenameToolStripLabel.Text = "Advanced Notepad in C#";
                this.Text = "Advanced Notepad in C#";
                UpdateWindowsList_WindowMenu();
            }
        }



        //******************************************************************************************
        //         treeView1_NodeMouseDoubleClick
        //******************************************************************************************
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            String str = treeView1.SelectedNode.ToString();
            String st = str.Substring(str.LastIndexOf(":") + 2);
            int treenode_length = st.Length;
            int tab_count = myTabControlZ.TabCount;

            System.Windows.Forms.TabControl.TabPageCollection tb = myTabControlZ.TabPages;
            foreach (TabPage tabpage in tb)
            {
                String tabstr = tabpage.Text;
                int tab_length = tabstr.Length;
                if (tabstr.Contains(st))
                {
                    myTabControlZ.SelectedTab = tabpage;
                }
            }

            if (myTabControlZ.SelectedIndex >= 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                _myRichTextBox.richTextBox1.Select();
            }

            this.UpdateWindowsList_WindowMenu();
            this.UpdateDocumentSelectorList();
        }



        //*************************************************************************************
        // Update windows list to Window menu
        //*************************************************************************************
        public void UpdateWindowsList_WindowMenu()
        {
            TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;

            int n = Window_MenuItem.DropDownItems.Count;
            for (int i = n - 1; i >= 4; i--)
            {
                Window_MenuItem.DropDownItems.RemoveAt(i);
            }


            foreach (TabPage tabpage in tabcoll)
            {
                ToolStripMenuItem menuitem = new ToolStripMenuItem();
                String s = tabpage.Text;
                menuitem.Text = s;
                if (myTabControlZ.SelectedTab == tabpage)
                {
                    menuitem.Checked = true;
                }
                else
                {
                    menuitem.Checked = false;
                }
                Window_MenuItem.DropDownItems.Add(menuitem);

                menuitem.Click += new System.EventHandler(WindowListEvent_Click);
            }
        }

        private void WindowListEvent_Click(object sender, EventArgs e)
        {
            ToolStripItem toolstripitem = (ToolStripItem)sender;
            TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;
            foreach (TabPage tb in tabcoll)
            {
                if (toolstripitem.Text == tb.Text)
                {
                    myTabControlZ.SelectedTab = tb;

                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    _myRichTextBox.richTextBox1.Select();

                    UpdateWindowsList_WindowMenu();
                }
            }
        }


        //*************************************************************************************
        //  File_MenuItem_DropDownOpening
        //*************************************************************************************
        private void File_MenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                File_New_MenuItem.Enabled = true;
                File_Open_MenuItem.Enabled = true;
                File_Save_MenuItem.Enabled = true;
                File_SaveAs_MenuItem.Enabled = true;
                File_SaveAll_MenuItem.Enabled = true;
                File_Close_MenuItem.Enabled = true;
                File_CloseAll_MenuItem.Enabled = true;
                File_OpenInSystemEditor_MenuItem.Enabled = true;
                File_Print_MenuItem.Enabled = true;
                File_PrintPreview_MenuItem.Enabled = true;
                File_Exit_MenuItem.Enabled = true;
            }
            else
            {
                File_New_MenuItem.Enabled = true;
                File_Open_MenuItem.Enabled = true;
                File_Save_MenuItem.Enabled = false;
                File_SaveAs_MenuItem.Enabled = false;
                File_SaveAll_MenuItem.Enabled = false;
                File_Close_MenuItem.Enabled = false;
                File_CloseAll_MenuItem.Enabled = false;
                File_OpenInSystemEditor_MenuItem.Enabled = false;
                File_Print_MenuItem.Enabled = false;
                File_PrintPreview_MenuItem.Enabled = false;
                File_Exit_MenuItem.Enabled = true;
            }
        }


        //*************************************************************************************
        //  Edit_MenuItem_DropDownOpening
        //*************************************************************************************
        private void Edit_Menu_DropDownOpening(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                Edit_Cut_MenuItem.Enabled = true;
                Edit_Copy_MenuItem.Enabled = true;
                Edit_Paste_MenuItem.Enabled = true;
                Edit_Undo_MenuItem.Enabled = true;
                Edit_Redo_MenuItem.Enabled = true;
                Edit_Find_MenuItem.Enabled = true;
                Edit_Replace_MenuItem.Enabled = true;
                Edit_GoTo_MenuItem.Enabled = true;
                Edit_SelectAll_MenuItem.Enabled = true;
                Edit_ChangeCase_MenuItem.Enabled = true;

                if(myTabControlZ.TabCount>1)
                {
                    Edit_NextDocument_MenuItem.Enabled = true;
                    Edit_PreviousDocument_MenuItem.Enabled = true;
                }
            }
            else
            {
                Edit_Cut_MenuItem.Enabled = false;
                Edit_Copy_MenuItem.Enabled = false;
                Edit_Paste_MenuItem.Enabled = false;
                Edit_Undo_MenuItem.Enabled = false;
                Edit_Redo_MenuItem.Enabled = false;
                Edit_Find_MenuItem.Enabled = false;
                Edit_Replace_MenuItem.Enabled = false;
                Edit_GoTo_MenuItem.Enabled = false;
                Edit_SelectAll_MenuItem.Enabled = false;
                Edit_ChangeCase_MenuItem.Enabled = false;
                Edit_NextDocument_MenuItem.Enabled = false;
                Edit_PreviousDocument_MenuItem.Enabled = false;
            }
        }


        //*************************************************************************************
        //  View_MenuItem_DropDownOpening
        //*************************************************************************************
        private void View_MenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                View_Font_MenuItem.Enabled = true;
                View_ForeColor_MenuItem.Enabled = true;
                View_BackColor_MenuItem.Enabled = true;
            }
            else
            {
                View_Font_MenuItem.Enabled = false;
                View_ForeColor_MenuItem.Enabled = false;
                View_BackColor_MenuItem.Enabled = false;
            }
        }


        //*************************************************************************************
        //  Run_MenuItem_DropDownOpening
        //*************************************************************************************
        private void Run_MenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                Run_RunInBrowser_MenuItem.Enabled = true;
                Run_PreviewHTMLPage_MenuItem.Enabled = true;
            }
            else
            {
                Run_RunInBrowser_MenuItem.Enabled = false;
                Run_PreviewHTMLPage_MenuItem.Enabled = false;
            }
        }


        public void UpdateDocumentSelectorList()
        {
            TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;
            treeView1.Nodes.Clear();
            foreach(TabPage tabpage in tabcoll)
            {
               String fname = tabpage.Text;
               Color color = Color.FromArgb(245, 255, 245);
                if (fname.Contains("*"))
                {
                    fname = fname.Remove(fname.Length - 1);
                }
                
                int imgindex = 4;

                if (fname.Contains(".c") || fname.Contains(".cpp"))
                {
                    imgindex = 0;
                }
                if(fname.Contains(".cs"))
                {
                    imgindex = 1;
                }
                if (fname.Contains(".html"))
                {
                    imgindex = 2;
                }
                if (fname.Contains(".vb"))
                {
                    imgindex = 3;
                }
                
                TreeNode trnode = new TreeNode();
                trnode.Text = fname;
                trnode.ImageIndex = imgindex;
                treeView1.Nodes.Add(trnode);
            }
        }




        //*************************************************************************************
        //  OpenAssociatedFiles_WhenApplicationStarts()
        //*************************************************************************************
        public void OpenAssociatedFiles_WhenApplicationStarts(String[] files)
        {
            StreamReader strReader;
            String str;
            foreach (string filename in files)
            {
                MyTabPage tabpage = new MyTabPage(this);

                strReader = new StreamReader(filename);
                str = strReader.ReadToEnd();
                strReader.Close();

                String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                tabpage.Text = fname;

                //add contextmenustrip to richTextBox1
                tabpage._myRichTextBox.richTextBox1.ContextMenuStrip = myContextMenuStrip;

                tabpage._myRichTextBox.richTextBox1.Text = str;
                myTabControlZ.TabPages.Add(tabpage);
                myTabControlZ.SelectedTab = tabpage;


                this.UpdateDocumentSelectorList();


                /* check (*) is available on TabPage Text
                 adding filename to tab page by removing (*) */
                fname = tabpage.Text;
                if (fname.Contains("*"))
                {
                    fname = fname.Remove(fname.Length - 1);
                }
                tabpage.Text = fname;

                //adding filenames to OpenedFilesList list
                OpenedFilesList.Add(filename);

                FilenameToolStripLabel.Text = filename;
                this.Text = "Advanced Notepad in C# [ " + fname + " ]";
            }


            if (myTabControlZ.SelectedIndex >= 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                _myRichTextBox.richTextBox1.Select();
            }
            UpdateWindowsList_WindowMenu();
        }



//*****************************************************************************************************************************
//                          File
//*****************************************************************************************************************************

        //***************************************************************************
        //         File -> New
        //***************************************************************************
        public static int count = 1;
        private void File_New_MenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tabpage = new MyTabPage(this);
            tabpage.Text = "Untitled " + count;
            myTabControlZ.TabPages.Add(tabpage);

            myTabControlZ.SelectedTab = tabpage;

            var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
            _myRichTextBox.richTextBox1.Select();

            //add contextmenustrip to richTextBox1
            _myRichTextBox.richTextBox1.ContextMenuStrip = myContextMenuStrip;

            this.UpdateDocumentSelectorList();

            this.Text = "Advanced Notepad in C# [ Untitled "+count+" ]";

            FilenameToolStripLabel.Text = tabpage.Text;

            UpdateWindowsList_WindowMenu();

            count++;
        }


        //***************************************************************************
        //          File -> Open
        //***************************************************************************
        private void File_Open_MenuItem_Click(object sender, EventArgs e)
        {
            StreamReader strReader;
            String str;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String[] files = openFileDialog1.FileNames;
                foreach (string filename in files)
                {
                    MyTabPage tabpage = new MyTabPage(this);

                    strReader = new StreamReader(filename);
                    str = strReader.ReadToEnd();
                    strReader.Close();

                    String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                    tabpage.Text = fname;

                    //add contextmenustrip to richTextBox1
                    tabpage._myRichTextBox.richTextBox1.ContextMenuStrip = myContextMenuStrip;

                    tabpage._myRichTextBox.richTextBox1.Text = str;
                    myTabControlZ.TabPages.Add(tabpage);
                    myTabControlZ.SelectedTab = tabpage;


                    this.UpdateDocumentSelectorList();


                    /* check (*) is available on TabPage Text
                     adding filename to tab page by removing (*) */
                    fname = tabpage.Text;
                    if (fname.Contains("*"))
                    {
                        fname = fname.Remove(fname.Length - 1);
                    }
                    tabpage.Text = fname;

                    //adding filenames to OpenedFilesList list
                    OpenedFilesList.Add(filename);

                    FilenameToolStripLabel.Text = filename;
                    this.Text = "Advanced Notepad in C# [ "+fname+" ]";
                }


                if (myTabControlZ.SelectedIndex >= 0)
                {
                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    _myRichTextBox.richTextBox1.Select();
                }
                UpdateWindowsList_WindowMenu();
            }
        }


        //***************************************************************************
        //         File -> Save
        //***************************************************************************
        private void File_Save_MenuItem_Click(object sender, EventArgs e)
        {
            TabPage seltab = myTabControlZ.SelectedTab;
            String selecttabname = seltab.Text;

            if (FilenameToolStripLabel.Text.Contains("\\"))
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                if (tabpage.Text.Contains("*"))
                {
                    String filename = FilenameToolStripLabel.Text;
                    if (File.Exists(filename))
                    {
                        var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                        File.WriteAllText(filename, "");
                        StreamWriter strwriter = System.IO.File.AppendText(filename);
                        strwriter.Write(_myRichTextBox.richTextBox1.Text);
                        strwriter.Close();
                        strwriter.Dispose();
                        tabpage.Text = tabpage.Text.Remove(tabpage.Text.Length - 1);

                        UpdateWindowsList_WindowMenu();

                        this.UpdateDocumentSelectorList();
                    }
                }
            }
            else
            {
                File_SaveAs_MenuItem_Click(sender, e);
            }
        }


        //***************************************************************************
        //         File -> Save As
        //***************************************************************************
        private void File_SaveAs_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                if (tabpage != null)
                {
                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        String filename = saveFileDialog1.FileName;
                        if (filename != "")
                        {
                            File.WriteAllText(filename, "");
                            StreamWriter strw = new StreamWriter(filename);
                            strw.Write(_myRichTextBox.richTextBox1.Text);
                            strw.Close();
                            strw.Dispose();

                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            tabpage.Text = fname;
                            this.Text = "Advanced Notepad in C# [ " + fname + " ]";
                            FilenameToolStripLabel.Text = filename;

                            OpenedFilesList.Add(filename);
                            UpdateWindowsList_WindowMenu();

                            this.UpdateDocumentSelectorList();
                        }
                    }
                }
            }
        }



        //***************************************************************************
        //         File -> Save All
        //***************************************************************************
        private void File_SaveAll_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                OpenedFilesList.Reverse();
                TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;

                foreach(TabPage tabpage in tabcoll)
                {
                    myTabControlZ.SelectedTab = tabpage;
                    myTabControlZ_SelectedIndexChanged(sender, e);
                    
                    if( ! tabpage.Text.Contains("Untitled"))
                    {
                        try
                        {
                            var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                            File.WriteAllText(FilenameToolStripLabel.Text, "");
                            StreamWriter strwriter = System.IO.File.AppendText(FilenameToolStripLabel.Text);
                            strwriter.Write(_myRichTextBox.richTextBox1.Text);
                            strwriter.Close();
                            strwriter.Dispose();
                        }
                        catch { }
                    }
                }

                System.Windows.Forms.TabControl.TabPageCollection tabcollection = myTabControlZ.TabPages;
                foreach (TabPage tabpage in tabcollection)
                {
                    String str = tabpage.Text;
                    if (str.Contains("*")&& !str.Contains("Untitled"))
                    {
                        str = str.Remove(str.Length - 1);
                    }
                    tabpage.Text = str;
                }
                UpdateWindowsList_WindowMenu();
            }
        }


        //***************************************************************************
        //         RemoveFileNamesFromTreeView()
        //***************************************************************************
        public void RemoveFileNamesFromTreeView(String filename)
        {
            TreeNodeCollection trcoll = treeView1.Nodes;
            foreach (TreeNode trnode in trcoll)
            {
                try
                {
                    if (trnode.Text == filename)
                    {
                        treeView1.Nodes.Remove(trnode);
                    }
                }
                catch (Exception e) { }
            }
        }



        //***************************************************************************
        //         File -> Close
        //***************************************************************************
        private void File_Close_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                if (tabpage.Text.Contains("*"))
                {
                    DialogResult dg = MessageBox.Show("Do you want to save " + tabpage.Text + " file before close ?", "Save before Close ?", MessageBoxButtons.YesNo);
                    if (dg == DialogResult.Yes)
                    {
                        //save file before close
                        File_Save_MenuItem_Click(sender, e);
                        //remove tab
                        myTabControlZ.TabPages.Remove(tabpage);

                        //RemoveFileNamesFromTreeView(tabpage.Text);
                        this.UpdateDocumentSelectorList();

                        UpdateWindowsList_WindowMenu();
                        myTabControlZ_SelectedIndexChanged(sender, e);

                        LineToolStripLabel.Text = "Line";
                        ColumnToolStripLabel.Text = "Col";

                        if (myTabControlZ.TabCount == 0)
                        {
                            FilenameToolStripLabel.Text = "Advanced Notepad in C#";
                            count = 1;
                        }
                    }
                    else
                    {
                        //remove tab
                        myTabControlZ.TabPages.Remove(tabpage);

                        UpdateDocumentSelectorList();

                        UpdateWindowsList_WindowMenu();
                        myTabControlZ_SelectedIndexChanged(sender, e);

                        LineToolStripLabel.Text = "Line";
                        ColumnToolStripLabel.Text = "Col";

                        if (myTabControlZ.TabCount == 0)
                        {
                            FilenameToolStripLabel.Text = "Advanced Notepad in C#";
                            count = 1;
                        }
                    }
                }
                else
                {
                    //remove tab
                    myTabControlZ.TabPages.Remove(tabpage);

                    RemoveFileNamesFromTreeView(tabpage.Text);
                    UpdateDocumentSelectorList();

                    UpdateWindowsList_WindowMenu();
                    myTabControlZ_SelectedIndexChanged(sender, e);

                    LineToolStripLabel.Text = "Line";
                    ColumnToolStripLabel.Text = "Col";

                    if (myTabControlZ.TabCount == 0)
                    {
                        FilenameToolStripLabel.Text = "Advanced Notepad in C#";
                        count = 1;
                    }
                }

                if (myTabControlZ.SelectedIndex >= 0)
                {
                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    _myRichTextBox.richTextBox1.Select();
                }

            }
            else
            {
                count = 1;
                FilenameToolStripLabel.Text = "Advanced Notepad in C#";

                LineToolStripLabel.Text = "Line";
                ColumnToolStripLabel.Text = "Col";
                File_New_MenuItem_Click(sender, e);
            }
        }



        //***************************************************************************
        //         File -> Close All
        //***************************************************************************
        private void File_CloseAll_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                System.Windows.Forms.TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;
                foreach (TabPage tabpage in tabcoll)
                {
                    myTabControlZ.SelectedTab = tabpage;

                    if (tabpage.Text.Contains("*"))
                    {
                        DialogResult dg = MessageBox.Show("Do you want to save file  " + tabpage.Text + "  before close ?", "Save before Close ?", MessageBoxButtons.YesNo);
                        if (dg == DialogResult.Yes)
                        {
                            //save file
                            File_Save_MenuItem_Click(sender, e);
                            //remove tab
                            myTabControlZ.TabPages.Remove(tabpage);
                            RemoveFileNamesFromTreeView(tabpage.Text);
                            UpdateWindowsList_WindowMenu();
                            myTabControlZ_SelectedIndexChanged(sender, e);
                            LineToolStripLabel.Text = "Line";
                            ColumnToolStripLabel.Text = "Col";

                            if (myTabControlZ.TabCount == 0)
                            {
                                count = 1;
                            }
                        }
                        else
                        {
                            //remove tab
                            myTabControlZ.TabPages.Remove(tabpage);
                            RemoveFileNamesFromTreeView(tabpage.Text);
                            UpdateWindowsList_WindowMenu();
                            myTabControlZ_SelectedIndexChanged(sender, e);
                            LineToolStripLabel.Text = "Line";
                            ColumnToolStripLabel.Text = "Col";

                            if (myTabControlZ.TabCount == 0)
                            {
                                count = 1;
                            }
                        }
                    }
                    else
                    {
                        //remove tab
                        myTabControlZ.TabPages.Remove(tabpage);
                        RemoveFileNamesFromTreeView(tabpage.Text);
                        UpdateWindowsList_WindowMenu();
                        myTabControlZ_SelectedIndexChanged(sender, e);
                        LineToolStripLabel.Text = "Line";
                        ColumnToolStripLabel.Text = "Col";

                        if (myTabControlZ.TabCount == 0)
                        {
                            count = 1;
                        }
                    }
                }
            }
            else
            {
                count = 1;
                FilenameToolStripLabel.Text = "Advanced Notepad in C#";
                LineToolStripLabel.Text = "Line";
                ColumnToolStripLabel.Text = "Col";
            }
        }


        //***************************************************************************
        //         File -> Open In System Editor
        //***************************************************************************
        private void File_OpenInSystemEditor_MenuItem_Click(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount >0)
            {
                if(FilenameToolStripLabel.Text.Contains("\\"))
                {
                    Process.Start(FilenameToolStripLabel.Text);
                }
            }
        }


        //***************************************************************************
        //         File -> Print
        //***************************************************************************
        private void File_Print_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                _myRichTextBox.richTextBox1.Print();
            }
        }


        //***************************************************************************
        //         File -> Print Preview
        //***************************************************************************
        private void File_PrintPreview_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                if (myTabControlZ.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControlZ.SelectedIndex;
                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    e.Graphics.DrawString(_myRichTextBox.richTextBox1.Text, _myRichTextBox.richTextBox1.Font, Brushes.Black, e.MarginBounds.Left, 0, new StringFormat());
                    e.Graphics.PageUnit = GraphicsUnit.Inch;
                }
            }
        }


        //***************************************************************************
        //         File -> Exit
        //***************************************************************************
        private void File_Exit_MenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


//*****************************************************************************************************************************
//                          Edit
//*****************************************************************************************************************************

        //***************************************************************************
        //         Edit -> Cut
        //***************************************************************************
        private void Edit_Cut_MenuItem_Click(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                if(_myRichTextBox.richTextBox1.SelectedText!="")
                {
                    if(Clipboard.ContainsText())
                    {
                        Clipboard.Clear();
                        Clipboard.SetText(_myRichTextBox.richTextBox1.SelectedText);
                        _myRichTextBox.richTextBox1.SelectedText = "";
                    }
                    else
                    {
                        Clipboard.SetText(_myRichTextBox.richTextBox1.SelectedText);
                        _myRichTextBox.richTextBox1.SelectedText = "";
                    }
                }
            }
        }

        //***************************************************************************
        //         Edit -> Copy
        //***************************************************************************
        private void Edit_Copy_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                if (_myRichTextBox.richTextBox1.SelectedText != "")
                {
                    if (Clipboard.ContainsText())
                    {
                        Clipboard.Clear();
                        Clipboard.SetText(_myRichTextBox.richTextBox1.SelectedText);
                    }
                    else
                    {
                        Clipboard.SetText(_myRichTextBox.richTextBox1.SelectedText);
                    }
                }
            }
        }

        //***************************************************************************
        //         Edit -> Paste
        //***************************************************************************
        private void Edit_Paste_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];

                if (Clipboard.ContainsText())
                {
                    String str = Clipboard.GetText();
                    _myRichTextBox.richTextBox1.Paste();
                }
            }
        }

        //***************************************************************************
        //         Edit -> Undo
        //***************************************************************************
        private void Edit_Undo_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                if(_myRichTextBox.richTextBox1.CanUndo)
                {
                    _myRichTextBox.richTextBox1.Undo();
                }
            }
        }

        //***************************************************************************
        //         Edit -> Redo
        //***************************************************************************
        private void Edit_Redo_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                if (_myRichTextBox.richTextBox1.CanRedo)
                {
                    _myRichTextBox.richTextBox1.Redo();
                }
            }
        }

        //***************************************************************************
        //         Edit -> Find
        //***************************************************************************
        private void Edit_Find_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                Find_Form f = new Find_Form(_myRichTextBox.richTextBox1);
                f.Show();
            }
        }

        //***************************************************************************
        //         Edit -> Replace
        //***************************************************************************
        private void Edit_Replace_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                Replace_Form f = new Replace_Form(_myRichTextBox.richTextBox1);
                f.ShowDialog();
            }
        }

        //***************************************************************************
        //         Edit -> GoTo
        //***************************************************************************
        private void Edit_GoTo_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                GoTo_Form rtf = new GoTo_Form(_myRichTextBox.richTextBox1);
                rtf.ShowDialog();
            }
        }

        //***************************************************************************
        //         Edit -> Select All
        //***************************************************************************
        private void Edit_SelectAll_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                _myRichTextBox.richTextBox1.SelectAll();
            }
        }

        //***************************************************************************
        //         Edit -> Change Case -> Upper
        //***************************************************************************
        private void Edit_ChangeCase_Upper_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                _myRichTextBox.richTextBox1.SelectedText=_myRichTextBox.richTextBox1.SelectedText.ToUpper();
            }
        }

        //***************************************************************************
        //         Edit -> Change Case -> Lower
        //***************************************************************************
        private void Edit_ChangeCase_Lower_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                _myRichTextBox.richTextBox1.SelectedText = _myRichTextBox.richTextBox1.SelectedText.ToLower();
            }
        }

        //***************************************************************************
        //         Edit -> Change Case -> Sentence
        //***************************************************************************
        private void Edit_ChangeCase_Sentence_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                String s = _myRichTextBox.richTextBox1.SelectedText;
                if (s != "")
                {
                    String firstchar = s[0].ToString();
                    firstchar = firstchar.ToUpper();
                    String str = firstchar + s.Remove(0, 1);
                    str = firstchar + str.Substring(1);
                    _myRichTextBox.richTextBox1.SelectedText = _myRichTextBox.richTextBox1.SelectedText.Replace(_myRichTextBox.richTextBox1.SelectedText, str);
                }
            }
        }

        //***************************************************************************
        //         Edit -> Next Document
        //***************************************************************************
        private void Edit_NextDocument_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int count = myTabControlZ.TabCount;
                if (myTabControlZ.SelectedIndex <= count)
                {
                    myTabControlZ.SelectedIndex = myTabControlZ.SelectedIndex + 1;
                }
                UpdateWindowsList_WindowMenu();
            }
        }

        //***************************************************************************
        //         Edit -> Previous Document
        //***************************************************************************
        private void Edit_PreviousDocument_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                if (myTabControlZ.SelectedIndex == 0)
                {
                }
                else
                {
                    myTabControlZ.SelectedIndex = myTabControlZ.SelectedIndex - 1;
                }
                UpdateWindowsList_WindowMenu();
            }
        }



//*****************************************************************************************************************************
//                           View
//*****************************************************************************************************************************

        //***************************************************************************
        //         View -> Font
        //***************************************************************************
        private void View_Font_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                FontDialog fd = new FontDialog();
                if(fd.ShowDialog()==DialogResult.OK)
                {
                    _myRichTextBox.richTextBox1.Font = fd.Font;
                }
            }
        }

        //***************************************************************************
        //         View -> Fore Color
        //***************************************************************************
        private void View_ForeColor_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    _myRichTextBox.richTextBox1.ForeColor = cd.Color;
                }
            }
        }

        //***************************************************************************
        //         View -> Back Color
        //***************************************************************************
        private void View_BackColor_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    _myRichTextBox.richTextBox1.BackColor = cd.Color;
                }
            }
        }

        //***************************************************************************
        //         View -> Document Selector
        //***************************************************************************
        private void View_DocumentSelector_MenuItem_Click(object sender, EventArgs e)
        {
            if(View_DocumentSelector_MenuItem.Checked==false)
            {
                splitContainer1.Panel1Collapsed = false;
                View_DocumentSelector_MenuItem.Checked = true;
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
                View_DocumentSelector_MenuItem.Checked = false;
            }
        }

        //***************************************************************************
        //         View -> ToolStrip
        //***************************************************************************
        private void View_ToolStrip_MenuItem_Click(object sender, EventArgs e)
        {
            if(View_ToolStrip_MenuItem.Checked==false)
            {
                myToolStripZ.Visible = true;
                toolstrip_backpanel.Visible = true;
                View_ToolStrip_MenuItem.Checked = true;
            }
            else
            {
                myToolStripZ.Visible = false;
                toolstrip_backpanel.Visible = false;
                View_ToolStrip_MenuItem.Checked = false;
            }
        }

        //***************************************************************************
        //         View -> Status Strip
        //***************************************************************************
        private void View_StatusStrip_MenuItem_Click(object sender, EventArgs e)
        {
            if (View_StatusStrip_MenuItem.Checked == false)
            {
                statusStrip1.Visible = true;
                statusstrip_backpanel.Visible = true;
                View_StatusStrip_MenuItem.Checked = true;
            }
            else
            {
                 statusStrip1.Visible = false;
                 statusstrip_backpanel.Visible = false;
                View_StatusStrip_MenuItem.Checked = false;
            }
        }


        //***************************************************************************
        //         View -> Tabs Align -> Full Screen
        //***************************************************************************
        Size formsizeholder = new Size(new Point(500, 300));
        Point formloc = new Point(0, 0);

        private void View_FullScreen_MenuItem_Click(object sender, EventArgs e)
        {
            if(View_FullScreen_MenuItem.Checked==false)
            {
                this.Visible = false;
                TopPanel.Visible = false;
                this.WindowState = FormWindowState.Maximized;
                this.Visible = true;

                formsizeholder = this.Size;
                formloc = this.Location;

                View_FullScreen_MenuItem.Checked = true;
            }
            else
            {
                this.Visible = false;
                TopPanel.Visible =true;
                this.Location = formloc;
                this.Size = formsizeholder;
                this.Visible = true;

                this.WindowState = FormWindowState.Normal;

                View_FullScreen_MenuItem.Checked =false ;
            }
        }

        //***************************************************************************
        //         View -> Tabs Align -> Top
        //***************************************************************************
        private void View_TabsAlign_Top_MenuItem_Click(object sender, EventArgs e)
        {
            if(View_TabsAlign_Top_MenuItem.Checked==false)
            {
                myTabControlZ.Alignment = TabAlignment.Top;
                View_TabsAlign_Top_MenuItem.Checked = true;

                if(View_TabsAlign_Bottom_MenuItem.Checked==true)
                {
                    View_TabsAlign_Bottom_MenuItem.Checked = false;
                }
            }
        }

        //***************************************************************************
        //         View -> Tabs Align -> Bottom
        //***************************************************************************
        private void View_TabsAlign_Bottom_MenuItem_Click(object sender, EventArgs e)
        {
            if (View_TabsAlign_Bottom_MenuItem.Checked == false)
            {
                myTabControlZ.Alignment = TabAlignment.Bottom;
                View_TabsAlign_Bottom_MenuItem.Checked = true;

                if (View_TabsAlign_Top_MenuItem.Checked == true)
                {
                    View_TabsAlign_Top_MenuItem.Checked = false;
                }
            }
        }


//*****************************************************************************************************************************
//                           Run
//*****************************************************************************************************************************

        //***************************************************************************
        //         Run -> Run
        //***************************************************************************
        private void Run_Run_MenuItem_Click(object sender, EventArgs e)
        {
            Run_Form run = new Run_Form();
            run.ShowDialog();
        }

        //***************************************************************************
        //         Run -> Run In Browser
        //***************************************************************************
        private void Run_RunInBrowser_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                if(FilenameToolStripLabel.Text.Contains("Untitled"))
                {
                    File_Save_MenuItem_Click(sender, e);
                }
                else
                {
                    RunInBrowser_Form rb = new RunInBrowser_Form(FilenameToolStripLabel.Text);
                    rb.ShowDialog();
                }
            }
        }

        //***************************************************************************
        //         Run -> Preview HTML Page
        //***************************************************************************
        private void Run_PreviewHTMLPage_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                PreviewHTMLPage_Form phtmlf = new PreviewHTMLPage_Form(_myRichTextBox.richTextBox1.Text,FilenameToolStripLabel.Text);
                phtmlf.Show();
            }
        }

        //***************************************************************************
        //         Run -> Google Search
        //***************************************************************************
        private void Run_GoogleSearch_MenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https:\\www.google.com");
        }

        //***************************************************************************
        //         Run -> Facebook
        //***************************************************************************
        private void Run_Facebook_MenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https:\\www.facebook.com");
        }

        //***************************************************************************
        //         Run -> Twitter
        //***************************************************************************
        private void Run_Twitter_MenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https:\\twitter.com"); 
        }


//*****************************************************************************************************************************
//                           Window
//*****************************************************************************************************************************
        //***************************************************************************
        //         Window -> Restart
        //***************************************************************************
        private void Window_Restart_MenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //***************************************************************************
        //         Window -> Close All Windows
        //***************************************************************************
        private void Window_CloseAllWindows_MenuItem_Click(object sender, EventArgs e)
        {
            File_CloseAll_MenuItem_Click(sender, e);
        }


//*****************************************************************************************************************************
//                           Help
//*****************************************************************************************************************************
        //***************************************************************************
        //         Help -> Help Contents
        //***************************************************************************
        private void Help_HelpContents_MenuItem_Click(object sender, EventArgs e)
        {
            String filename=Application.StartupPath+"\\Advanced Notepad Help.pdf";
            if(File.Exists(filename))
            {
                Process.Start(filename);
            }
        }

        //***************************************************************************
        //         Help -> Online Help
        //***************************************************************************
        private void Help_OnlineHelp_MenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https:\\www.google.com");
        }

        //***************************************************************************
        //         Help -> About
        //***************************************************************************
        private void Help_About_MenuItem_Click(object sender, EventArgs e)
        {
            About_Form af = new About_Form();
            af.ShowDialog();
        }




//*****************************************************************************************************************************
//                           Tool Strip Buttons Actions
//*****************************************************************************************************************************
        private void New_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_New_MenuItem_Click(sender, e);
        }

        private void Open_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_Open_MenuItem_Click(sender, e);
        }

        private void Save_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_Save_MenuItem_Click(sender, e);
        }

        private void SaveAs_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_SaveAs_MenuItem_Click(sender, e);
        }

        private void Print_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_Print_MenuItem_Click(sender, e);
        }

        private void Cut_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Cut_MenuItem_Click(sender, e);
        }

        private void Copy_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Copy_MenuItem_Click(sender, e);
        }

        private void Paste_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Paste_MenuItem_Click(sender, e);
        }

        private void Undo_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Undo_MenuItem_Click(sender, e);
        }

        private void Redo_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Redo_MenuItem_Click(sender, e);
        }

        private void Find_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Find_MenuItem_Click(sender, e);
        }

        private void GoTo_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_GoTo_MenuItem_Click(sender, e);
        }

        private void Font_ToolStripButton_Click(object sender, EventArgs e)
        {
            View_Font_MenuItem_Click(sender, e);
        }

        private void PreviewHTMLPage_ToolStripButton_Click(object sender, EventArgs e)
        {
            Run_PreviewHTMLPage_MenuItem_Click(sender, e);
        }




//*****************************************************************************************************************************
//                        richTextBox1 Context Menu Strip menus Actions
//*****************************************************************************************************************************
        private void Cut_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Cut_MenuItem_Click(sender, e);
        }

        private void Copy_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Copy_MenuItem_Click(sender, e);
        }

        private void Paste_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Paste_MenuItem_Click(sender, e);
        }

        private void SelectAll_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_SelectAll_MenuItem_Click(sender, e);
        }

        private void Upper_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_ChangeCase_Upper_MenuItem_Click(sender, e);
        }

        private void Lower_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_ChangeCase_Lower_MenuItem_Click(sender, e);
        }

        private void Sentence_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_ChangeCase_Sentence_MenuItem_Click(sender, e);
        }

        private void SetFont_ContextMenuItem_Click(object sender, EventArgs e)
        {
            View_Font_MenuItem_Click(sender, e);
        }

        private void PreviewHTMLPage_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Run_PreviewHTMLPage_MenuItem_Click(sender, e);
        }





//*****************************************************************************************************************************
//                        myTabControlZ Context Menu Strip menus Actions
//*****************************************************************************************************************************
        private void myTabControl_ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                myTabControl_Save_MenuItem.Text = "Save  " + tabpage.Text;
            }
        }

        private void myTabControl_Save_MenuItem_Click(object sender, EventArgs e)
        {
            File_Save_MenuItem_Click(sender, e);
        }

        private void myTabControl_SaveAll_MenuItem_Click(object sender, EventArgs e)
        {
            File_SaveAll_MenuItem_Click(sender, e);
        }

        private void myTabControl_Close_MenuItem_Click(object sender, EventArgs e)
        {
            File_Close_MenuItem_Click(sender, e);
        }

        private void myTabControl_CloseAll_MenuItem_Click(object sender, EventArgs e)
        {
            File_CloseAll_MenuItem_Click(sender, e);
        }


        private void myTabControl_CloseAllButThis_MenuItem_Click(object sender, EventArgs e)
        {
            String tabtext = myTabControlZ.SelectedTab.Text;
            if (myTabControlZ.TabCount > 1)
            {
                TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;
                foreach (TabPage tabpage in tabcoll)
                {
                    myTabControlZ.SelectedTab = tabpage;
                    if (myTabControlZ.SelectedTab.Text != tabtext)
                    {
                        File_Close_MenuItem_Click(sender, e);
                    }
                }
            }
            else if (myTabControlZ.TabCount == 1)
            {
                File_Close_MenuItem_Click(sender, e);
            }
        }


        private void myTabControl_OpenFileFolder_MenuItem_Click(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                if( ! myTabControlZ.SelectedTab.Text.Contains("Untitled"))
                {
                    if(FilenameToolStripLabel.Text.Contains("\\"))
                    {
                        TabPage tabpage = myTabControlZ.SelectedTab;
                        String tabtext = tabpage.Text;
                        if(tabtext.Contains("*"))
                        {
                            tabtext = tabtext.Remove(tabtext.Length - 1);
                        }
                        String fname = FilenameToolStripLabel.Text;
                        String filename=fname.Remove(fname.Length-(tabtext.Length+1));
                        Process.Start(filename);
                    }
                }
            }
        }




    }
}

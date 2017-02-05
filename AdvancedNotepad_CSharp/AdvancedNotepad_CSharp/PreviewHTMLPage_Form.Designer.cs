namespace AdvancedNotepad_CSharp
{
    partial class PreviewHTMLPage_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewHTMLPage_Form));
            this.panel1 = new System.Windows.Forms.Panel();
            this.close_button = new System.Windows.Forms.Button();
            this.foreword_button = new System.Windows.Forms.Button();
            this.back_button = new System.Windows.Forms.Button();
            this.reload_button = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.close_button);
            this.panel1.Controls.Add(this.foreword_button);
            this.panel1.Controls.Add(this.back_button);
            this.panel1.Controls.Add(this.reload_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(875, 30);
            this.panel1.TabIndex = 0;
            // 
            // close_button
            // 
            this.close_button.Location = new System.Drawing.Point(350, 4);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(75, 23);
            this.close_button.TabIndex = 3;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // foreword_button
            // 
            this.foreword_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foreword_button.Location = new System.Drawing.Point(239, 1);
            this.foreword_button.Name = "foreword_button";
            this.foreword_button.Size = new System.Drawing.Size(53, 26);
            this.foreword_button.TabIndex = 2;
            this.foreword_button.Text = ">";
            this.foreword_button.UseVisualStyleBackColor = true;
            this.foreword_button.Click += new System.EventHandler(this.foreword_button_Click);
            // 
            // back_button
            // 
            this.back_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_button.Location = new System.Drawing.Point(165, 1);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(55, 26);
            this.back_button.TabIndex = 1;
            this.back_button.Text = "<";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // reload_button
            // 
            this.reload_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reload_button.Location = new System.Drawing.Point(13, 1);
            this.reload_button.Name = "reload_button";
            this.reload_button.Size = new System.Drawing.Size(99, 26);
            this.reload_button.TabIndex = 0;
            this.reload_button.Text = "Reload";
            this.reload_button.UseVisualStyleBackColor = true;
            this.reload_button.Click += new System.EventHandler(this.reload_button_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 30);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(875, 504);
            this.webBrowser1.TabIndex = 1;
            // 
            // PreviewHTMLPage_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 534);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreviewHTMLPage_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview HTML Page";
            this.Load += new System.EventHandler(this.PreviewHTMLPage_Form_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button foreword_button;
        private System.Windows.Forms.Button back_button;
        private System.Windows.Forms.Button reload_button;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button close_button;
    }
}
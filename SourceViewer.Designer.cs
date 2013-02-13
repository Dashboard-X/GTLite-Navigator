namespace GTLite
{
    partial class SourceViewer
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
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.SuspendLayout();
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AllowDrop = true;
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(0, 15);
            this.fastColoredTextBox1.CommentPrefix = null;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.HTML;
            this.fastColoredTextBox1.LeftBracket = '<';
            this.fastColoredTextBox1.LeftBracket2 = '(';
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.RightBracket = '>';
            this.fastColoredTextBox1.RightBracket2 = ')';
            this.fastColoredTextBox1.Size = new System.Drawing.Size(535, 390);
            this.fastColoredTextBox1.TabIndex = 1;
            this.fastColoredTextBox1.WordWrap = true;
            // 
            // SourceViewer
            // 
            this.ClientSize = new System.Drawing.Size(535, 390);
            this.Controls.Add(this.fastColoredTextBox1);
            this.Name = "SourceViewer";
            this.ShowIcon = false;
            this.Text = "Source Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SourceViewer_FormClosed);
            this.Load += new System.EventHandler(this.SourceViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
    }
}
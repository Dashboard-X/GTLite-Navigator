namespace Tester
{
    partial class JokeSample
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
            this.components = new System.ComponentModel.Container();
            FastColoredTextBoxNS.SyntaxHighlighter syntaxHighlighter1 = new FastColoredTextBoxNS.SyntaxHighlighter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JokeSample));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoIndent = false;
            this.fastColoredTextBox1.AutoScroll = true;
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(0, 675);
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fastColoredTextBox1.IsChanged = true;
            this.fastColoredTextBox1.LeftBracket = '(';
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.RightBracket = ')';
            this.fastColoredTextBox1.SelectedText = "";
            this.fastColoredTextBox1.SelectionStart = 585;
            this.fastColoredTextBox1.ShowLineNumbers = false;
            this.fastColoredTextBox1.Size = new System.Drawing.Size(438, 287);
            this.fastColoredTextBox1.SyntaxHighlighter = syntaxHighlighter1;
            this.fastColoredTextBox1.TabIndex = 0;
            this.fastColoredTextBox1.Text = resources.GetString("fastColoredTextBox1.Text");
            this.fastColoredTextBox1.WordWrap = true;
            // 
            // JokeSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 287);
            this.Controls.Add(this.fastColoredTextBox1);
            this.Name = "JokeSample";
            this.Text = "JokeSample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
    }
}
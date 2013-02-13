namespace Tester
{
    partial class DynamicSyntaxHighlighting
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
            FastColoredTextBoxNS.SyntaxHighlighter syntaxHighlighter1 = new FastColoredTextBoxNS.SyntaxHighlighter();
            this.label1 = new System.Windows.Forms.Label();
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 77);
            this.label1.TabIndex = 3;
            this.label1.Text = "This example finds the functions declared in the program and dynamically highligh" +
                "ts all of their entry into the code of LISP.\r\nChange function name \'fibonacci\' a" +
                "nd \'fibonacci\' it will not highlighted.";
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoScroll = true;
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(360, 65);
            this.fastColoredTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DelayedTextChangedInterval = 400;
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.fastColoredTextBox1.IsChanged = true;
            this.fastColoredTextBox1.LeftBracket = '(';
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 77);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.RightBracket = ')';
            this.fastColoredTextBox1.SelectedText = "";
            this.fastColoredTextBox1.SelectionStart = 111;
            this.fastColoredTextBox1.ShowLineNumbers = false;
            this.fastColoredTextBox1.Size = new System.Drawing.Size(406, 204);
            this.fastColoredTextBox1.SyntaxHighlighter = syntaxHighlighter1;
            this.fastColoredTextBox1.TabIndex = 4;
            this.fastColoredTextBox1.Text = "\r\n(defun fibonacci(n)\r\n    (if (or (= n 0) (= n 1))\r\n     1\r\n     (+ (fibonacci (" +
                "- n 1)) (fibonacci (- n 2)))))";
            this.fastColoredTextBox1.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChangedDelayed);
            // 
            // DynamicSyntaxHighlighting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 281);
            this.Controls.Add(this.fastColoredTextBox1);
            this.Controls.Add(this.label1);
            this.Name = "DynamicSyntaxHighlighting";
            this.Text = "DynamicSyntaxHighlighting";
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private System.Windows.Forms.Label label1;
    }
}
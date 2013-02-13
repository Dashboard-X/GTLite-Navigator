﻿namespace Tester
{
    partial class AutocompleteSample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutocompleteSample));
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
            this.label1.Size = new System.Drawing.Size(354, 50);
            this.label1.TabIndex = 2;
            this.label1.Text = "This example shows how to create simplest autocomplete functionality.\r\nPopup menu" +
                " contains 500000 words.";
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoIndent = false;
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(0, 120);
            this.fastColoredTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DelayedEventsInterval = 500;
            this.fastColoredTextBox1.DelayedTextChangedInterval = 500;
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.LeftBracket = '(';
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 50);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.RightBracket = ')';
            this.fastColoredTextBox1.ShowLineNumbers = false;
            this.fastColoredTextBox1.Size = new System.Drawing.Size(354, 211);
            this.fastColoredTextBox1.TabIndex = 3;
            this.fastColoredTextBox1.Text = resources.GetString("fastColoredTextBox1.Text");
            this.fastColoredTextBox1.WordWrap = true;
            // 
            // AutocompleteSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 261);
            this.Controls.Add(this.fastColoredTextBox1);
            this.Controls.Add(this.label1);
            this.Name = "AutocompleteSample";
            this.Text = "AutocompleteSample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
    }
}
﻿namespace Tester
{
    partial class VisibleRangeChangedDelayedSample
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
            this.label1.Size = new System.Drawing.Size(371, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "This example shows how to highlight syntax for extremally large text by VisibleRa" +
                "ngeChangedDelayed event";
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoScroll = true;
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(25, 13);
            this.fastColoredTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DelayedEventsInterval = 1000;
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.fastColoredTextBox1.IsChanged = true;
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 48);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.SelectedText = "";
            this.fastColoredTextBox1.Size = new System.Drawing.Size(371, 235);
            this.fastColoredTextBox1.TabIndex = 1;
            this.fastColoredTextBox1.VisibleRangeChangedDelayed += new System.EventHandler(this.fastColoredTextBox1_VisibleRangeChangedDelayed);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 283);
            this.Controls.Add(this.fastColoredTextBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "VisibleRangeChangedDelayed sample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
    }
}


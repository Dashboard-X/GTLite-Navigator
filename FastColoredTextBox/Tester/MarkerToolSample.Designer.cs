namespace Tester
{
    partial class MarkerToolSample
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
            this.cmMark = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markAsYellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsRedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsGreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.markLineBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearMarkedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.cmMark.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmMark
            // 
            this.cmMark.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAsYellowToolStripMenuItem,
            this.markAsRedToolStripMenuItem,
            this.markAsGreenToolStripMenuItem,
            this.toolStripMenuItem1,
            this.markLineBackgroundToolStripMenuItem,
            this.toolStripMenuItem2,
            this.clearMarkedToolStripMenuItem});
            this.cmMark.Name = "contextMenuStrip1";
            this.cmMark.Size = new System.Drawing.Size(191, 126);
            // 
            // markAsYellowToolStripMenuItem
            // 
            this.markAsYellowToolStripMenuItem.Name = "markAsYellowToolStripMenuItem";
            this.markAsYellowToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.markAsYellowToolStripMenuItem.Tag = "yellow";
            this.markAsYellowToolStripMenuItem.Text = "Mark as Yellow";
            this.markAsYellowToolStripMenuItem.Click += new System.EventHandler(this.markAsYellowToolStripMenuItem_Click);
            // 
            // markAsRedToolStripMenuItem
            // 
            this.markAsRedToolStripMenuItem.Name = "markAsRedToolStripMenuItem";
            this.markAsRedToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.markAsRedToolStripMenuItem.Tag = "red";
            this.markAsRedToolStripMenuItem.Text = "Mark as Red";
            this.markAsRedToolStripMenuItem.Click += new System.EventHandler(this.markAsYellowToolStripMenuItem_Click);
            // 
            // markAsGreenToolStripMenuItem
            // 
            this.markAsGreenToolStripMenuItem.Name = "markAsGreenToolStripMenuItem";
            this.markAsGreenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.markAsGreenToolStripMenuItem.Tag = "green";
            this.markAsGreenToolStripMenuItem.Text = "Mark as Green";
            this.markAsGreenToolStripMenuItem.Click += new System.EventHandler(this.markAsYellowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(187, 6);
            // 
            // markLineBackgroundToolStripMenuItem
            // 
            this.markLineBackgroundToolStripMenuItem.Name = "markLineBackgroundToolStripMenuItem";
            this.markLineBackgroundToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.markLineBackgroundToolStripMenuItem.Tag = "lineBackground";
            this.markLineBackgroundToolStripMenuItem.Text = "Mark line background";
            this.markLineBackgroundToolStripMenuItem.Click += new System.EventHandler(this.markAsYellowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(187, 6);
            // 
            // clearMarkedToolStripMenuItem
            // 
            this.clearMarkedToolStripMenuItem.Name = "clearMarkedToolStripMenuItem";
            this.clearMarkedToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.clearMarkedToolStripMenuItem.Text = "Clear marked";
            this.clearMarkedToolStripMenuItem.Click += new System.EventHandler(this.clearMarkedToolStripMenuItem_Click);
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoIndent = false;
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(0, 15);
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DelayedEventsInterval = 500;
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.LeftBracket = '(';
            this.fastColoredTextBox1.LeftPadding = 15;
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.RightBracket = ')';
            this.fastColoredTextBox1.Size = new System.Drawing.Size(284, 262);
            this.fastColoredTextBox1.TabIndex = 0;
            this.fastColoredTextBox1.WordWrap = true;
            this.fastColoredTextBox1.SelectionChangedDelayed += new System.EventHandler(this.fastColoredTextBox1_SelectionChangedDelayed);
            this.fastColoredTextBox1.VisualMarkerClick += new System.EventHandler<FastColoredTextBoxNS.VisualMarkerEventArgs>(this.fastColoredTextBox1_VisualMarkerClick);
            this.fastColoredTextBox1.PaintLine += new System.EventHandler<FastColoredTextBoxNS.PaintLineEventArgs>(this.fastColoredTextBox1_PaintLine);
            // 
            // MarkerToolSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.fastColoredTextBox1);
            this.Name = "MarkerToolSample";
            this.Text = "MarkerTool Sample";
            this.cmMark.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private System.Windows.Forms.ContextMenuStrip cmMark;
        private System.Windows.Forms.ToolStripMenuItem markAsYellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAsRedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAsGreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearMarkedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markLineBackgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace GTLite
{
    public partial class Developer : DevComponents.DotNetBar.Office2007Form
    {
        public Developer()
        {
            InitializeComponent();
        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            webKitBrowser1.DocumentText = fastColoredTextBox1.Text;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opn = new OpenFileDialog())
            {
                opn.Filter = "HTML Files|*.html;*.htm|All Files|*.*";
                if (opn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string contents = System.IO.File.ReadAllText(opn.FileName);
                    fastColoredTextBox1.Text = contents;
                    webKitBrowser1.DocumentText = contents;
                }
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog svf = new SaveFileDialog())
            {
                svf.Filter = "HTML Files|*.html;*.htm|All Files|*.*";
                if (svf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.File.WriteAllText(svf.FileName, fastColoredTextBox1.Text);
                }
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            webKitBrowser1.ShowPrintDialog();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Developer s = new Developer();
            s.Show();
        }

        private void Developer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count < 2)
            {
                Application.OpenForms[0].Close();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webKitBrowser1.Navigate(textBox1.Text);
            webKitBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webKitBrowser1_DocumentCompleted);
            webKitBrowser1.Error += new WebKit.WebKitBrowserErrorEventHandler(webKitBrowser1_Error);
            fastColoredTextBox1.Text = "Getting source...";
            fastColoredTextBox1.Enabled = false;
            toolStrip1.Enabled = false;
        }

        void webKitBrowser1_Error(object sender, WebKit.WebKitBrowserErrorEventArgs e)
        {
            MessageBoxEx.Show(e.Description);
        }

        void webKitBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebKit.WebKitBrowser br = (sender as WebKit.WebKitBrowser);
            fastColoredTextBox1.Enabled = true;
            toolStrip1.Enabled = true;
            fastColoredTextBox1.Text = br.DocumentText;
            webKitBrowser1.DocumentText = br.DocumentText;
            br.Dispose();
            MessageBoxEx.Show("Source succesfully obtained!", "GTLite Developer",MessageBoxButtons.OK ,MessageBoxIcon.Information);
        }

        private void Developer_Load(object sender, EventArgs e)
        {
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBoxEx.Show("GTLite Developer Version 1.0 \r\n\r\n All rights reserved \r\n Copyright GT Web Software 2011", "GTLite Developer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.SelectAll();
        }
    }
}
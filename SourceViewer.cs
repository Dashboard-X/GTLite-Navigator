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
    public partial class SourceViewer : DevComponents.DotNetBar.Office2007Form
    {
        public SourceViewer(string source)
        {
            InitializeComponent();
            fastColoredTextBox1.Text = source;
        }

        private void SourceViewer_Load(object sender, EventArgs e)
        {

        }

        private void SourceViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count < 2)
            {
                Application.OpenForms[0].Close();
            }
        }
    }
}
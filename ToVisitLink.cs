using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GTLite
{
    public partial class ToVisitLink : UserControl
    {
        public delegate void VisitLink(object sender, ToVisitLinkEventArgs e);
        public event VisitLink VisitLinkButtonClicked = delegate { };
        public ToVisitLink(string name, string url, Image img)
        {
            InitializeComponent();
            labelX1.Text = name;
            labelX2.Text = url;
            pictureBox1.Image = img;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            VisitLinkButtonClicked(this, new ToVisitLinkEventArgs(labelX2.Text));
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
    public class ToVisitLinkEventArgs : EventArgs
    {
        public string Url { get; internal set; }
        public ToVisitLinkEventArgs(string url)
        {
            this.Url = url;
        }
    }
}

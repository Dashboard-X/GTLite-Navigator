using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebKit;

namespace GTLite
{
    public partial class PopupBlocked : UserControl
    {
        public Popup Form = null;
        public Main Owner = null;
        internal WebKitBrowser br;
        public PopupBlocked(WebKitBrowser browser, Main o)
        {
            InitializeComponent();
            Owner = o;
            br = browser;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            Form.Dispose();
            this.Dispose();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Form.Show();
            Form.Focus();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PopupExceptions.Add(Owner.Browser.Url.ToString());
            Owner.AddTab(br);
            Form.Dispose();
            this.Hide();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PopupExceptionsL.Add(Owner.Browser.Url.ToString());
            Form.Dispose();
            this.Dispose();
        }
    }
}

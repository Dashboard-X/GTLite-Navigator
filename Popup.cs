using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using WebKit;

namespace GTLite
{
    public partial class Popup : Form
    {
        public Popup(WebKitBrowser webKitBrowser)
        {
            InitializeComponent();
            this.webKitBrowser1 = webKitBrowser;
            this.webKitBrowser1.AllowDrop = true;
            this.webKitBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webKitBrowser1.BackColor = System.Drawing.Color.White;
            this.webKitBrowser1.Location = new System.Drawing.Point(4, 33);
            this.webKitBrowser1.Name = "webKitBrowser1";
            this.webKitBrowser1.PrivateBrowsing = true;
            this.webKitBrowser1.Size = new System.Drawing.Size(681, 426);
            this.webKitBrowser1.TabIndex = 0;
            this.webKitBrowser1.DocumentTitleChanged += new System.EventHandler(this.webKitBrowser1_DocumentTitleChanged);
            this.webKitBrowser1.CloseWindowRequest += new System.EventHandler(this.webKitBrowser1_CloseWindowRequest);
            this.webKitBrowser1.StatusTextChanged += new WebKit.StatusTextChanged(this.webKitBrowser1_StatusTextChanged);
            this.webKitBrowser1.ProgressChanged += new WebKit.ProgressChangedEventHandler(this.webKitBrowser1_ProgressChanged);
            this.webKitBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webKitBrowser1_DocumentCompleted);
            this.webKitBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webKitBrowser1_Navigated);
            this.webKitBrowser1.Navigating += new WebKit.WebKitBrowserNavigatingEventHandler(this.webKitBrowser1_Navigating);
            this.webKitBrowser1.CanGoBackChanged += new WebKit.CanGoBackChanged(this.webKitBrowser1_CanGoBackChanged);
            this.webKitBrowser1.CanGoForwardChanged += new WebKit.CanGoForwardChanged(this.webKitBrowser1_CanGoForwardChanged);
            this.webKitBrowser1.NewWindowCreated += new NewWindowCreatedEventHandler(webKitBrowser1_NewWindowCreated);
            this.webKitBrowser1.PopupCreated += new NewWindowCreatedEventHandler(webKitBrowser1_PopupCreated);
            this.Controls.Add(webKitBrowser1);
        }

        void webKitBrowser1_PopupCreated(object sender, NewWindowCreatedEventArgs e)
        {
            Popup p = new Popup(e.WebKitBrowser);
            p.Show();
        }

        Main FindFirstForm()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is Main)
                {
                    return (Main)f;
                }
            }
            return new Main();
        }

        void webKitBrowser1_NewWindowCreated(object sender, NewWindowCreatedEventArgs e)
        {
            Main f = FindFirstForm();
            foreach(Control c in f.qTabControl1.Controls)
            {
               if (c is GTTabPage)
               {
                   f.Controls.Remove(c);
               }
            }
            f.AddTab(e.WebKitBrowser);
        }

        private void Popup_Load(object sender, EventArgs e)
        {
            if (FindFirstForm().IsAero())
            {
                Functions.ExtendIntoClientArea(this, 40, 7, 7, 7);
                this.mainbar.Renderer = new NcRenderer.NcRenderer();
            }
            else { this.BackgroundImage = Properties.Resources.Form; }
        }

        private void webKitBrowser1_CloseWindowRequest(object sender, EventArgs e)
        {
            this.Close();
        }

        private void webKitBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            qTextBox1.Text = e.Url.ToString();
        }

        private void webKitBrowser1_StatusTextChanged(object sender, WebKit.WebKitBrowserStatusChangedEventArgs e)
        {
            toolStrip1.Visible = !String.IsNullOrEmpty(e.StatusText);
            label2.Text = e.StatusText;
        }

        private void webKitBrowser1_ProgressChanged(object sender, WebKit.ProgressChangesEventArgs e)
        {
            panelEx2.Value = e.Percent;
        }

        private void webKitBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = "Popup - " + (sender as WebKitBrowser).DocumentTitle;
        }

        private void webKitBrowser1_CanGoBackChanged(object sender, CanGoBackChangedEventArgs e)
        {
            toolStripButton1.Enabled = e.CanGoBack;
        }

        private void webKitBrowser1_CanGoForwardChanged(object sender, CanGoForwardChangedEventArgs e)
        {
            toolStripButton2.Enabled = e.CanGoForward;
        }

        private void webKitBrowser1_Navigating(object sender, WebKitBrowserNavigatingEventArgs e)
        {
            toolStripButton3.Enabled = false;
            toolStripButton4.Enabled = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            webKitBrowser1.Reload();
            toolStripButton3.Enabled = false;
            toolStripButton4.Enabled = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            webKitBrowser1.Stop();
            toolStripButton3.Enabled = true;
            toolStripButton4.Enabled = false;
        }

        private void webKitBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripButton3.Enabled = true;
            toolStripButton4.Enabled = false;
        }

        private void qTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                webKitBrowser1.Navigate(qTextBox1.Text);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            groupPanel2.Visible = true;
            qTextBox2.Text = webKitBrowser1.DocumentTitle;
            qTextBox3.Text = webKitBrowser1.Url.ToString();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            Bookmarking.Add(qTextBox2.Text, qTextBox3.Text);
            int i = Bookmarking.GetItemsCount() - 1;
            ToolStripButton bk = new ToolStripButton();
            bk.AutoToolTip = false;
            if (Bookmarking.Name(i).Length > 20 == true)
            {
                bk.Text = Bookmarking.Name(i).Substring(0, 17) + "...";
            }
            else { bk.Text = Bookmarking.Name(i); }
            bk.ToolTipText = Bookmarking.Url(i);
            bk.ForeColor = Color.Black;
            bk.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            bk.Image = Functions.GetIcon(bk.ToolTipText);
            Main._col.Add(bk);
            foreach (Form f in Application.OpenForms)
            {
                try
                {
                    ((Main)f).itemPanel1.Items.Add(bk);
                    bk.MouseDown += new MouseEventHandler(((Main)f).bk_MouseDown);
                }
                catch { }
            }
            buttonX5.Parent.Visible = false;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            groupPanel2.Visible = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webKitBrowser1.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webKitBrowser1.GoForward();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Qios.DevSuite.Components;

namespace GTLite
{
    public partial class Resources : DevComponents.DotNetBar.Office2007Form
    {
        private List<WebKit.WebKitResource> resources;
        public Resources(List<WebKit.WebKitResource> r)
        {
            InitializeComponent();
            resources = r;
        }

        private void Resources_Load(object sender, EventArgs e)
        {
            List<WebKit.WebKitResource> all = new List<WebKit.WebKitResource>();
            List<WebKit.WebKitResource> img = new List<WebKit.WebKitResource>();
            List<WebKit.WebKitResource> css = new List<WebKit.WebKitResource>();
            List<WebKit.WebKitResource> other = new List<WebKit.WebKitResource>();
            List<WebKit.WebKitResource> rss = new List<WebKit.WebKitResource>();
            List<WebKit.WebKitResource> js = new List<WebKit.WebKitResource>();
            foreach (WebKit.WebKitResource res in resources)
            {
                all.Add(res);
                if (res.MimeType.Contains("image"))
                {
                    img.Add(res);
                }
                else if (res.MimeType.Contains("rss"))
                {
                    rss.Add(res);
                }
                else if (res.MimeType.Contains("css"))
                    css.Add(res);
                else if (res.MimeType.Contains("script"))
                    js.Add(res);
                else
                    other.Add(res);
            }
            All.DataSource = all;
            Images.DataSource = img;
            CSS.DataSource = css;
            JS.DataSource = js;
            RSS.DataSource = rss;
            Other.DataSource = other;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DataGridViewX d = (DataGridViewX)superTabControl1.SelectedPanel.Controls[0];
            if (d.SelectedRows.Count == 1)
            {
                WebKit.WebKitResource r;
                string filter;
                r = (WebKit.WebKitResource)d.SelectedRows[0].DataBoundItem;
                string t;
                t = (string)r.Url.Split(Convert.ToChar(".")).GetValue(r.Url.Split(Convert.ToChar(".")).Length - 1);
                if (r.MimeType.Contains("flv"))
                    filter = "Flash Video (*.flv)|*.flv|All Files|*.*";
                else if (r.MimeType.Contains("script") && r.Url.EndsWith(".js") != true)
                    filter = "JavaScript Files (*.js)|*.js";
                else if (r.MimeType.Contains("text/html") && !r.Url.EndsWith(".asp"))
                    filter = "HTML Files (*.html)|*.html";
                else
                {
                    filter = t.ToUpper() + @" Files (*." + t.ToLower() + ")|*." + t + "|All Files|*.*";
                }
                SaveFileDialog s = new SaveFileDialog();
                s.Filter = filter;
                if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.Net.WebClient c = new System.Net.WebClient();
                    c.DownloadFileAsync(new Uri(r.Url), s.FileName);
                }
            }
            if (d.SelectedRows.Count > 1)
            {
                List<WebKit.WebKitResource> res = new List<WebKit.WebKitResource>();
                foreach (DataGridViewRow ra in ((DataGridViewX)superTabControl1.SelectedTab.AttachedControl.Controls[0]).SelectedRows)
                    res.Add((WebKit.WebKitResource)ra.DataBoundItem);
                ResourceDownloader dl = new ResourceDownloader(res);
                dl.Show();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            bool found = false;
            WebKit.WebKitResource r;
            DataGridViewX d = (DataGridViewX)superTabControl1.SelectedPanel.Controls[0];
            r = (WebKit.WebKitResource)d.SelectedRows[0].DataBoundItem;
            foreach(Form f in Application.OpenForms)
                if (f is Main)
                {
                    (f as Main).AddTab(r.Url);
                    found = true;
                }
            if (found == false)
            {
                Main f = new Main();
                foreach (Control s in f.qTabControl1.Controls)
                    if (s is QTabPage)
                        f.qTabControl1.Controls.Remove(s);
                f.AddTab(r.Url);
                f.Show();
            }
        }

        private void Resources_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count < 2)
            {
                Application.OpenForms[0].Close();
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            List<WebKit.WebKitResource> res = new List<WebKit.WebKitResource>();
            foreach (DataGridViewRow r in ((DataGridViewX)superTabControl1.SelectedTab.AttachedControl.Controls[0]).Rows)
                res.Add((WebKit.WebKitResource)r.DataBoundItem);
            ResourceDownloader dl = new ResourceDownloader(res);
            dl.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using GTLite;

namespace RSS_Reader
{
    public partial class Reader : DevComponents.DotNetBar.Office2007Form 
    {
        XmlTextReader rssReader;
        XmlDocument rssDoc;
        XmlNode nodeRss;
        XmlNode nodeChannel;
        XmlNode nodeItem;
        ListViewItem rowNews;
        List<Feed> MyFeeds = new List<Feed>();
        public Reader(string feedurl)
        {
            InitializeComponent();
            txtUrl.Text = feedurl;
            buttonX1.PerformClick();
            Url = feedurl;
            groupPanel1.Visible = false;
        } 
        public Reader()
        {
            InitializeComponent();
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            lstNews.Items.Clear();
            this.Cursor = Cursors.WaitCursor;

            rssReader = new XmlTextReader(txtUrl.Text);
            Url = txtUrl.Text;
            rssDoc = new XmlDocument();

            rssDoc.Load(rssReader);
            
            for (int i = 0; i < rssDoc.ChildNodes.Count; i++)
            {
                if (rssDoc.ChildNodes[i].Name == "rss")
                {
                    nodeRss = rssDoc.ChildNodes[i];
                }
            }

            for (int i = 0; i < nodeRss.ChildNodes.Count; i++)
            {
                if (nodeRss.ChildNodes[i].Name == "channel")
                {
                    nodeChannel = nodeRss.ChildNodes[i];
                }
            }

            lblTitle.Text = "Title: " + nodeChannel["title"].InnerText;
            lblDescription.Text = "Description: " + nodeChannel["description"].InnerText;

            for (int i = 0; i < nodeChannel.ChildNodes.Count; i++)
            {
                if (nodeChannel.ChildNodes[i].Name == "item")
                {
                    try
                    {
                        nodeItem = nodeChannel.ChildNodes[i];
                        rowNews = new ListViewItem();
                        rowNews.Text = nodeItem["title"].InnerText;
                        rowNews.SubItems.Add(nodeItem["link"].InnerText);
                        lstNews.Items.Add(rowNews);
                    }
                    catch { }
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void lstNews_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNews.SelectedItems.Count == 1)
            {
                for (int i = 0; i < nodeChannel.ChildNodes.Count; i++)
                {
                    if (nodeChannel.ChildNodes[i].Name == "item")
                    {
                        nodeItem = nodeChannel.ChildNodes[i];
                        if (nodeItem["title"].InnerText == lstNews.SelectedItems[0].Text)
                        {
                            txtContent.Text = nodeItem["description"].InnerText;
                            break;
                        }
                    }
                }
            }
        }

        private void lstNews_DoubleClick(object sender, EventArgs e)
        {
            bool found = false;
            string url = (string)lstNews.SelectedItems[0].SubItems[1].Text;
            foreach (Form f in Application.OpenForms)
                if (f is Main)
                {
                    (f as Main).AddTab(url);
                    found = true;
                }
            if (found == false)
            {
                Main f = new Main();
                f.AddTab(url);
                f.Show();
            }  
        }
        public string Url;
        private void frmMain_Load(object sender, EventArgs e)
        {
            string[] f = File.ReadAllLines(Application.StartupPath + @"\Properties\Feeds.data");
            foreach (string f1 in f)
                MyFeeds.Add(new Feed((string)f1.Split(Convert.ToChar(";")).GetValue(0), (string)f1.Split(Convert.ToChar(";")).GetValue(1)));
            dataGridViewX1.DataSource = MyFeeds;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (lstNews.SelectedItems.Count == 0) 
                return;
            bool found = false;
            string url = (string)lstNews.SelectedItems[0].SubItems[1].Text;
            foreach (Form f in Application.OpenForms)
                if (f is Main)
                {
                    (f as Main).AddTab(url);
                    found = true;
                }
            if (found == false)
            {
                Main f = new Main();
                f.AddTab(url);
                f.Show();
            }
        }

        private void Reader_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count < 2)
            {
                Application.OpenForms[0].Close();
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Url))
                return;
            MyFeeds.Add(new Feed(Url, lblTitle.Text));
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = MyFeeds; 
            Save();
        }
        void Save()
        {
            string tow = "";
            foreach (DataGridViewRow r in dataGridViewX1.Rows)
            {
                Feed c = (Feed)r.DataBoundItem;
                tow = tow + c.Url + ";" + c.Name + "\r\n";
            }
            System.IO.File.WriteAllText(Application.StartupPath + @"\Properties\Feeds.data", tow);
        }
        private void buttonX5_Click(object sender, EventArgs e)
        {
            groupPanel1.Visible = !groupPanel1.Visible;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            MyFeeds.Remove((Feed)dataGridViewX1.SelectedRows[0].DataBoundItem);
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = MyFeeds;
            Save();
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            MyFeeds.Clear();
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = MyFeeds; Save();
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            txtUrl.Text = ((Feed)dataGridViewX1.SelectedRows[0].DataBoundItem).Url;
            btnRead_Click(this, new EventArgs());
            groupPanel1.Visible = false;
        }

        private void dataGridViewX1_Click(object sender, EventArgs e)
        {
            bool en = (dataGridViewX1.SelectedRows.Count != 0);
            buttonX8.Enabled = en;
            buttonX6.Enabled = en;
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    public class Feed
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public Feed(string u, string n)
        {
            Url = u;
            Name = n;
        }
    }
}
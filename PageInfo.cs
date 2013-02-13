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
    public partial class PageInfo : DevComponents.DotNetBar.Office2007Form
    {
        private WebKitBrowser _b;
        public PageInfo(WebKit.WebKitBrowser wb)
        {
            try
            {
                InitializeComponent();
                WebKit.DOM.Document doc = wb.Document;
                textBoxX1.Text = wb.Url.ToString();
                richTextBox1.Text = wb.DocumentText;
                label1.Text = wb.DocumentTitle;
                List<WebKit.DOM.Node> l = new List<WebKit.DOM.Node>();
                foreach (WebKit.DOM.Node n in wb.Document.GetElementsByTagName("*"))
                { l.Add(n); }
                dataGridView1.DataSource = l;
                pictureBox2.Image = Functions.GetIcon(wb.Url.ToString());
                foreach (string s in History.AllHisItems)
                {
                    if (s.Contains(wb.Url.Host) == true)
                    {
                        labelX5.Text = "You have visited this site at: " + s.Split(Convert.ToChar("|")).GetValue(2).ToString();
                        break;
                    }
                }
                _b = ((WebKitBrowser)wb);
            }
            catch { MessageBoxEx.Show("Info for this page can not be displayed. Sorry for the inconvenience.", "Error"); this.Dispose(); }
        }


        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxX4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxX5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxX6_TextChanged(object sender, EventArgs e)
        {

        }

        private void PageInfo_Load(object sender, EventArgs e)
        {

        }

        private void superTabControlPanel1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
           _b.ShowPrintDialog();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
           _b.ShowPrintPreviewDialog();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            _b.ShowPageSetupDialog();
        }

        private void PageInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count < 2)
            {
                Application.OpenForms[0].Close();
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            _b.ShowSaveAsDialog();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
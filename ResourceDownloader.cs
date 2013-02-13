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
    public partial class ResourceDownloader : Form
    {
        int total = 0;
        int failed = 0;
        int count = 0;
        bool stopped = false;
        public ResourceDownloader(List<WebKit.WebKitResource> col)
        {
            InitializeComponent();
            progressBar1.Maximum = col.Count;
            progressBar1.Validated += new EventHandler(progressBar1_Validated);
            count = col.Count;
            using (FolderBrowserDialog dl = new FolderBrowserDialog())
            {
                dl.Description = "Please select the folder where you want the files to be saved.";
                if (dl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (WebKit.WebKitResource res in col)
                    {
                        if (!stopped)
                        {
                            using (System.Net.WebClient c = new System.Net.WebClient())
                            {
                                if (res.Url != null)
                                {
                                    string t;
                                    t = (string)res.Url.Split(Convert.ToChar(".")).GetValue(res.Url.Split(Convert.ToChar(".")).Length - 1);
                                    if (res.MimeType.Contains("flv"))
                                        t = ".flv";
                                    else if (res.MimeType.Contains("script") && res.Url.EndsWith(".js") != true)
                                        t = ".js";
                                    else if (res.MimeType.Contains("text/html") && !res.Url.EndsWith(".asp"))
                                        t = ".html";
                                    else
                                    {
                                        t = "." + t.ToLower().Replace(@"\", "");
                                    }
                                    string fn = ((string)((string)res.Url.Split(Convert.ToChar(@"/")).GetValue(res.Url.Split(Convert.ToChar("/")).Length - 1)).Replace(@"\", "").Split(Convert.ToChar(".")).GetValue(0)).Replace("?", "");
                                    try
                                    {
                                        c.DownloadFileAsync(new Uri(res.Url), dl.SelectedPath + "\\" + fn + t);
                                        c.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(c_DownloadProgressChanged);
                                        c.DownloadFileCompleted += new AsyncCompletedEventHandler(c_DownloadFileCompleted);
                                        
                                        UpdateStatus();
                                    }
                                    catch { failed++; UpdateStatus(); }
                                }
                            }
                        }
                    }
                }
            }
        }
        void UpdateStatus()
        {
            label3.Text = total.ToString() + " have been downloaded and " + failed.ToString() + " failed out of " + count.ToString(); 
        }
        void progressBar1_Validated(object sender, EventArgs e)
        {
            if ((sender as ProgressBar).Value == (sender as ProgressBar).Maximum)
            {
                MessageBoxEx.Show(total.ToString() + " files were saved and " + failed.ToString() + " failed to be downloaded.");
                button1.Enabled = false;
            }
        }

        void c_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value++;
            total++;
            UpdateStatus();
            if (progressBar2.Maximum != -1)
            progressBar2.Value = progressBar2.Maximum;
        }

        void c_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            if ((int)e.BytesReceived != -1)
            {
                try
                {
                    progressBar2.Maximum = (int)e.TotalBytesToReceive;
                    progressBar2.Value = (int)e.BytesReceived;
                }
                catch { }
            }
        }

        private void ResourceDownloader_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            stopped = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}

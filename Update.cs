using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace GTLite
{
    public partial class Update : DevComponents.DotNetBar.Office2007Form
    {
        string DownloadLink = "";
        int VersionNum;
        public Update()
        {
            InitializeComponent();
            label3.Text = "Current Version: " + Application.ProductVersion.ToString();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                using (WebClient wbc = new WebClient())
                {
                    wbc.DownloadFileCompleted += new AsyncCompletedEventHandler(wbc_DownloadFileCompleted);

                    wbc.DownloadFileAsync(new Uri("http://gt-web-software.webs.com/Version_Info.txt"), Application.StartupPath + @"\_temp.txt");
                }
            }
            else
            {
                MessageBox.Show("An active network connection is required for the update operation to function. Please check your connection settings and try again.");
                this.Close();
            }
        }

        void wbc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string[] updc = System.IO.File.ReadAllLines(Application.StartupPath + @"\_temp.txt");
                VersionNum = Convert.ToInt32(((string)updc.GetValue(0)).Replace(".", ""));
                label4.Text = "Latest Version: " + (string)updc.GetValue(0);
                int CurVersion = Convert.ToInt32(Application.ProductVersion.ToString().Replace(".", ""));
                DownloadLink = (string)updc.GetValue(1);
                t = updc;
                if (CurVersion < VersionNum)
                {
                    label1.Text = "A new update is available for downloading";
                    buttonX1.Enabled = true;
                    timer1.Enabled = true;
                }
                else
                    label1.Text = "GTLite Navigator is up to date";
                System.IO.File.Delete(Application.StartupPath + @"\_temp.txt");
            }
            catch (Exception ex) { MessageBox.Show("An error occured and the update operation could not be successfully completed. \r\n error:" + ex.Message); }
        }
        string[] t;
        void ShowChanges(string[] updc)
        {
            label2.Text = "Changes: \r\n";
            for (int i = 2; i < updc.Length; i++)
            {
                label2.Text = label2.Text + updc.GetValue(i).ToString() + "\r\n";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Height <= 287)
            {
                this.Height = this.Height + 3;
                this.Invalidate();
            }
            else
            {
                (sender as Timer).Enabled = false;
                ShowChanges(t);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            using (WebClient downloader = new WebClient())
            {
                label1.Text = "Starting download...";
                dest = System.IO.Path.GetTempPath() + @"\setup.exe";
                downloader.DownloadFileAsync(new Uri(DownloadLink), dest);
                downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloader_DownloadProgressChanged);
                downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(downloader_DownloadFileCompleted);
            }
        }
        string dest;
        void downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value = progressBar1.Maximum;
            MessageBox.Show("You now need to close GTLite Navigator so that the update successfully completes. The Setup window will be shown to you. Follow the steps provided to complete the installation.");
            Process.Start(dest);
        }

        void downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            label1.Text = e.BytesReceived + " bytes received out of " + e.TotalBytesToReceive + " (" + e.ProgressPercentage + "%)";
            progressBar1.Maximum = (int)e.TotalBytesToReceive;
            progressBar1.Minimum = 0;
            progressBar1.Value = (int)e.BytesReceived;
        }
    }
}

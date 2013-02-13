using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace GTLite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackgroundWorker bckw = new BackgroundWorker();
            bckw.DoWork += new DoWorkEventHandler(bckw_DoWork);
            bckw.RunWorkerAsync();
            if (!Properties.Settings.Default.IsFirstTime)
            {
                SplashScreen.SplashScreen.ShowSplashScreen();
                new Main().Show();
                this.Visible = false;
            }
            else { new StartupForm().Show(); }
            
        }

        void bckw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                using (WebClient wbc = new WebClient())
                {
                    wbc.DownloadFileCompleted += new AsyncCompletedEventHandler(wbc_DownloadFileCompleted);

                    wbc.DownloadFileAsync(new Uri("http://gt-web-software.webs.com/Version_Info.txt"), Application.StartupPath + @"\_temp.txt");
                }
            }
        }
        void wbc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string[] updc = System.IO.File.ReadAllLines(Application.StartupPath + @"\_temp.txt");
                int CurVersion = Convert.ToInt32(Application.ProductVersion.ToString().Replace(".", ""));
                int VersionNum = Convert.ToInt32(((string)updc.GetValue(0)).Replace(".", ""));
                if (!Properties.Settings.Default.InformedAboutUpdate.Contains(VersionNum.ToString()))
                {
                    if (CurVersion < VersionNum)
                    {
                        if (MessageBox.Show("An update is available for download! Would you like to open the Updater and download it?", "Update", MessageBoxButtons.YesNoCancel) == System.Windows.Forms.DialogResult.Yes)
                            this.Invoke(new MethodInvoker(delegate { new GTLite.Update().Show();}));
                    }
                    GTLite.Properties.Settings.Default.InformedAboutUpdate.Add(VersionNum.ToString()); 
                    GTLite.Properties.Settings.Default.Save();
                }
                System.IO.File.Delete(Application.StartupPath + @"\_temp.txt");
            }
            catch { }
          }
    }
}

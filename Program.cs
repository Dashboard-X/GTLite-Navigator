using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic;
using System.IO;
using DevComponents.DotNetBar;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace GTLite
{

    static class HistoryCollection
    {
        public static AutoCompleteStringCollection _col;
        public static void init()
        {
            _col = new AutoCompleteStringCollection();
            for (int i = 0; i <= History.GetItemsCount() - 1; i++)
            {
                _col.Add(History.Url(i));
            }
        }
    }
    static class Program
    {
        public static bool HasUsedArguments = false;
        public static bool HasWarnedForInternetConnectionProblems = false;
        public static bool DeleteCacheAtExit = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (System.Environment.OSVersion.Version.Major < 6) { MessageBoxEx.Show("Your system is not supported. GTLite Navigator only supports Windows Vista/7."); Application.Exit(); }
            if (File.Exists(Application.StartupPath + @"\hasrun.file"))
            {
                Properties.Settings.Default.Upgrade();
                File.Create(Application.StartupPath + @"\hasrun.file");
            }
            if (!Directory.Exists(Application.StartupPath + @"\Properties"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Properties");
            }
            try
            {
                Bookmarking.initialize();
                History.initialize();
            }
            catch { }
            if (System.IO.Directory.Exists(Application.StartupPath + @"\GTLite Downloads") == false) { System.IO.Directory.CreateDirectory(Application.StartupPath + @"\GTLite Downloads"); }
            try
            {
                if (System.IO.Directory.Exists(Properties.Settings.Default.CustomDownloadPath) == false) { System.IO.Directory.CreateDirectory(Properties.Settings.Default.CustomDownloadPath); }
            }
            catch { }
            if (!System.IO.File.Exists(Application.StartupPath + @"\Properties\Feeds.data"))
                File.WriteAllText(Application.StartupPath + @"\Properties\Feeds.data", "");
            if (Directory.Exists(Application.StartupPath + @"\Saved Pages") == false)
                Directory.CreateDirectory(Application.StartupPath + @"\Saved Pages");
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            WebKit.GlobalPreferences.ApplicationName = "GTLite Navigator";
            if (GTLite.Properties.Settings.Default.DownloadsOption == 1)
            {
                if (!String.IsNullOrEmpty(GTLite.Properties.Settings.Default.CustomDownloadPath))
                    WebKit.GlobalPreferences.DownloadsFolder = GTLite.Properties.Settings.Default.CustomDownloadPath;
                else
                    WebKit.GlobalPreferences.DownloadsFolder = Application.StartupPath + @"\GTLite Downloads";
            }
            else
            {
                WebKit.GlobalPreferences.DownloadsFolder = Application.StartupPath + @"\GTLite Downloads";
            }
            
            WebKit.GlobalPreferences.ApplicationName = "GTLite Navigator";
            try
            {
                HistoryCollection.init();
            }
            catch { }
            System.Windows.Forms.Timer tmr = new Timer();
            tmr.Interval = 5000;
            tmr.Enabled = true;
            tmr.Tick += new EventHandler(tmr_Tick);
            Application.Run(new Form1());
        }

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        static void tmr_Tick(object sender, EventArgs e)
        {
            EmptyWorkingSet(System.Diagnostics.Process.GetCurrentProcess().Handle);
        }
        
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DeleteCookiesWhenClosing)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string cookiespath = path + @"\Apple Computer\Cookies\Cookies.plist";
                
                 Microsoft.VisualBasic.MsgBoxResult result = Interaction.MsgBox("Are you sure you want to delete all cookies?", MsgBoxStyle.OkCancel, "GTLite Navigator");
                 if (result == MsgBoxResult.Ok)
                 {
                      try
                      {
                         File.Delete(cookiespath);
                      }
                 catch
                 {
                 }
            
                }
            }
            if (Properties.Settings.Default.DeleteHistoryWhenClosing)
            {
                History.Clear();
            }
            Bookmarking.SaveAll();
            History.SaveAll();
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            if (DeleteCacheAtExit)
            {
                files.Add(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "Local") + @"\Apple Computer\Cache.db");
            }
            foreach (string f in ToDel)
            {
                try
                {
                    files.Add(f);
                }
                catch { }
            }
            if (DeleteCacheAtExit || ToDel.Count > 0)
            {
                string[] tow = new string[ToDel.Count + 1];
                files.CopyTo(tow, 0);
                File.WriteAllLines(Application.StartupPath + @"\todel.temp", tow);
                System.Diagnostics.Process.Start(Application.StartupPath + @"\del.exe");
            }
            Properties.Settings.Default.Save();
        }
        public static System.Collections.Specialized.StringCollection ToDel = new System.Collections.Specialized.StringCollection();
    }
}

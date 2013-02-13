using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WebKit;

namespace GTLite
{
    public partial class Page : UserControl
    {
        public Image GetImageTemp(string path)
        {
            if (Directory.Exists(Application.StartupPath + @"\Temp") == false)
                Directory.CreateDirectory(Application.StartupPath + @"\Temp");
            int k = new Random().Next(100000000);
            System.IO.File.Copy(path, Application.StartupPath + @"\Temp\" + k + ".png");
            return Image.FromFile(Application.StartupPath + @"\Temp\" + k + ".png");
        }
        public Page(string pagename)
        {
            InitializeComponent();
            button2.Enabled = true;
            this.Height = this.Height - ((this.Height / 2) - 5);
                label1.Text = pagename;
                try
                {
                    pictureBox1.Image = GetImageTemp(Application.StartupPath + @"\Saved Pages\" + pagename + @"\favicon.png");

                }
                catch { pictureBox1.Image = GTLite.Properties.Resources.New_document.ToBitmap(); }            
        }
        public Page(Main Owner)
        {
            InitializeComponent();
            SavedPages sa = new SavedPages();
            sa.Show();
            sa.panel1.Controls.Add(this);
            this.Dock = DockStyle.Top;
            WebKitBrowser br = Owner.Browser;
            int total = 0;
            int failed = 0;
            string docmane = br.DocumentTitle;
            foreach (char s in System.IO.Path.GetInvalidFileNameChars())
            {
                docmane = docmane.Replace(s.ToString(), @" ").Replace("™", @" ");
            }
            label1.Text = docmane;
            System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Saved Pages\\" + docmane);
            List<WebKitResource> toret;
            Owner.res.TryGetValue(br, out toret);
            progressBar1.Maximum = toret.Count;
            foreach (WebKit.WebKitResource r in toret)
            {
                using (System.Net.WebClient c = new System.Net.WebClient())
                {
                    if (r.Url != null)
                    {
                        string t;
                        t = (string)r.Url.Split(Convert.ToChar(".")).GetValue(r.Url.Split(Convert.ToChar(".")).Length - 1);
                        if (r.MimeType.Contains("flv"))
                            t = ".flv";
                        else if (r.MimeType.Contains("script") && r.Url.EndsWith(".js") != true)
                            t = ".js";
                        else if (r.MimeType.Contains("text/html") && !r.Url.EndsWith(".asp"))
                            t = ".html";
                        else
                        {
                            t = "." + t.ToLower().Replace(@"\", "");
                        }
                        string fn = ((string)((string)r.Url.Split(Convert.ToChar(@"/")).GetValue(r.Url.Split(Convert.ToChar("/")).Length - 1)).Replace(@"\", "").Split(Convert.ToChar(".")).GetValue(0)).Replace("?", "");
                        try
                        {
                            c.DownloadFileAsync(new Uri(r.Url), Application.StartupPath + "\\Saved Pages\\" + docmane + "\\" + fn + t);
                            total++;
                            progressBar1.Value++;
                        }
                        catch { failed++; progressBar1.Value++; }
                    }
                }
            }
            try
            {
                Image m = Functions.GetIcon(br.Url.ToString());
                m.Save(Application.StartupPath + "\\Saved Pages\\" + docmane + "\\favicon.png");
                m.Dispose();
            }
            catch { }
            System.IO.File.WriteAllText(Application.StartupPath + "\\Saved Pages\\" + docmane + "\\page.html", br.DocumentText);
            button2.Enabled = true;
            label2.Text = "Completed!";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.Directory.Delete(Application.StartupPath + @"\Saved Pages\" + label1.Text, true);
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool found = false;
            foreach (Form f in Application.OpenForms)
                if (f is Main)
                {
                    (f as Main).AddTab("about:blank");
                    (f as Main).Browser.OpenDocument(Application.StartupPath + @"\Saved Pages\" + label1.Text + @"\page.html");
                    found = true;
                }
            if (found == false)
            {
                Main f = new Main();
                f.AddTab("about:blank");
                f.Browser.OpenDocument(Application.StartupPath + @"\Saved Pages\" + label1.Text + @"\page.html");
                f.Show();
            }
        }
        public static bool IsDirectoryValid(string path)
        {
           foreach (string s in (string[])System.IO.Directory.GetFiles(path))
           {
               if (s.Contains("page.html"))
                   return true;
           }
           return false;
        }
    }
}

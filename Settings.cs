using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Microsoft.VisualBasic;
using Qios.DevSuite.Components;
using WebKit;

namespace GTLite
{
    public partial class Settings : DevComponents.DotNetBar.Office2007Form
    {
        void LoadSettings()
        {
            listBox1.Items.Clear();
                foreach (string pg in Properties.Settings.Default.Home)
                {
                    listBox1.Items.Add(pg);
                }
                listBox3.Items.Clear();
                foreach (string i in Properties.Settings.Default.FormAutofillExceptions)
                    listBox3.Items.Add(i);
                comboBoxEx3.SelectedIndex = Properties.Settings.Default.DownloadsOption;
                textBoxX2.Text = Properties.Settings.Default.CustomDownloadPath;
                textWithCaption2.Text = Properties.Settings.Default.NewTabPage;
                securityLevel.Value = Properties.Settings.Default.SecurityLevel;
                switchButton1.Value = Properties.Settings.Default.UseFavicon;
                textBoxX4.Text = Properties.Settings.Default.cssstyle;
                if (securityLevel.Value == 2)
                {
                    label2.Text = "Security Level: Recommended \r\n \r\n This security level is recommended \r\n for everyday users and will not cause \r\n any problems with loading websites.";
                }
                if (securityLevel.Value == 1)
                {
                    label2.Text = "Security Level: No Security \r\n \r\n This security level is not recommended \r\n for everyday users and your computer \r\n might be vulnerable to some dangerous \r\n sites.";
                }
                if (securityLevel.Value == 3)
                {
                    label2.Text = "Security Level: Moderate \r\n \r\n This security level might cause problems \r\n while browsing but will keep your computer safe \r\n from most dangerous sites.";
                }
                if (securityLevel.Value == 4)
                {
                    label2.Text = "Security Level: Strict \r\n \r\n This security level might cause problems \r\n  with more websites than the moderate security level \r\n but sites will not be able to harm \r\n your computer.";
                }
                willRecordHistory.Value = Properties.Settings.Default.UseHistory;
                willSaveCookies.Value = Properties.Settings.Default.UseCookies;
                listBox2.Items.Clear();
                foreach (string s in Properties.Settings.Default.PluginsDirectories)
                {
                    listBox2.Items.Add(s);
                }
                AcceleratingCompositing.Value = Properties.Settings.Default.UseAcceleratingCompositing;
                checkBox1.Checked = Properties.Settings.Default.DeleteHistoryWhenClosing;
                checkBox2.Checked = Properties.Settings.Default.DeleteCookiesWhenClosing;
                comboBoxEx2.SelectedIndex = (int)Properties.Settings.Default.Theme;
                textBoxX3.Text = Properties.Settings.Default.ImagePath;
            comboBoxEx3.SelectedIndexChanged +=new EventHandler(comboBoxEx3_SelectedIndexChanged);
            pageCache.Value = GTLite.Properties.Settings.Default.Cache;
            switchButton2.Value = !GTLite.Properties.Settings.Default.UseAero;
            popupBlock.Value = GTLite.Properties.Settings.Default.PopupBlocker;
            adblock.Value = GTLite.Properties.Settings.Default.AdBlock;
            switchButton3.Value = Properties.Settings.Default.AutoFill;
        }
        void ApplySettings()
        {
            Properties.Settings.Default.FormAutofillExceptions.Clear();
            foreach (string s in listBox3.Items)
            {
                Properties.Settings.Default.FormAutofillExceptions.Add(s);
            }
            Properties.Settings.Default.Home.Clear();
            Properties.Settings.Default.CustomDownloadPath = textBoxX2.Text;
            Properties.Settings.Default.DownloadsOption = comboBoxEx3.SelectedIndex;
            foreach (string s in listBox1.Items)
            {
                Properties.Settings.Default.Home.Add(s);
            }
            Properties.Settings.Default.NewTabPage = textWithCaption2.Text;
            Properties.Settings.Default.SecurityLevel = securityLevel.Value;
            Properties.Settings.Default.UseCookies = willSaveCookies.Value;
            Properties.Settings.Default.UseHistory = willRecordHistory.Value;
            Properties.Settings.Default.AutoFill = switchButton3.Value;
            Properties.Settings.Default.PluginsDirectories.Clear();
            foreach (string s in listBox2.Items)
            {
                Properties.Settings.Default.PluginsDirectories.Add(s);
            }
            Properties.Settings.Default.UseFavicon = switchButton1.Value;
            Properties.Settings.Default.cssstyle = textBoxX4.Text;
            Properties.Settings.Default.DeleteCookiesWhenClosing = checkBox2.Checked;
            Properties.Settings.Default.DeleteHistoryWhenClosing = checkBox1.Checked;
            Properties.Settings.Default.Theme = (eStyle)comboBoxEx2.SelectedIndex;
            Properties.Settings.Default.UseAcceleratingCompositing = AcceleratingCompositing.Value;
            Properties.Settings.Default.UseAero = !switchButton2.Value;
            Properties.Settings.Default.ImagePath = textBoxX3.Text;
            Properties.Settings.Default.AdBlock = adblock.Value;
            Properties.Settings.Default.PopupBlocker = popupBlock.Value;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is Main)
                {
                    (frm as Main).styleManager1.ManagerStyle = Properties.Settings.Default.Theme;
                    break;
                }
            }
            if (switchButton2.Value)
            {
                bool haswarnder = false;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is Main)
                    {
                        try
                        {
                            frm.BackgroundImage = Image.FromFile(Properties.Settings.Default.ImagePath);
                        }
                        catch
                        {
                            if (haswarnder)
                            {
                                MessageBox.Show("The image you selected could not be applied as a background");
                                haswarnder = true;
                            }
                            Properties.Settings.Default.ImagePath = Application.StartupPath + "\\background.jpg";
                            frm.BackgroundImage = Image.FromFile(Application.StartupPath + "\\background.jpg");
                        }
                    }
                }
            }
            if (GTLite.Properties.Settings.Default.DownloadsOption == 1)
            {
                WebKit.GlobalPreferences.DownloadsFolder = GTLite.Properties.Settings.Default.CustomDownloadPath;
            }
            else
            {
                WebKit.GlobalPreferences.DownloadsFolder = Application.StartupPath + @"\GTLite Downloads";
            }
                foreach (Form m in Application.OpenForms)
                {
                    if (m is Main)
                    {
                        foreach (Control q in (m as Main).qTabControl1.Controls)
                        {
                            if (q is QTabControl)
                                ((WebKitBrowser)q.Controls[0]).AllowCookies = Properties.Settings.Default.UseCookies;
                        }
                    }
                }    
            
            GTLite.Properties.Settings.Default.Cache = pageCache.Value;
            Properties.Settings.Default.Save();
        }
        public Settings()
        {
            InitializeComponent();
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            LoadSettings();
            
        }
        #region reorder items
        private void ListBox1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            listBox1.Items.Insert(listBox1.IndexFromPoint(listBox1.PointToClient(new Point(e.X, e.Y))), e.Data.GetData(DataFormats.Text));
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void ListBox1_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void ListBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            listBox1.DoDragDrop(listBox1.Text, DragDropEffects.All);
        }
        #endregion
        private void buttonX4_Click(object sender, EventArgs e)
        {
            textBoxX1.Visible = true;
            buttonX8.Visible = true;
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBoxX1.Text);
            buttonX8.Visible = false;
            textBoxX1.Visible = false;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            int itemsfound = 0;
            string Data = "";
            foreach (string s in History.AllHisItems)
            {
                Data = Data + s + @"\";
            }
            foreach (string his in Data.Split(Convert.ToChar(@"\")))
            {
                try
                {
                    string date = (string)his.Split(Convert.ToChar("|")).GetValue(2);
                    if (date.StartsWith(monthCalendarAdv1.SelectedDate.ToShortDateString()) == true)
                    {
                        try
                        {
                            History.AllHisItems.RemoveAt(History.AllHisItems.IndexOf(his));
                            itemsfound = itemsfound + 1;
                        }
                        catch { }
                    }
                }
                catch { }
            }
            History.SaveAll();
            History.initialize();
            MessageBoxEx.Show(itemsfound + " items were found and deleted");
        }


        private void buttonX9_Click(object sender, EventArgs e)
        {
            History.Clear();
            History.SaveAll();
            MessageBoxEx.Show("History has been cleared.");
        }

        private void buttonX12_Click(object sender, EventArgs e)
        {
            Library l = new Library();
            l.Show();
        }

        private void buttonX16_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listBox2.Items.Add(folderBrowserDialog1.SelectedPath);
            }
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }


        private void buttonX10_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string cookiespath = path + @"\Apple Computer\Cookies\Cookies.binarycookies";
            
            Microsoft.VisualBasic.MsgBoxResult result = Interaction.MsgBox("Are you sure you want to delete all cookies?", MsgBoxStyle.OkCancel, "GTLite Navigator");
            if (result == MsgBoxResult.Ok)
            {
                try
                {
                   File.Delete(cookiespath);
                }
                catch
                {
                    MessageBoxEx.Show("No cookies were found or they might have already been cleared.","Cookies",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        private void securityLevel_ValueChanged(object sender, EventArgs e)
        {
            if (securityLevel.Value == 2)
            {
                label2.Text = "Security Level: Recommended \r\n \r\n This security level is recommended \r\n for everyday users and will not cause \r\n any problems with loading websites.";
            }
            if (securityLevel.Value == 1)
            {
                label2.Text = "Security Level: No Security \r\n \r\n This security level is not recommended \r\n for everyday users and your computer \r\n might be vulnerable to some dangerous \r\n sites.";
            }
            if (securityLevel.Value == 3)
            {
                label2.Text = "Security Level: Moderate \r\n \r\n This security level might cause problems \r\n while browsing but will keep your computer safe \r\n from most dangerous sites.";
            }
            if (securityLevel.Value == 4)
            {
                label2.Text = "Security Level: Strict \r\n \r\n This security level might cause problems \r\n with more websites than the moderate security level \r\n but sites will not be able to harm \r\n your computer.";
            }

        }

        private void buttonX17_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            WebKitBrowser s = new WebKitBrowser();
            f.ShowIcon = false;
            f.Text = "Plugins";
            f.Controls.Add(s);
            s.Dock = DockStyle.Fill;
            s.OpenDocument(Application.StartupPath + @"\Help\en.lproj\Plug-ins.html");
            f.FormClosed += new FormClosedEventHandler(Settings_FormClosed);
            f.Show();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count < 2)
            {
                Application.OpenForms[0].Close();
            }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog f = new FolderBrowserDialog())
            {
                f.Description = "Please select the path where you want downloads to be saved.";
                f.RootFolder = Environment.SpecialFolder.MyComputer;
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxX2.Text = f.SelectedPath;
                }
                else
                {
                    if (textBoxX2.Text == "")
                    {
                        comboBoxEx3.SelectedIndex = 0;
                    }
                }
            }
        }

        private void comboBoxEx3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx3.SelectedIndex == 1 && textBoxX2.Text == "")
            {
                buttonX7.PerformClick();
            }
        }

        private void buttonX11_Click_1(object sender, EventArgs e)
        {
            BlockedSites b = new BlockedSites();
            b.Show();
        }

        private void buttonX18_Click(object sender, EventArgs e)
        {
            if (comboBoxEx3.SelectedIndex == 0)
                Process.Start(Application.StartupPath + @"\GTLite Downloads");
            else
                Process.Start(textBoxX2.Text);
        }

        private void switchButton2_ValueChanged(object sender, EventArgs e)
        {
            textBoxX3.Enabled = switchButton2.Value;
            buttonX19.Enabled = switchButton2.Value;
        }

        private void buttonX20_Click(object sender, EventArgs e)
        {
            textBoxX3.Text = Application.StartupPath + "\\background.jpg";
        }

        private void buttonX19_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fld = new OpenFileDialog())
            {
                fld.Title = "Please select the image you want to be used for your custom background";
                fld.Filter = "Image Files|*.jpg; *.jpeg; *.bmp; *.png";
                if (fld.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxX3.Text = fld.FileName;
                }
            }
        }

        private void buttonX21_Click(object sender, EventArgs e)
        {
            StartupForm s = new StartupForm(false);
            s.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PopupExceptions p = new PopupExceptions();
            p.Show();
        }
        long bytesdeleted = 0;
        private void buttonX22_Click(object sender, EventArgs e)
        {
            labelX6.Visible = true;
            pictureBox1.Visible = true;
            ImageAnimator.Animate(pictureBox1.Image, new EventHandler(delegate { this.Invalidate(); }));
            string hisf = Application.StartupPath + @"\Properties\History.data";
            string bookf = Application.StartupPath + @"\Properties\Bookmarks.data";
            string cookiesf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Apple Computer\Cookies\Cookies.binarycookies";
            string cachef = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "Local") + @"\Apple Computer\Cache.db";
            if ((itemPanel1.Items[0] as CheckBoxItem).Checked == true)
            {
                if (File.Exists(hisf))
                {
                    FileInfo fl = new FileInfo(hisf);
                    bytesdeleted = bytesdeleted + fl.Length;
                    File.Delete(hisf);
                }
            }
            if ((itemPanel1.Items[1] as CheckBoxItem).Checked == true)
            {
                if (File.Exists(bookf))
                {
                    FileInfo fl = new FileInfo(bookf);
                    bytesdeleted = bytesdeleted + fl.Length;
                    File.Delete(bookf);
                }
            }
            if ((itemPanel1.Items[2] as CheckBoxItem).Checked == true)
            {
                if (File.Exists(cookiesf))
                {
                    FileInfo fl = new FileInfo(cookiesf);
                    bytesdeleted = bytesdeleted + fl.Length;
                    File.Delete(cookiesf);
                }
            }
            if ((itemPanel1.Items[3] as CheckBoxItem).Checked == true)
            {
                if (File.Exists(cachef))
                {
                    MessageBoxEx.Show("Cache will be deleted when GTLite Navigator will exit");
                    Program.DeleteCacheAtExit = true;
                }
            }
            if ((itemPanel1.Items[4] as CheckBoxItem).Checked == true)
            {
                foreach(string f in Directory.GetFiles(Application.StartupPath + @"\Properties"))
                {
                    if (f.EndsWith(".png"))
                    {
                        try
                        {
                            FileInfo fl = new FileInfo(f);
                            bytesdeleted = bytesdeleted + fl.Length;
                            File.Delete(f);
                        }
                        catch { Program.ToDel.Add(f); }
                    }
                }
            }
            if ((itemPanel1.Items[5] as CheckBoxItem).Checked == true)
            {
                foreach (string f in Directory.GetFiles(Application.StartupPath + @"\Temp"))
                {
                    FileInfo fl = new FileInfo(f);
                    bytesdeleted = bytesdeleted + fl.Length;
                    File.Delete(f);
                }
            }
            labelX6.Text = bytesdeleted + " bytes were deleted.";
            pictureBox1.Visible = false;
        }

        private void buttonX24_Click(object sender, EventArgs e)
        {
            string i = Microsoft.VisualBasic.Interaction.InputBox("Please enter the Url or a part of it where you don't want to be prompted for saving data");
            if (!string.IsNullOrEmpty(i))
            {
                listBox3.Items.Add(i);
            }
        }

        private void buttonX25_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object o in listBox3.SelectedItems)
                {
                    listBox3.Items.Remove(o);
                }
            }
            catch { }
        }

        private void buttonX26_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
        }

        private void buttonX27_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.Filter = "CSS Files (*.css)|*.css";
                if (o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    textBoxX4.Text = o.FileName;
            }
        }
    }
}
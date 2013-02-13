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
    public partial class StartupForm : DevComponents.DotNetBar.Office2007Form
    {
        bool FO;
        public StartupForm(bool FromStartup = true)
        {
            InitializeComponent();
            FO = FromStartup;
            switchButton1.Value = Properties.Settings.Default.UseFavicon;
            AcceleratingCompositing.Value = Properties.Settings.Default.UseAcceleratingCompositing;
            checkBox1.Checked = Properties.Settings.Default.DeleteHistoryWhenClosing;
            checkBox2.Checked = Properties.Settings.Default.DeleteCookiesWhenClosing;
            textBoxX3.Text = Properties.Settings.Default.ImagePath;
            pageCache.Value = GTLite.Properties.Settings.Default.Cache;
            switchButton2.Value = !GTLite.Properties.Settings.Default.UseAero;
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)   
        {
        }

        private void switchButton2_ValueChanged(object sender, EventArgs e)
        {
            textBoxX3.Enabled = switchButton2.Value;
            buttonX19.Enabled = switchButton2.Value;
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UseFavicon = switchButton1.Value;
            Properties.Settings.Default.DeleteCookiesWhenClosing = checkBox2.Checked;
            Properties.Settings.Default.DeleteHistoryWhenClosing = checkBox1.Checked;
            Properties.Settings.Default.UseAcceleratingCompositing = AcceleratingCompositing.Value;
            Properties.Settings.Default.UseAero = !switchButton2.Value;
            Properties.Settings.Default.ImagePath = textBoxX3.Text;
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
            GTLite.Properties.Settings.Default.Cache = pageCache.Value;
            Properties.Settings.Default.Save();
            this.Close();
            if (FO)
            {
                Properties.Settings.Default.IsFirstTime = false;
                Main f = new Main();
                f.Show();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectNextTab();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectPreviousTab();
        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {

        }

        private void StartupForm_Load(object sender, EventArgs e)
        {
            labelX1.Text = labelX1.Text.Replace("{0}", Application.ProductVersion.ToString());
            superTabControl1.SelectedTab = superTabItem1;
            comboBoxEx1.SelectedIndex = comboBoxEx1.Items.Count - 1;
            this.Focus();
            this.BringToFront();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (FO)
            {
                Application.Exit();
            }
            else { this.Close(); } 
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            this.Close();
            if (FO)
            {
                Main f = new Main();
                f.Show();
                Properties.Settings.Default.IsFirstTime = false;
                Properties.Settings.Default.Save();
            }
        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }
    }
}
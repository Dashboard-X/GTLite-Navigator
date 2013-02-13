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
    public partial class BlockedSites : DevComponents.DotNetBar.Office2007Form
    {
        public BlockedSites()
        {
            InitializeComponent();
        }
        void LoadItems()
        {
            listBox1.Items.Clear();
            foreach (string s in Properties.Settings.Default.BlockedSites)
            {
                listBox1.Items.Add(s);
            }
        }
        private void BlockedSites_Load(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            textBoxX1.Visible = true;
            buttonX8.Visible = true;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void BlockedSites_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.BlockedSites.Clear();
            foreach (string s in listBox1.Items)
            {
                if (s != "")
                {
                    Properties.Settings.Default.BlockedSites.Add(s);
                }
            }
            if (Application.OpenForms.Count < 2)
            {
                Application.OpenForms[0].Close();
            }
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBoxX1.Text);
            buttonX8.Visible = false;
        }
    }
}
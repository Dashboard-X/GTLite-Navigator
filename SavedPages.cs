using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GTLite
{
    public partial class SavedPages : Form
    {
        public SavedPages()
        {
            InitializeComponent();
        }

        private void SavedPages_Load(object sender, EventArgs e)
        {
            foreach (string d in Directory.GetDirectories(Application.StartupPath + @"\Saved Pages"))
            {
                if (Page.IsDirectoryValid(d))
                {
                    string s = Path.GetDirectoryName(d);
                    panel1.Controls.Add(new Page(d.Replace(s, "").Replace("\\", "")) { Dock = DockStyle.Top, Visible = true });
                }
            }
        }

        private void SavedPages_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1)
                Application.OpenForms[0].Close();
        }
    }
}

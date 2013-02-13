using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using GTLite;

namespace GTLite
{
    public partial class Library : DevComponents.DotNetBar.Office2007Form
    {
        #region LoadData
        void LoadHistory()
        {
            List<HisItem> All = new List<HisItem>();
            for (int i = 0; i <= History.GetItemsCount() - 1; i++)
            {
                HisItem s = new HisItem();
                s.Import(History.Name(i), History.Url(i), History.Date(i));
                All.Add(s);
            }
            HistoryMgr.DataSource = null;
            HistoryMgr.DataSource = All;
        }
        void LoadBookmarks()
        {
            List<FavItem> All = new List<FavItem>();
            for (int i = 0; i <= Bookmarking.GetItemsCount() - 1; i++)
            {
                FavItem s = new FavItem();
                s.Import(Bookmarking.Name(i), Bookmarking.Url(i));
                All.Add(s);
            }
            BookmarksMgr.DataSource = null;
            BookmarksMgr.DataSource = All;
        }
        #endregion
        public Library()
        {
            InitializeComponent();
            toolTip1.SetToolTip(BookmarksMgr, "Tip!");
        }

        private void Library_Load(object sender, EventArgs e)
        {
            LoadBookmarks();
            LoadHistory();
        }

        private void tabControlPanel3_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow f in HistoryMgr.SelectedRows)
            {
                string it = ((HisItem)f.DataBoundItem).Name + "|" + ((HisItem)f.DataBoundItem).Url + "|" + ((HisItem)f.DataBoundItem).Date;
                
                History.AllHisItems.Remove(it);
            }
            History.SaveAll();
            LoadHistory();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            History.Clear();
            LoadHistory();
            GTLite.HistoryCollection._col.Clear();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Main f = null;
            foreach (Form frm in Application.OpenForms)
            {
                try { f = ((Main)(frm)); break; }
                catch { }
            }
            foreach (DataGridViewRow h in HistoryMgr.SelectedRows)
            {
                if (f != null)
                {
                    f.AddTab(((HisItem)h.DataBoundItem).Url);
                }
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow f in BookmarksMgr.SelectedRows)
            {
                string it = ((FavItem)f.DataBoundItem).Name + "|" + ((FavItem)f.DataBoundItem).Url;
                Bookmarking.AllBookItems.Remove(it);
            }
            Bookmarking.SaveAll();
            LoadBookmarks();
            foreach (Form f in Application.OpenForms)
            {
                try
                {
                    ((Main)f).UpdateBookmarks();
                }
                catch { }
            }
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            Bookmarking.Clear();
            LoadBookmarks();
            foreach (Form f in Application.OpenForms)
            {
                try
                {
                    ((Main)f).AvailableBookmarks.Clear();
                }
                catch { }
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            Main f = null;
            foreach (Form frm in Application.OpenForms)
            {
                try { f = ((Main)(frm)); break; }
                catch { }
            }
            foreach (DataGridViewRow h in BookmarksMgr.SelectedRows)
            {
                if (f != null)
                {
                    f.AddTab(((FavItem)h.DataBoundItem).Url);
                }
            }
        }

        private void BookmarksMgr_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            FavItem hs = ((FavItem)BookmarksMgr.Rows[e.RowIndex].DataBoundItem);
            Bookmarking.AllBookItems[e.RowIndex] = hs.Name + "|" + hs.Url; 
        }

        private void Library_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach(Form f in Application.OpenForms)
            {
                try
                {
                    ((Main)f).UpdateBookmarks();
                }
                catch { }
            }
            if (Application.OpenForms.Count < 2)
            {
                Application.OpenForms[0].Close();
            }
        }
        bool IsHistory(string path)
        {
            string[] items = System.IO.File.ReadAllLines(path);
            try
            {
                items[0].Split(Convert.ToChar("|")).GetValue(2);
                return true;
            }
            catch { return false; }
        }
        bool IsValidFile(string path)
        {
            bool _isvalid = true;
            foreach (string s in LoadData(path))
            {
                if (s.Contains("|") == false)
                {
                    _isvalid = false;
                }
            }
            return _isvalid;
        }
        string[] LoadData(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opn = new OpenFileDialog())
            {
                opn.Filter = "GTLite History Files(*.data)|*.data";
                if (opn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (IsHistory(opn.FileName) && IsValidFile(opn.FileName))
                    {
                        History.AllHisItems.AddRange(LoadData(opn.FileName));
                        History.SaveAll();
                        History.initialize();
                    }
                }
                LoadHistory();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sv = new SaveFileDialog())
            {
                sv.Filter = "GTLite History Files(*.data)|*.data";
                if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                        string ToWrite = "";
                        foreach(string s in History.AllHisItems)
                        {
                            ToWrite = ToWrite + "\r\n" + s;
                        }
                        System.IO.File.WriteAllText(sv.FileName, ToWrite);
                 }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opn = new OpenFileDialog())
            {
                opn.Filter = "GTLite Bookmark Files(*.data)|*.data";
                if (opn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (IsHistory(opn.FileName) == false && IsValidFile(opn.FileName))
                    {
                        Bookmarking.AllBookItems.AddRange(LoadData(opn.FileName));
                        Bookmarking.SaveAll();
                        Bookmarking.initialize();
                    }
                    else { MessageBoxEx.Show("The file you selected could not be recognized as a bookmark file."); }
                }
                LoadBookmarks();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sv = new SaveFileDialog())
            {
                sv.Filter = "GTLite Bookmarks Files(*.data)|*.data";
                if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                        string ToWrite = "";
                        foreach (string s in Bookmarking.AllBookItems)
                        {
                            ToWrite = ToWrite + "\r\n" + s;
                        }
                        System.IO.File.WriteAllText(sv.FileName, ToWrite);
                    
                }
            }
        }

        private void BookmarksMgr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void BookmarksMgr_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex == 0)
                e.ToolTipText = "Tip: You can double-click this cell to change the bookmark's name";

            if (e.RowIndex == 1)
                e.ToolTipText = "Tip: You can double-click this cell to change the bookmark's URL";
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            DataGridViewX curd = (DataGridViewX)tabControl1.SelectedPanel.Controls[0];
            string text = textBoxX1.Text;
            buttonX7.Visible = !string.IsNullOrEmpty(text);
            foreach (DataGridViewRow o in curd.Rows)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    CurrencyManager cm = (CurrencyManager)BindingContext[curd.DataSource];
                    cm.SuspendBinding();
                    if (o.DataBoundItem is FavItem)
                    {
                        if ((((FavItem)o.DataBoundItem).Name.Contains(text) || ((FavItem)o.DataBoundItem).Url.Contains(text)) == true)
                            o.Visible = true;
                        else
                            o.Visible = false;
                    }
                    if (o.DataBoundItem is HisItem)
                    {
                        if ((((HisItem)o.DataBoundItem).Name.Contains(text) || ((HisItem)o.DataBoundItem).Url.Contains(text) || ((HisItem)o.DataBoundItem).Date.Contains(text)) == true)
                            o.Visible = true;
                        else
                            o.Visible = false;
                    }
                    cm.ResumeBinding();
                }
                else
                    o.Visible = true;
            }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            textBoxX1.Text = "";
            buttonX7.Visible = false;
        }

        private void BookmarksMgr_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        
    }
}
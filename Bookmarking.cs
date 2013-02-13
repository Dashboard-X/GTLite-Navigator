using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Specialized;
using Microsoft.VisualBasic;

namespace GTLite
{
    public class Bookmarking
    {
        public static string Path = Application.StartupPath + "\\Properties\\bookmarks.data";
        public static StringCollection AllBookItems = new StringCollection();
        public static string Name(int index)
        {
            if (string.IsNullOrEmpty(AllBookItems[index]) == false)
            {
            return AllBookItems[index].Split(Convert.ToChar("|")).GetValue(0).ToString().Replace("|", "(*~)");
            }
            else {
                return "";
            }
        }
        public static void Add(string name, string url, bool save = true)
        {
            AllBookItems.Add(name.Replace("|", "(*~)") + "|" + url.Replace("|", "(*~)"));
            Uri _t = new Uri(url);
            string path = Application.StartupPath + @"\Properties\" + _t.Host + ".png";
            try
            {
                Bitmap _temp = ((Bitmap)Functions.GetIcon(url));
                _temp.Save(path);
            }
            catch { }
            if (save == true)
            {
                SaveAll();
            }

        }
        public static void initialize()
        {
            AllBookItems.Clear();
            try
            {
                if (System.IO.File.Exists(Path) == false)
                    System.IO.File.WriteAllText(Path, Properties.Resources.BookmarksD);
                foreach (string s in File.ReadAllLines(Path))
                {
                    AllBookItems.Add(s);
                }
            }
            catch
            {
                File.Create(Path);
            }
        }
        public static Image Favicon(int i)
        {
            Uri _t = new Uri(Url(i));
            return Image.FromFile(Application.StartupPath + @"\Properties\" + _t.Host + ".png");
        }

        public static int GetItemsCount()
        {
            return AllBookItems.Count;
        }
        public static string Url(int index)
        {
            if (string.IsNullOrEmpty(AllBookItems[index]) == false)
            {
                return AllBookItems[index].Split(Convert.ToChar("|")).GetValue(1).ToString().Replace("|", "(*~)");
            }
            else {
                return "";
            }
        }

        public static void SaveAll()
        {
            string filedata = "";
            foreach (string s in AllBookItems)
            {
                if (string.IsNullOrEmpty(filedata) == true)
                {
                    filedata = s;
                }
                else
                {
                    filedata = filedata + Constants.vbNewLine + s;
                }
            }
            File.WriteAllText(Path, filedata);
        }
        public static void Clear()
        {
            AllBookItems.Clear();
            SaveAll();
        }
        public static void Delete(int index,bool save = false)
        {
            AllBookItems.RemoveAt(index);
            if (save == true)
            {
                SaveAll();
            }
        }
    }
    public class FavItem
    {
        private string _name;
        private string _url;
        public string Name
        {
            get{ return _name; }
            set { _name = value; }
        }
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public void Import(string name, string url)
        {
            Name = name;
            _url = url;
        }
    }
}

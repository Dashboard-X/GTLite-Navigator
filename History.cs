using System;
using System.Text;
using System.Windows.Forms;
using GTLite;
using System.IO;
using System.Collections.Specialized;
using Microsoft.VisualBasic;

namespace GTLite
{
    public class History
    {
        public static string Path = Application.StartupPath + "\\Properties\\history.data";
        public static StringCollection AllHisItems = new StringCollection();
        public static string Name(int index)
        {
            if (string.IsNullOrEmpty(AllHisItems[index]) == false)
            {
                try
                {
                    return AllHisItems[index].Split(Convert.ToChar("|")).GetValue(0).ToString().Replace("|", "(*~)");
                }
                catch { return ""; }
            }
            else
            {
                return ""; 
            }
        }
        public static void Add(string name, string url, DateTime date, bool save = false)
        {
            string _name = name.Replace("|", "(*~)").Replace(Constants.vbNewLine, " - ");
            string _url = url.Replace("|", "(*~)");
            AllHisItems.Add(_name + "|" + _url + "|" + date.ToString() );
            if (save == true)
            {
                SaveAll();
            }
        }
        public static void initialize()
        {
            AllHisItems.Clear();

            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, Properties.Resources.HistoryD);
            }
            else
            {
                foreach (string s in File.ReadAllLines(Path))
                {
                    AllHisItems.Add(s);
                }
            }
        }
        public static int GetItemsCount()
        {
            return AllHisItems.Count;
        }
        public static string Url(int index)
        {
            if (string.IsNullOrEmpty(AllHisItems[index]) == false)
            {
                try
                {
                    return AllHisItems[index].Split(Convert.ToChar("|")).GetValue(1).ToString().Replace("|", "(*~)");
                }
                catch { return ""; }
            }
            else
            {
                return ""; 
            }
        }

        public static string Date(int index)
        {
            if (string.IsNullOrEmpty(AllHisItems[index]) == false)
            {
                try 
                {
                return AllHisItems[index].Split(Convert.ToChar("|")).GetValue(2).ToString().Replace("(*~)","|");
                    }
                catch { return ""; }
            }
            else
            {
                return DateTime.Now.ToString() ; 
            }
        }
        public static void SaveAll()
        {
            string filedata = "";
            foreach (string s in AllHisItems)
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
            AllHisItems.Clear();
            SaveAll();
        }
        public static void Delete(int index,bool save = true)
        {
            AllHisItems.RemoveAt(index);
            if (save == true)
            {
                SaveAll();
            }
        }      
    }
    public class HisItem
    {
            string _name;
            string _url;
            string _date;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
            public string Url
            {
                get { return _url; }
                set { _url = value; }
            }
            public string Date
            {
                get { return _date; }
                set { _date = value; }
            }
            public void Import(string n, string u, string d)
            {
                this.Name = n;
                this.Url = u;
                this.Date = d;
            }
    }
}


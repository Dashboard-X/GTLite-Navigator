using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using WebKit.DOM;
using WebKit;
using System.Collections.Specialized;

namespace GTLite
{
    static class AutoFillManager
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GTLite Forms\forms.list";
        public static Dictionary<string, List<WebKitFormData>> Elements = new Dictionary<string, List<WebKitFormData>>();
        public static void SaveDataFromForm(WebKit.WebKitBrowser sender, string u)
        {
            try
            {
                if (!CanFillForm(sender))
                {
                    if (System.IO.File.Exists(path) == false)
                    {
                        System.IO.File.Create(path);
                    }
                    string towrite = File.ReadAllText(path) + "\r\n" + "~" + sender.Url.Host + "\r\n";
                    foreach (WebKitFormData wkf in Elements[u])
                    {
                        towrite = towrite + wkf.Element.ID + ";" + wkf.Value + "\r\n";
                    }
                    File.WriteAllText(path, towrite);
                }
                else
                {
                    int stind = -1;
                    int endind = 0;
                    string[] data = File.ReadAllLines(path);
                    for (int i = 0; i <= data.Length; i++)
                    {
                        if (stind != -1 && data[i].StartsWith("~"))
                        {
                            endind = i;
                            break;
                        }
                        if (data[i].StartsWith("~") && data[i].EndsWith(sender.Url.Host))
                        {
                            stind = i;
                        }
                    }
                    string datas = "";
                    foreach (string d in data)
                    {
                        datas = datas + d + "\r\n";
                    }
                    string dt = string.Empty;
                    for (int i = stind; i < endind; i++)
                    {
                        dt = dt + data[i] + "\r\n";
                    }
                    string towrite = "\r\n" + "~" + sender.Url.Host + "\r\n";
                    foreach (WebKitFormData wkf in Elements[u])
                    {
                        towrite = towrite + wkf.Element.ID + ";" + wkf.Value + "\r\n";
                    }
                    datas = datas.Replace(dt, towrite);
                    File.WriteAllText(path, datas);
                }
            }
            catch { }
        }

        public static bool CanFillForm(WebKitBrowser browser)
        {
            if (File.Exists(path) == false)
                return false;
            foreach (string f in File.ReadAllLines(path))
            {
                if (f.StartsWith("~") && f.EndsWith(browser.Url.Host))
                {
                   return true;
                }
            }
            return false;
        }
        public static void FillForm(WebKitBrowser browser)
        {
            string[] data = File.ReadAllLines(path);
            int startindex = -1;
            int endindex = -1;
            foreach (string f in data)
            {
                if (f.StartsWith("~") && f.EndsWith(browser.Url.Host))
                {
                    startindex = Array.IndexOf(data, f) + 1;
                }
                if (f.StartsWith("~") && endindex == -1)
                {
                    endindex = Array.IndexOf(data, f) - 1;
                }
            }
            if (endindex <= startindex)
                endindex = data.Length - 1;
            if (startindex != -1 && endindex != -1)
            {
                StringCollection fdata = new StringCollection();
                for (int i = startindex; i <= endindex; i++)
                {
                    fdata.Add((string)data.GetValue(i));
                }
                foreach (string d in fdata)
                {
                    try
                    {
                        string id = (string)d.Split(Convert.ToChar(";")).GetValue(0);
                        string val = (string)d.Split(Convert.ToChar(";")).GetValue(1);
                        browser.StringByEvaluatingJavaScriptFromString(@"document.getElementById('" + id + @"').setAttribute('value', '" + val + "');");
                        string src = @"document.getElementsByName('" + id + @"')[0].setAttribute('value', '" + val + "');";
                        browser.StringByEvaluatingJavaScriptFromString(src);
                    }
                    catch { }
                }
            }
        }
    }
    public class LoginItem
    {
        public string Url;
        public List<LoginValue> Items;
        public LoginItem(string[] data)
        {
            Url = data[0].Substring(1);
            Items = new List<LoginValue>();
            for (int i = 1; i < data.Length; i++)
            {
                Items.Add(new LoginValue((string)((string)data.GetValue(i)).Split(Convert.ToChar(";")).GetValue(0), (string)((string)data.GetValue(i)).Split(Convert.ToChar(";")).GetValue(1)));
            }
        }
    }

    public class LoginValue
    {
        public string Name;
        public string Value;
        public LoginValue(string n, string v)
        {
            Name = n;
            Value = v;
        }
    }
}

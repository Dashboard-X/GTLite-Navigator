using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace GTLite
{
    class Functions
    {
        

[StructLayout(LayoutKind.Sequential)]
public struct MARGINS
{
	public int cxLeftWidth;
	public int cxRightWidth;
	public int cyTopHeight;
	public int cyButtomheight;
}
[DllImport("DwmApi.dll")]
public static extern int DwmExtendFrameIntoClientArea(
    IntPtr hwnd,
    ref MARGINS pMarInset);
        public static void ExtendIntoClientArea(Form frm, int top, int bottom, int left, int right)
        {
            if (Environment.OSVersion.Version.Major > 5)
            {
                foreach (Process clsProcess in Process.GetProcesses())
                {
                    if (clsProcess.ProcessName.Contains("dwm"))
                    {
                        MARGINS margins = default(MARGINS);
                        margins.cxLeftWidth = left;
                        margins.cxRightWidth = right;
                        margins.cyTopHeight = top;
                        margins.cyButtomheight = bottom;
                        frm.BackColor = System.Drawing.Color.Black;
                        IntPtr hwnd = frm.Handle;
                        int i = DwmExtendFrameIntoClientArea(hwnd, ref margins);
                        break;
                    }
                }
            }
        }
        public static Image GetIcon(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Uri a = new Uri(url);
                try
                {
                    return Image.FromFile(Application.StartupPath + @"\Properties\" + a.Host + ".png");
                }
                catch
                {
                    try
                    {
                        System.Net.WebClient webclient = new System.Net.WebClient();
                        Uri iconurl = new Uri(a.AbsoluteUri);
                        System.IO.MemoryStream MemoryStream = new System.IO.MemoryStream(webclient.DownloadData("http://" + iconurl.Host + "/favicon.ico"));
                        Image webicon = Image.FromStream(MemoryStream);
                        if (webicon.Height > 0)
                        {
                            webclient.Dispose();
                            MemoryStream.Flush();
                            webicon.Save(Application.StartupPath + @"\Properties\" + a.Host + ".png");
                            return webicon;
                        }
                        else
                        {
                            webclient.Dispose();
                            MemoryStream.Flush();
                            ((Image)Bitmap.FromHicon(GTLite.Properties.Resources.New_document.Handle)).Save(Application.StartupPath + @"\Properties\" + a.Host + ".png");
                            return ((Image)Bitmap.FromHicon(GTLite.Properties.Resources.New_document.Handle));
                        }
                    }
                    catch
                    {
                        ((Image)Bitmap.FromHicon(GTLite.Properties.Resources.New_document.Handle)).Save(Application.StartupPath + @"\Properties\" + a.Host + ".png");
                        return ((Image)Bitmap.FromHicon(GTLite.Properties.Resources.New_document.Handle));
                    }
                }
            }
            else
                return((Image)Bitmap.FromHicon(GTLite.Properties.Resources.New_document.Handle));   
        }
    }
}

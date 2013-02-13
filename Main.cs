/*
 * Created by SharpDevelop.
 * User: Antwan
 * Date: 6/4/2011
 * Time: 1:27 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using NcRenderer;
using WebKit;
using System.ComponentModel;
using Qios.DevSuite.Components;
using WebKit.Interop;
using System.Diagnostics;
using DevComponents.DotNetBar;
using WebKit.DOM;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace GTLite
{
    
	/// <summary>
	/// Description of Main.
	/// </summary>
    public partial class Main : DevComponents.DotNetBar.Office2007Form
    {
        private const int HTCAPTION = 0x2;
        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ExtendMargins(2, mainbar.Height + 35, 10, 2, true, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            MenuItem enc = contextMenu1.MenuItems.Add("Encoding");
            foreach (EncodingInfo e in Encoding.GetEncodings())
            {
                MenuItem a = enc.MenuItems.Add(e.GetEncoding().EncodingName);
                a.RadioCheck = true;
                a.Click += new EventHandler(Main_Click);
                a.Tag = e.Name;
            }
            
            contextMenu1.MenuItems.Add("-");
            MenuItem r = contextMenu1.MenuItems.Add("Add to the ToVisit List");
            r.Click += new EventHandler(a_Click);
            MenuItem i = contextMenu1.MenuItems.Add("Launch Inspector");
            i.Click += new EventHandler(i_Click);
            MenuItem s = contextMenu1.MenuItems.Add("Save Whole Page");
            s.Click +=new EventHandler(savePageToolStripMenuItem_Click);
            if (string.IsNullOrEmpty(System.IO.File.ReadAllText(Application.StartupPath + @"\Properties\Bookmarks.data")))
                System.IO.File.WriteAllText(Application.StartupPath + @"\Properties\Bookmarks.data", Properties.Resources.BookmarksD);

        }

        void a_Click(object sender, EventArgs e)
        {
            
            if (Browser.Url != null)
            {
                ToVisitLink tvl = new ToVisitLink(Browser.DocumentTitle, Browser.Url.ToString(), Functions.GetIcon(Browser.Url.ToString()));
                ToVisit.Controls.Add(tvl);
                tvl.Dock = DockStyle.Top;
                tvl.VisitLinkButtonClicked += new ToVisitLink.VisitLink(tvl_VisitLinkButtonClicked);
                timer5.Enabled = true;
                timer5.Interval = 20;
            }
            else
            {
                MessageBoxEx.Show("This site could not be added to the ToVisit links");
            }
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int
        lParam);

        public bool HighSecurity = false;
        public static void MoveForm(IntPtr Handle)
        {
            ReleaseCapture();
            int Status = SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        #region Bookmarks
        public static ToolStripItemCollection _col = new ToolStripItemCollection(new ToolStrip(), new ToolStripItem[] { });

        public ToolStripItemCollection AvailableBookmarks
        {
            get
            {
                return _col;
            }
            set { _col = value; }
        }
        #endregion
        #region Extend Frame
        #region Constants
        // windowpos
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOREDRAW = 0x0008;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_FRAMECHANGED = 0x0020;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SWP_HIDEWINDOW = 0x0080;
        private const int SWP_NOCOPYBITS = 0x0100;
        private const int SWP_NOOWNERZORDER = 0x0200;
        private const int SWP_NOSENDCHANGING = 0x0400;
        // redraw
        private const int RDW_INVALIDATE = 0x0001;
        private const int RDW_INTERNALPAINT = 0x0002;
        private const int RDW_ERASE = 0x0004;
        private const int RDW_VALIDATE = 0x0008;
        private const int RDW_NOINTERNALPAINT = 0x0010;
        private const int RDW_NOERASE = 0x0020;
        private const int RDW_NOCHILDREN = 0x0040;
        private const int RDW_ALLCHILDREN = 0x0080;
        private const int RDW_UPDATENOW = 0x0100;
        private const int RDW_ERASENOW = 0x0200;
        private const int RDW_FRAME = 0x0400;
        private const int RDW_NOFRAME = 0x0800;
        // frame
        private const int FRAME_WIDTH = 8;
        private const int CAPTION_HEIGHT = 30;
        private const int FRAME_SMWIDTH = 4;
        private const int CAPTION_SMHEIGHT = 24;
        // misc
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_RESTORE = 0xF120;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SM_SWAPBUTTON = 23;
        private const int WM_GETTITLEBARINFOEX = 0x033F;
        private const int VK_LBUTTON = 0x1;
        private const int VK_RBUTTON = 0x2;
        private const int KEY_PRESSED = 0x1000;
        private const int BLACK_BRUSH = 4;
        // proc
        private const int WM_CREATE = 0x0001;
        private const int WM_NCCALCSIZE = 0x83;
        private const int WM_NCHITTEST = 0x84;
        private const int WM_SIZE = 0x5;
        private const int WM_PAINT = 0xF;
        private const int WM_TIMER = 0x113;
        private const int WM_ACTIVATE = 0x6;
        private const int WM_NCMOUSEMOVE = 0xA0;
        private const int WM_NCMOUSEHOVER = 0x02A0;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int WM_NCLBUTTONUP = 0xA2;
        private const int WM_NCLBUTTONDBLCLK = 0xA3;
        private const int WM_NCRBUTTONDOWN = 0xA4;
        private const int WM_NCRBUTTONUP = 0xA5;
        private const int WM_NCRBUTTONDBLCLK = 0xA6;
        private const int WM_DWMCOMPOSITIONCHANGED = 0x031E;
        private const int WVR_ALIGNTOP = 0x0010;
        private const int WVR_ALIGNLEFT = 0x0020;
        private const int WVR_ALIGNBOTTOM = 0x0040;
        private const int WVR_ALIGNRIGHT = 0x0080;
        private const int WVR_HREDRAW = 0x0100;
        private const int WVR_VREDRAW = 0x0200;
        private const int WVR_REDRAW = (WVR_HREDRAW | WVR_VREDRAW);
        private const int WVR_VALIDRECTS = 0x400;
        private static IntPtr MSG_HANDLED = new IntPtr(0);
        #endregion

        #region Enums
        private enum HIT_CONSTANTS : int
        {
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21
        }

        private enum PART_TYPE : int
        {
            WP_MINBUTTON = 15,
            WP_MAXBUTTON = 17,
            WP_CLOSEBUTTON = 18,
            WP_RESTOREBUTTON = 21
        }
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            internal int X;
            internal int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SIZE
        {
            public int cx;
            public int cy;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            internal RECT(int X, int Y, int Width, int Height)
            {
                this.Left = X;
                this.Top = Y;
                this.Right = Width;
                this.Bottom = Height;
            }
            internal int Left;
            internal int Top;
            internal int Right;
            internal int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PAINTSTRUCT
        {
            internal IntPtr hdc;
            internal int fErase;
            internal RECT rcPaint;
            internal int fRestore;
            internal int fIncUpdate;
            internal int Reserved1;
            internal int Reserved2;
            internal int Reserved3;
            internal int Reserved4;
            internal int Reserved5;
            internal int Reserved6;
            internal int Reserved7;
            internal int Reserved8;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
            public MARGINS(int Left, int Right, int Top, int Bottom)
            {
                this.cxLeftWidth = Left;
                this.cxRightWidth = Right;
                this.cyTopHeight = Top;
                this.cyBottomHeight = Bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NCCALCSIZE_PARAMS
        {
            internal RECT rect0, rect1, rect2;
            internal IntPtr lppos;
        }
        #endregion

        #region API
        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hdc, ref MARGINS marInset);

        [DllImport("dwmapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DwmDefWindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, ref IntPtr result);

        [DllImport("dwmapi.dll")]
        private static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int x, int y, int cx, int cy, uint flags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PtInRect([In] ref RECT lprc, Point pt);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetClientRect(IntPtr hWnd, ref RECT r);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        private static extern int FillRect(IntPtr hDC, [In] ref RECT lprc, IntPtr hbr);

        [DllImport("gdi32.dll")]
        private static extern IntPtr GetStockObject(int fnObject);

        [DllImport("user32.dll")]
        private static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool InflateRect(ref RECT lprc, int dx, int dy);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OffsetRect(ref RECT lprc, int dx, int dy);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);
        #endregion

        #region Fields
        private bool _bPaintWindow = false;
        private bool _bDrawCaption = false;
        private bool _bIsCompatible = false;
        private bool _bIsAero = false;
        private bool _bPainting = false;
        private bool _bExtendIntoFrame = false;
        private int _iCaptionHeight = CAPTION_HEIGHT;
        private int _iFrameHeight = FRAME_WIDTH;
        private int _iFrameWidth = FRAME_WIDTH;
        private int _iFrameOffset = 100;
        private int _iStoreHeight = 0;
        private RECT _tClientRect = new RECT();
        private MARGINS _tMargins = new MARGINS();
        private RECT[] _tButtonSize = new RECT[3];
        #endregion

        #region Properties
        private int CaptionHeight
        {
            get { return _iCaptionHeight; }
        }

        private int FrameWidth
        {
            get { return _iFrameWidth; }
        }

        private int FrameHeight
        {
            get { return _iFrameHeight; }
        }
        #endregion

        #region Methods
        private void ExtendMargins(int left, int top, int right, int bottom, bool drawcaption, bool intoframe)
        {
            // any negative value causes whole window client to extend
            if (left < 0 || top < 0 || right < 0 || bottom < 0)
            {
                _bPaintWindow = true;
                _tMargins.cyTopHeight = -1;
            }
            // only caption can be extended
            else if (intoframe)
            {
                _tMargins.cxLeftWidth = 0;
                _tMargins.cyTopHeight = top;
                _tMargins.cxRightWidth = 0;
                _tMargins.cyBottomHeight = 0;
            }
            // normal extender
            else
            {
                _tMargins.cxLeftWidth = left;
                _tMargins.cyTopHeight = top;
                _tMargins.cxRightWidth = right;
                _tMargins.cyBottomHeight = bottom;
            }
            _bExtendIntoFrame = intoframe;
            _bDrawCaption = drawcaption;
            _bIsCompatible = IsCompatableOS();
            _bIsAero = IsAero();
        }

        private void GetFrameSize()
        {
            if (this.MinimizeBox)
                _iFrameOffset = 100;
            else
                _iFrameOffset = 40;
            switch (this.FormBorderStyle)
            {
                case FormBorderStyle.Sizable:
                    {
                        _iCaptionHeight = CAPTION_HEIGHT;
                        _iFrameHeight = FRAME_WIDTH;
                        _iFrameWidth = FRAME_WIDTH;
                        break;
                    }
                case FormBorderStyle.Fixed3D:
                    {
                        _iCaptionHeight = 27;
                        _iFrameHeight = 4;
                        _iFrameWidth = 4;
                        break;
                    }
                case FormBorderStyle.FixedDialog:
                    {
                        _iCaptionHeight = 25;
                        _iFrameHeight = 2;
                        _iFrameWidth = 2;
                        break;
                    }
                case FormBorderStyle.FixedSingle:
                    {
                        _iCaptionHeight = 25;
                        _iFrameHeight = 2;
                        _iFrameWidth = 2;
                        break;
                    }
                case FormBorderStyle.FixedToolWindow:
                    {
                        _iFrameOffset = 20;
                        _iCaptionHeight = 21;
                        _iFrameHeight = 2;
                        _iFrameWidth = 2;
                        break;
                    }
                case FormBorderStyle.SizableToolWindow:
                    {
                        _iFrameOffset = 20;
                        _iCaptionHeight = 26;
                        _iFrameHeight = 4;
                        _iFrameWidth = 4;
                        break;
                    }
                default:
                    {
                        _iCaptionHeight = CAPTION_HEIGHT;
                        _iFrameHeight = FRAME_WIDTH;
                        _iFrameWidth = FRAME_WIDTH;
                        break;
                    }
            }
        }

        private HIT_CONSTANTS HitTest()
        {
            RECT windowRect = new RECT();
            Point cursorPoint = new Point();
            RECT posRect;
            GetCursorPos(ref cursorPoint);
            GetWindowRect(this.Handle, ref windowRect);
            cursorPoint.X -= windowRect.Left;
            cursorPoint.Y -= windowRect.Top;
            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;

            posRect = new RECT(0, 0, FrameWidth, FrameHeight);
            if (PtInRect(ref posRect, cursorPoint))
                return HIT_CONSTANTS.HTTOPLEFT;

            posRect = new RECT(width - FrameWidth, 0, width, FrameHeight);
            if (PtInRect(ref posRect, cursorPoint))
                return HIT_CONSTANTS.HTTOPRIGHT;

            posRect = new RECT(FrameWidth, 0, width - (FrameWidth * 2) - _iFrameOffset, FrameHeight);
            if (PtInRect(ref posRect, cursorPoint))
                return HIT_CONSTANTS.HTTOP;

            posRect = new RECT(FrameWidth, FrameHeight, width - ((FrameWidth * 2) + _iFrameOffset), _tMargins.cyTopHeight);
            if (PtInRect(ref posRect, cursorPoint))
                return HIT_CONSTANTS.HTCAPTION;

            posRect = new RECT(0, FrameHeight, FrameWidth, height - FrameHeight);
            if (PtInRect(ref posRect, cursorPoint))
                return HIT_CONSTANTS.HTLEFT;

            posRect = new RECT(0, height - FrameHeight, FrameWidth, height);
            if (PtInRect(ref posRect, cursorPoint))
                return HIT_CONSTANTS.HTBOTTOMLEFT;

            posRect = new RECT(FrameWidth, height - FrameHeight, width - FrameWidth, height);
            if (PtInRect(ref posRect, cursorPoint))
                return HIT_CONSTANTS.HTBOTTOM;

            posRect = new RECT(width - FrameWidth, height - FrameHeight, width, height);
            if (PtInRect(ref posRect, cursorPoint))
                return HIT_CONSTANTS.HTBOTTOMRIGHT;

            posRect = new RECT(width - FrameWidth, FrameHeight, width, height - FrameHeight);
            if (PtInRect(ref posRect, cursorPoint))
                return HIT_CONSTANTS.HTRIGHT;

            return HIT_CONSTANTS.HTCLIENT;
        }

        public bool IsAero()
        {
            if (!Properties.Settings.Default.UseAero)
                return false;
            else
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1);
            }
        }

        public bool IsCompatableOS()
        {
            return (Environment.OSVersion.Version.Major >= 6);
        }

        private void FrameChanged()
        {
            RECT rcClient = new RECT();
            GetWindowRect(this.Handle, ref rcClient);
            // force a calc size message
            SetWindowPos(this.Handle,
                         IntPtr.Zero,
                         rcClient.Left, rcClient.Top,
                         rcClient.Right - rcClient.Left, rcClient.Bottom - rcClient.Top,
                         SWP_FRAMECHANGED);
        }

        private void InvalidateWindow()
        {
            RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_UPDATENOW | RDW_INVALIDATE | RDW_ERASE);
        }

        private void PaintThis(IntPtr hdc, RECT rc)
        {
            RECT clientRect = new RECT();
            GetClientRect(this.Handle, ref clientRect);
            if (_bExtendIntoFrame)
            {
                clientRect.Left = _tClientRect.Left - _tMargins.cxLeftWidth;
                clientRect.Top = _tMargins.cyTopHeight;
                clientRect.Right -= _tMargins.cxRightWidth;
                clientRect.Bottom -= _tMargins.cyBottomHeight;
            }
            else if (!_bPaintWindow)
            {
                clientRect.Left = _tMargins.cxLeftWidth;
                clientRect.Top = _tMargins.cyTopHeight;
                clientRect.Right -= _tMargins.cxRightWidth;
                clientRect.Bottom -= _tMargins.cyBottomHeight;
            }
            if (!_bPaintWindow)
            {
                int clr;
                IntPtr hb;
                using (ClippingRegion cp = new ClippingRegion(hdc, clientRect, rc))
                {
                    if (IsAero())
                    {
                        FillRect(hdc, ref rc, GetStockObject(BLACK_BRUSH));
                    }
                    else
                    {
                        clr = ColorTranslator.ToWin32(Color.FromArgb(0xC2, 0xD9, 0xF7));
                        hb = CreateSolidBrush(clr);
                        FillRect(hdc, ref clientRect, hb);
                        DeleteObject(hb);
                    }
                }
                clr = ColorTranslator.ToWin32(this.BackColor);
                hb = CreateSolidBrush(clr);
                FillRect(hdc, ref clientRect, hb);
                DeleteObject(hb);
            }
            else
            {
                FillRect(hdc, ref rc, GetStockObject(BLACK_BRUSH));
            }
            if (_bExtendIntoFrame && _bDrawCaption)
            {
                Rectangle captionBounds = new Rectangle(4, 4, rc.Right, CaptionHeight);
                using (Graphics g = Graphics.FromHdc(hdc))
                {
                    using (Font fc = new Font("Segoe UI", 12, FontStyle.Regular))
                    {
                        SizeF sz = g.MeasureString(this.Text, fc);
                        int offset = (rc.Right - (int)sz.Width) / 2;
                        if (offset < 2 * FrameWidth)
                            offset = 2 * FrameWidth;
                        captionBounds.X = offset;
                        captionBounds.Y = 4;
                        using (StringFormat sf = new StringFormat())
                        {
                            sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
                            sf.FormatFlags = StringFormatFlags.NoWrap;
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Near;
                            using (GraphicsPath path = new GraphicsPath())
                            {
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                path.AddString(this.Text, fc.FontFamily, (int)fc.Style, fc.Size, captionBounds, sf);
                                g.FillPath(Brushes.Black, path);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region WndProc
        protected override void WndProc(ref Message m)
        {
            if (_bIsCompatible)
            {
                CustomProc(ref m);
            }
            else
                base.WndProc(ref m);
        }

        protected void CustomProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_PAINT:
                    {
                        PAINTSTRUCT ps = new PAINTSTRUCT();
                        if (!_bPainting)
                        {
                            _bPainting = true;
                            BeginPaint(m.HWnd, ref ps);
                            PaintThis(ps.hdc, ps.rcPaint);
                            EndPaint(m.HWnd, ref ps);
                            _bPainting = false;
                            base.WndProc(ref m);
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    }
                case WM_CREATE:
                    {
                        GetFrameSize();
                        FrameChanged();
                        m.Result = MSG_HANDLED;
                        base.WndProc(ref m);
                        break;
                    }
                case WM_NCCALCSIZE:
                    {
                        if (m.WParam != IntPtr.Zero && m.Result == IntPtr.Zero)
                        {
                            if (_bExtendIntoFrame)
                            {
                                NCCALCSIZE_PARAMS nc = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));
                                nc.rect0.Top -= (_tMargins.cyTopHeight > CaptionHeight ? CaptionHeight : _tMargins.cyTopHeight);
                                nc.rect1 = nc.rect0;
                                Marshal.StructureToPtr(nc, m.LParam, false);
                                m.Result = (IntPtr)WVR_VALIDRECTS;
                            }
                            base.WndProc(ref m);
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    }
                case WM_SYSCOMMAND:
                    {
                        UInt32 param;
                        if (IntPtr.Size == 4)
                            param = (UInt32)(m.WParam.ToInt32());
                        else
                            param = (UInt32)(m.WParam.ToInt64());
                        if ((param & 0xFFF0) == SC_RESTORE)
                        {
                            this.Height = _iStoreHeight;
                        }
                        else if (this.WindowState == FormWindowState.Normal)
                        {
                            _iStoreHeight = this.Height;
                        }
                        base.WndProc(ref m);
                        break;
                    }
                case WM_NCHITTEST:
                    {
                        if (m.Result == (IntPtr)HIT_CONSTANTS.HTNOWHERE)
                        {
                            IntPtr res = IntPtr.Zero;
                            if (DwmDefWindowProc(m.HWnd, (uint)m.Msg, m.WParam, m.LParam, ref res))
                                m.Result = res;
                            else
                                m.Result = (IntPtr)HitTest();
                        }
                        else
                            base.WndProc(ref m);
                        break;
                    }
                case WM_DWMCOMPOSITIONCHANGED:
                case WM_ACTIVATE:
                    {
                        DwmExtendFrameIntoClientArea(this.Handle, ref _tMargins);
                        m.Result = MSG_HANDLED;
                        base.WndProc(ref m);
                        break;
                    }
                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }
        #endregion

        #region Clipping Region
        /// <summary>Clip rectangles or rounded rectangles</summary>
        internal class ClippingRegion : IDisposable
        {
            #region Enum
            private enum CombineRgnStyles : int
            {
                RGN_AND = 1,
                RGN_OR = 2,
                RGN_XOR = 3,
                RGN_DIFF = 4,
                RGN_COPY = 5,
                RGN_MIN = RGN_AND,
                RGN_MAX = RGN_COPY
            }
            #endregion

            #region API
            [DllImport("gdi32.dll")]
            private static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

            [DllImport("gdi32.dll")]
            private static extern int GetClipRgn(IntPtr hdc, [In, Out]IntPtr hrgn);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateEllipticRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);

            [DllImport("gdi32.dll")]
            private static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, CombineRgnStyles fnCombineMode);

            [DllImport("gdi32.dll")]
            private static extern int ExcludeClipRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

            [DllImport("gdi32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool DeleteObject(IntPtr hObject);
            #endregion

            #region Fields
            private IntPtr _hClipRegion;
            private IntPtr _hDc;
            #endregion

            #region Methods
            public ClippingRegion(IntPtr hdc, Rectangle cliprect, Rectangle canvasrect)
            {
                CreateRectangleClip(hdc, cliprect, canvasrect);
            }

            public ClippingRegion(IntPtr hdc, RECT cliprect, RECT canvasrect)
            {
                CreateRectangleClip(hdc, cliprect, canvasrect);
            }

            public ClippingRegion(IntPtr hdc, Rectangle cliprect, Rectangle canvasrect, uint radius)
            {
                CreateRoundedRectangleClip(hdc, cliprect, canvasrect, radius);
            }

            public ClippingRegion(IntPtr hdc, RECT cliprect, RECT canvasrect, uint radius)
            {
                CreateRoundedRectangleClip(hdc, cliprect, canvasrect, radius);
            }

            public void CreateRectangleClip(IntPtr hdc, Rectangle cliprect, Rectangle canvasrect)
            {
                _hDc = hdc;
                IntPtr clip = CreateRectRgn(cliprect.Left, cliprect.Top, cliprect.Right, cliprect.Bottom);
                IntPtr canvas = CreateRectRgn(canvasrect.Left, canvasrect.Top, canvasrect.Right, canvasrect.Bottom);
                _hClipRegion = CreateRectRgn(canvasrect.Left, canvasrect.Top, canvasrect.Right, canvasrect.Bottom);
                CombineRgn(_hClipRegion, canvas, clip, CombineRgnStyles.RGN_DIFF);
                SelectClipRgn(_hDc, _hClipRegion);
                DeleteObject(clip);
                DeleteObject(canvas);
            }

            public void CreateRectangleClip(IntPtr hdc, RECT cliprect, RECT canvasrect)
            {
                _hDc = hdc;
                IntPtr clip = CreateRectRgn(cliprect.Left, cliprect.Top, cliprect.Right, cliprect.Bottom);
                IntPtr canvas = CreateRectRgn(canvasrect.Left, canvasrect.Top, canvasrect.Right, canvasrect.Bottom);
                _hClipRegion = CreateRectRgn(canvasrect.Left, canvasrect.Top, canvasrect.Right, canvasrect.Bottom);
                CombineRgn(_hClipRegion, canvas, clip, CombineRgnStyles.RGN_DIFF);
                SelectClipRgn(_hDc, _hClipRegion);
                DeleteObject(clip);
                DeleteObject(canvas);
            }

            public void CreateRoundedRectangleClip(IntPtr hdc, Rectangle cliprect, Rectangle canvasrect, uint radius)
            {
                int r = (int)radius;
                _hDc = hdc;
                // create rounded regions
                IntPtr clip = CreateRoundRectRgn(cliprect.Left, cliprect.Top, cliprect.Right, cliprect.Bottom, r, r);
                IntPtr canvas = CreateRectRgn(canvasrect.Left, canvasrect.Top, canvasrect.Right, canvasrect.Bottom);
                _hClipRegion = CreateRoundRectRgn(canvasrect.Left, canvasrect.Top, canvasrect.Right, canvasrect.Bottom, r, r);
                CombineRgn(_hClipRegion, canvas, clip, CombineRgnStyles.RGN_DIFF);
                // add it in
                SelectClipRgn(_hDc, _hClipRegion);
                DeleteObject(clip);
                DeleteObject(canvas);
            }

            public void CreateRoundedRectangleClip(IntPtr hdc, RECT cliprect, RECT canvasrect, uint radius)
            {
                int r = (int)radius;
                _hDc = hdc;
                // create rounded regions
                IntPtr clip = CreateRoundRectRgn(cliprect.Left, cliprect.Top, cliprect.Right, cliprect.Bottom, r, r);
                IntPtr canvas = CreateRectRgn(canvasrect.Left, canvasrect.Top, canvasrect.Right, canvasrect.Bottom);
                _hClipRegion = CreateRoundRectRgn(canvasrect.Left, canvasrect.Top, canvasrect.Right, canvasrect.Bottom, r, r);
                CombineRgn(_hClipRegion, canvas, clip, CombineRgnStyles.RGN_DIFF);
                // add it in
                SelectClipRgn(_hDc, _hClipRegion);
                DeleteObject(clip);
                DeleteObject(canvas);
            }

            public void Release()
            {
                if (_hClipRegion != IntPtr.Zero)
                {
                    // remove region
                    SelectClipRgn(_hDc, IntPtr.Zero);
                    // delete region
                    DeleteObject(_hClipRegion);
                }
            }

            public void Dispose()
            {
                Release();
            }
            #endregion
        }
        #endregion

        #endregion
        public void DecideBrowserPreferences(WebKitBrowser wb)
        {
            try
            {
                wb.Preferences.UseCache = Properties.Settings.Default.Cache;
            }
            catch { }
            try
            {
                wb.WebView.preferences().setAcceleratedCompositingEnabled(Convert.ToInt32(Properties.Settings.Default.UseAcceleratingCompositing));

                wb.WebView.setApplicationNameForUserAgent("GTLite Navigator");
            }
            catch { }
            try
            {
                wb.WebView.preferences().setAcceleratedCompositingEnabled(Convert.ToInt32(Properties.Settings.Default.UseAcceleratingCompositing));
            }
            catch { }

            try { Browser.Preferences.AlwaysCheckSpelling = true; }
            catch { }
            try { wb.EnableHTTPPipelining = true; }
            catch { }
            foreach (string pl in Properties.Settings.Default.PluginsDirectories)
            {
                wb.WebView.addAdditionalPluginDirectory(pl.Replace("<GTLite>", Application.StartupPath));
            }
            try
            {
                wb.WebView.preferences().setCookieStorageAcceptPolicy(WebKit.Interop.WebKitCookieStorageAcceptPolicy.WebKitCookieStorageAcceptPolicyAlways);
            }
            catch { }
        }
        public void AddTab(WebKitBrowser br)
        {
            GTTabPage tb = new GTTabPage();
            string n = "";
            string u = "";
            if (!string.IsNullOrEmpty(br.DocumentTitle))
                n = br.DocumentTitle;
            if (br.Url != null)
                u = br.Url.ToString();
            tb.ButtonConfiguration.Appearance.Shape = qShape1;
            tb.ButtonConfiguration.AppearanceActive.Shape = qShape1;
            tb.ButtonConfiguration.AppearanceHot.Shape = qShape1;
            tb.ButtonConfiguration.MinimumSize = new System.Drawing.Size(130, 25);
            tb.ButtonConfiguration.MaximumSize = new System.Drawing.Size(231, 25);
            WebKitBrowser wb = new WebKitBrowser();
            wb = br;
            wb.AllowDownloads = true;
            wb.UseDefaultContextMenu = true;
            wb.Visible = true;
            wb.Name = "browser";
            wb.Dock = DockStyle.Fill;
            tb.Controls.Add(wb);
            qTabControl1.Controls.Add(tb);
            qTabControl1.ActiveTabPage = tb;
            qTextBox1.Text = u;
            tb.Text = n;
            AddEvents(wb);
            DecideBrowserPreferences(wb);
            MenuItem enc = wb.CustomContextMenuManager.BodyMenu.MenuItems.Add("Encoding");
            if (!string.IsNullOrEmpty(Properties.Settings.Default.cssstyle))
                wb.CSSManager.SetPageStyleSheetFromLocalFile(Properties.Settings.Default.cssstyle);
            else
                wb.CSSManager.SetPageDefaultStyleSheet();
            
            foreach (EncodingInfo e in Encoding.GetEncodings())
            {
                MenuItem a = enc.MenuItems.Add(e.GetEncoding().EncodingName);
                a.RadioCheck = true;
                a.Click += new EventHandler(Main_Click);
                a.Tag = e.Name;
            }
            timer1.Enabled = true;
            wb.CustomContextMenuManager.ShowContextMenu += new ShowContextMenu(CustomContextMenuManager_ShowContextMenu);

            try
            {
                wb.ResourceIntercepter.ResourceFinishedLoadingEvent += new ResourceFinishedLoadingHandler(ResourceIntercepter_ResourceFinishedLoadingEvent);
            }
            catch { }
            try
            {
                wb.ResourceIntercepter.ResourcesSendRequest += new ResourceSendRequestEventHandler(res_s);
            }
            catch { }
        }

        void wb_ProgressChanged(object sender, ProgressChangesEventArgs e)
        {
            try
            {
                panelEx2.Maximum = 100;
                panelEx2.Minimum = 0;
                panelEx2.Value = e.Percent;
            }
            catch{ }
        }

        public void AddEvents(WebKitBrowser wb)
        {
            wb.Error += new WebKitBrowserErrorEventHandler(wb_Error);
            wb.FormSubmit += new WillSubmitForm(wb_FormSubmit);
            wb.TextFieldEndEditing += new TextFieldDidEndEditing(wb_TextFieldEndEditing);            
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            wb.DocumentTitleChanged += new EventHandler(wb_DocumentTitleChanged);
            wb.Navigated += new WebBrowserNavigatedEventHandler(wb_Navigated);
            wb.Click += new EventHandler(wb_Click);
            wb.ProgressChanged += new WebKit.ProgressChangedEventHandler(wb_ProgressChanged);
            wb.Navigating += new WebKitBrowserNavigatingEventHandler(wb_Navigating);
            wb.NewWindowCreated += new NewWindowCreatedEventHandler(wb_NewWindowCreated);
            wb.DownloadBegin += new FileDownloadBeginEventHandler(wb_DownloadBegin);
            wb.StatusTextChanged += new StatusTextChanged(wb_StatusTextChanged);
            wb.CloseWindowRequest += new EventHandler(wb_CloseWindowRequest);
            wb.ShowJavaScriptAlertPanel += new ShowJavaScriptAlertPanelEventHandler(wb_ShowJavaScriptAlertPanel);
            wb.ShowJavaScriptConfirmPanel += new ShowJavaScriptConfirmPanelEventHandler(wb_ShowJavaScriptConfirmPanel);
            wb.ShowJavaScriptPromptPanel += new ShowJavaScriptPromptPanelEventHandler(wb_ShowJavaScriptPromptPanel);
            wb.PluginFailed += new PluginFailedEventHandler(wb_PluginFailed);
            wb.PopupCreated += new NewWindowCreatedEventHandler(wb_PopupCreated);
            wb.DangerousSiteDetected += new EventHandler(wb_DangerousSiteDetected);
            res.Add(wb, new List<WebKitResource>());
            feeds.Add(wb, new List<string>());
            if (wb.Url == null)
            UrlValues.Add(wb, new _gtlitetype_s(""));
            else
                UrlValues.Add(wb, new _gtlitetype_s(wb.Url.ToString()));
        }

        void wb_TextFieldEndEditing(object sender, FormDelegateElementEventArgs e)
        {
            if (Properties.Settings.Default.AutoFill)
            {
                if (!string.IsNullOrEmpty(e.Element.ID))
                {
                    try
                    {
                        Prompt[(sender as WebKitBrowser)]._v = true;
                    }
                    catch { }
                    try
                    {
                        string scr = @"document.getElementById('" + e.Element.ID + @"').value";
                        string value = (sender as WebKitBrowser).StringByEvaluatingJavaScriptFromString(scr);
                        AutoFillManager.Elements[(sender as WebKitBrowser).Url.ToString()].Add(new WebKitFormData(e.Element, value));
                    }
                    catch
                    {
                        try
                        {
                            string scr = @"document.getElementById('" + e.Element.ID + @"').value";
                            string value = (sender as WebKitBrowser).StringByEvaluatingJavaScriptFromString(scr);
                            if (string.IsNullOrEmpty(value))
                                value = (sender as WebKitBrowser).StringByEvaluatingJavaScriptFromString(@"document.getElementById('" + e.Element.GetAttribute("name") + @"').value");
                            AutoFillManager.Elements.Add((sender as WebKitBrowser).Url.ToString(), new List<WebKitFormData>());
                            AutoFillManager.Elements[(sender as WebKitBrowser).Url.ToString()].Add(new WebKitFormData(e.Element, value));
                        }
                        catch { }
                    }
                }
            }
        }
        public List<WebKitResource> r(WebKitBrowser br)
        {
            List<WebKitResource> toret;
            res.TryGetValue(br, out toret);
            return toret;
        }
        public List<string> f(WebKitBrowser br)
        {
            List<string> toret;
            feeds.TryGetValue(br, out toret);
            return toret;
        }
        public Dictionary<WebKitBrowser, List<string>> feeds = new Dictionary<WebKitBrowser, List<string>>();
        public Dictionary<WebKitBrowser, List<WebKitResource>> res = new Dictionary<WebKitBrowser, List<WebKitResource>>();
        void ResourceIntercepter_ResourceFinishedLoadingEvent(object sender, WebKitResourcesEventArgs e)
        {
            r((sender as ResourcesIntercepter).Owner).Add(e.Resource);
            if (e.Resource.MimeType != null)
            if (e.Resource.MimeType.Contains("rss"))
            {
                if ((sender as ResourcesIntercepter).Owner.Equals(Browser))
                {
                    buttonX20.Visible = true;
                    buttonX20.Tooltip = "Click or Expand to view available feeds";
                    ButtonItem btn = new ButtonItem(e.Resource.Url);
                    btn.Text = e.Resource.Url;
                    buttonX20.SubItems.Add(btn);
                    btn.Click += new EventHandler(btn_Click);
                }
                f((sender as ResourcesIntercepter).Owner).Add(e.Resource.Url);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            ButtonItem b = (sender as ButtonItem);
            RSS_Reader.Reader rd = new RSS_Reader.Reader(b.Text);
            rd.Show();
        }
        void res_s(object o, WebKitResourceRequestEventArgs e)
        {
            if (Properties.Settings.Default.AdBlock)
            {
                if (e.ResourceUrl.EndsWith("show_ads.js") || e.ResourceUrl.Contains("ads.ad4game") || e.ResourceUrl.Contains("googleadservices") || e.ResourceUrl.Contains("ads.lycos.com") || e.ResourceUrl.Contains("scripts.lycos.com") || e.ResourceUrl.Contains("/ad/") || e.ResourceUrl.Contains("adbanner") || e.ResourceUrl.Contains("ad-emea") || e.ResourceUrl.Contains("ad.yieldmanager"))
                    e.SendRequest = false;
                if (e.ResourceUrl.Contains("echo.cx") && e.ResourceUrl.EndsWith(".gif"))
                    e.SendRequest = false;
            }
        }
        void CustomContextMenuManager_ShowContextMenu(object sender, ShowContextMenuEventArgs e)
        {
            if (e.MenuType == ContextMenuType.Body)
            {
                (sender as WebKitBrowser).UseDefaultContextMenu = false;
                contextMenu1.Show((sender as WebKitBrowser), e.Location);
            }
            else{
                (sender as WebKitBrowser).UseDefaultContextMenu = true;
            }
        }

        void wb_PopupCreated(object sender, NewWindowCreatedEventArgs e)
        {
            foreach (string s in Properties.Settings.Default.PopupExceptionsL)
            {
                if (s == (sender as WebKitBrowser).Url.Host || s == (sender as WebKitBrowser).Url.ToString())
                {
                    return;
                }
            }
            foreach (string s in Properties.Settings.Default.PopupExceptions)
            {
                if (s == (sender as WebKitBrowser).Url.Host || s == (sender as WebKitBrowser).Url.ToString())
                {
                    wb_NewWindowCreated(sender, e);
                    return;                
                }
            }
            if (Properties.Settings.Default.PopupBlocker == true)
            {
                PopupBlocked p = new PopupBlocked(e.WebKitBrowser, this);
                (sender as WebKitBrowser).Parent.Controls.Add(p);
                p.Dock = DockStyle.Bottom;
                p.Form = new Popup(e.WebKitBrowser);
                p.Form.Show();
                p.Form.Hide();
            }
            else
            {
                Popup p = new Popup(e.WebKitBrowser);
                p.Show();
                p.Focus();
            }
        }

        void wb_PluginFailed(object sender, PluginFailedErrorEventArgs e)
        {
            MessageBoxEx.Show("A plugin has failed giving the following error: \r\n \r\n " + e.ErrorDescription);
        }

        void wb_DocumentTitleChanged(object sender, EventArgs e)
        {
            WebKitBrowser br = (sender as WebKitBrowser);
            if (br != null)
            {
                ((GTTabPage)(br.Parent)).Text = br.DocumentTitle;
            }
        }

        void wb_ShowJavaScriptPromptPanel(object sender, ShowJavaScriptPromptPanelEventArgs e)
        {
            e.ReturnValue = Microsoft.VisualBasic.Interaction.InputBox(e.Message, (sender as WebKitBrowser).DocumentTitle, e.DefaultValue);
        }

        void wb_ShowJavaScriptConfirmPanel(object sender, ShowJavaScriptConfirmPanelEventArgs e)
        {
            if (MessageBoxEx.Show(e.Message, (sender as WebKitBrowser).DocumentTitle, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                e.ReturnValue = true;
            }
            else
            {
                e.ReturnValue = false;
            }
        }

        void wb_ShowJavaScriptAlertPanel(object sender, ShowJavaScriptAlertPanelEventArgs e)
        {
            MessageBoxEx.Show(e.Message);
        }

        void wb_DangerousSiteDetected(object sender, EventArgs e)
        {
            warningBox1.Tag = (sender as WebKitBrowser).Url.Host;
            tableLayoutPanel1.Visible = true;
            warningBox1.Visible = true;
        }

        public void RemoveEvents(WebKitBrowser wb)
        {
            wb.Error -= new WebKitBrowserErrorEventHandler(wb_Error);
            wb.FormSubmit -= new WillSubmitForm(wb_FormSubmit);
            wb.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            wb.Navigated -= new WebBrowserNavigatedEventHandler(wb_Navigated);
            wb.Click -= new EventHandler(wb_Click);
            wb.ProgressChanged -= new WebKit.ProgressChangedEventHandler(wb_ProgressChanged);
            wb.Navigating -= new WebKitBrowserNavigatingEventHandler(wb_Navigating);
            wb.NewWindowCreated -= new NewWindowCreatedEventHandler(wb_NewWindowCreated);
            wb.DownloadBegin -= new FileDownloadBeginEventHandler(wb_DownloadBegin);
            wb.StatusTextChanged -= new StatusTextChanged(wb_StatusTextChanged);
            wb.CloseWindowRequest -= new EventHandler(wb_CloseWindowRequest);
        }
        void wb_CloseWindowRequest(object sender, EventArgs e)
        {
            GTTabPage t = ((sender as WebKitBrowser).Parent as GTTabPage);
            qTabControl1.Controls.Remove(t);
        }

        void wb_StatusTextChanged(object sender, WebKitBrowserStatusChangedEventArgs e)
        {
            if (Browser != null)
            if (sender.Equals(Browser))
            {
                label2.Visible = true;
                if (e.StatusText != null)
                {
                    string s;
                    if (e.StatusText.Length > 45)
                        s = e.StatusText.Substring(0, 45) + "...";
                    else
                        s = e.StatusText;
                    if (!Uri.IsWellFormedUriString(e.StatusText, UriKind.RelativeOrAbsolute) && Browser.IsBusy == true)
                        label2.Text = e.StatusText;
                    if (Browser.IsBusy)
                        label2.Text = s;
                    if (Uri.IsWellFormedUriString(e.StatusText, UriKind.RelativeOrAbsolute) && e.StatusText != "" && e.StatusText != "Completed")
                        label2.Text = "Go to: " + s;
                    if (Browser.IsBusy == false && e.StatusText == "")
                    {
                        panel1.Visible = false;
                    }
                    else
                    {
                        panel1.Visible = true;
                    }
                }
            }
        }

        public void AddTab(string url, bool background = false)
        {
            GTTabPage tb = new GTTabPage();
            WebKitBrowser wb = new WebKitBrowser();
            tb.ButtonConfiguration.Appearance.Shape = qShape1;
            tb.ButtonConfiguration.AppearanceActive.Shape = qShape1;
            tb.ButtonConfiguration.AppearanceHot.Shape = qShape1;
            tb.ButtonConfiguration.MinimumSize = new System.Drawing.Size(130, 25);
            tb.ButtonConfiguration.MaximumSize = new System.Drawing.Size(231, 25);
            tb.Controls.Add(wb);
            qTabControl1.Controls.Add(tb);
            wb.Dock = DockStyle.Fill;
            wb.AllowDownloads = true;
            wb.AllowNewWindows = true;
            wb.UseDefaultContextMenu = false;
            DecideBrowserPreferences(wb);

            InputContextMenu mn = new InputContextMenu(wb);
            DecideSecuritySettings(wb);
            AddEvents(wb);
            foreach (string pl in Properties.Settings.Default.PluginsDirectories)
            {
                wb.WebView.addAdditionalPluginDirectory(pl.Replace("<GTLite>", Application.StartupPath));
            }
            wb.Navigate(url);
            timer1.Enabled = true;
            if (background == false) { qTabControl1.ActivateTabPage(tb); }
            wb.CustomContextMenuManager.ShowContextMenu += new ShowContextMenu(CustomContextMenuManager_ShowContextMenu);
            try
            {
                wb.ResourceIntercepter.ResourceFinishedLoadingEvent += new ResourceFinishedLoadingHandler(ResourceIntercepter_ResourceFinishedLoadingEvent);
            }
            catch { }
            try
            {
                wb.ResourceIntercepter.ResourcesSendRequest += new ResourceSendRequestEventHandler(res_s);
            }
            catch { }
        }

        void i_Click(object sender, EventArgs e)
        {
            Browser.ShowInspector();
        }

        void Main_Click(object sender, EventArgs e)
        {
            string t = (string)(sender as MenuItem).Tag;
            Browser.DocumentEncoding = Encoding.GetEncoding(t);
            foreach (MenuItem mn in (sender as MenuItem).Parent.MenuItems)
                mn.Checked = false;
            (sender as MenuItem).Checked = true;
        }

        void wb_FormSubmit(object sender, FormDelegateFormEventArgs e)
        {
            if (Properties.Settings.Default.AutoFill && AutoFillManager.Elements[(sender as WebKitBrowser).Url.ToString()].Count != 0 && Prompt[(WebKitBrowser)sender]._v == true)
            {
                if (!AutoFillManager.CanFillForm((sender as WebKitBrowser)))
                    labelX3.Text = "Would you like GTLite Navigator to remember the form you submitted?";               
                else
                    labelX3.Text = "Would you like to change your current login details to the ones you just submitted?";
            
                groupPanel5.Tag = new AutoFillConfirm(sender as WebKitBrowser, (sender as WebKitBrowser).Url.ToString());
                groupPanel5.Visible = true;
            }
            e.Listener.continueSubmit();
        }

        void DecideSecuritySettings(WebKitBrowser wb)
        {
            int securityLevel = Properties.Settings.Default.SecurityLevel;
            try
            {
                wb.Preferences.EnableWebKitWebSecurity = (Properties.Settings.Default.SecurityLevel > 1);
            }
            catch { }
            if (securityLevel == 2)
            {
                try
                {
                    wb.WebView.setAllowSiteSpecificHacks(0);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaEnabled(1);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaScriptCanOpenWindowsAutomatically(1);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaScriptEnabled(1);

                }
                catch { }

            }
            if (securityLevel == 1)
            {
                try
                {
                    wb.WebView.setAllowSiteSpecificHacks(1);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaEnabled(1);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaScriptCanOpenWindowsAutomatically(1);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaScriptEnabled(1);

                }
                catch { }
            }
            if (securityLevel == 3)
            {
                try
                {
                    wb.WebView.setAllowSiteSpecificHacks(0);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaEnabled(1);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaScriptCanOpenWindowsAutomatically(0);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaScriptEnabled(0);

                }
                catch { }
            }
            if (securityLevel == 4)
            {
                try
                {
                    wb.WebView.setAllowSiteSpecificHacks(0);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaEnabled(0);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaScriptCanOpenWindowsAutomatically(0);

                }
                catch { }
                try
                {
                    wb.WebView.preferences().setJavaScriptEnabled(0);

                }
                catch { }
            }
        }
        void wb_Click(object sender, EventArgs e)
        {
        }

        void wb_DownloadBegin(object sender, FileDownloadBeginEventArgs e)
        {

        }


        void wb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                if (e.KeyCode == Keys.Oemplus)
                {
                    Browser.IncreaseZoom();
                }
                if (e.KeyCode == Keys.OemMinus)
                {
                    Browser.DecreaseZoom();
                }
            }
            if (e.Shift == true)
            {
                if (e.KeyCode == Keys.Oemplus)
                {
                    Browser.IncreaseTextZoom();
                }
                if (e.KeyCode == Keys.OemMinus)
                {
                    Browser.DecreaseTextZoom();
                }
            }
        }

        void wb_NewWindowCreated(object sender, NewWindowCreatedEventArgs e)
        {
            AddTab(e.WebKitBrowser);
        }
        void wb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            try
            {
                WebKitBrowser br = ((WebKitBrowser)sender);
                if (br.Equals(Browser))
                {
                    qTextBox1.Text = e.Url.ToString().Replace(" ", "%20");
                    ((GTTabPage)br.Parent).Text = "Downloading data...";
                    if (br.Url.ToString().StartsWith("https://") == true)
                    {
                        buttonX1.Image = GTLite.Properties.Resources._1291449967_security_high;
                        HighSecurity = true;
                    }
                    else
                    {
                        buttonX1.Image = GTLite.Properties.Resources._1291732552_lock;
                        HighSecurity = false;
                    }
                }
                if (Properties.Settings.Default.UseFavicon == true)
                {
                    if (br.Url != null)
                    {
                        Image favicon = Functions.GetIcon(br.Url.ToString());
                        ((GTTabPage)(br.Parent)).Icon = Icon.FromHandle((favicon as Bitmap).GetHicon());
                        toolStripButton7.Image = favicon;
                    }
                    else
                        ((GTTabPage)(br.Parent)).Icon = Properties.Resources.New_document;
                }
                else
                {
                    ((GTTabPage)(br.Parent)).Icon = Properties.Resources.New_document;
                    toolStripButton7.Image = Properties.Resources.New_document.ToBitmap();
                }
            }
            catch { }
        }

        void wb_Error(object sender, WebKitBrowserErrorEventArgs e)
        {
            if (e.Description != "" && !string.IsNullOrEmpty(e.Description) && e.Description != "cancelled")
            {
                string fhtml;
                fhtml = Properties.Resources.String1.Replace("arg1", e.Description).Replace("arg2", UrlValues[(WebKitBrowser)sender]._v);
                (sender as WebKitBrowser).DocumentText = fhtml;
            }

        }
        public bool IsRSS(WebKitBrowser br)
        {
            List<string> feedsa = f(br);
            if (feedsa.Count != 0)
                {
                    buttonX20.SubItems.Clear();
                    foreach (string d in feedsa)
                    {
                        ButtonItem btn = new ButtonItem(d);
                        btn.Text = d;
                        buttonX20.SubItems.Add(btn);
                        btn.Click += new EventHandler(btn_Click);
                    }
                }
                return (f(br).Count != 0);
        }
        Dictionary<WebKitBrowser, _gtlitetype_s> UrlValues = new Dictionary<WebKitBrowser, _gtlitetype_s>();
        void wb_Navigating(object sender, WebKitBrowserNavigatingEventArgs e)
        {
            BackgroundWorker navigating = new BackgroundWorker();
            UrlValues[(WebKitBrowser)sender]._v = e.Url.ToString();
            navigating.DoWork += new DoWorkEventHandler(navigating_DoWork);
            if (!Prompt.ContainsKey((sender as WebKitBrowser)))
                Prompt.Add((sender as WebKitBrowser), new _gtlitetype_p(false));
            else
                Prompt[(sender as WebKitBrowser)]._v = false;
            _url = e.Url.ToString();
            if (_url.Contains("type=RSS") || _url.EndsWith(".rss"))
            {
                groupPanel4.Tag = _url;
                groupPanel4.Visible = true;
                e.Cancel = true;
            }
            navigating.RunWorkerAsync(sender);
            r(sender as WebKitBrowser).Clear();
        }

        
        private string _url;
        void navigating_DoWork(object sender, DoWorkEventArgs e)
        {
            WebKitBrowser br = (e.Argument as WebKitBrowser);
            try
            {
                panelEx2.Visible = true;
                buttonX20.Visible = false;
                buttonX20.SubItems.Clear();
                if (Properties.Settings.Default.BlockedSites.Contains(new Uri(_url).Host) || Properties.Settings.Default.BlockedSites.Contains(_url))
                {
                    e.Cancel = true;
                    br.OpenDocument(Application.StartupPath + @"\Help\blocked.htm");
                }
                br.Parent.Invoke(new MethodInvoker(delegate {
                if (((GTTabPage)br.Parent).PrivateBrowsing == false)
                {
                    br.WebView.setCookieEnabled(Convert.ToInt32(Properties.Settings.Default.UseCookies));
                    br.WebView.preferences().setPrivateBrowsingEnabled(0);
                }
                else
                {
                    br.WebView.setCookieEnabled(0);
                    br.WebView.preferences().setPrivateBrowsingEnabled(1);
                }}));
                    (br.Parent as GTTabPage).Icon = Properties.Resources.New_document;
                    (br.Parent as GTTabPage).Text = "Connecting...";
                if (br == Browser)
                {
                    toolStripButton7.Image = Properties.Resources.ajax_loader;
                    AnimateImage(toolStripButton7.Image);
                }
                br.Parent.Invoke(new MethodInvoker(delegate
                {
                    if (string.IsNullOrEmpty(Properties.Settings.Default.cssstyle))
                        br.CSSManager.SetPageDefaultStyleSheet();
                    else
                        br.CSSManager.SetPageStyleSheetFromLocalFile(Properties.Settings.Default.cssstyle);
                }));
                f(br).Clear();
            }
            catch { }
        }

        void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                WebKitBrowser br = ((WebKitBrowser)sender);
                if (((GTTabPage)br.Parent).PrivateBrowsing == false)
                {
                    History.Add(br.DocumentTitle.Replace(
                        "|", ";"), br.Url.ToString().Replace("|", ";"), DateTime.Now, false);
                    HistoryCollection._col.Add(br.Url.ToString().Replace(" ", "%20"));
                }
                else
                {
                    ((GTTabPage)(br.Parent)).Icon = Properties.Resources.PrivateBrowsing;
                    toolStripButton7.Image = Properties.Resources.PrivateBrowsing.ToBitmap();
                    ((GTTabPage)(br.Parent)).Text = br.DocumentTitle + " - Private Browsing";
                }
                panelEx2.Visible = false;
                buttonX2.Image = Properties.Resources.globe;
            }
            catch { }
            try
            {
                AutoFillManager.Elements[(sender as WebKitBrowser).Url.ToString()].Clear();
            }
            catch { }
            if (Properties.Settings.Default.AutoFill && !Properties.Settings.Default.FormAutofillExceptions.Contains(e.Url.ToString()) && !Properties.Settings.Default.FormAutofillExceptions.Contains(e.Url.Host))
                AutoFillManager.FillForm((WebKitBrowser)sender);
        }
        public Dictionary<WebKitBrowser, _gtlitetype_p> Prompt = new Dictionary<WebKitBrowser, _gtlitetype_p>();
        public WebKitBrowser Browser
        { get { return (((GTTabPage)qTabControl1.ActiveTabPage).Controls[0] as WebKitBrowser); } }
        void MainLoad(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = Properties.Settings.Default.Theme;
            qTabControl1.ControlAdded += new ControlEventHandler(qTabControl1_ControlAdded);
            qTabControl1.ControlRemoved += new ControlEventHandler(qTabControl1_ControlAdded);
            
            if (IsCompatableOS() && IsAero())
            {
                mainbar.Renderer = new NcRenderer.NcRenderer();
                itemPanel1.BackgroundImage = GTLite.Properties.Resources.Form;
            }
            else
            {
                mainbar.RenderMode = ToolStripRenderMode.System;
                mainbar.Dock = DockStyle.Top;
                try
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(Properties.Settings.Default.ImagePath);
                    BackgroundImage = Image.FromStream(reader.BaseStream);
                    reader.Close();
                }
                catch { BackgroundImage = Properties.Resources.wave_background; }
            }
            _col.Clear();
            for (int i = 0; i <= Bookmarking.GetItemsCount() - 1; i++)
            {
                ToolStripButton bk = new ToolStripButton();
                if (Bookmarking.Name(i).Length > 20 == true)
                {
                    bk.Text = Bookmarking.Name(i).Substring(0, 17) + "...";
                }
                else { bk.Text = Bookmarking.Name(i); }
                bk.ToolTipText = Bookmarking.Url(i);
                bk.AutoToolTip = false;
                bk.ForeColor = Color.Black;
                bk.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                try
                {
                    bk.Image = Bookmarking.Favicon(i);
                }
                catch
                {
                    try
                    {
                        using (Image _temp = Functions.GetIcon(Bookmarking.Url(i)))
                        {
                            Uri _t = new Uri(Bookmarking.Url(i));
                            string path = Application.StartupPath + @"\Properties\" + _t.Host + ".png";
                            _temp.Save(path);
                            bk.Image = _temp;
                            _t = null;
                        }
                    }
                    catch
                    {
                        bk.Image = GTLite.Properties.Resources.New_document.ToBitmap();
                    }
                }
                bk.MouseUp += new MouseEventHandler(bk_MouseDown);
                _col.Add(bk);
            }
            this.contextMenuStrip2.Renderer = new GTLite.GTLiteToolStripRender();
            this.contextMenuStrip3.Renderer = new GTLite.GTLiteToolStripRender();
            this.contextMenuStrip1.Renderer = new GTLite.GTLiteToolStripRender();
            qTextBox1.AutoCompleteCustomSource = HistoryCollection._col;
            UpdateBookmarks();
            try
            {
                SplashScreen.SplashScreen.CloseForm();
            }
            catch { }
            if (!Program.HasUsedArguments)
            {
                try
                {
                    string tonav;
                    string path = (string)Environment.GetCommandLineArgs().GetValue(1);
                    if (!path.StartsWith("http") || !path.StartsWith("ftp"))
                    {
                        tonav = "file:///" + path.Replace(" ", "%20").Replace(@"\", "/");
                    }
                    else
                    {
                        tonav = path;
                    }
                    AddTab(tonav);
                }
                catch
                {
                    if (Properties.Settings.Default.Home.Count > 0)
                    {
                        foreach (string url in Properties.Settings.Default.Home)
                        {
                            if (url != "")
                                AddTab(url);
                        }
                    }
                    else { AddTab("http://www.gt-web-software.webs.com/"); Properties.Settings.Default.Home.Add("http://www.gt-web-software.webs.com/"); }
                }
            }
            else
            {
                if (Properties.Settings.Default.Home.Count > 0)
                {
                    foreach (string url in Properties.Settings.Default.Home)
                    {
                        AddTab(url);
                    }
                }
                else { AddTab("http://www.gt-web-software.webs.com/"); }
            }
            try
            {
                foreach (string l in System.IO.File.ReadAllLines(Application.StartupPath + @"\tovisit.list"))
                {
                    try
                    {
                        ToVisitLink t = new ToVisitLink((string)l.Split(Convert.ToChar(";")).GetValue(0), (string)l.Split(Convert.ToChar(";")).GetValue(1), Functions.GetIcon((string)l.Split(Convert.ToChar(";")).GetValue(1)));
                        ToVisit.Controls.Add(t);
                        t.Dock = DockStyle.Top;
                        t.VisitLinkButtonClicked += new ToVisitLink.VisitLink(tvl_VisitLinkButtonClicked);
                    }
                    catch { }
                }
            }
            catch { }
            if (ToVisit.Controls.Count > 1)
                timer5.Enabled = true;
            Program.HasUsedArguments = true;
            if (Properties.Settings.Default.ShowGettingStarted == true)
            {
                AddTab("http://www.wix.com/tsumalis96/gtlitenavigator");
                Properties.Settings.Default.ShowGettingStarted = false;
                Properties.Settings.Default.Save();
            }
            bool isn = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            if (Program.HasWarnedForInternetConnectionProblems == false && !isn)
            {
                Program.HasWarnedForInternetConnectionProblems = true;
                groupPanel3.Show();
            }
            this.Focus();
            ToolStripButton8Click(null, null);
        }
        void qTabControl1_ControlAdded(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            int AvailbaleSpace = qTabControl1.Width - qTabControl1.TabStripTopConfiguration.StripPadding.Left - qTabControl1.TabStripTopConfiguration.StripPadding.Right;
            if (AvailbaleSpace > qTabControl1.TabStripTop.TabButtons.Count * 225)
            {
                qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize = new Size(225, qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize.Height);
                qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MinimumSize = qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize;
            }
            else
            {
                int width = AvailbaleSpace / qTabControl1.TabStripTop.TabButtons.Count;
                if (AvailbaleSpace % (qTabControl1.TabStripTop.TabButtons.Count / 1) > 0)
                    width -= 1;
                qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize = new Size(width, qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize.Height);
                qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MinimumSize = qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize;
            }
            if (qTabControl1.TabStripTop.TabButtons.Count * qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize.Width + 40 <= this.Width)
            {
                cloudDesktopButton5.Left = qTabControl1.TabStripTop.TabButtons.Count * qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize.Width + qTabControl1.TabStripTop.TabButtons.Count * qTabControl1.TabStripTop.Configuration.ButtonSpacing + qTabControl1.TabStripTop.TabButtons.Count * 7;
            }
            else { cloudDesktopButton5.Left = this.Right - 70; }
        }
        void MainbarMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) MoveForm(this.Handle);
        }

        void ClosetolsripClick(object sender, EventArgs e)
        {
            this.Close();
        }

        void ToolStripButton8Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            { 
                this.WindowState = FormWindowState.Normal;
                Properties.Settings.Default.winst = (int)FormWindowState.Normal;
                mainbar.Padding = new System.Windows.Forms.Padding(0, 0, -1, 0); 
            }
            else 
            { 
                this.WindowState = FormWindowState.Maximized;
                Properties.Settings.Default.winst = (int)FormWindowState.Maximized;
                mainbar.Padding = new System.Windows.Forms.Padding(0, 7, -1, 0); 
            }
        }

        void ToolStripButton9Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Browser.GoBack();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Browser.GoForward();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                Browser.Navigate(Browser.Url.ToString());
            }
            catch { Browser.Navigate(qTextBox1.Text); }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Browser.Stop();
        }

        private void qTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (IsURL(qTextBox1.Text) == true)
            { buttonX2.Image = GTLite.Properties.Resources.globe; }
            else { buttonX2.Image = GTLite.Properties.Resources.find; }
        }
        private void Search(string term)
        {
            Browser.Navigate("http://www.google.com/search?q=" + term);
        }

        private void cloudDesktopButton5_Click(object sender, EventArgs e)
        {
            AddTab(Properties.Settings.Default.NewTabPage);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Browser.IsBusy == true)
                {
                    toolStripButton3.Enabled = false;
                    toolStripButton4.Enabled = true;

                }
                else
                {
                    toolStripButton4.Enabled = false;
                    toolStripButton3.Enabled = true;
                }
                toolStripButton2.Enabled = Browser.CanGoForward;
                toolStripButton1.Enabled = Browser.CanGoBack;
            }
            catch { }

        }

        private bool IsURL(string text)
        {
            if (text.StartsWith("about:") && text.Contains(" ") == false)
                return true;
            return (Uri.IsWellFormedUriString(text, UriKind.RelativeOrAbsolute) && text.Contains("."));           
        }
        public void AnimateImage(Image img)
        {
            if (!ImageAnimator.CanAnimate(img))
            {
                return;
            }
            ImageAnimator.Animate(img, new EventHandler(delegate { buttonX2.Invalidate(); }));
        }
        void ShowPageSecurity(Image im, string message)
        {
            pictureBox1.Image = im;
            label1.Text = message;
            groupPanel1.Visible = true;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (HighSecurity == true) { ShowPageSecurity(GTLite.Properties.Resources._1291449967_security_high, "The security of this page" + "\r\n" + "is verified by: " + "\r\n" + Browser.Url.Host); }
            else
            {
                ShowPageSecurity(GTLite.Properties.Resources._1291732552_lock, "The security of this page" + "\r\n"
                    + "is not verified");
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            groupPanel1.Visible = false;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            groupPanel2.Visible = false;
        }

        public void UpdateBookmarks()
        {
            _col.Clear();
            for (int i = 0; i <= Bookmarking.GetItemsCount() - 1; i++)
            {
                ToolStripButton bk = new ToolStripButton();
                if (Bookmarking.Name(i).Length > 20 == true)
                {
                    bk.Text = Bookmarking.Name(i).Substring(0, 17) + "...";
                }
                else { bk.Text = Bookmarking.Name(i); }
                bk.ToolTipText = Bookmarking.Url(i);
                bk.AutoToolTip = false;
                bk.ForeColor = Color.Black;
                bk.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                
                try
                {
                    bk.Image = Bookmarking.Favicon(i);
                }
                catch
                {
                    try
                    {
                        using (Image _temp = Functions.GetIcon(Bookmarking.Url(i)))
                        {
                            Uri _t = new Uri(Bookmarking.Url(i));
                            string path = Application.StartupPath + @"\Properties\" + _t.Host + ".png";
                            _temp.Save(path);
                            bk.Image = _temp;
                            _t = null;
                        }
                    }
                    catch
                    {
                        bk.Image = GTLite.Properties.Resources.New_document.ToBitmap();
                    }
                }
                bk.MouseUp += new MouseEventHandler(bk_MouseDown);
                _col.Add(bk);
            }
            itemPanel1.Items.Clear();
            itemPanel1.Items.AddRange(_col);
        }

        internal void bk_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Browser.Navigate(((ToolStripButton)sender).ToolTipText);
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                AddTab(((ToolStripButton)sender).ToolTipText);
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                curu = (sender as ToolStripButton).Text + "|" + (sender as ToolStripButton).ToolTipText;
                Point screenOrigin = this.PointToScreen(new Point(0, 0));
                Point currentpoint = new System.Drawing.Point(Cursor.Position.X - screenOrigin.X, Cursor.Position.Y - screenOrigin.Y);
                contextMenuStrip4.Show(currentpoint);
            }
        }
        string curu;
        private void buttonX5_Click(object sender, EventArgs e)
        {
            Bookmarking.Add(qTextBox2.Text, qTextBox3.Text, true);
            groupPanel2.Visible = false;
            int i = Bookmarking.GetItemsCount() - 1;
            ToolStripButton bk = new ToolStripButton();
            if (Bookmarking.Name(i).Length > 20 == true)
            {
                bk.Text = Bookmarking.Name(i).Substring(0, 17) + "...";
            }
            else { bk.Text = Bookmarking.Name(i); }
            bk.AutoToolTip = false;
            bk.ToolTipText = Bookmarking.Url(i);
            bk.ForeColor = Color.Black;
            bk.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            bk.Image = Functions.GetIcon(bk.ToolTipText);
            bk.MouseUp += new MouseEventHandler(bk_MouseDown);
            _col.Add(bk);
            foreach (Form f in Application.OpenForms)
            {
                try
                {
                    ((Main)f).itemPanel1.Items.Add(bk);
                }
                catch { }
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            groupPanel2.Visible = true;
            qTextBox2.Text = Browser.DocumentTitle;
            qTextBox3.Text = Browser.Url.ToString();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Browser.ShowPrintDialog();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Browser.ShowPrintPreviewDialog();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupPanel2.Visible = true;
            qTextBox2.Text = Browser.DocumentTitle;
            qTextBox3.Text = Browser.Url.ToString();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main frm = new Main();
            frm.Show();
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab(Properties.Settings.Default.NewTabPage);
        }

        private void libraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Library lb = new Library();
            lb.Show();
        }


        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Filter = "HTML Files(*.htm;*.html)|*.html;*.htm|All Files|*.*";
            DialogResult res = opn.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                qTextBox1.Text = "file:///" + opn.FileName.Replace(" ", "%20").Replace(@"\", "/");
                Browser.Navigate("file:///" + opn.FileName.Replace(" ", "%20").Replace(@"\", "/"));
            }
            opn.Dispose();
        }

        private void saveDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sv = new SaveFileDialog())
            {
                sv.Filter = "HTML Document(*.html)|*.html;*.htm|All Files|*.*";

                if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sv.FileName, Browser.DocumentText);
                }
            }
        }


        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            privateBrowsingForThisTabToolStripMenuItem.Checked = ((GTTabPage)(qTabControl1.ActiveTabPage)).PrivateBrowsing;
        }

        private void privateBrowsingForThisTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((GTTabPage)qTabControl1.ActiveTabPage).PrivateBrowsing = privateBrowsingForThisTabToolStripMenuItem.Checked;
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(qTextBox4.Text))
                Browser.WebView.searchFor(qTextBox4.Text, 1, 0, 1);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelEx1.Show();
            qTextBox4.Focus();
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(qTextBox4.Text))
                Browser.WebView.searchFor(qTextBox4.Text, 0, 0, 1);
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            panelEx1.Hide();
        }


        private void newIncognitoTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab("about:blank");
            (Browser.Parent as GTTabPage).PrivateBrowsing = true;
        }

        private void newIncognitoWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main frm = new Main();
            frm.HandleCreated += new EventHandler(frm_HandleCreated);
            frm.Show();
        }

        static void frm_HandleCreated(object sender, EventArgs e)
        {
            ((Main)sender).timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (Control tb in qTabControl1.Controls)
            {
                try { ((GTTabPage)tb).PrivateBrowsing = true; }
                catch { }
            }
        }


        private void buttonX7_Click_1(object sender, EventArgs e)
        {
            Browser.WebView.unmarkAllTextMatches();
            Browser.WebView.searchFor(qTextBox4.Text, 1, 0, 1);
            uint total;
            Browser.WebView.markAllMatchesForText(qTextBox4.Text, 0, 1, Convert.ToUInt32(500), out total);
        }

        private void buttonX8_Click_1(object sender, EventArgs e)
        {
            Browser.WebView.unmarkAllTextMatches();
            Browser.WebView.searchFor(qTextBox4.Text, 0, 0, 0);
            uint total;
            Browser.WebView.markAllMatchesForText(qTextBox4.Text, 0, 1, Convert.ToUInt32(500), out total);
        }

        private void buttonX3_Click_1(object sender, EventArgs e)
        {
            try
            {
                groupPanel2.Show();
                qTextBox2.Text = Browser.DocumentTitle;
                qTextBox3.Text = Browser.Url.ToString();
            }
            catch { }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab("http://www.google.com");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            qTabControl1.ActiveTabPage.Close();
        }

        private void addBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab("http://www.google.com", true);
        }

        private void closeOtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GTTabPage tb = ((GTTabPage)qTabControl1.ActiveTabPage);
            foreach (Control ta in qTabControl1.Controls)
            {
                try
                {
                    GTTabPage t = ((GTTabPage)ta);
                    if (!tb.Equals(t))
                    {
                        t.Close();
                    }
                }
                catch { }
            }
        }

        private void bookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebKitBrowser wb = ((GTTabPage)qTabControl1.ActiveTabPage).Browser;
            Bookmarking.Add(wb.DocumentTitle, wb.Url.ToString());
            int i = Bookmarking.GetItemsCount() - 1;
            ToolStripButton bk = new ToolStripButton();
            if (Bookmarking.Name(i).Length > 20 == true)
            {
                bk.Text = Bookmarking.Name(i).Substring(0, 17) + "...";
            }
            else { bk.Text = Bookmarking.Name(i); }
            bk.ToolTipText = Bookmarking.Url(i);
            bk.ForeColor = Color.Black;
            bk.AutoToolTip = false;
            bk.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            bk.Image = Functions.GetIcon(bk.ToolTipText);
            bk.MouseUp += new MouseEventHandler(bk_MouseDown);
            _col.Add(bk);
            foreach (Form f in Application.OpenForms)
            {
                try
                {
                    ((Main)f).itemPanel1.Items.Add(bk);
                }
                catch { }
            }
        }

        private void bookmarkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control tb in qTabControl1.Controls)
            {
                try
                {
                    GTTabPage t = ((GTTabPage)tb);
                    WebKitBrowser wb = t.Browser;
                    Bookmarking.Add(wb.DocumentTitle, wb.Url.ToString());
                    int i = Bookmarking.GetItemsCount() - 1;
                    ToolStripButton bk = new ToolStripButton();
                    if (Bookmarking.Name(i).Length > 20 == true)
                    {
                        bk.Text = Bookmarking.Name(i).Substring(0, 17) + "...";
                    }
                    else { bk.Text = Bookmarking.Name(i); }
                    bk.ToolTipText = Bookmarking.Url(i);
                    bk.AutoToolTip = false;
                    bk.ForeColor = Color.Black;
                    bk.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    bk.Image = Functions.GetIcon(bk.ToolTipText);
                    bk.MouseUp += new MouseEventHandler(bk_MouseDown);
                    _col.Add(bk);
                    foreach (Form f in Application.OpenForms)
                    {
                        try
                        {
                            ((Main)f).itemPanel1.Items.Add(bk);
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }

        private void privateBrowsingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonX9_Click_1(object sender, EventArgs e)
        {
            panelEx1.Hide();
            Browser.WebView.unmarkAllTextMatches();
        }

        private void buttonX5_Click_1(object sender, EventArgs e)
        {
            Bookmarking.Add(qTextBox2.Text, qTextBox3.Text);
            int i = Bookmarking.GetItemsCount() - 1;
            ToolStripButton bk = new ToolStripButton();
            bk.AutoToolTip = false;
            if (Bookmarking.Name(i).Length > 20 == true)
            {
                bk.Text = Bookmarking.Name(i).Substring(0, 17) + "...";
            }
            else { bk.Text = Bookmarking.Name(i); }
            bk.ToolTipText = Bookmarking.Url(i);
            bk.ForeColor = Color.Black;
            bk.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            bk.Image = Functions.GetIcon(bk.ToolTipText);
            bk.MouseUp += new MouseEventHandler(bk_MouseDown);
            _col.Add(bk);
            foreach (Form f in Application.OpenForms)
            {
                try
                {
                    ((Main)f).itemPanel1.Items.Add(bk);
                }
                catch { }
            }
            buttonX5.Parent.Visible = false;
        }

        private void buttonX6_Click_1(object sender, EventArgs e)
        {
            groupPanel2.Hide();
        }

        private void buttonX4_Click_1(object sender, EventArgs e)
        {
            groupPanel1.Hide();
        }

        private void qTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            panelEx2.Visible = Browser.IsBusy;
            WebKitBrowser br = Browser;
            if (br.Url.ToString().StartsWith("https://") == true)
            {
                buttonX1.Image = GTLite.Properties.Resources._1291449967_security_high;
                HighSecurity = true;
            }
            else
            {
                buttonX1.Image = GTLite.Properties.Resources._1291732552_lock;
                HighSecurity = false;
            }
        }

        private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
        {
            privateBrowsingToolStripMenuItem.Checked = ((GTTabPage)qTabControl1.ActiveTabPage).PrivateBrowsing;

        }

        private void contextMenuStrip2_Opening_1(object sender, CancelEventArgs e)
        {
            privateBrowsingForThisTabToolStripMenuItem.Checked = ((GTTabPage)qTabControl1.ActiveTabPage).PrivateBrowsing;
        }



        private void integerInput1_ValueChanged(object sender, EventArgs e)
        {
            Browser.SetPageZoom(integerInput1.Value);
        }

        private void integerInput2_ValueChanged(object sender, EventArgs e)
        {
            Browser.SetTextZoom(integerInput2.Value);
        }

        private void zoomPaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomPane.Show();
        }

        private void cloudDesktopButton1_Click(object sender, EventArgs e)
        {
            ZoomPane.Hide();
        }

        private void printToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Browser.ShowPrintDialog();
        }

        private void printPreviewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Browser.ShowPrintPreviewDialog();
            }
            catch { MessageBoxEx.Show("No printer could be found. Please check your printer settings and try again."); }
        }

        private void pageSetupToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Browser.ShowPageSetupDialog();
            }
            catch { MessageBoxEx.Show("No printer could be found. Please check your printer settings and try again."); }

        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Browser.IsBusy == false)
            {
                PageInfo pg = new PageInfo(Browser);
                if (pg != null || pg.IsDisposed == false)
                    pg.Show();
            }
            else { MessageBoxEx.Show("Please wait until the page finishes loading and then try getting information about this page"); }
        }

        private void addToFavoritesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            groupPanel2.Show();
            qTextBox2.Text = Browser.DocumentTitle;
            qTextBox3.Text = Browser.Url.ToString();
            contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void reportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (HighSecurity == true) { ShowPageSecurity(GTLite.Properties.Resources._1291449967_security_high, "The security of this page" + "\r\n" + "is verified by: " + "\r\n" + Browser.Url.Host); }
            else
            {
                ShowPageSecurity(GTLite.Properties.Resources._1291732552_lock, "The security of this page" + "\r\n"
                    + "is not verified");
            }
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {

        }


        private void qTabControl1_ActivePageChanged(object sender, QTabPageChangeEventArgs e)
        {
            try
            {
                if (Browser != null)
                {
                    if (Browser.Url == null)
                        qTextBox1.Text = UrlValues[Browser]._v;
                    else
                        qTextBox1.Text = Browser.Url.ToString();
                    if (Browser.Url.ToString().StartsWith("https://") == true)
                    {
                        buttonX1.Image = GTLite.Properties.Resources._1291449967_security_high;
                        HighSecurity = true;
                    }
                    else
                    {
                        buttonX1.Image = GTLite.Properties.Resources._1291732552_lock;
                        HighSecurity = false;
                    }
                    buttonX20.Visible = IsRSS(Browser);
                    prefAllowCookies.Value = Browser.AllowCookies;
                    prefImages.Value = Browser.Preferences.LoadImages;
                    try
                    {
                        prefJavaScript.Value = Browser.UseJavaScript;
                    }
                    catch { }
                    try
                    {
                        toolStripButton10.Checked = ur[Browser]._v;
                    }
                    catch
                    {
                        toolStripButton10.Checked = false;
                    }
                    prefPlugins.Value = Browser.Preferences.AllowPlugins;
                    integerInput1.Value = (int)Browser.WebView.pageSizeMultiplier();
                    integerInput2.Value = (int)Browser.WebView.textSizeMultiplier();
                    if (Browser.IsBusy)
                    {
                        toolStripButton7.Image = Properties.Resources.ajax_loader;
                        AnimateImage(toolStripButton7.Image);
                    }
                    else
                        toolStripButton7.Image = Functions.GetIcon(Browser.Url.ToString());
                }
            }
            catch { }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab("about:blank");
            string path = Application.StartupPath + @"\Help\about.htm";
            string tonav = "file:///" + path.Replace(" ", "%20").Replace(@"\", "/");
            Browser.Navigate(tonav);
        }

        private void qTextBox4_TextChanged(object sender, EventArgs e)
        {
            Browser.WebView.unmarkAllTextMatches();
            if (!String.IsNullOrEmpty(qTextBox4.Text))
            {
                Browser.WebView.searchFor(qTextBox4.Text, 1, 0, 1);
                uint total;
                Browser.WebView.markAllMatchesForText(qTextBox4.Text, 0, 1, Convert.ToUInt32(500), out total);
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            try
            {
                int AvailbaleSpace = qTabControl1.Width - qTabControl1.TabStripTopConfiguration.StripPadding.Left - qTabControl1.TabStripTopConfiguration.StripPadding.Right;
                if (AvailbaleSpace > qTabControl1.TabStripTop.TabButtons.Count * 225)
                {
                    qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize = new Size(225, qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize.Height);
                    qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MinimumSize = qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize;
                }
                else
                {
                    int width = AvailbaleSpace / qTabControl1.TabStripTop.TabButtons.Count;
                    if (AvailbaleSpace % (qTabControl1.TabStripTop.TabButtons.Count / 1) > 0)
                        width -= 1;
                    qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize = new Size(width, qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize.Height);
                    qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MinimumSize = qTabControl1.TabStripTopConfiguration.ButtonConfiguration.MaximumSize;
                }
            }
            catch { }
        }

        private void mainbar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            string towrite = "";
            foreach (Control c in ToVisit.Controls)
            {
                if (c is ToVisitLink)
                {
                    towrite = towrite + (c as ToVisitLink).labelX1.Text + ";" + (c as ToVisitLink).labelX2.Text + "\r\n";

                }
            }
            System.IO.File.WriteAllText(Application.StartupPath + @"\tovisit.list", towrite);
            if (Application.OpenForms.Count < 2)
            {
                Application.OpenForms[0].Close();
            }
        }

        private void privateBrowsingToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            ((GTTabPage)qTabControl1.ActiveTabPage).PrivateBrowsing = privateBrowsingToolStripMenuItem.Checked;

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (this.Size.Height < this.MinimumSize.Height)
            {
                this.Size = new Size(500, 400);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void addIncognitoTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab("about:blank");
            (qTabControl1.ActiveTabPage as GTTabPage).PrivateBrowsing = true;
        }
        private void duplicateCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                WebKitBrowser current = Browser;
                AddTab(Browser.Url.ToString());
                Browser.WebView.loadBackForwardListFromOtherView(current.WebView);
            }
            catch { AddTab("about:blank"); }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                qTextBox1.Top = 12;
            else
                qTextBox1.Top = 4;
            buttonX1.Top = qTextBox1.Top;
            buttonX2.Top = qTextBox1.Top;
            buttonX3.Top = qTextBox1.Top;
            buttonX20.Top = qTextBox1.Top;
            qTextBox1.Width = buttonX3.Left - buttonX2.Left;
            mainbar.Padding = new System.Windows.Forms.Padding(0, 7, -1, 0); 
        }

        private void timer4_Tick_1(object sender, EventArgs e)
        {
            if (qTabControl1.TabStripTop.TabButtons.Count <= 0)
            {
                this.Close();
            }
        }

        private void launchGTLiteDeveloperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Developer dv = new Developer();
            dv.fastColoredTextBox1.Text = Browser.DocumentText;
            dv.Show();
        }

        private void viewSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SourceViewer src = new SourceViewer(Browser.DocumentText);
            src.Show();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            s.Show();
        }

        private void openLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Library lb = new Library();
            lb.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Browser.Navigate(Properties.Settings.Default.Home[0]);
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            savePageToolStripMenuItem.Enabled = (r(Browser).Count > 1);
        }

        private void launchWebInspectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browser.WebView.inspector().show();
            Browser.WebView.inspector().attach();
        }

        private void downloadsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void prefPlugins_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Browser.Preferences.AllowPlugins = prefPlugins.Value;
            }
            catch { }
        }

        private void prefImages_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Browser.Preferences.LoadImages = prefImages.Value;
            }
            catch { }
        }

        private void prefJavaScript_ValueChanged(object sender, EventArgs e)
        {
            try { Browser.UseJavaScript = prefJavaScript.Value; }
            catch { }
        }

        private void buttonX10_Click_1(object sender, EventArgs e)
        {
            Browser.ShowSaveAsDialog();
        }

        private void prefAllowCookies_ValueChanged(object sender, EventArgs e)
        {
            Browser.AllowCookies = prefAllowCookies.Value;
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            infoToolStripMenuItem_Click(this, new EventArgs());
        }

        private void buttonX12_Click(object sender, EventArgs e)
        {
            viewSourceToolStripMenuItem_Click(this, new EventArgs());
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            panelEx3.Visible = false;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            panelEx3.Visible = true;
        }

        private void panelEx3_VisibleChanged(object sender, EventArgs e)
        {
            if (Browser != null)
            {
                prefAllowCookies.Value = Browser.AllowCookies;
                prefImages.Value = Browser.Preferences.LoadImages;
                prefJavaScript.Value = Browser.UseJavaScript;
                prefPlugins.Value = Browser.Preferences.AllowPlugins;
            }
        }

        private void warningBox1_OptionsClick(object sender, EventArgs e)
        {
            warningBox1.Hide();
            tableLayoutPanel1.Visible = false;
            Properties.Settings.Default.BlockedSites.Add((string)warningBox1.Tag);
            MessageBoxEx.Show("The site has succesfully been added to the blocked sites list.");
        }

        private void openTranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Browser.Url != null)
            {
                Translator tl = new Translator(Browser.Url.ToString());
                tl.Show();
            }
        }

        private void warningBox1_CloseClick(object sender, EventArgs e)
        {
            warningBox1.Visible = false;
            tableLayoutPanel1.Visible = false;
        }

        private void Main_Validated(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            ToVisit.Visible = true;
            if (ToVisit.Right >= ToVisit.Width)
            {
                timer5.Enabled = false;
                return;
            }
            ToVisit.Left = ToVisit.Left + 20;
        }

        private void openToVisitLinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer5.Enabled = true;
            timer5.Interval = 20;
        }

        private void buttonX16_Click(object sender, EventArgs e)
        {
            ToVisit.Left = this.Left - ToVisit.Width;
            ToVisit.Visible = false;
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            if (Browser.Url != null)
            {
                ToVisitLink tvl = new ToVisitLink(Browser.DocumentTitle, Browser.Url.ToString(), Functions.GetIcon(Browser.Url.ToString()));
                ToVisit.Controls.Add(tvl);
                tvl.Dock = DockStyle.Top;
                tvl.VisitLinkButtonClicked += new ToVisitLink.VisitLink(tvl_VisitLinkButtonClicked);
            }
            else
            {
                MessageBoxEx.Show("This site could not be added to the ToVisit links");
            }
        }

        void tvl_VisitLinkButtonClicked(object sender, ToVisitLinkEventArgs e)
        {
            Browser.Navigate(e.Url);
            if (switchButton1.Value == true)
                (sender as ToVisitLink).Dispose();
        }

        private void reportAnIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab("https://docs.google.com/spreadsheet/viewform?pli=1&formkey=dGtBXzZpcWVleXZSU216c1VBNFJMSWc6MQ#gid=0");
        }
        private void forumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab("http://gt-web-software.webs.com/apps/forums/show/6217883-gtlite-navigator");
        }
        private void ContactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab("http://gt-web-software.webs.com/contactus.htm");
        }

        private void downloadsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Browser.ShowDownloader();
        }

        bool IsLeft = true;

        private void toolStrip1_MouseEnter(object sender, EventArgs e)
        {
            if (IsLeft)
            {
                panel1.Left = this.Width - panel1.Width;
                panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                IsLeft = false;
            }
            else
            {
                panel1.Left = 1;
                panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                IsLeft = true;
            }
        }

        private void toolStrip1_MouseLeave(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Browser.GoBack();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Browser.GoForward();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            if (!Browser.IsBusy)
                Browser.Reload();
            else
                Browser.Stop();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            Browser.ShowSaveAsDialog();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            Browser.ShowPrintDialog();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            openTranslatorToolStripMenuItem_Click(this, new EventArgs());
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            viewSourceToolStripMenuItem_Click(this, new EventArgs());
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            launchGTLiteDeveloperToolStripMenuItem_Click(this, new EventArgs());
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            infoToolStripMenuItem_Click(this, new EventArgs());
        }

        private void resourceViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resources s = new Resources(r(Browser));
            s.Show();
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browser.WebView.inspector().show();
            Browser.WebView.inspector().showConsole();
        }

        private void inspectorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Browser.ShowInspector();
        }

        private void savePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Page p = new Page(this);
        }

        private void savedPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavedPages svd = new SavedPages();
            svd.Show();
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            SavedPages sv = new SavedPages();
            sv.Show();
            groupPanel3.Dispose();
        }

        private void buttonX17_Click(object sender, EventArgs e)
        {
            openFileToolStripMenuItem_Click(null, null);
            groupPanel3.Dispose();
        }

        private void buttonX18_Click(object sender, EventArgs e)
        {
            groupPanel3.Dispose();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GTLite.Update updater = new Update();
            updater.Show();
        }

        private void launchReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RSS_Reader.Reader rd = new RSS_Reader.Reader();
            rd.Show();
        }

        private void buttonX19_Click(object sender, EventArgs e)
        {
            groupPanel4.Visible = false;
        }

        private void buttonX21_Click(object sender, EventArgs e)
        {
            RSS_Reader.Reader read = new RSS_Reader.Reader((string)groupPanel4.Tag);
            read.Show();
            groupPanel4.Visible = false;
        }

        private void buttonX20_Click(object sender, EventArgs e)
        {
            buttonX20.Popup(buttonX20.PointToScreen(new Point(buttonX20.Location.X, buttonX20.Location.Y - 5)));
        }

        private void buttonX23_Click(object sender, EventArgs e)
        {
            ((sender as Control).Parent.Parent.Parent.Tag as AutoFillConfirm).Confirm();
            (sender as Control).Parent.Parent.Parent.Visible = false;
        }

        private void buttonX24_Click(object sender, EventArgs e)
        {
            (sender as Control).Parent.Parent.Parent.Visible = false;
        }

        private void buttonX22_Click(object sender, EventArgs e)
        {
            ((sender as Control).Parent.Parent.Parent.Tag as AutoFillConfirm).Never();
            (sender as Control).Parent.Parent.Parent.Visible = false;
        }

        private void buttonX25_Click(object sender, EventArgs e)
        {
            ((sender as Control).Parent.Parent.Parent.Tag as AutoFillConfirm).Never(true);
            (sender as Control).Parent.Parent.Parent.Visible = false;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            qTextBox1.BackColor = Color.Silver;
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            qTextBox1.BackColor = Color.Snow;
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            qTextBox1.BackColor = Color.WhiteSmoke;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            qTextBox1.BackColor = Color.LightSlateGray;
        }

        private void qTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (IsURL(qTextBox1.Text))
                {
                    Browser.Navigate(qTextBox1.Text);
                }
                else
                {
                    Search(qTextBox1.Text.Replace(" ", "+"));
                }
            }
        }
        Dictionary<WebKitBrowser, _gtlitetype_p> ur = new Dictionary<WebKitBrowser, _gtlitetype_p>();
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (toolStripButton10.Checked == false)
            {
                toolStripButton10.Checked = true;
                if (ur.ContainsKey(Browser))
                    ur[Browser]._v = true;
                else
                    ur.Add(Browser, new _gtlitetype_p(true));
                Browser.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/18.6.872.0 Safari/535.2 UNTRUSTED/1.0 3gpp-gba UNTRUSTED/1.0";
            }
            else
            {
                toolStripButton10.Checked = false;
                if (ur.ContainsKey(Browser))
                    ur[Browser]._v = false;
                else
                    ur.Add(Browser, new _gtlitetype_p(false)); 
                Browser.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) GTLite Navigator";
            
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Browser != null)
                Browser.Navigate(curu.Split(Convert.ToChar("|"))[1]);
            else
                AddTab(curu.Split(Convert.ToChar("|"))[1]);
        }

        private void openInNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab(curu.Split(Convert.ToChar("|"))[1]);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bookmarking.Delete(Bookmarking.AllBookItems.IndexOf(curu));
            UpdateBookmarks();
        }

        private void openLibraryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Library lb = new Library();
            lb.Show();
        }
        }
        public class GTTabPage : QTabPage
        {
            bool _privateb = false;
            public WebKitBrowser Browser
            {
                get { return ((WebKitBrowser)this.Controls[0]); }
                set { this.Controls.Add(value); }
            }
            public bool PrivateBrowsing
            {
                get { return _privateb; }
                set { _privateb = value; }
            }
        }
        public class InputContextMenu : ContextMenu
        {
            private WebKitBrowser browser;
            public InputContextMenu(WebKitBrowser br)
            {
                this.MenuItems.Add("Cut").Click += new EventHandler(InputContextMenu_Click);
                this.MenuItems.Add("Copy").Click += new EventHandler(CopyContextMenu_Click);
                this.MenuItems.Add("Paste").Click += new EventHandler(PasteContextMenu_Click);
                this.MenuItems.Add("Delete").Click += new EventHandler(DeleteContextMenu_Click);
                this.browser = br;
                this.Popup += new EventHandler(InputContextMenu_Popup);
            }

            void InputContextMenu_Popup(object sender, EventArgs e)
            {
                MenuItems[0].Enabled = (browser.SelectedText != null);
                MenuItems[1].Enabled = (browser.SelectedText != null);
                MenuItems[2].Enabled = Clipboard.ContainsText();
                MenuItems[3].Enabled = (browser.SelectedText != null);
            }
            void DeleteContextMenu_Click(object sender, EventArgs e)
            {
                browser.WebView.deleteSelection();
            }
            void PasteContextMenu_Click(object sender, EventArgs e)
            {
                browser.WebView.paste(browser.GetWebViewAsObject());
            }
            void CopyContextMenu_Click(object sender, EventArgs e)
            {
                browser.WebView.copy(browser.GetWebViewAsObject());
            }
            void InputContextMenu_Click(object sender, EventArgs e)
            {
                browser.WebView.cut(browser.GetWebViewAsObject());
            }
        }
        public class _gtlitetype_s
        {
            public _gtlitetype_s(string v)
            {
                _v = v;
            }
            public string _v { get; set; }
        }
        public class _gtlitetype_p
        {
            public _gtlitetype_p(bool v)
            {
                _v = v;
            }
            public bool _v { get; set; }
        }
        public class AutoFillConfirm
        {
            private WebKitBrowser _o;
            private string u;
            public AutoFillConfirm(WebKitBrowser brow, string ur)
            {
                u = ur;
                _o = brow;
            }
            public void Confirm()
            {
                AutoFillManager.SaveDataFromForm(_o, u);
            }
            public void Never(bool host = false)
            {
                if (!host)
                    Properties.Settings.Default.FormAutofillExceptions.Add(_o.Url.ToString());
                else
                    Properties.Settings.Default.FormAutofillExceptions.Add(_o.Url.Host);
                Properties.Settings.Default.Save();
            }
        }
}

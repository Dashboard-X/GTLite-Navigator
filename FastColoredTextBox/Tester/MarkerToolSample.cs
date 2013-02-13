using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Drawing.Drawing2D;

namespace Tester
{
    public partial class MarkerToolSample : Form
    {
        //Shortcut style
        ShortcutStyle shortCutStyle = new ShortcutStyle(Pens.Maroon);
        //Marker styles
        MarkerStyle YellowStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(180, Color.Yellow)));
        MarkerStyle RedStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(180, Color.Red)));
        MarkerStyle GreenStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(180, Color.Green)));

        public MarkerToolSample()
        {
            InitializeComponent();
            //append text
            fastColoredTextBox1.Text = "This example shows how to create Marker Tool and usage of ShortcutStyle class.\nAlso VisualMarkerClick event handling is present.\nAlso it shows how to set priority of styles.\n\nSelect any text, please.";
            //add style explicitly to control for define priority of style drawing
            fastColoredTextBox1.AddStyle(YellowStyle);//render first
            fastColoredTextBox1.AddStyle(RedStyle);//red will be rendering over yellow
            fastColoredTextBox1.AddStyle(GreenStyle);//green will be rendering over yellow and red
            fastColoredTextBox1.AddStyle(shortCutStyle);//render last, over all other styles
        }

        private void fastColoredTextBox1_SelectionChangedDelayed(object sender, EventArgs e)
        {
            //here we draw shortcut for selection area
            Range selection = fastColoredTextBox1.Selection;
            //clear previous shortcuts
            fastColoredTextBox1.VisibleRange.ClearStyle(shortCutStyle);
            //create shortcuts
            if (selection.Start != selection.End)//user selected one or more chars?
            {
                //find last char
                var r = selection.Clone();
                r.Normalize();
                r.Start = r.End;//go to last char
                r.GoLeft(true);//select last char
                //apply ShortCutStyle
                r.SetStyle(shortCutStyle);
            }
        }

        private void fastColoredTextBox1_VisualMarkerClick(object sender, VisualMarkerEventArgs e)
        {
            //is it our style ?
            if (e.Style == shortCutStyle)
            {
                //show popup menu
                cmMark.Show(fastColoredTextBox1.PointToScreen(e.Location));
            }
        }

        private void markAsYellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set background style
            switch((string)((sender as ToolStripMenuItem).Tag))
            {
                case "yellow": fastColoredTextBox1.Selection.SetStyle(YellowStyle); break;
                case "red": fastColoredTextBox1.Selection.SetStyle(RedStyle); break;
                case "green": fastColoredTextBox1.Selection.SetStyle(GreenStyle); break;
                case "lineBackground": fastColoredTextBox1[fastColoredTextBox1.Selection.Start.iLine].BackgroundBrush = Brushes.Pink; break;
            }
            //clear shortcut style
            fastColoredTextBox1.Selection.ClearStyle(shortCutStyle);
        }

        private void clearMarkedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Selection.ClearStyle(YellowStyle, RedStyle, GreenStyle);
            fastColoredTextBox1[fastColoredTextBox1.Selection.Start.iLine].BackgroundBrush = null;
        }

        private void fastColoredTextBox1_PaintLine(object sender, PaintLineEventArgs e)
        {
            //draw current line marker
            if (e.LineIndex == fastColoredTextBox1.Selection.Start.iLine)
                e.Graphics.FillEllipse(new LinearGradientBrush(new Rectangle(0, e.LineRect.Top, 15, 15), Color.LightPink, Color.Red, 45), 0, e.LineRect.Top, 15, 15);
        }
    }
}

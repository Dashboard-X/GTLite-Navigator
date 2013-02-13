using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

namespace Tester
{
    public partial class ImageDrawingSample : Form
    {
        ImageStyle style;
        static string RegexSpecSymbolsPattern = @"[\^\$\[\]\(\)\.\\\*\+\|\?\{\}]";

        public ImageDrawingSample()
        {
            style = new ImageStyle();
            style.ImagesByText.Add(":)", Properties.Resources.smile_16x16);
            style.ImagesByText.Add(":-)", Properties.Resources.smile_16x16);
            style.ImagesByText.Add(":(", Properties.Resources.sad_16x16);
            style.ImagesByText.Add(":-(", Properties.Resources.sad_16x16);

            InitializeComponent();

            fastColoredTextBox1.Text = "This example draws smile image instead text smile\r\nSad smile example :(\r\nHappy smile example :)";
        }

        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(StyleIndex.All);
            foreach (var key in style.ImagesByText.Keys)
            {
                string pattern = Regex.Replace(key, RegexSpecSymbolsPattern, "\\$0");
                e.ChangedRange.SetStyle(style, pattern);
            }
        }
    }

    /// <summary>
    /// This class is used as text renderer for smiles
    /// </summary>
    class ImageStyle : TextStyle
    {
        public Dictionary<string, Image> ImagesByText { get; private set; }

        public ImageStyle()
            : base(null, null, FontStyle.Regular)
        {
            ImagesByText = new Dictionary<string, Image>();
        }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            string text = range.Text;
            int iChar = range.Start.iChar;

            while (text != "")
            {
                bool replaced = false;
                foreach (var pair in ImagesByText)
                {
                    if (text.StartsWith(pair.Key))
                    {
                        float k = (float)range.tb.CharHeight / pair.Value.Height;
                        text = text.Substring(pair.Key.Length);
                        gr.DrawImage(pair.Value, position.X + range.tb.CharWidth * pair.Key.Length / 2 - pair.Value.Width * k/2, position.Y, pair.Value.Width * k, pair.Value.Height * k);
                        position.Offset(range.tb.CharWidth * pair.Key.Length, 0);
                        replaced = true;
                        iChar+=pair.Key.Length;
                        break;
                    }
                }
                if (!replaced && text.Length>0)
                {
                    Range r = new Range(range.tb, iChar, range.Start.iLine, iChar+1, range.Start.iLine);
                    base.Draw(gr, position, r);
                    position.Offset(range.tb.CharWidth, 0);
                    text = text.Substring(1);
                }
            }
        }
    }
}

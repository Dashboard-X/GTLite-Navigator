using System;
using System.Windows.Forms;
using System.Drawing;
using FastColoredTextBoxNS;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Tester
{
    public partial class PowerfulSample : Form
    {
        string lang = "CSharp";

        //styles
        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        public PowerfulSample()
        {
            InitializeComponent();
            InitStylesPriority();
            //            
            fastColoredTextBox1.Text = @"
    #region Char

    /// <summary>
    /// Char and style
    /// </summary>
    struct Char
    {
        public char c;
        public StyleIndex style;

        public Char(char c)
        {
            this.c = c;
            style = StyleIndex.None;
        }
    }
    #endregion";
            //move caret to start text
            fastColoredTextBox1.Selection.Start = Place.Empty;
            fastColoredTextBox1.DoCaretVisible();
            fastColoredTextBox1.IsChanged = false;
            fastColoredTextBox1.ClearUndo();
        }

        private void InitStylesPriority()
        {
            fastColoredTextBox1.ClearStylesBuffer();
            
            //add this style explicitly for drawing under other styles
            fastColoredTextBox1.AddStyle(SameWordsStyle);
        }

        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch(lang)
            {
                case "CSharp":
                    //For sample, we will highlight the syntax of C# manually, although could use built-in highlighter
                    CSharpSyntaxHighlight(e);//custom highlighting
                    break;
                default:
                    break;//for highlighting of other languages, we using built-in FastColoredTextBox highlighter
            }
        }

        private void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            fastColoredTextBox1.LeftBracket = '(';
            fastColoredTextBox1.RightBracket = ')';
            fastColoredTextBox1.LeftBracket2 = '\x0';
            fastColoredTextBox1.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);
            //string highlighting
            e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|[^@](?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //comment highlighting
            e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline|RegexOptions.RightToLeft);
            //number highlighting
            e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            //attribute highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            //class name highlighting
            e.ChangedRange.SetStyle(BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(BlueStyle, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.ShowFindDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.ShowReplaceDialog();
        }

        private void miLanguage_DropDownOpening(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem mi in miLanguage.DropDownItems)
                mi.Checked = mi.Text == lang;
        }

        private void miCSharp_Click(object sender, EventArgs e)
        {
            //set language
            lang = (sender as ToolStripMenuItem).Text;
            fastColoredTextBox1.ClearStylesBuffer();
            fastColoredTextBox1.Range.ClearStyle(StyleIndex.All);
            InitStylesPriority();
            fastColoredTextBox1.AutoIndentNeeded -= fastColoredTextBox1_AutoIndentNeeded;
            //
            switch (lang)
            {
                //For sample, we will highlight the syntax of C# manually, although could use built-in highlighter
                case "CSharp":
                    fastColoredTextBox1.Language = Language.Custom;
                    fastColoredTextBox1.CommentPrefix = "//";
                    fastColoredTextBox1.AutoIndentNeeded += fastColoredTextBox1_AutoIndentNeeded;
                    //call OnTextChanged for refresh syntax highlighting
                    fastColoredTextBox1.OnTextChanged();
                    break;
                case "VB": fastColoredTextBox1.Language = Language.VB; break;
                case "HTML": fastColoredTextBox1.Language = Language.HTML; break;
                case "SQL": fastColoredTextBox1.Language = Language.SQL; break;
                case "PHP": fastColoredTextBox1.Language = Language.PHP; break;
            }
            fastColoredTextBox1.OnSyntaxHighlight(new TextChangedEventArgs(fastColoredTextBox1.Range));
        }

        private void collapseSelectedBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.CollapseBlock(fastColoredTextBox1.Selection.Start.iLine, fastColoredTextBox1.Selection.End.iLine);
        }

        private void collapseAllregionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this example shows how to collapse all #region blocks (C#)
            if (lang != "CSharp") return;
            for (int iLine = 0; iLine < fastColoredTextBox1.LinesCount; iLine++)
            {
                if (fastColoredTextBox1[iLine].FoldingStartMarker == @"#region\b")//marker @"#region\b" was used in SetFoldingMarkers()
                    fastColoredTextBox1.CollapseFoldingBlock(iLine);
            }
        }

        private void exapndAllregionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this example shows how to expand all #region blocks (C#)
            if (lang != "CSharp") return;
            for (int iLine = 0; iLine < fastColoredTextBox1.LinesCount; iLine++)
            {
                if (fastColoredTextBox1[iLine].FoldingStartMarker == @"#region\b")//marker @"#region\b" was used in SetFoldingMarkers()
                    fastColoredTextBox1.ExpandFoldedBlock(iLine);
            }
        }

        private void increaseIndentSiftTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.IncreaseIndent();
        }

        private void decreaseIndentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.DecreaseIndent();
        }

        private void hTMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "HTML with <PRE> tag|*.html|HTML without <PRE> tag|*.html";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string html = "";

                if (sfd.FilterIndex == 1)
                {
                    html = fastColoredTextBox1.Html;
                }
                if (sfd.FilterIndex == 2)
                {
                    
                    ExportToHTML exporter = new ExportToHTML();
                    exporter.UseBr = true;
                    exporter.UseNbsp = false;
                    exporter.UseForwardNbsp = true;
                    exporter.UseStyleTag = true;
                    html = exporter.GetHtml(fastColoredTextBox1);
                }
                File.WriteAllText(sfd.FileName, html);
            }
        }

        private void fastColoredTextBox1_SelectionChangedDelayed(object sender, EventArgs e)
        {
            fastColoredTextBox1.VisibleRange.ClearStyle(SameWordsStyle);

            if (fastColoredTextBox1.Selection.Start != fastColoredTextBox1.Selection.End)
                return;//user selected diapason

            //get fragment around caret
            var fragment = fastColoredTextBox1.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            var ranges = fastColoredTextBox1.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();
            if(ranges.Length>1)
            foreach(var r in ranges)
                r.SetStyle(SameWordsStyle);
        }

        private void goForwardCtrlShiftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.NavigateForward();
        }

        private void goBackwardCtrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.NavigateBackward();
        }

        private void autoIndentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.DoAutoIndent();
        }

        const int maxBracketSearchIterations = 2000;

        void GoLeftBracket(FastColoredTextBox tb, char LeftBracket, char RightBracket)
        {
            Range range = tb.Selection.Clone();//need clone because we will move caret
            int counter = 0;
            int maxIterations = maxBracketSearchIterations;
            while (range.GoLeftThroughFolded())//move caret left
            {
                if (range.CharAfterStart == LeftBracket) counter++;
                if (range.CharAfterStart == RightBracket) counter--;
                if (counter == 1)
                {
                    //found
                    tb.Selection.Start = range.Start;
                    tb.DoSelectionVisible();
                    break;
                }
                //
                maxIterations--;
                if (maxIterations <= 0) break;
            }
            tb.Invalidate();
        }

        void GoRightBracket(FastColoredTextBox tb, char LeftBracket, char RightBracket)
        {
            var range = tb.Selection.Clone();//need clone because we will move caret
            int counter = 0;
            int maxIterations = maxBracketSearchIterations;
            do
            {
                if (range.CharAfterStart == LeftBracket) counter++;
                if (range.CharAfterStart == RightBracket) counter--;
                if (counter == -1)
                {
                    //found
                    tb.Selection.Start = range.Start;
                    tb.Selection.GoRightThroughFolded();
                    tb.DoSelectionVisible();
                    break;
                }
                //
                maxIterations--;
                if (maxIterations <= 0) break;
            } while (range.GoRightThroughFolded());//move caret right

            tb.Invalidate();
        }

        private void goLeftBracketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoLeftBracket(fastColoredTextBox1, '{', '}');
        }

        private void goRightBracketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoRightBracket(fastColoredTextBox1, '{', '}');
        }

        private void fastColoredTextBox1_AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //label
            if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
                !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
            {
                args.Shift = -args.TabLength;
                return;
            }
            //some statements: case, default
            if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
            {
                args.Shift = -args.TabLength / 2;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)"))//operator is unclosed
                {
                    args.Shift = args.TabLength;
                    return;
                }
        }

        /*
        private void saveFoldingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tb = fastColoredTextBox1;
            //save folded blocks
            List<int> collapsedBlocks = new List<int>();
            VisibleState prev = VisibleState.Visible;
            for(int i=0;i<tb.LinesCount;i++)
            {
                if (tb[i].VisibleState != VisibleState.Visible && prev == VisibleState.Visible)
                    collapsedBlocks.Add(i);//start of folded block
                if (tb[i].VisibleState == VisibleState.Visible && prev != VisibleState.Visible)
                    collapsedBlocks.Add(i-1);//end of folded block
                prev = tb[i].VisibleState;
             }
            if (prev != VisibleState.Visible)
                collapsedBlocks.Add(tb.LinesCount - 1);
            //
            using(var fs = File.Create("c:\\myFolded.bin"))
                new BinaryFormatter().Serialize(fs, collapsedBlocks);
        }

        private void loadFoldingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tb = fastColoredTextBox1;
            //load folded blocks
            tb.ExpandBlock(0, tb.LinesCount-1);//expand all current foldings
            List<int> collapsedBlocks;
            using(var fs = File.Open("c:\\myFolded.bin", FileMode.Open))
                collapsedBlocks = (List<int>)new BinaryFormatter().Deserialize(fs);
            for (int i = 0; i < collapsedBlocks.Count; i += 2)
                tb.CollapseBlock(collapsedBlocks[i], collapsedBlocks[i+1]);
        }*/
    }
}

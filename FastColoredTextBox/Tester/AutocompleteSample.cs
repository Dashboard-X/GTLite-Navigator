using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Drawing;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Tester
{
    public partial class AutocompleteSample : Form
    {
        FastColoredTextBoxNS.AutocompleteMenu popupMenu;

        public AutocompleteSample()
        {
            InitializeComponent();

            //create autocomplete popup menu
            popupMenu = new FastColoredTextBoxNS.AutocompleteMenu(fastColoredTextBox1);

            //generate 456976 words
            var randomWords = new List<string>();
            int codeA = Convert.ToInt32('a');
            for (int i = 0; i < 26; i++)
            for (int j = 0; j < 26; j++)
            for (int k = 0; k < 26; k++)
            for (int l = 0; l < 26; l++)
                randomWords.Add(
                    new string(new char[]{Convert.ToChar(i + codeA), Convert.ToChar(j + codeA), Convert.ToChar(k + codeA), Convert.ToChar(l + codeA)}));

            //set words as autocomplete source
            popupMenu.Items.SetAutocompleteItems(randomWords);
        }
    }
}

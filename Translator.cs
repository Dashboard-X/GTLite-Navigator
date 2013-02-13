using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace GTLite
{
    public partial class Translator : DevComponents.DotNetBar.Office2007Form
    {
        public Translator(string url)
        {
            InitializeComponent();
            webKitBrowser1.Navigate("http://translate.google.com/translate?hl=en&sl=auto&tl=en&u=" + url);
        }

        private void Translator_Load(object sender, EventArgs e)
        {

        }
    }
}
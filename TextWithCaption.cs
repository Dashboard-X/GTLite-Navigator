using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GTLite
{
    public partial class TextWithCaption : UserControl
    {
        public TextWithCaption()
        {
            InitializeComponent();
        }
        public override string Text
        {
            get { return textBoxX1.Text; }
            set
            {
                textBoxX1.Text = value;
            }
        }
        public string Caption
        {
            get { return buttonX1.Text; }
            set
            {
                buttonX1.Text = value;
            }
        }
    }
}

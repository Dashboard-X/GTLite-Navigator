using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GTLite
{
    public partial class PopupExceptions : Form
    {
        public PopupExceptions()
        {
            InitializeComponent();
            foreach (string i in Properties.Settings.Default.PopupExceptions)
                listBox1.Items.Add(i);
            foreach (string i in Properties.Settings.Default.PopupExceptionsL)
                listBox2.Items.Add(i);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (object o in listBox1.SelectedItems)
            {
                listBox1.Items.Remove(o);
                Properties.Settings.Default.PopupExceptions.Remove((string)o);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Properties.Settings.Default.PopupExceptions.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = Microsoft.VisualBasic.Interaction.InputBox("Please enter the url or the host of the url that you want to add to the exceptions");
            listBox1.Items.Add(url);
            GTLite.Properties.Settings.Default.PopupExceptions.Add(url);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string url = Microsoft.VisualBasic.Interaction.InputBox("Please enter the url or the host of the url that you want to add to the exceptions");
            listBox2.Items.Add(url);
            GTLite.Properties.Settings.Default.PopupExceptionsL.Add(url);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (object o in listBox2.SelectedItems)
            {
                listBox2.Items.Remove(o);
                Properties.Settings.Default.PopupExceptionsL.Remove((string)o);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            GTLite.Properties.Settings.Default.PopupExceptionsL.Clear();
        }

        private void PopupExceptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}

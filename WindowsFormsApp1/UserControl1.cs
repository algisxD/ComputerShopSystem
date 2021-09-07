using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            label1.Text = "1";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            label1.Text = "2";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            label1.Text = "3";
            
        }
    }
}

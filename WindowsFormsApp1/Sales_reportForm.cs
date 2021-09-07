using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Sales_reportForm : Form
    {
        public Sales_reportForm()
        {
            InitializeComponent();
        }

        private void Sales_reportForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Sales_reportForm_Closing);
        }

        private void Sales_reportForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }
    }
}

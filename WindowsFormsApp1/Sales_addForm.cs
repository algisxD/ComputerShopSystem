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
    public partial class Sales_addForm : Form
    {
        public Sales_addForm()
        {
            InitializeComponent();
        }

        private void Sales_addForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Sales_addForm_Closing);
        }

        private void Sales_addForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }
    }
}


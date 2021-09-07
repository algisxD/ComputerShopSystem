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
    public partial class Repair_viewForm : Form
    {
        public Repair_viewForm()
        {
            InitializeComponent();
        }

        private void Repair_viewForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Repair_viewForm_Closing);
        }

        private void Repair_viewForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }
    }
}

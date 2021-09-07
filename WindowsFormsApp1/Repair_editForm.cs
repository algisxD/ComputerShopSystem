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
    public partial class Repair_editForm : Form
    {
        public Repair_editForm()
        {
            InitializeComponent();
        }

        private void Repair_editForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Rent_editForm_Closing);
        }

        private void Rent_editForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }
    }
}

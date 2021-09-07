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
    public partial class Rent_reportForm : Form
    {
        public Rent_reportForm()
        {
            InitializeComponent();
        }

        private void Rent_reportForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Rent_reportForm_Closing);
        }

        private void Rent_reportForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }
    }
}

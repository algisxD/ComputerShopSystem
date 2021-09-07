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
    public partial class Rent_viewForm : Form
    {
        public Rent_viewForm()
        {
            InitializeComponent();
        }

        private void Rent_viewForm_Load(object sender, EventArgs e)
        {

            this.FormClosing += new FormClosingEventHandler(Rent_viewForm_Closing);
        }

        private void Rent_viewForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }
    }
}

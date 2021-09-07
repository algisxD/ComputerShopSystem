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
    public partial class RentForm : Form
    {
        public RentForm()
        {
            InitializeComponent();
        }

        private void RentForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(RentForm_Closing);
        }

        private void RentForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form addContract = new Rent_addForm();
            addContract.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form editContract = new Rent_editForm();
            editContract.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Form viewContract = new Rent_viewForm();
            viewContract.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form report = new Rent_reportForm();
            report.Show();
        }
    }
}

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
    public partial class SalesForm : Form
    {
        public SalesForm()
        {
            InitializeComponent();
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(SalesForm_Closing);
        }

        private void SalesForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form addContract = new Sales_addForm();
            addContract.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form editContract = new Sales_editForm();
            editContract.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form viewContract = new Sales_viewForm();
            viewContract.Show();
        }

        private void Button7_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Form report = new Sales_reportForm();
            report.Show();
        }
    }
}

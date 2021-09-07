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
    public partial class RepairForm : Form
    {
        public static Form LoginForm;

        public Form accountManagement = new AccountManagementForm();

        public static int userID = 0;
        public RepairForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LoginForm.Show();
            this.Dispose();
        }

        private void RepairForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(RepairForm_Closing);
            label2.Text = RepairForm.userID.ToString();
        }
        private void RepairForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            accountManagement.Dispose();
            this.Dispose();
            LoginForm.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form addContract = new Repair_addForm();
            addContract.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form editContract = new Repair_editForm();
            editContract.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form viewContract = new Repair_viewForm();
            viewContract.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            accountManagement.Show();
        }
    }
}

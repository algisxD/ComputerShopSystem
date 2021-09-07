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
    public partial class SellerForm : Form
    {
        public static Form LoginForm;

        public Form accountManagement = new AccountManagementForm();

        public Form sales = new SalesForm();

        public Form rent = new RentForm();

        public static int userID;
        public SellerForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LoginForm.Show();
            this.Dispose();
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(SellerForm_Closing);
            label2.Text = SellerForm.userID.ToString();
        }

        private void SellerForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            accountManagement.Dispose();
            sales.Dispose();
            rent.Dispose();
            this.Dispose();
            LoginForm.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            accountManagement.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            rent.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            sales.Show();
        }
    }
}

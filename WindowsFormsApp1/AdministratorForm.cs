using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class AdministratorForm : MaterialForm
    {
        public static Form LoginForm;

        public Form accountManagement = new AccountManagementForm();
        private readonly MaterialSkinManager materialSkinManager;
        public static int userID = 0;
        public AdministratorForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Redagavimas";
        }

        private void AdministratorForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(AdministratorForm_Closing);
            label2.Text = AdministratorForm.userID.ToString();
        }

        private void AdministratorForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            accountManagement.Dispose();
            this.Dispose();
            LoginForm.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LoginForm.Show();
            this.Dispose();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            accountManagement.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form newAccount = new Registration();
            newAccount.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form newRole = new UserList();
            newRole.Show();
        }
    }
}

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
    public partial class AccountManagementForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public AccountManagementForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Paskyros redagavimas";
        }

        private void AccountManagementForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(AccountManagementForm_Closing);
        }

        private void AccountManagementForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form accountinfo = new AccountManagement_infoForm();
            accountinfo.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form accountedit = new AccountManagement_editForm();
            accountedit.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form accountpassword = new AccountManagement_passwordupdateForm();
            accountpassword.Show();
        }
    }
}

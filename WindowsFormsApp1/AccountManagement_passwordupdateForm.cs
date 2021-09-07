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
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class AccountManagement_passwordupdateForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public AccountManagement_passwordupdateForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Slaptažodžio atnaujinimas";
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void AccountManagement_passwordupdateForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(AccountManagement_passwordupdateForm_Closing);
        }

        private void AccountManagement_passwordupdateForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField2.Text.Length == 0)
            {
                materialLabel4.Text = "Įveskite slaptazodi";
                return;
            }
            if (materialSingleLineTextField3.Text.Length == 0)
            {
                materialLabel4.Text = "Pakartokite slaptazodi";
                return;
            }
            if (materialSingleLineTextField3.Text != materialSingleLineTextField2.Text)
            {
                materialLabel4.Text = "Nesutampa slaptazodziai";
                return;
            }
            string password = materialSingleLineTextField2.Text;
            string oldpassword = materialSingleLineTextField1.Text;

            string cs = Registration.connection;
            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT slaptazodis, typeSelector FROM is_vartotojas WHERE id = '" + Form1.id + "'";
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            int correct = -1;
            string role = "";
            bool cracked = false;
            bool isgood = true;
            while (rdr.Read())
            {
                string hashedPassword = rdr.GetString(0);
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(oldpassword, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                        isgood = false;
            }
            rdr.Close();
            //con.Close();


            if (isgood)
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string savedPasswordHash = Convert.ToBase64String(hashBytes);
                sql = "UPDATE is_vartotojas SET slaptazodis = '" + savedPasswordHash + "' WHERE id = " + Form1.id;
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                if (correct > 0)
                {
                    Hide();
                    Form x;
                    switch (role)
                    {
                        case "Administratorius":
                            x = new AdministratorForm();
                            AdministratorForm.LoginForm = this;
                            AdministratorForm.userID = correct;
                            x.Show();
                            break;
                        case "Sandėlininkas":
                            x = new WarehouseForm();
                            WarehouseForm.LoginForm = this;
                            WarehouseForm.userID = correct;
                            x.Show();
                            break;
                        case "Taisytojas":
                            x = new RepairForm();
                            RepairForm.LoginForm = this;
                            RepairForm.userID = correct;
                            x.Show();
                            break;
                        case "Pardavėjas":
                            x = new SellerForm();
                            SellerForm.LoginForm = this;
                            SellerForm.userID = correct;
                            x.Show();
                            break;
                    }
                    materialLabel4.Text = "Slaptazodis pakeistas";
                }
                else
                {
                    materialLabel4.Text = "Netinka senas slaptazodis";
                }
                rdr.Close();
                con.Close();
            }
        } 
    }
}

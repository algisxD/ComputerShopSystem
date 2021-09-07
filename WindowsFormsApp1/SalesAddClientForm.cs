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
    public partial class SalesAddClientForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public static Form LoginForm;
        public static int userID;
        public SalesAddClientForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Kliento pridėjimas";
        }

        private void SalesAddClientForm_Load(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField2_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            string vardas = materialSingleLineTextField1.Text;
            string pavarde = materialSingleLineTextField2.Text;
            string elpastas = materialSingleLineTextField3.Text;
            string telefonas = materialSingleLineTextField4.Text;
            string adresas = materialSingleLineTextField5.Text;
            string pastoKodas = materialSingleLineTextField6.Text;
            if (vardas.Length == 0)
            {
                materialLabel1.Text = "Vardas negali būti tuščias";
                return;
            }
            if (pavarde.Length == 0)
            {
                materialLabel1.Text = "Pavardė negali būti tuščia";
                return;
            }
            if (adresas.Length == 0)
            {
                materialLabel1.Text = "Adresas negali būti tuščias";
                return;
            }
            if (pastoKodas.Length == 0)
            {
                materialLabel1.Text = "Pašto kodas negali būti tuščias";
                return;
            }

            string cs = Form1.connection;
            var con = new MySqlConnection(cs);
            con.Open();
            var sql = "INSERT INTO klientas(vardas, pavarde, ";
            if (elpastas.Length != 0)
            {
                sql = sql + " el_pastas,";
            }
            if (telefonas.Length != 0)
            {
                sql = sql + " telefono_nr,";
            }
            sql = sql + "adresas, pasto_kodas) VALUES ('" + vardas + "', '" + pavarde + "', '";
            if (elpastas.Length != 0)
            {
                sql = sql + elpastas + "', '";
            }
            if (telefonas.Length != 0)
            {
                sql = sql + telefonas + "', '";
            }
            sql = sql + adresas + "', '" + pastoKodas + "')";
            materialLabel1.Text = sql;
            var cmd = new MySqlCommand(sql, con);

            try
            {
                int numberOfDeleted = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                materialLabel1.Text = "Nepavyko pridėti kliento";
                return;
            }
            SalesChooseClientForm.errorMessage = "Klientas sėkmingai pridėtas";
            con.Close();
            this.Close();
        }
    }
}

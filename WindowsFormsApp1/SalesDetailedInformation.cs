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
    public partial class SalesDetailedInformation : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public static Form LoginForm;
        public static int userID;
        public static string orderID = "";
        public SalesDetailedInformation()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Sutarties detali informacija";
        }

        private void SalesDetailedInformation_Load(object sender, EventArgs e)
        {
            materialListView1.Items.Clear();
            string cs = Form1.connection;

            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT pardavimo_sutartis.sudarymo_data, pardavimo_sutartis.busena, klientas.vardas, klientas.pavarde, pardavimo_sutartis.kaina, " +
                "is_vartotojas.vardas, is_vartotojas.pavarde FROM pardavimo_sutartis INNER JOIN klientas ON pardavimo_sutartis.fk_Klientasid = klientas.id_Klientas" +
                " INNER JOIN is_vartotojas ON is_vartotojas.id = pardavimo_sutartis.fk_ISvartotojas WHERE pardavimo_sutartis.id_Pardavimo_sutartis = " + orderID;
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string[] result = new string[5];

                result[0] = rdr.GetString(0);
                result[1] = rdr.GetString(1);
                result[2] = rdr.GetString(2) + " " + rdr.GetString(3);
                result[3] = rdr.GetString(4);
                result[4] = rdr.GetString(5) + " " + rdr.GetString(6);

                materialLabel6.Text = result[0];
                materialLabel7.Text = result[1];
                materialLabel8.Text = result[2];
                materialLabel9.Text = result[3];
                materialLabel10.Text = result[4];
            }
            rdr.Close();

            string sql2 = "SELECT daiktas.pavadinimas, daiktas.aprasymas, daiktas.kaina, daikto_kiekis_pardavimas.kiekis FROM daiktas " +
                "INNER JOIN daikto_kiekis_pardavimas ON daiktas.kodas - daikto_kiekis_pardavimas.fk_Daiktaskodas WHERE " +
                "daikto_kiekis_pardavimas.fk_Pardavimo_sutartisid = " + orderID;
            var cmd2 = new MySqlCommand(sql2, con);

            MySqlDataReader rdr2 = cmd2.ExecuteReader();

            while (rdr2.Read())
            {
                string[] result = new string[4];

                result[0] = rdr2.GetString(0);
                result[1] = rdr2.GetString(1);
                result[2] = rdr2.GetString(2);
                result[3] = rdr2.GetString(3);


                var item = new ListViewItem(result);
                materialListView1.Items.Add(item);
            }


            con.Close();
        }

        private void materialLabel7_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}

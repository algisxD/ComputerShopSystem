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
    public partial class SalesAddGoods : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public static Form LoginForm;
        public static int userID;
        public static int contractID = 0;
        public SalesAddGoods()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Prekės pridėjimas prie sutarties";
        }

        private void SalesAddGoods_Load(object sender, EventArgs e)
        {
            materialListView1.HideSelection = true;
            Refresh_materialListView1();
        }

        public void Refresh_materialListView1()
        {
            materialListView1.Items.Clear();
            string cs = Form1.connection;

            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT kodas, pavadinimas, kiekis, kaina FROM daiktas";
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string[] result = new string[4];

                result[0] = rdr.GetString(0);
                result[1] = rdr.GetString(1);
                result[2] = rdr.GetString(2);
                result[3] = rdr.GetString(3);

                var item = new ListViewItem(result);
                materialListView1.Items.Add(item);
            }
            rdr.Close();
            con.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            materialListView1.Items.Clear();
            string cs = Form1.connection;

            string pavadinimas = materialSingleLineTextField1.Text;
            string kainaNuo = materialSingleLineTextField2.Text;
            string kainaIki = materialSingleLineTextField3.Text;

            if (pavadinimas.Length == 0 && kainaNuo.Length == 0 && kainaIki.Length == 0)
            {
                Refresh_materialListView1();
                return;
            }

            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT kodas, pavadinimas, kiekis, kaina FROM daiktas WHERE ";

            int countof = 0;

            if (pavadinimas.Length > 0)
            {
                sql = sql + "pavadinimas LIKE '%" + pavadinimas + "%'";
                countof++;
            }
            if ((kainaNuo.Length > 0) && (pavadinimas.Length > 0))
            {
                sql = sql + " AND kaina >= " + kainaNuo;
                countof++;
            }
            else if ((kainaNuo.Length > 0) && (pavadinimas.Length == 0))
            {
                sql = sql + "kaina >= " + kainaNuo;
                countof++;
            }

            if (countof > 0 && kainaIki.Length > 0)
            {
                sql = sql + " AND kaina < " + kainaIki;
            }
            else if (kainaIki.Length > 0 && countof == 0)
            {
                sql = sql + "kaina < " + kainaIki;
            }

            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string[] result = new string[4];

                result[0] = rdr.GetString(0);
                result[1] = rdr.GetString(1);
                result[2] = rdr.GetString(2);
                result[3] = rdr.GetString(3);

                var item = new ListViewItem(result);
                materialListView1.Items.Add(item);
            }
            rdr.Close();
            con.Close();
        }


        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            if (materialListView1.SelectedItems.Count == 1)
            {
                string kiekis = materialSingleLineTextField4.Text;
                if (kiekis.Length == 0)
                {
                    materialLabel1.Text = "Įveskite prekės kiekį";
                    return;
                }

                int kiekisInt;
                if (int.TryParse(kiekis, out kiekisInt) == false)
                {
                    materialLabel1.Text = "Prekės kiekis gali būti sudarytas tik iš sveikųjų skaičių";
                    return;
                }

                if (kiekisInt < 0)
                {
                    materialLabel1.Text = "Prekės kiekis gali būti tik teigiamas skaičius";
                    return;
                }

                string cs = Form1.connection;
                var con = new MySqlConnection(cs);
                con.Open();
                var sql = "INSERT INTO daikto_kiekis_pardavimas(kiekis, fk_Pardavimo_sutartisid, fk_Daiktaskodas) VALUES" +
                    " (" + kiekis + ", " + contractID + ", " + materialListView1.SelectedItems[0].Text + ")";
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
}

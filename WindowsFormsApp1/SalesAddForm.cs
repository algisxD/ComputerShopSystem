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
    public partial class SalesAddForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public static Form LoginForm;
        public static int userID;
        public static string chosenClient;
        public static string chosenClientID = "";
        public SalesAddForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Pardavimo sutarties sudarymas";
        }

        private void SalesAddForm_Load(object sender, EventArgs e)
        {
            materialListView1.HideSelection = true;
        }

        public void Refresh_materialListView1()
        {
            materialListView1.Items.Clear();
            string cs = Form1.connection;

            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "Select id_Daikto_kiekis_pardavimas, pavadinimas, daikto_kiekis_pardavimas.kiekis, kaina * daikto_kiekis_pardavimas.kiekis FROM daiktas " +
                "INNER JOIN daikto_kiekis_pardavimas ON fk_Daiktaskodas = kodas WHERE fk_Pardavimo_sutartisid = " + SalesAddGoods.contractID;
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
            Form salesChooseClientForm = new SalesChooseClientForm();
            salesChooseClientForm.FormClosing += new FormClosingEventHandler(SalesChooseClientForm_Closing);
            salesChooseClientForm.Show();
        }

        private void SalesChooseClientForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            materialLabel1.Text = "Klientas: " + chosenClient;
            materialRaisedButton1.Visible = false;

            int error = 0;
            string cs = Form1.connection;
            var con = new MySqlConnection(cs);
            con.Open();
            //TODO IS VARTOTOJO ID
            var sql = "INSERT INTO pardavimo_sutartis(sudarymo_data, kaina, busena, fk_Klientasid, fk_ISvartotojas)" +
                "VALUES (CURRENT_DATE, 0, 'nesudaryta', " + chosenClientID + ", 1)";
            var cmd = new MySqlCommand(sql, con);

            try
            {
                int numberOfDeleted = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                materialLabel2.Text = "Nepavyko sukurti pradinės sutarties";
                error = 1;
            }
            if (error == 0)
            {
                var sql2 = "SELECT `auto_increment` FROM INFORMATION_SCHEMA.TABLES WHERE table_name = 'pardavimo_sutartis'";
                var cmd2 = new MySqlCommand(sql2, con);
                MySqlDataReader rdr = cmd2.ExecuteReader();

                while (rdr.Read())
                {
                    SalesAddGoods.contractID = int.Parse(rdr.GetString(0)) - 1;
                }
                rdr.Close();

                materialLabel2.Text = "Pradinė sutartis sukurta";
            }

            con.Close();
        }

        private void salesAddGoods_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            materialLabel2.Text = "Prekė pridėta į sutartį";
            Refresh_materialListView1();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            if (chosenClientID == "")
            {
                materialLabel2.Text = "Pirma pasirinkite klientą";
            }
            else
            {
                Form salesAddGoods = new SalesAddGoods();
                salesAddGoods.FormClosing += new FormClosingEventHandler(salesAddGoods_Closing);
                salesAddGoods.Show();
            }
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            if (materialListView1.SelectedItems.Count == 1)
            {
                string cs = Form1.connection;
                var con = new MySqlConnection(cs);
                con.Open();
                var sql = "DELETE FROM daikto_kiekis_pardavimas WHERE id_Daikto_kiekis_pardavimas = " + materialListView1.SelectedItems[0].Text;

                var cmd = new MySqlCommand(sql, con);

                try
                {
                    int numberOfDeleted = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    materialLabel1.Text = "Nepavyko panaikinti prekės";
                    return;
                }
                SalesChooseClientForm.errorMessage = "Prekė sėkmingai panaikinta";
                con.Close();
                Refresh_materialListView1();
            }
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            string cs = Form1.connection;
            var con = new MySqlConnection(cs);
            con.Open();
            var sql = "UPDATE pardavimo_sutartis SET busena = 'vykdoma', kaina = (SELECT SUM(daikto_kiekis_pardavimas.kiekis * daiktas.kaina) " +
                "FROM daiktas INNER JOIN daikto_kiekis_pardavimas ON daikto_kiekis_pardavimas.fk_Daiktaskodas = daiktas.kodas WHERE " +
                "daikto_kiekis_pardavimas.fk_Pardavimo_sutartisid = " + SalesAddGoods.contractID + ") WHERE id_Pardavimo_sutartis = " + SalesAddGoods.contractID;
            var cmd = new MySqlCommand(sql, con);

            try
            {
                int numberOfDeleted = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                materialLabel2.Text = "Nepavyko sukurti sutarties";
                return;
            }
            SalesChooseClientForm.errorMessage = "Prekė sėkmingai panaikinta";
            con.Close();
            this.Close();
        }
    }
}

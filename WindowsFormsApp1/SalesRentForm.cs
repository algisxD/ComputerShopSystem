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
    public partial class SalesRentForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public static Form LoginForm;
        public static int userID;
        public SalesRentForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Pardavimų ir nuomos valdymas";
        }

        private void SalesRentForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(SalesRentForm_Closing);
            materialListView1.HideSelection = true;
            materialLabel1.Text = "";
            Refresh_materialListView1();
        }

        public void Refresh_materialListView1()
        {
            materialListView1.Items.Clear();
            string cs = Form1.connection;

            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT pardavimo_sutartis.id_Pardavimo_sutartis, klientas.vardas, klientas.pavarde, pardavimo_sutartis.kaina, " +
                "(SELECT SUM(daikto_kiekis_pardavimas.kiekis) FROM daikto_kiekis_pardavimas WHERE fk_Pardavimo_sutartisid = " +
                "pardavimo_sutartis.id_Pardavimo_sutartis) AS prekiu_kiekis, pardavimo_sutartis.sudarymo_data, is_vartotojas.vardas, " +
                "is_vartotojas.pavarde, pardavimo_sutartis.busena FROM pardavimo_sutartis INNER JOIN klientas ON pardavimo_sutartis.fk_Klientasid = " +
                "klientas.id_Klientas INNER JOIN is_vartotojas ON pardavimo_sutartis.fk_ISvartotojas = is_vartotojas.id WHERE " +
                "pardavimo_sutartis.busena != 'istrinta'";
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string[] result = new string[7];

                result[0] = rdr.GetString(0);
                result[1] = rdr.GetString(1) + " " + rdr.GetString(2);
                result[2] = rdr.GetString(3);
                if (!rdr.IsDBNull(4))
                {
                    result[3] = rdr.GetString(4);
                }
                else
                {
                    result[3] = "0";
                }
                result[4] = rdr.GetString(5).Split()[0];
                result[5] = rdr.GetString(6) + " " + rdr.GetString(7);
                result[6] = rdr.GetString(8);

                var item = new ListViewItem(result);
                materialListView1.Items.Add(item);
            }
            rdr.Close();
            con.Close();
        }

        private void SalesRentForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
            LoginForm.Close();
        }

        private void materialListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void materialListView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            int error = 0;
            if (materialListView1.SelectedItems.Count == 1)
            {
                string id = materialListView1.SelectedItems[0].Text;
                string cs = Form1.connection;
                var con = new MySqlConnection(cs);
                con.Open();
                var sql = "UPDATE pardavimo_sutartis SET busena = 'istrinta' WHERE id_Pardavimo_sutartis = " + id;
                var cmd = new MySqlCommand(sql, con);

                try
                {
                    int numberOfDeleted = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    materialLabel1.Text = "Nepavyko pašalinti įrašo";
                    error = 1;
                }
                if (error == 0)
                {
                    materialLabel1.Text = "Įrašas pašalintas sėkmingai";
                }

                con.Close();
                Refresh_materialListView1();
            }
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            int error = 0;
            if (materialListView1.SelectedItems.Count == 1)
            {
                string id = materialListView1.SelectedItems[0].Text;
                string cs = Form1.connection;
                var con = new MySqlConnection(cs);
                con.Open();
                var sql = "UPDATE pardavimo_sutartis SET busena = 'ivykdyta' WHERE id_Pardavimo_sutartis = " + id;
                var cmd = new MySqlCommand(sql, con);

                try
                {
                    int numberOfDeleted = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    materialLabel1.Text = "Nepavyko patvirtinti įrašo";
                    error = 1;
                }
                if (error == 0)
                {
                    materialLabel1.Text = "Įrašas patvirtintas";
                }

                con.Close();
                Refresh_materialListView1();
            }
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            LoginForm.Show();
            this.Dispose();
        }

        private void materialRaisedButton7_Click(object sender, EventArgs e)
        {
            Form accountManagement = new AccountManagementForm();
            accountManagement.Show();
        }

        private void salesAddForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            materialLabel1.Text = "Sutartis sudaryta, bet nepatvirtinta";
            Refresh_materialListView1();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Form salesAddForm = new SalesAddForm();
            salesAddForm.FormClosing += new FormClosingEventHandler(salesAddForm_Closing);
            salesAddForm.Show();
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            int numberOf = 0;
            string sql = "SELECT pardavimo_sutartis.id_Pardavimo_sutartis, klientas.vardas, klientas.pavarde, pardavimo_sutartis.kaina, " +
                "(SELECT SUM(daikto_kiekis_pardavimas.kiekis) FROM daikto_kiekis_pardavimas WHERE fk_Pardavimo_sutartisid = " +
                "pardavimo_sutartis.id_Pardavimo_sutartis) AS prekiu_kiekis, pardavimo_sutartis.sudarymo_data, is_vartotojas.vardas, " +
                "is_vartotojas.pavarde, pardavimo_sutartis.busena FROM pardavimo_sutartis INNER JOIN klientas ON pardavimo_sutartis.fk_Klientasid = " +
                "klientas.id_Klientas INNER JOIN is_vartotojas ON pardavimo_sutartis.fk_ISvartotojas = is_vartotojas.id";

            if (materialSingleLineTextField1.Text == "" && materialSingleLineTextField2.Text == "" && materialSingleLineTextField3.Text == "" && materialSingleLineTextField4.Text == "" && materialSingleLineTextField5.Text == "")
            {
                Refresh_materialListView1();
                return;
            }
            else
            {
                sql += " WHERE";
            }
            int temp = -1;
            if (materialSingleLineTextField1.Text != "")
            {
                if (int.TryParse(materialSingleLineTextField1.Text, out temp))
                {
                    if (numberOf != 0)
                    {
                        sql += " AND";
                    }
                    sql += " kaina >= " + temp;
                    numberOf++;
                }
                else
                {
                    materialSingleLineTextField1.Text = "";
                }
            }
            if (materialSingleLineTextField2.Text != "")
            {
                if (int.TryParse(materialSingleLineTextField2.Text, out temp))
                {
                    if (numberOf != 0)
                    {
                        sql += " AND";
                    }
                    sql += " kaina < " + temp;
                    numberOf++;
                }
                else
                {
                    materialSingleLineTextField2.Text = "";
                }
            }
            DateTime tempDate;
            if (materialSingleLineTextField3.Text != "")
            {
                if (DateTime.TryParse(materialSingleLineTextField3.Text, out tempDate))
                {
                    if (numberOf != 0)
                    {
                        sql += " AND";
                    }
                    sql += " sudarymo_data >= '" + materialSingleLineTextField3.Text + "'";
                    numberOf++;
                }
                else
                {
                    materialSingleLineTextField3.Text = "";
                }
            }
            if (materialSingleLineTextField4.Text != "")
            {
                if (DateTime.TryParse(materialSingleLineTextField4.Text, out tempDate))
                {
                    if (numberOf != 0)
                    {
                        sql += " AND";
                    }
                    sql += " sudarymo_data <= '" + materialSingleLineTextField4.Text + "'";
                    numberOf++;
                }
                else
                {
                    materialSingleLineTextField4.Text = "";
                }
            }
            if (materialSingleLineTextField5.Text != "")
            {
                if (numberOf != 0)
                {
                    sql += " AND";
                }
                sql += " busena LIKE '%" + materialSingleLineTextField5.Text + "%'";
                numberOf++;
            }
            if (numberOf == 0)
            {
                return;
            }

            materialListView1.Items.Clear();
            string cs = Form1.connection;

            var con = new MySqlConnection(cs);
            con.Open();
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string[] result = new string[7];

                result[0] = rdr.GetString(0);
                result[1] = rdr.GetString(1) + " " + rdr.GetString(2);
                result[2] = rdr.GetString(3);
                if (!rdr.IsDBNull(4))
                {
                    result[3] = rdr.GetString(4);
                }
                else
                {
                    result[3] = "0";
                }
                result[4] = rdr.GetString(5).Split()[0];
                result[5] = rdr.GetString(6) + " " + rdr.GetString(7);
                result[6] = rdr.GetString(8);

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
                Form salesDetailedInformation = new SalesDetailedInformation();
                SalesDetailedInformation.orderID = materialListView1.SelectedItems[0].Text;
                salesDetailedInformation.Show();
            }
        }
    }
}

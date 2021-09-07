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
    public partial class SalesChooseClientForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public static Form LoginForm;
        public static int userID;
        public static string errorMessage;
        public SalesChooseClientForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Kliento pasirinkimas";
            Refresh_materialListView1();
        }

        private void SalesChooseClientForm_Load(object sender, EventArgs e)
        {
            materialListView1.HideSelection = true;
        }

        private void materialSingleLineTextField2_Click(object sender, EventArgs e)
        {

        }

        public void Refresh_materialListView1()
        {
            materialListView1.Items.Clear();
            string cs = Form1.connection;
            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT id_Klientas, vardas, pavarde, adresas FROM klientas";
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

            string vardas = materialSingleLineTextField1.Text;
            string pavarde = materialSingleLineTextField2.Text;

            if ((vardas.Length == 0) && (pavarde.Length == 0))
            {
                Refresh_materialListView1();
                return;
            }

            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT id_Klientas, vardas, pavarde, adresas FROM klientas WHERE ";
            if (vardas.Length > 0)
            {
                sql = sql + "vardas LIKE '%" + vardas + "%'";
            }
            if ((vardas.Length > 0) &&(pavarde.Length > 0))
            {
                sql = sql + " AND ";
            }
            if (pavarde.Length > 0)
            {
                sql = sql + "pavarde LIKE '%" + pavarde + "%'";
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

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            Form salesAddClientForm = new SalesAddClientForm();
            salesAddClientForm.FormClosing += new FormClosingEventHandler(SalesAddClientForm_Closing);
            salesAddClientForm.Show();
        }

        private void SalesAddClientForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            materialLabel1.Text = errorMessage;
            Refresh_materialListView1();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            if (materialListView1.SelectedItems.Count == 1)
            {
                SalesAddForm.chosenClient = materialListView1.SelectedItems[0].SubItems[1].Text + " " +
                    materialListView1.SelectedItems[0].SubItems[2].Text;
                SalesAddForm.chosenClientID = materialListView1.SelectedItems[0].SubItems[0].Text;
                this.Close();
            }
        }
    }
}

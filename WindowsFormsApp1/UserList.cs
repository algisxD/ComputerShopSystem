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
    
    public partial class UserList : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public UserList()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Redagavimas";
        }

        private void materialListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void UserList_Load(object sender, EventArgs e)
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
            string sql = "SELECT  id, vardas, pavarde, el_Pastas, adresas, alga, parduotuves_adresas  FROM is_vartotojas";
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string[] result = new string[7];
                result[0] = rdr.GetString(0);
                result[1] = rdr.GetString(1);
                result[2] = rdr.GetString(2);
                result[3] = rdr.GetString(3);
                result[4] = rdr.GetString(4);
                result[5] = rdr.GetString(5);
                result[6] = rdr.GetString(6);
                var item = new ListViewItem(result);
                materialListView1.Items.Add(item);
            }
            rdr.Close();
            con.Close();
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton5_Click_1(object sender, EventArgs e)
        {
            int error = 0;
            if (materialListView1.SelectedItems.Count == 1)
            {
                string id = materialListView1.SelectedItems[0].Text;
                string cs = Form1.connection;
                var con = new MySqlConnection(cs);
                con.Open();
                var sql = "DELETE FROM is_vartotojas WHERE id = " + id;
                materialLabel4.Text = sql;
                var cmd = new MySqlCommand(sql, con);

                try
                {
                    int numberOfDeleted = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    materialLabel4.Text = "Šio įrašo negalima pašalinti";
                    error = 1;
                }
                if (error == 0)
                {
                    materialLabel4.Text = "";
                }



                con.Close();
                Refresh_materialListView1();


            }
        }

        private void materialRaisedButton4_Click_1(object sender, EventArgs e)
        {
            if (materialListView1.SelectedItems.Count == 1)
            {
                string id = materialListView1.SelectedItems[0].Text;
                Form1.selectid = int.Parse(id);
                Form newRole = new Administrator_changeRoleForm();
                newRole.Show();
                this.Close();
            }
        }
    }
}

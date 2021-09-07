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
    public partial class Warehouse_viewForm : MaterialForm
    {
        public int itemId;

        MaterialSkinManager materialSkinManager;
        public Warehouse_viewForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Prekės peržiūra";
        }

        private void Warehouse_viewForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Warehouse_viewForm_Closing);
            materialLabel1.ForeColor = Color.Gray;
            materialLabel3.ForeColor = Color.Gray;
            materialLabel7.ForeColor = Color.Gray;
            materialLabel5.ForeColor = Color.Gray;
            materialLabel9.ForeColor = Color.Gray;
            materialLabel11.ForeColor = Color.Gray;
            materialLabel13.ForeColor = Color.Gray;
            materialLabel15.ForeColor = Color.Gray;
            materialLabel17.ForeColor = Color.Gray;
            materialLabel19.ForeColor = Color.Gray;
            string cs = Form1.connection;

            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT kodas, pavadinimas, kaina, aprasymas, kiekis, bukle, YEAR(pagaminimo_data), Month(pagaminimo_data), fk_Sandelisid, parduodamas FROM daiktas WHERE kodas = " + itemId;
            var cmd = new MySqlCommand(sql, con);
            
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            materialLabel2.Text = rdr.GetString(0);
            materialLabel4.Text = rdr.GetString(1);
            materialLabel8.Text = rdr.GetString(2);
            materialLabel6.Text = rdr.GetString(3);
            materialLabel10.Text = rdr.GetString(4);
            materialLabel12.Text = rdr.GetString(5);
            materialLabel14.Text = rdr.GetString(6);
            materialLabel16.Text = rdr.GetString(7);
            materialLabel18.Text = rdr.GetString(8);
            if (rdr.GetInt32(9) == 1)
            {
                materialLabel20.Text = "Taip";
            }
            else
            {
                materialLabel20.Text = "Ne";
            }
            rdr.Close();
            con.Close();
        }

        private void Warehouse_viewForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }

        private void MaterialLabel10_Click(object sender, EventArgs e)
        {

        }
    }
}

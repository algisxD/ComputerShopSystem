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
    public partial class Warehouse_editForm : MaterialForm
    {
        public int itemId;

        public WarehouseForm form;

        MaterialSkinManager materialSkinManager;
        
        public Warehouse_editForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Daikto redagavimas";
        }

        private void Warehouse_editForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Warehouse_editForm_Closing);
            
            materialSingleLineTextField3.Text = itemId.ToString();
            materialSingleLineTextField3.Enabled = false;
            materialListView3.HideSelection = true;
            materialLabel10.Text = "";
            Fill_information();
        }

        private void Warehouse_editForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }

        private void Fill_information()
        {
            string cs = Form1.connection;
            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT pavadinimas, aprasymas, kaina, kiekis, bukle, Year(Pagaminimo_data), Month(Pagaminimo_data), parduodamas, fk_Sandelisid FROM daiktas WHERE kodas = " + itemId;

            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            materialSingleLineTextField1.Text = rdr.GetString(0);
            materialSingleLineTextField2.Text = rdr.GetString(1);
            materialSingleLineTextField3.Text = itemId.ToString();
            materialSingleLineTextField4.Text = rdr.GetString(2);
            materialSingleLineTextField5.Text = rdr.GetString(3);
            materialSingleLineTextField6.Text = rdr.GetString(4);
            materialSingleLineTextField7.Text = rdr.GetString(5);
            materialSingleLineTextField8.Text = rdr.GetString(6);
            if (rdr.GetInt32(7) == 1)
            {
                materialCheckBox1.Checked = true;
            }
            else
            {
                materialCheckBox1.Checked = false;
            }
            rdr.Close();

            materialListView3.Items.Clear();
            sql = "SELECT id_Sandelis, adresas, pasto_kodas, plotas FROM sandelis";
            cmd = new MySqlCommand(sql, con);

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string[] result = new string[4];
                result[0] = rdr.GetString(0);
                result[1] = rdr.GetString(1);
                result[2] = rdr.GetString(2);
                result[3] = rdr.GetString(3);
                var item = new ListViewItem(result);
                materialListView3.Items.Add(item);
            }
            rdr.Close();
            con.Close();
        }

        private void MaterialFlatButton2_Click(object sender, EventArgs e)
        {
            string pavadinimas = materialSingleLineTextField1.Text;
            if (pavadinimas.Length == 0)
            {
                materialLabel10.Text = "Pavadinimas negali būti tuščias";
                return;
            }
            string aprasymas = materialSingleLineTextField2.Text;
            if (aprasymas.Length == 0)
            {
                materialLabel10.Text = "Aprašymas negali būti tuščias";
                return;
            }
            int kodas;
            if (Int32.TryParse(materialSingleLineTextField3.Text, out kodas) == false)
            {
                materialLabel10.Text = "Kodas turi būti skaičius";
                return;
            }
            double kaina;
            if (Double.TryParse(materialSingleLineTextField4.Text, out kaina) == false)
            {
                materialLabel10.Text = "Kaina turi būti skaičius";
                return;
            }
            kaina = Math.Round(kaina, 2);
            int kiekis;
            if (Int32.TryParse(materialSingleLineTextField5.Text, out kiekis) == false)
            {
                materialLabel10.Text = "Kiekis turi būti skaičius";
                return;
            }
            string bukle = materialSingleLineTextField6.Text;
            if (bukle.Length == 0)
            {
                materialLabel10.Text = "Būklė negali būti tuščia";
                return;
            }
            int pagaminimometai;
            if (Int32.TryParse(materialSingleLineTextField7.Text, out pagaminimometai) == false)
            {
                materialLabel10.Text = "Pagaminimo metai turi būti skaičius";
                return;
            }
            if (pagaminimometai > 2025 || pagaminimometai < 1970)
            {
                materialLabel10.Text = "Neteisingi metai";
                return;
            }
            int pagaminimomenuo;
            if (Int32.TryParse(materialSingleLineTextField8.Text, out pagaminimomenuo) == false)
            {
                materialLabel10.Text = "Pagaminimo mėnuo turi būti skaičius";
                return;
            }
            if (pagaminimomenuo > 12 || pagaminimomenuo < 0)
            {
                materialLabel10.Text = "Neteisingas mėnuo";
                return;
            }
            string parduodamas = materialCheckBox1.Checked.ToString();
            int sandelioId;
            if (materialListView3.SelectedItems.Count == 1)
            {
                sandelioId = Int32.Parse(materialListView3.SelectedItems[0].Text);
            }
            else
            {
                sandelioId = -1;
            }

            //sql = "INSERT INTO daiktas(pavadinimas, aprasymas, kodas, kaina, parduodamas, kiekis, bukle, pagaminimo_data, fk_Sandelisid) " +
            // "VALUES('" + pavadinimas + "', '" + aprasymas + "', " + kodas + ", " + kaina.ToString(System.Globalization.CultureInfo.InvariantCulture) + ", " + parduodamas + ", " + kiekis + ", '" + bukle + "', '" + pagaminimometai.ToString() + "-" + pagaminimomenuo.ToString() + "-" + "0'" + ", " + sandelioid + ")";
            string sql = "";
            if (sandelioId == -1)
            {
                sql = "UPDATE daiktas SET pavadinimas = '" + pavadinimas + "', aprasymas = '" + aprasymas + "', kaina = " + kaina.ToString(System.Globalization.CultureInfo.InvariantCulture) + ", parduodamas = " + parduodamas + ", kiekis = " + kiekis + ", bukle = '" + bukle + "', pagaminimo_data = '" + pagaminimometai.ToString() + "-" + pagaminimomenuo.ToString() + "-0' WHERE kodas = " + itemId;
            }
            else
            {
                sql = "UPDATE daiktas SET pavadinimas = '" + pavadinimas + "', aprasymas = '" + aprasymas + "', kaina = " + kaina.ToString(System.Globalization.CultureInfo.InvariantCulture) + ", parduodamas = " + parduodamas + ", kiekis = " + kiekis + ", bukle = '" + bukle + "', pagaminimo_data = '" + pagaminimometai.ToString() + "-" + pagaminimomenuo.ToString() + "-0', fk_Sandelisid = " + sandelioId + " WHERE kodas = " + itemId;
            }

            string cs = Form1.connection;
            var con = new MySqlConnection(cs);
            con.Open();
            var cmd = new MySqlCommand(sql, con);
            cmd = new MySqlCommand(sql, con);

            cmd.ExecuteNonQuery();


            con.Close();
            form.Refresh_materialListView1();
            this.Close();
        }
    }
}

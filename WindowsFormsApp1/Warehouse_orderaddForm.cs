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
    public partial class Warehouse_orderaddForm : MaterialForm
    {
        public int itemId;

        public MaterialTabControl selector;

        public WarehouseForm form;

        MaterialSkinManager materialSkinManager;
        public Warehouse_orderaddForm()
        {
            InitializeComponent();
            this.Text = "Sukurti užsakymą";
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void Warehouse_orderaddForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Warehouse_orderaddForm_Closing);
            materialLabel2.Text = itemId.ToString();
            materialLabel3.Text = "";
        }

        private void Warehouse_orderaddForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            int kiekis;
            bool tryParse = Int32.TryParse(materialSingleLineTextField1.Text, out kiekis);
            if (tryParse == false)
            {
                materialLabel3.Text = "Reikia įvesti skaičių";
                return;
            }
            if (kiekis < 1)
            {
                materialLabel3.Text = "Skaičius turi būti teigiamas";
                return;
            }
            string cs = Form1.connection;

            var con = new MySqlConnection(cs);
            con.Open();
            var sql = "INSERT INTO uzsakymas (sukurimo_data, kiekis, fk_ISvartotojas, fk_daiktokodas) VALUES ('" + DateTime.Now.ToString("yyyy-MM-dd") + "', " + kiekis + ", " + WarehouseForm.userID+", "+ itemId+")";
            var cmd = new MySqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            con.Close();
            selector.SelectedIndex = 2;
            form.Refresh_materialListView2();
            this.Close();
        }
    }
}

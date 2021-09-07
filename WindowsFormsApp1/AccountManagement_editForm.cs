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
    public partial class AccountManagement_editForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public AccountManagement_editForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Redagavimas";
        }

        private void AccountManagement_editForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(AccountManagement_infoForm_Closing);
            string cs = Form1.connection;

            var con = new MySqlConnection(cs);
            con.Open();
            string sql = "SELECT id_sandelis, adresas FROM sandelis";
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("id_sandelis", typeof(string));
            dt.Columns.Add("adresas", typeof(string));
            dt.Load(rdr);

            comboBox2.ValueMember = "id_Sandelis";
            comboBox2.DisplayMember = "id_Sandelis";
            comboBox2.DataSource = dt;
            rdr.Close();

            sql = "SELECT vardas, pavarde, gimimo_data, el_pastas, adresas, dirba_nuo, darbo_valandos, alga, adresas, typeSelector , slapyvardis, fk_sandelisid FROM is_vartotojas WHERE id = " + Form1.id;
            cmd = new MySqlCommand(sql, con);
            rdr = cmd.ExecuteReader();


            rdr.Read();
            materialLabel15.Text = rdr.GetString(10);
            materialLabel16.Text = rdr.GetString(5);
            materialSingleLineTextField3.Text = rdr.GetString(2);
            materialSingleLineTextField9.Text = rdr.GetString(7);
            materialSingleLineTextField1.Text = rdr.GetString(0);
            materialSingleLineTextField2.Text = rdr.GetString(1);
            materialSingleLineTextField4.Text = rdr.GetString(3);
            materialSingleLineTextField5.Text = rdr.GetString(4);
            materialSingleLineTextField8.Text = rdr.GetString(6);
            materialSingleLineTextField10.Text = rdr.GetString(8);
            comboBox1.Text = rdr.GetString(9);
            comboBox2.Text = rdr.GetString(11);




            rdr.Close();
            con.Close();
        }

        private void AccountManagement_infoForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }

        private void materialLabel7_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel15_Click(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            double alga = 0;
            DateTime gimimo_data = DateTime.Now;
            if (materialSingleLineTextField1.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite vardą";
                return;
            }
            if (materialSingleLineTextField2.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite pavardę";
                return;
            }
            if (materialSingleLineTextField3.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite Gimimo datą";
                return;
            }
            if (materialSingleLineTextField4.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite Elektroninį paštą";
                return;
            }
            if (materialSingleLineTextField5.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite adresą";
                return;
            }
            if (materialSingleLineTextField8.Text.Length == 0)
            {
                materialLabel12.Text = "Iveskite darbo laika";
                return;
            }

            if (materialSingleLineTextField9.Text.Length == 0)
            {
                materialLabel12.Text = "Iveskite alga";
                return;
            }
            if (materialSingleLineTextField10.Text.Length == 0)
            {
                materialLabel12.Text = "Iveskite parduotuves adresa";
                return;
            }
            bool tryParse = DateTime.TryParse(materialSingleLineTextField3.Text, out gimimo_data);
            if (!tryParse)
            {
                materialLabel12.Text = "Netinkamas datos formatas";
                return;
            };
            tryParse = double.TryParse(materialSingleLineTextField9.Text, out alga);
            if (!tryParse)
            {
                materialLabel12.Text = "Netinkamas algos formatas";
                return;
            }
            string cs = Form1.connection;
            string format = "yyyy-MM-dd";


            string vardas = materialSingleLineTextField1.Text;
            string pavarde = materialSingleLineTextField2.Text;
            string el = materialSingleLineTextField4.Text;
            string adresas = materialSingleLineTextField5.Text;
            string darbas = materialSingleLineTextField8.Text;
            string parduotuve = materialSingleLineTextField10.Text;
            string sandelis = comboBox2.Text;
            string role = comboBox1.Text;


            if (role.Length == 0)
            {
                materialLabel12.Text = "pasirinkite role";
                return;
            }

            string data = gimimo_data.ToString("s");
            data = data.Substring(0, 10);



            var con = new MySqlConnection(cs);
            con.Open();
            var sql = "UPDATE is_vartotojas SET" +
                " vardas = '" + vardas + "', pavarde = '" + pavarde + "', gimimo_data = '" + data + "', el_pastas = '" + el + "', adresas = '" + adresas + "', " +
                "darbo_valandos = '" + darbas + "', alga = " + alga + ", parduotuves_adresas = '" + parduotuve + "', " +
                " fk_sandelisid = " + sandelis + ", typeSelector = '" + role + "' WHERE id = " + Form1.id;
            materialSingleLineTextField8.Text = sql;

            materialLabel12.Text = sandelis;
            var cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            this.Close();
        }

        private void materialFlatButton1_Click_1(object sender, EventArgs e)
        {
            double alga = 0;
            DateTime gimimo_data = DateTime.Now;
            if (materialSingleLineTextField1.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite vardą";
                return;
            }
            if (materialSingleLineTextField2.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite pavardę";
                return;
            }
            if (materialSingleLineTextField3.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite Gimimo datą";
                return;
            }
            if (materialSingleLineTextField4.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite Elektroninį paštą";
                return;
            }
            if (materialSingleLineTextField5.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite adresą";
                return;
            }
            if (materialSingleLineTextField8.Text.Length == 0)
            {
                materialLabel12.Text = "Iveskite darbo laika";
                return;
            }

            if (materialSingleLineTextField9.Text.Length == 0)
            {
                materialLabel12.Text = "Iveskite alga";
                return;
            }
            if (materialSingleLineTextField10.Text.Length == 0)
            {
                materialLabel12.Text = "Iveskite parduotuves adresa";
                return;
            }
            bool tryParse = DateTime.TryParse(materialSingleLineTextField3.Text, out gimimo_data);
            if (!tryParse)
            {
                materialLabel12.Text = "Netinkamas datos formatas";
                return;
            };
            tryParse = double.TryParse(materialSingleLineTextField9.Text, out alga);
            if (!tryParse)
            {
                materialLabel12.Text = "Netinkamas algos formatas";
                return;
            }
            string cs = Form1.connection;
            string format = "yyyy-MM-dd";


            string vardas = materialSingleLineTextField1.Text;
            string pavarde = materialSingleLineTextField2.Text;
            string el = materialSingleLineTextField4.Text;
            string adresas = materialSingleLineTextField5.Text;
            string darbas = materialSingleLineTextField8.Text;
            string parduotuve = materialSingleLineTextField10.Text;
            string sandelis = comboBox2.Text;
            string role = comboBox1.Text;


            if (role.Length == 0)
            {
                materialLabel12.Text = "pasirinkite role";
                return;
            }

            string data = gimimo_data.ToString("s");
            data = data.Substring(0, 10);



            var con = new MySqlConnection(cs);
            con.Open();
            var sql = "UPDATE is_vartotojas SET" +
                " vardas = '" + vardas + "', pavarde = '" + pavarde + "', gimimo_data = '" + data + "', el_pastas = '" + el + "', adresas = '" + adresas + "', " +
                "darbo_valandos = '" + darbas + "', alga = " + alga + ", parduotuves_adresas = '" + parduotuve + "', " +
                " fk_sandelisid = " + sandelis + ", typeSelector = '" + role + "' WHERE id = " + Form1.id;
            materialSingleLineTextField8.Text = sql;

            materialLabel12.Text = sandelis;
            var cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            this.Close();
        }
    }
}

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
using System.Security.Cryptography;



namespace WindowsFormsApp1
{
    public partial class Registration : MaterialForm
    {
        public static string connection = @"server=localhost;userid=root;password=;database=itproject";
        private readonly MaterialSkinManager materialSkinManager;
        public Registration()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Prisijungimas";
        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Registration_Load(object sender, EventArgs e)
        {
            //this.FormClosing += new FormClosingEventHandler(Registration_Closing);

            //------------------------------------
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
            //comboBox2.DisplayMember = "adresas";
            comboBox2.DisplayMember = "id_Sandelis";
            comboBox2.DataSource = dt;

            rdr.Close();
            con.Close();
            //---------------------------------------
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = comboBox2.SelectedValue.ToString();
        }

        private void materialSingleLineTextField3_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField2_Click(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            double alga = 0;
            DateTime gimimo_data = DateTime.Now;
            if(materialSingleLineTextField1.Text.Length == 0)
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
            if (materialSingleLineTextField6.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite slapyvardį";
                return;
            }
            if (materialSingleLineTextField7.Text.Length == 0)
            {
                materialLabel12.Text = "Įveskite slaptažodį";
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
            if (!tryParse) {
                materialLabel12.Text = "Netinkamas datos formatas";
                    return; };
            tryParse = double.TryParse(materialSingleLineTextField9.Text, out alga);
            if (!tryParse) { materialLabel12.Text = "Netinkamas algos formatas";
                return; }
            string cs = Form1.connection;


            string vardas = materialSingleLineTextField1.Text;
            string pavarde = materialSingleLineTextField2.Text;
            string el = materialSingleLineTextField4.Text;
            string adresas = materialSingleLineTextField5.Text;
            string slapyvardis = materialSingleLineTextField6.Text;
            string password = materialSingleLineTextField7.Text;
            string darbas = materialSingleLineTextField8.Text;
            string parduotuve = materialSingleLineTextField10.Text;
            string sandelis = comboBox2.Text;
            string role = comboBox1.Text;

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            if (role.Length == 1)
            {
                materialLabel12.Text = "pasirinkite role";
                return;
            }

            var con = new MySqlConnection(cs);
            con.Open();
            var sql = "INSERT INTO is_vartotojas " +
                "( vardas, pavarde, gimimo_data, el_pastas, adresas, slapyvardis, slaptazodis, darbo_valandos, alga, parduotuves_adresas, fk_Sandelisid, typeSelector) " +
                " VALUES " + 
                "( '"+ vardas + "','" + pavarde + "','" + gimimo_data.ToString("yyyy-MM-dd") + "','" + el + "', '" + adresas +"', '" + slapyvardis + "', '" +
                 savedPasswordHash + "','" + darbas + "'," + alga + ",'" +parduotuve+"'," +sandelis + ",'" + role+"' )";

            textBox1.Text = sql;
            materialLabel12.Text =sandelis;
            var cmd = new MySqlCommand(sql, con);
            try
            {
                int numberOfDeleted = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                materialLabel4.Text = "Nepavyko sukurti vartotojo. Naudokite kita vartotojo varda";
                //error = 1;
            }
            //cmd.ExecuteNonQuery();
            con.Close();
            this.Close();
        }

        private void materialLabel8_Click(object sender, EventArgs e)
        {
            
        }

        private void materialLabel13_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField10_Click(object sender, EventArgs e)
        {

        }
    }
    
}

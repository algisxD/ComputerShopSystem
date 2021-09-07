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
    public partial class Administrator_changeRoleForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Administrator_changeRoleForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            this.Text = "Roles pakeitimas";
        }

        private void Administrator_changeRoleForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Administrator_changeRoleForm_Closing);
        }

        private void Administrator_changeRoleForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string role = comboBox1.Text;
            string cs = Form1.connection;
            var con = new MySqlConnection(cs);
            con.Open();
            var sql = "UPDATE is_vartotojas SET " +
                "typeSelector = '" + role + "' WHERE id = " + Form1.selectid;
            label1.Text = sql;

            var cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            this.Close();

            Form accountedit = new UserList();
            this.Close();
            accountedit.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synergy
{
    public partial class login : Form
    {
        encryption encrypt = new encryption();
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "User Name";
            txtPwd.Text = "Password";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void login_FontChanged(object sender, EventArgs e)
        {

        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                Login();
            }
        }

        int adminID;

        public bool True { get; private set; }

        private void Login()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(Connection.connection_string);
                string select = "select * from admin where username = @name and password = @pwd;";
                MySqlCommand cmd = new MySqlCommand(select, connection);
                cmd.Parameters.AddWithValue("@name", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@pwd", encrypt.passHash(txtPwd.Text.Trim()));

                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    string query = "select admin_id from admin where username = @user;";
                    MySqlCommand cmd1 = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                        
                    adminID = int.Parse(reader[0].ToString());
                    //MessageBox.Show("Welcome " + txtUsername.Text, "Logged", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Dashboard dash = new Dashboard();
                    dash.Admin_id = adminID;
                    dash.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect Credentials", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Retrieve Error " + ex, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "User Name")
            {
                txtUsername.Text = "";

                txtUsername.ForeColor = Color.Black;
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.Text = "User Name";

                txtUsername.ForeColor = Color.Silver;
            }
        }

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            if (txtPwd.Text == "Password")
            {
                txtPwd.Text = "";
                txtPwd.UseSystemPasswordChar = true;
                txtPwd.ForeColor = Color.Black;
            }
        }

        private void txtPwd_Leave(object sender, EventArgs e)
        {
            if (txtPwd.Text == "")
            {
                txtPwd.Text = "Password";
                txtPwd.UseSystemPasswordChar = false;
                txtPwd.ForeColor = Color.Silver;
            }
        }

        private void txtUsername_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void txtUsername_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void txtPwd_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}

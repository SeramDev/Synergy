using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synergy
{
    public partial class Admin : Form
    {
        encryption encrypt = new encryption();
        public Admin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!txtFname.Text.Equals("") || !txtLname.Text.Equals("") || !txtEmail.Text.Equals("") || !txtNic.Text.Equals("") || !txtTel.Text.Equals("") || !txtUsername.Text.Equals("") || !txtPassword.Text.Equals(""))
            {
                if (string.IsNullOrEmpty(picpath))
                {
                    MySqlConnection conn = new MySqlConnection(Connection.connection_string);
                    string query = "UPDATE admin SET admin_fname =@fname, admin_lname = @lname, admin_email = @email, admin_nic = @nic, admin_tel = @tel, username = @username, password = @password WHERE admin_id  = @id LIMIT 1";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    try
                    {
                        command.Parameters.AddWithValue("@id", update_id);
                        command.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                        command.Parameters.AddWithValue("@lname", txtLname.Text.Trim());
                        command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@nic", txtNic.Text.Trim());
                        command.Parameters.AddWithValue("@tel", txtTel.Text.Trim());
                        command.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        command.Parameters.AddWithValue("@password", encrypt.passHash(txtPassword.Text.Trim()));
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        conn.Close();
                        fill_data();
                        if (result < 0)
                        {
                            MessageBox.Show("Update Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Successfully Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear_all();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data Update Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MySqlConnection conn = new MySqlConnection(Connection.connection_string);
                    string query = "UPDATE admin SET admin_fname = @fname, admin_lname = @lname, admin_email = @email, admin_nic = @nic, admin_tel = @tel, username = @username, password = @password, image = @image WHERE admin_id  = @id LIMIT 1";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    try
                    {
                        byte[] imagebt = null;
                        FileStream fs = new FileStream(picpath, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        imagebt = br.ReadBytes((int)fs.Length);
                        command.Parameters.AddWithValue("@id", update_id);
                        command.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                        command.Parameters.AddWithValue("@lname", txtLname.Text.Trim());
                        command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@nic", txtNic.Text.Trim());
                        command.Parameters.AddWithValue("@tel", txtTel.Text.Trim());
                        command.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        command.Parameters.AddWithValue("@password", encrypt.passHash(txtPassword.Text.Trim()));
                        command.Parameters.AddWithValue("@image", imagebt);
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        conn.Close();
                        fill_data();
                        if (result < 0)
                        {
                            MessageBox.Show("Update Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Successfully Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear_all();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data Update Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill valid values for all the feilds", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!txtFname.Text.Equals("") || !txtLname.Text.Equals("") || !txtEmail.Text.Equals("") || !txtTel.Text.Equals("") || !txtUsername.Text.Equals("") || !txtPassword.Text.Equals(""))
            {
                MySqlConnection connection = new MySqlConnection(Connection.connection_string);
                string query = "INSERT INTO admin (admin_fname, admin_lname, admin_email, admin_nic, admin_tel, username, password, image, register_date) VALUES (@fname, @lname, @email, @nic, @tel, @username, @password, @image, @date)";
                MySqlCommand command = new MySqlCommand(query, connection);
                try
                {
                    byte[] imagebt = null;
                    FileStream fs = new FileStream(picpath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imagebt = br.ReadBytes((int)fs.Length);
                    DateTime today = DateTime.Today;
                    command.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                    command.Parameters.AddWithValue("@lname", txtLname.Text.Trim());
                    command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    command.Parameters.AddWithValue("@nic", txtNic.Text.Trim());
                    command.Parameters.AddWithValue("@tel", txtTel.Text.Trim());
                    command.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    command.Parameters.AddWithValue("@password", encrypt.passHash(txtPassword.Text.Trim()));
                    command.Parameters.AddWithValue("@image", imagebt);
                    command.Parameters.AddWithValue("@date", today);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    fill_data();
                    if (result < 0)
                    {
                        MessageBox.Show("Insert Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Successfully Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear_all();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Insert Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill valid data for all the feilds", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void clear_all()
        {
            txtFname.Clear();
            txtLname.Clear();
            txtEmail.Clear();
            txtNic.Clear();
            txtTel.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            picbox.Image = null;
        }

        string picpath;
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                picbox.Image = Image.FromFile(opf.FileName);
                picpath = opf.FileName.ToString();
            }
            button1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!txtFname.Text.Equals(""))
            {
                MySqlConnection conn = new MySqlConnection(Connection.connection_string);
                string query = "SELECT * FROM admin WHERE admin_fname= @fname LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                conn.Open();
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                try
                {
                    txtFname.Text = reader[1].ToString();
                    txtLname.Text = reader[2].ToString();
                    txtEmail.Text = reader[3].ToString();
                    txtNic.Text = reader[4].ToString();
                    txtTel.Text = "0"+reader[5].ToString();
                    txtUsername.Text = reader[6].ToString();
                    txtPassword.Text = reader[7].ToString();
                    picbox.Image = Image.FromStream(new MemoryStream((byte[])reader[8]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Insert Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Please enter first name for search", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = MessageBox.Show("are you sure want to delete?", "Delete", MessageBoxButtons.YesNo);
            if (dlgresult == DialogResult.Yes)
            {

                if (!txtFname.Text.Equals(""))
                {
                    MySqlConnection conn = new MySqlConnection(Connection.connection_string);
                    string query = "SELECT admin_id FROM admin WHERE admin_fname= @fname LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int id = Convert.ToInt32(reader[0].ToString());
                    if (id==1)
                    {
                        MessageBox.Show("Administrator account can not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MySqlConnection con = new MySqlConnection(Connection.connection_string);
                        string query2 = "DELETE FROM admin WHERE admin_fname= @fname LIMIT 1";
                        MySqlCommand cmd2 = new MySqlCommand(query2, con);
                        try
                        {
                            cmd2.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                            con.Open();
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Successfully Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear_all();
                            fill_data();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Data Delete Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please select user for delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            fill_data();
        }
        DataTable dt = new DataTable();
        private void fill_data()
        {
            MySqlConnection connection = new MySqlConnection(Connection.connection_string);
            string query = "select * from admin";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter sqldata = new MySqlDataAdapter();
            sqldata.SelectCommand = cmd;
            dt = new DataTable();
            sqldata.Fill(dt);
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            Grid.DataSource = bs;
        }

        int update_id;
        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                Grid.CurrentRow.Selected = true;
                update_id = int.Parse(Grid.Rows[e.RowIndex].Cells["admin_id"].FormattedValue.ToString());
                txtFname.Text = Grid.Rows[e.RowIndex].Cells["admin_fname"].FormattedValue.ToString();
                txtLname.Text = Grid.Rows[e.RowIndex].Cells["admin_lname"].FormattedValue.ToString();
                txtEmail.Text = Grid.Rows[e.RowIndex].Cells["admin_email"].FormattedValue.ToString();
                txtNic.Text = Grid.Rows[e.RowIndex].Cells["admin_nic"].FormattedValue.ToString();
                txtTel.Text = "0"+Grid.Rows[e.RowIndex].Cells["admin_tel"].FormattedValue.ToString();
                txtUsername.Text = Grid.Rows[e.RowIndex].Cells["username"].FormattedValue.ToString();
                txtPassword.Text = Grid.Rows[e.RowIndex].Cells["password"].FormattedValue.ToString();

                byte[] img = (byte[])Grid.CurrentRow.Cells["image"].Value;
                picbox.Image = Image.FromStream(new MemoryStream(img));
            }
        }

        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = string.Format("admin_fname Like '%{0}%'", textBox1.Text);
                Grid.DataSource = dv;
            }
            catch
            {

            }
        }
    }
}

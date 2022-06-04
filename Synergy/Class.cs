using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synergy
{
    public partial class Class : Form
    {
        public Class()
        {
            InitializeComponent();
        }
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        DataTable dt = new DataTable();
        private void class_create_Load(object sender, EventArgs e)
        {
            fill_table();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtYear.Text))
            {
                MessageBox.Show("Please fill all fields with valid data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(Connection.connection_string);
                string query = "INSERT INTO class (class_name, year, created_admin_id) VALUES (@name, @year, @id)";
                MySqlCommand command = new MySqlCommand(query, connection);
                try
                {
                    command.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    command.Parameters.AddWithValue("@year", txtYear.Text.Trim());
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    fill_table();
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
        }

        private void clear_all()
        {
            txtName.Clear();
            txtYear.Clear();
            
        }

        private void fill_table()
        {
            MySqlConnection connection = new MySqlConnection(Connection.connection_string);
            string query = "select * from class";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter sqldata = new MySqlDataAdapter();
            sqldata.SelectCommand = cmd;
            dt = new DataTable();
            sqldata.Fill(dt);
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
        }

        int class_id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                class_id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["class_id"].FormattedValue.ToString());
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["class_name"].FormattedValue.ToString();
                txtYear.Text = dataGridView1.Rows[e.RowIndex].Cells["year"].FormattedValue.ToString();
                   
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = string.Format("class_name Like '%{0}%'", txtSerach.Text);
                dataGridView1.DataSource = dv;
            }
            catch
            {

            }
        }

        private void txtUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) && string.IsNullOrEmpty(txtYear.Text))
            {
                MessageBox.Show("Please select a record first to update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(Connection.connection_string);
                string query = "UPDATE class SET class_name = @name, year = @year WHERE class_id = @id LIMIT 1";
                MySqlCommand command = new MySqlCommand(query, connection);
                try
                {
                    command.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    command.Parameters.AddWithValue("@year", txtYear.Text.Trim());
                    command.Parameters.AddWithValue("@id", class_id);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    fill_table();
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

        private void txtDelete_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = MessageBox.Show("are you sure want to delete?", "Delete", MessageBoxButtons.YesNo);
            if (dlgresult == DialogResult.Yes)
            {
                if (!txtName.Text.Equals(""))
                {
                    MySqlConnection conn = new MySqlConnection(Connection.connection_string);
                    string query = "DELETE FROM class WHERE class_id = @id LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", class_id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear_all();
                        fill_table();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data Delete Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Please select class for delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
    }

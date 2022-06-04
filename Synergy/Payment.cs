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
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        private int adm_id;

        public int ID
        {
            get { return adm_id; }
            set { adm_id = value; }
        }

        string method = "";
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            method = "Cash";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            method = "Bank Transfer";
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            load_cmb();
        }

        private void load_cmb()
        {
            MySqlConnection connection = new MySqlConnection(Connection.connection_string);
            string query = "select * from class";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string c_name = reader.GetString("class_name");
                    comboBox1.Items.Add(c_name);
                }
                connection.Close();
            }
            catch
            {

            }
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_student_cmb();
        }

        private void load_student_cmb()
        {
            MySqlConnection connection = new MySqlConnection(Connection.connection_string);
            string query = "SELECT * FROM student WHERE class_name = '"+comboBox1.Text+"'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                Dictionary<int, string> test = new Dictionary<int, string>();
                while (reader.Read())
                {
                    string fname = reader.GetString("std_fname");
                    string lname = reader.GetString("std_lname");
                    int std_id = reader.GetInt32("student_id");
                    test.Add(std_id, fname + " " + lname);
                }
                cmbStd.DataSource = new BindingSource(test, null);
                connection.Close();
            }
            catch
            {

            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(cmbStd.Text) || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(txtAmount.Text) || string.IsNullOrEmpty(method))
            {
                MessageBox.Show("Please fill all fields with valid data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                MySqlConnection connection = new MySqlConnection(Connection.connection_string);
                string query = "INSERT INTO payment (payment_date, amount, payment_method, student_id, admin_id,class_id) VALUES (@date, @amount, @method, @std_id, @admin_id,@class_id)";
                MySqlCommand command = new MySqlCommand(query, connection);
                try
                {
                    command.Parameters.AddWithValue("@date", dateTimePicker1.Text);
                    command.Parameters.AddWithValue("@amount", txtAmount.Text.Trim());
                    command.Parameters.AddWithValue("@method", method);
                    command.Parameters.AddWithValue("@std_id", ((KeyValuePair<int, string>)cmbStd.SelectedItem).Key);
                    command.Parameters.AddWithValue("@admin_id", adm_id);
                    command.Parameters.AddWithValue("@class_id", comboBox1.Text);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();                  
                    if (result < 0)
                    {
                        MessageBox.Show("Payment Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Payment Approved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dateTimePicker1.Text = null; ;
            txtAmount.Clear();
            comboBox1.Text = null;
            cmbStd.Text = null;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_student_cmb();
        }
    }
}

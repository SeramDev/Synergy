using MySql.Data.MySqlClient;
using Synergy.rpt;
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
    public partial class Payment_report : Form
    {
        public Payment_report()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        DataTable dataset;
        private void button1_Click(object sender, EventArgs e)
        {;
            MySqlConnection mycon = new MySqlConnection(Connection.connection_string);
            MySqlCommand cmd = new MySqlCommand("Select student.std_fname ,student.std_lname, payment.payment_method,payment.amount , payment.payment_date from payment inner join student on payment.student_id = student.student_id  where payment.class_id = '" + comboBox1.Text + "' and payment_date between '"+dateTimePicker1.Text+"' and '"+dateTimePicker2.Text+"' ;", mycon);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                dataset = new DataTable();
                sda.Fill(dataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dataset);

                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Payment_report_Load(object sender, EventArgs e)
        {
            loadd_class_cmb();
        }

        private void loadd_class_cmb()
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

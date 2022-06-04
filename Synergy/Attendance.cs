using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Synergy
{
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                gte_std_data();
            }
        }

        private void save_data()
        {
            DateTime date = DateTime.Now;
            string time = DateTime.Now.ToString("h:mm:ss tt");
            MySqlConnection con = new MySqlConnection(Connection.connection_string);
            string query = "INSERT INTO student_attendence(date,time,student_id,class_name)VALUES (@date, @time, @std, @class) ";
            MySqlCommand cmd = new MySqlCommand(query,con);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@std", std_id);
            cmd.Parameters.AddWithValue("@class", class_name);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result < 0)
            {
                MessageBox.Show("Insert Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Attended", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
            }
        }

        int std_id;
        string class_name;
        private void gte_std_data()
        {
            MySqlConnection con = new MySqlConnection(Connection.connection_string);
            string query = "select * from student where student_id = @id";
            MySqlCommand cmd = new MySqlCommand(query,con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text.Trim());
            con.Open();
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                try
                {
                    std_id = int.Parse(reader.GetString("student_id"));
                    class_name = reader.GetString("class_name");

                    //MessageBox.Show(class_name.ToString());
                    save_data();
                }
                catch
                {
                    MessageBox.Show("Insert Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                }
            }
            else
            {
                MessageBox.Show("No Record Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Attendance_Load(object sender, EventArgs e)
        {

        }
    }
}

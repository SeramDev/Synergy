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
    public partial class Quickscan : Form
    {
        public Quickscan()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                MySqlConnection con = new MySqlConnection(Connection.connection_string);
                string query = "select * from student where student_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", textBox1.Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    try
                    {
                        pictureBox1.Image = null;
                        string fname = reader.GetString("std_fname");
                        string lname = reader.GetString("std_lname");
                        textBox3.Text = fname + " " + lname;
                        textBox4.Text = reader.GetString("std_email");
                        textBox5.Text = "0"+reader.GetString("std_tel");
                        textBox6.Text = reader.GetString("std_address");
                        textBox7.Text = reader.GetString("guardian_name");
                        textBox8.Text = "0"+reader.GetString("guardian_tel");
                        textBox2.Text = reader.GetString("admission_date");
                        MemoryStream ms = new MemoryStream((byte[])reader["image"]);
                        pictureBox1.Image = new Bitmap(ms);
                    }
                    catch
                    {
                        pictureBox1.Image = null;
                        string fname = reader.GetString("std_fname");
                        string lname = reader.GetString("std_lname");
                        textBox3.Text = fname + " " + lname;
                        textBox4.Text = reader.GetString("std_email");
                        textBox5.Text = "0"+reader.GetString("std_tel");
                        textBox6.Text = reader.GetString("std_address");
                        textBox7.Text = reader.GetString("guardian_name");
                        textBox8.Text = "0"+reader.GetString("guardian_tel");
                        textBox2.Text = reader.GetString("admission_date");
                    }
                }
                else
                {
                    clear_all();
                    MessageBox.Show("No Record Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student std = new Student();
            this.Hide();
            std.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Payment payObj = new Payment();
            this.Hide();
            payObj.Show();
        }

        private void clear_all()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            pictureBox1.Image = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

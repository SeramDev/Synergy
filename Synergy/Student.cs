using MySql.Data.MySqlClient;
using Synergy.rpt;
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
    public partial class Student : Form
    {
        public Student()
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
        private void Student_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        int std_id;
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(picpath))
            {
                if (!txtFname.Text.Equals("") || !txtLname.Text.Equals("") || !txtEmail.Text.Equals("") || !txtNic.Text.Equals("") || !txtTel.Text.Equals("") || !txtAddress.Text.Equals("") || !txtGurdian.Text.Equals("") || !txtGurdianNo.Text.Equals(""))
                {
                    MySqlConnection connection = new MySqlConnection(Connection.connection_string);
                    string query = "INSERT INTO student (std_fname, std_lname, std_email, std_nic, std_tel, std_address, guardian_name, guardian_tel, created_admin_id, admission_date, class_name) VALUES (@fname, @lname, @email, @nic, @tel, @address, @guardian, @guardian_tel, @admin, @admission_date, @class_name)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    try
                    {
                        DateTime today = DateTime.Today;
                        DateTime date = today.Date;
                        command.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                        command.Parameters.AddWithValue("@lname", txtLname.Text.Trim());
                        command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@nic", txtNic.Text.Trim());
                        command.Parameters.AddWithValue("@tel", txtTel.Text.Trim());
                        command.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                        command.Parameters.AddWithValue("@guardian", txtGurdian.Text.Trim());
                        command.Parameters.AddWithValue("@guardian_tel", txtGurdianNo.Text.Trim());
                        command.Parameters.AddWithValue("@admin", id);
                        command.Parameters.AddWithValue("@admission_date", date);
                        command.Parameters.AddWithValue("@class_name", cmbClass.Text);
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        std_id = (int)command.LastInsertedId;
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
                else
                {
                    MessageBox.Show("Please fill valid data for all the feilds", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (!txtFname.Text.Equals("") || !txtLname.Text.Equals("") || !txtEmail.Text.Equals("") || !txtNic.Text.Equals("") || !txtTel.Text.Equals("") || !txtAddress.Text.Equals("") || !txtGurdian.Text.Equals("") || !txtGurdianNo.Text.Equals(""))
                {
                    MySqlConnection connection = new MySqlConnection(Connection.connection_string);
                    string query = "INSERT INTO student (std_fname, std_lname, std_email, std_nic, std_tel, std_address, guardian_name, guardian_tel, created_admin_id, admission_date, image, class_name) VALUES (@fname, @lname, @email, @nic, @tel, @address, @guardian, @guardian_tel, @admin, @admission_date, @image, @class_name)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    try
                    {
                        byte[] imagebt = null;
                        FileStream fs = new FileStream(picpath, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        imagebt = br.ReadBytes((int)fs.Length);

                        DateTime today = DateTime.Today;
                        DateTime date = today.Date;
                        command.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                        command.Parameters.AddWithValue("@lname", txtLname.Text.Trim());
                        command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@nic", txtNic.Text.Trim());
                        command.Parameters.AddWithValue("@tel", txtTel.Text.Trim());
                        command.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                        command.Parameters.AddWithValue("@guardian", txtGurdian.Text.Trim());
                        command.Parameters.AddWithValue("@guardian_tel", txtGurdianNo.Text.Trim());
                        command.Parameters.AddWithValue("@admin", id);
                        command.Parameters.AddWithValue("@admission_date", date);
                        command.Parameters.AddWithValue("@image", imagebt);
                        command.Parameters.AddWithValue("@class_name", cmbClass.Text);
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
                else
                {
                    MessageBox.Show("Please fill valid data for all the feilds", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void clear_all()
        {
            txtFname.Clear();
            txtLname.Clear();
            txtEmail.Clear();
            txtNic.Clear();
            txtTel.Clear();
            txtAddress.Clear();
            txtGurdian.Clear();
            txtGurdianNo.Clear();
            picbox2.Image = null;
            cmbClass.Text = null;
        }

        string picpath;
        private void button5_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Choose Image(*.JPG;*.JPEG;*.PNG;*.GIF)|*.jpg;*.jpeg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                picbox2.Image = Image.FromFile(opf.FileName);
                picpath = opf.FileName.ToString();
            }
            button1.Enabled = true;
        }

        private void Student_Load(object sender, EventArgs e)
        {
            fill_table();
            load_cmb();
        }

        private void load_cmb()
        {
            MySqlConnection connection = new MySqlConnection(Connection.connection_string);
            string query = "select * from class";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string c_name = reader.GetString("class_name");
                    cmbClass.Items.Add(c_name);
                }
                connection.Close();
            }
            catch
            {

            }
        }

        private void fill_table()
        {
            MySqlConnection connection = new MySqlConnection(Connection.connection_string);
            string query = "select * from student";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            MySqlDataAdapter sqldata = new MySqlDataAdapter();
            sqldata.SelectCommand = cmd;
            dt = new DataTable();
            sqldata.Fill(dt);
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!txtFname.Text.Equals("") || !txtLname.Text.Equals("") || !txtEmail.Text.Equals("") || !txtNic.Text.Equals("") || !txtTel.Text.Equals("") || !txtAddress.Text.Equals("") || !txtGurdian.Text.Equals("") || !txtGurdianNo.Text.Equals(""))
            {
                if (string.IsNullOrEmpty(picpath))
                {
                    MySqlConnection conn = new MySqlConnection(Connection.connection_string);
                    string query = "UPDATE student SET std_fname = @fname,std_lname = @lname, std_email = @email, std_nic = @nic, std_tel = @tel, std_address = @address, guardian_name = @gname, guardian_tel =@gtel, class_name =@class  WHERE student_id = @id LIMIT 1";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    try
                    {
                        command.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                        command.Parameters.AddWithValue("@lname", txtLname.Text.Trim());
                        command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@nic", txtNic.Text.Trim());
                        command.Parameters.AddWithValue("@tel", txtTel.Text.Trim());
                        command.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                        command.Parameters.AddWithValue("@gname", txtGurdian.Text.Trim());
                        command.Parameters.AddWithValue("@gtel", txtGurdianNo.Text.Trim());
                        command.Parameters.AddWithValue("@class", cmbClass.Text);
                        command.Parameters.AddWithValue("@id", update_id);
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        conn.Close();
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
                else
                {
                    MySqlConnection conn = new MySqlConnection(Connection.connection_string);
                    string query = "UPDATE student SET std_fname = @fname,std_lname = @lname, std_email = @email, std_nic = @nic, std_tel = @tel, std_address = @address, guardian_name = @gname, guardian_tel = @gtel, image = @image, class_name =@class WHERE student_id = @id LIMIT 1";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    try
                    {
                        byte[] imagebt = null;
                        FileStream fs = new FileStream(picpath, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        imagebt = br.ReadBytes((int)fs.Length);

                        command.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                        command.Parameters.AddWithValue("@lname", txtLname.Text.Trim());
                        command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@nic", txtNic.Text.Trim());
                        command.Parameters.AddWithValue("@tel", txtTel.Text.Trim());
                        command.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                        command.Parameters.AddWithValue("@gname", txtGurdian.Text.Trim());
                        command.Parameters.AddWithValue("@gtel", txtGurdianNo.Text.Trim());
                        command.Parameters.AddWithValue("@class", cmbClass.Text);
                        command.Parameters.AddWithValue("@id", update_id);
                        command.Parameters.AddWithValue("@image", imagebt);
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        conn.Close();
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
            else
            {
                MessageBox.Show("Please fill valid values for all the feilds", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        int update_id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["image"].Value is System.DBNull)
                {
                    dataGridView1.CurrentRow.Selected = true;
                    picbox2.Image = null;
                    update_id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["student_id"].FormattedValue.ToString());
                    txtFname.Text = dataGridView1.Rows[e.RowIndex].Cells["std_fname"].FormattedValue.ToString();
                    txtLname.Text = dataGridView1.Rows[e.RowIndex].Cells["std_lname"].FormattedValue.ToString();
                    txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells["std_email"].FormattedValue.ToString();
                    txtNic.Text = dataGridView1.Rows[e.RowIndex].Cells["std_nic"].FormattedValue.ToString();
                    txtTel.Text = "0"+dataGridView1.Rows[e.RowIndex].Cells["std_tel"].FormattedValue.ToString();
                    txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["std_address"].FormattedValue.ToString();
                    txtGurdian.Text = dataGridView1.Rows[e.RowIndex].Cells["guardian_name"].FormattedValue.ToString();
                    txtGurdianNo.Text = "0"+dataGridView1.Rows[e.RowIndex].Cells["guardian_tel"].FormattedValue.ToString();
                    cmbClass.Text = dataGridView1.Rows[e.RowIndex].Cells["class_name"].FormattedValue.ToString();
                }
                else
                {
                    dataGridView1.CurrentRow.Selected = true;
                    update_id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["student_id"].FormattedValue.ToString());
                    txtFname.Text = dataGridView1.Rows[e.RowIndex].Cells["std_fname"].FormattedValue.ToString();
                    txtLname.Text = dataGridView1.Rows[e.RowIndex].Cells["std_lname"].FormattedValue.ToString();
                    txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells["std_email"].FormattedValue.ToString();
                    txtNic.Text = dataGridView1.Rows[e.RowIndex].Cells["std_nic"].FormattedValue.ToString();
                    txtTel.Text = "0"+dataGridView1.Rows[e.RowIndex].Cells["std_tel"].FormattedValue.ToString();
                    txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["std_address"].FormattedValue.ToString();
                    txtGurdian.Text = dataGridView1.Rows[e.RowIndex].Cells["guardian_name"].FormattedValue.ToString();
                    txtGurdianNo.Text = "0"+dataGridView1.Rows[e.RowIndex].Cells["guardian_tel"].FormattedValue.ToString();
                    cmbClass.Text = dataGridView1.Rows[e.RowIndex].Cells["class_name"].FormattedValue.ToString();
                    byte[] img = (byte[])dataGridView1.CurrentRow.Cells["image"].Value;
                    picbox2.Image = Image.FromStream(new MemoryStream(img));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = MessageBox.Show("are you sure want to delete?", "Delete", MessageBoxButtons.YesNo);
            if(dlgresult == DialogResult.Yes)
            {
                if (!txtFname.Text.Equals(""))
                {
                    MySqlConnection conn = new MySqlConnection(Connection.connection_string);
                    string query = "DELETE FROM student WHERE student_id = @id LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", update_id);
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
                    MessageBox.Show("Please select user for delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = string.Format("std_fname Like '%{0}%'",textBox1.Text);
                dataGridView1.DataSource = dv;
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            MySqlConnection mycon = new MySqlConnection(Connection.connection_string);
            MySqlCommand cmd;
            MySqlDataAdapter dr;
            try
            {
                mycon.Open();
                DataTable dt = new DataTable();
                cmd = new MySqlCommand("select * from student where student_id ='"+update_id+"' ", mycon);
                dr = new MySqlDataAdapter(cmd);
                dr.Fill(dt);
                mycon.Close();

                // Bill_rpt cr2 = new Bill_rpt();
                ID_rpt cr2 = new ID_rpt();
                cr2.Database.Tables["student"].SetDataSource(dt);
                this.crystalReportViewer1.ReportSource = cr2;
                cr2.SetParameterValue("sign", "C:\\Users\\Sachin\\source\\repos\\Synergy\\sign.png");
                cr2.SetParameterValue("logo", "C:\\Users\\Sachin\\source\\repos\\Synergy\\logo.png");
                //cr2.PrintToPrinter(1, false, 0, 0);

            }
            catch
            {

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}

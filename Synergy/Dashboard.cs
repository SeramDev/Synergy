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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        static Form admObj;
        private int admin_id;

        public int Admin_id
        {
            get { return admin_id; }
            set { admin_id = value; }
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (admObj == null)
            {
                admObj = new Admin();
                admObj.MdiParent = this;
                admObj.FormClosed += new FormClosedEventHandler(admObjClearReference);
                admObj.Show();
            }
            else
            {
                admObj.Focus();
            }
        }

        private void admObjClearReference(object sender, FormClosedEventArgs e)
        {
            admObj = null;
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = MessageBox.Show("are you sure want to logout?", "Logout", MessageBoxButtons.YesNo);
            if (dlgresult == DialogResult.Yes)
            {
                string path = "C:\\Users\\sachi\\OneDrive\\Synergy_DB\\backup.sql";
                string connectionString = "datasource=localhost;port=3306;username=root;password=;database=ict_db; convert zero datetime = true;";
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = con;
                            con.Open();
                            mb.ExportToFile(path);
                            con.Close();
                            MessageBox.Show("Backup Compleated");
                        }
                    }
                }
                login loginObj = new login();
                loginObj.Show();
                this.Hide();
            }
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student std = new Student();
            std.MdiParent = this;
            std.ID = admin_id;
            std.Show();
        }

        private void stdObjClearReference(object sender, FormClosedEventArgs e)
        {
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        static Form attObj;
        private void attendenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (attObj == null)
            {
                attObj = new Attendance();
                attObj.MdiParent = this;
                attObj.FormClosed += new FormClosedEventHandler(attObjClearReference);
                attObj.Show();
            }
            else
            {
                attObj.Focus();
            }
        }

        private void attObjClearReference(object sender, FormClosedEventArgs e)
        {
            attObj = null;
        }

        static Form payObj;
        private void paymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Payment payObj = new Payment();
            payObj.MdiParent = this;
            payObj.ID = admin_id;
            payObj.Show();
        }

        private void payObjClearReference(object sender, FormClosedEventArgs e)
        {
            payObj = null;
        }

        static Form rptObj;
        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rptObj == null)
            {
                rptObj = new Payment_report();
                rptObj.MdiParent = this;
                rptObj.FormClosed += new FormClosedEventHandler(rptObjClearReference);
                rptObj.Show();
            }
            else
            {
                rptObj.Focus();
            }
        }

        private void rptObjClearReference(object sender, FormClosedEventArgs e)
        {
            rptObj = null;
        }

        private void createClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Class clzObj = new Class();
            clzObj.MdiParent = this;
            clzObj.ID = admin_id;
            clzObj.Show();
        }

        private void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quickscan quickObj = new Quickscan();
            quickObj.MdiParent = this;
            quickObj.Show();
        }
    }
}

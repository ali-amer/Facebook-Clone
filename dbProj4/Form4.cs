using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbProj4
{
    public partial class Form4 : Form
    {
        sqlOperations_class query = new sqlOperations_class();
        public Form4()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string password = textBox2.Text;
            DataSet ds = new DataSet();

            ds = query.search_User(userName, password);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Invalid Username or Password");
            }
            else
            {
                this.Hide();
                ActiveUser_Form ActiveUser = new ActiveUser_Form(ds);
                ActiveUser.Show();
            }
        }

        private void empName(object sender, EventArgs e)
        {
            textBox1.Text = ("");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void empPass(object sender, EventArgs e)
        {
            textBox2.Text = ("");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            //Form1 frm1 = new Form1();
            //frm1.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}

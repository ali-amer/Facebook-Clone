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
    public partial class Form1 : Form
    {
        sqlOperations_class query = new sqlOperations_class();
        string Fname, Lname, Passw, Email, DOB, Gender = "";

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Female";
        }

        private void bunifuMaterialTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Male";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fname = bunifuMaterialTextbox1.Text;
            Lname = bunifuMaterialTextbox2.Text;
            Email = bunifuMaterialTextbox4.Text;
            Passw = bunifuMaterialTextbox5.Text;
            DOB = bunifuMaterialTextbox3.Text;
            if (Fname == "" || Lname == "" || Email == "" || Passw == "" || DOB == "" || Gender == "")
            {
                MessageBox.Show("Please Fill all the Fields!");
            }
            else
            {
                bool status = query.register_user(Fname, Lname, Passw, Email, DOB, Gender);
                if (status)
                {
                    MessageBox.Show("Your account has been registered successfully!!");
                    this.Close();
                    Form4 form4 = new Form4();
                    form4.Show();
                }
                else
                {
                    MessageBox.Show("Cannot Register the Account..\n This GMAIL/Password has already been used");
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void bunifuMaterialTextbox4_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace dbProj4
{
    public partial class ActiveUser_Form : Form
    {
        sqlOperations_class query = new sqlOperations_class();
        DataSet DatSet = new DataSet();
        PictureBox[] picBoxes;

        public ActiveUser_Form(DataSet ds)
        {
            //var picture = new PictureBox
            //{
            //    Name = "pictureBox",
            //    Size = new Size(121, 88),
            //    Location = new Point(100, 100),
            //    Image = Image.FromFile("C:\\Users\\M Ibtisam\\Desktop\\Proj pics\\profIbti3.jpg"),

            //};
            //picture.SizeMode= PictureBoxSizeMode.Zoom;
            //this.Controls.Add(picture);

            InitializeComponent();
            DatSet = ds;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PictureData"].ToString() != "")
                {
                    MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["PictureData"]);
                    pictureBox2.Image = new Bitmap(ms);
                }

                string profName = ds.Tables[0].Rows[0]["First_Name"].ToString() + " " + ds.Tables[0].Rows[0]["Last_Name"].ToString();
                label2.Text = profName;
                string mail = ds.Tables[0].Rows[0]["Email"].ToString();
                label3.Text = mail;
                PictureBox[] picBoxes1 = { picBox1, picBox2, picBox3, picBox4, picBox5, picBox6, picBox7, picBox8, picBox9, picBox10, picBox11, picBox12, picBox13, picBox14, picBox15 };
                picBoxes = picBoxes1;
                int ID = (int)DatSet.Tables[0].Rows[0]["ID"];

                DataSet postData = query.search_User_Post(ID);

                fill_Prof_Posts(postData);

            }
        }
        private bool IsNullOrEmpty(PictureBox pb)
        {
            return pb == null || pb.Image == null;
        }

        private void ActiveUser_Form_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            User_Newsfeed ActiveUser = new User_Newsfeed(DatSet);
            ActiveUser.Show();
        }
        private void fill_Prof_Posts(DataSet postData)
        {
            int rowsCount = postData.Tables[0].Rows.Count;

            for (int i = 0; i < picBoxes.Length; i++)
            {
                picBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;
                if (rowsCount > 0)
                {
                    if (postData.Tables[0].Rows[i]["Post_Data"].ToString() != "")
                    {
                        rowsCount--;
                        MemoryStream ms = new MemoryStream((byte[])postData.Tables[0].Rows[i]["Post_Data"]);
                        picBoxes[i].Image = new Bitmap(ms);
                    }
                }

                else
                {
                    break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string imageLocation;
            //   PictureBox[] picBoxes = { picBox1, picBox2, picBox3, picBox4, picBox5, picBox6, picBox7, picBox8, picBox9, picBox10, picBox11, picBox12, picBox13, picBox14, picBox15 };
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png| All Files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;



                    string filePath = "";
                    if (imageLocation.Contains(@"\"))
                    {
                        filePath = imageLocation.Substring(0, imageLocation.LastIndexOf(@"\")).TrimEnd();
                    }
                    string[] parts = imageLocation.Split('\\');
                    string File_Name = parts[parts.Length - 1];
                    int ID = (int)DatSet.Tables[0].Rows[0]["ID"];
                    query.Upload_Profile(filePath, File_Name, ID);

                    string userName = DatSet.Tables[0].Rows[0]["Email"].ToString();
                    string password = DatSet.Tables[0].Rows[0]["Password"].ToString();

                    DatSet = query.search_User(userName, password);
                    if (DatSet.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("Could not Update Profile Picture");
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream((byte[])DatSet.Tables[0].Rows[0]["PictureData"]);
                        pictureBox2.Image = new Bitmap(ms);
                        MessageBox.Show("Profile Picture has Updated Successfully!!");
                    }

                    query.Upload_Post(File_Name, filePath, File_Name, ID);

                    DataSet postData = query.search_User_Post(ID);

                    fill_Prof_Posts(postData);
                }
            }
            catch
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void picBox2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string imageLocation;

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png| All Files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    string filePath = "";
                    if (imageLocation.Contains(@"\"))
                    {
                        filePath = imageLocation.Substring(0, imageLocation.LastIndexOf(@"\")).TrimEnd();
                    }
                    string[] parts = imageLocation.Split('\\');
                    string File_Name = parts[parts.Length - 1];
                    int ID = (int)DatSet.Tables[0].Rows[0]["ID"];

                    query.Upload_Post(File_Name, filePath, File_Name, ID);
                    DataSet postData = query.search_User_Post(ID);

                    fill_Prof_Posts(postData);
                }
            }
            catch
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

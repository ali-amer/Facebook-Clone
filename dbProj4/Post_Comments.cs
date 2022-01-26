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
    public partial class Post_Comments : Form
    {
        string post_id, user_id;
        sqlOperations_class query = new sqlOperations_class();
        DataSet cmntTable = new DataSet();
        int totalComments;
        int currComent = 0;
        public Post_Comments(string postId,string userId)
        {
            InitializeComponent();
            post_id = postId;
            user_id = userId;
            cmntTable = query.All_Comments(post_id);
            totalComments = cmntTable.Tables[0].Rows.Count;
            if(totalComments>0)
            {
                load_All_Comments();
            }
            else
            {
                commentUserName1.Text = "    No any Comments So far..";
                commentUserName2.Text = "";
                commentUserName3.Text = "";
                commentUserName4.Text = "";    
               
                comment1.Text = "";
                comment2.Text = "";
                comment3.Text = "";
                comment4.Text = "";
            }
        }

        public void load_All_Comments()
        {
            int temp = 0;
            Label[] commentContent = { comment1, comment2, comment3, comment4 };
            PictureBox[] profilePic = { commentProf1, commentProf2, commentProf3, commentProf4 };
            Label[] profileName = { commentUserName1, commentUserName2, commentUserName3, commentUserName4 };

            DataSet userTable = new DataSet();
            cmntTable = query.All_Comments(post_id);
            totalComments = cmntTable.Tables[0].Rows.Count;

            for (int i=0; i<commentContent.Length && currComent < totalComments; i++)
            {
                userTable = query.search_UserByID((int)cmntTable.Tables[0].Rows[currComent]["User_id"]);
                profilePic[i].SizeMode = PictureBoxSizeMode.Zoom;
                if (userTable.Tables[0].Rows[0]["PictureData"].ToString() != "")
                {
                    MemoryStream ms = new MemoryStream((byte[])userTable.Tables[0].Rows[0]["PictureData"]);
                    profilePic[i].Image = new Bitmap(ms);
                }
                string profName = userTable.Tables[0].Rows[0]["First_Name"].ToString() + " " + userTable.Tables[0].Rows[0]["Last_Name"].ToString();
                profileName[i].Text = profName;

                commentContent[i].Text = balance_string(cmntTable.Tables[0].Rows[currComent]["Content"].ToString());
                temp = i;
                currComent++;
            }
            for (int i = temp+1; i < commentContent.Length; i++)
            {
                commentContent[i].Text = "";
                profileName[i].Text = "";
                profilePic[i].Image = null;
            }
         }

        public string balance_string(string a)
        {
            int move = 45;
            if (a.Length > move)
            {
                int i = 1;
                while (i * move < a.Length)
                {
                    a = a.Insert(move * i, Environment.NewLine);
                    i++;
                }
            }
            return a;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void commentProf1_Click(object sender, EventArgs e)
        {

        }

        private void Post_Comments_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel2.AutoScrollPosition = new Point(0, 0);
            if (currComent>= cmntTable.Tables[0].Rows.Count)
            {
                currComent = 0;
                MessageBox.Show("Comments have finished!");
            }
            load_All_Comments();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string comntContent = bunifuMaterialTextbox4.Text;
            if(comntContent!="")
            {
               if( query.post_theComment(post_id, user_id, comntContent))
               {
                    bunifuMaterialTextbox4.Text = "";
                    load_All_Comments();
                    MessageBox.Show("Comment has Posted!");
               }
            }
            else
            {
                MessageBox.Show("You cannot Post Empty Comment!");
            }
        }
    }
}

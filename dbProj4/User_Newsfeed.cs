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
    public partial class User_Newsfeed : Form
    {
        DataSet DatSet=new DataSet();
        DataSet AllPosts_Set=new DataSet();
        sqlOperations_class query = new sqlOperations_class();
        int currPost = 0;
        string[] posId = new string[3];
        public User_Newsfeed(DataSet ds)
        {
            InitializeComponent();
            DatSet = ds;
            DataSet temp = new DataSet();
            temp = query.All_Post(0);
            if (temp.Tables[0].Rows.Count > 0)
            {
                load_All_Posts();
            }
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
            }

        }

        private void User_Newsfeed_Load(object sender, EventArgs e)
        {
           
        }
        public void load_All_Posts()
        {
            DataSet Post_User_Table=new DataSet();
            int id = (int)DatSet.Tables[0].Rows[0]["ID"];
            AllPosts_Set = query.All_Post(id);
            int Post_User_ID= (int)AllPosts_Set.Tables[0].Rows[0]["User_ID"];
            Post_User_Table = query.search_UserByID(Post_User_ID);

            PictureBox[] ProfileBoxes = { Profile1, Profile2, Profile3 };
            Label[] userNameLabels = { labelUserName1,labelUserName2,labelUserName3 };
            PictureBox[] PostBoxes = { Post1, Post2, Post3 };
            Label[] dateLabels = { labelTime1, labelTime2, labelTime3 };
            Label[] likeLabels = { Like1, Like2, Like3 };
            PictureBox[] likePic = { likePoster1, likePoster2, likePoster3 };

            for(int i=0; i<PostBoxes.Length; i++)
            {
                Post_User_ID = (int)AllPosts_Set.Tables[0].Rows[currPost]["User_ID"];
                Post_User_Table = query.search_UserByID(Post_User_ID);

                PostBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                if(AllPosts_Set.Tables[0].Rows.Count > 0)
                {
                    if (AllPosts_Set.Tables[0].Rows[currPost]["Post_Data"].ToString() != "")
                    {
                        MemoryStream ms = new MemoryStream((byte[])AllPosts_Set.Tables[0].Rows[currPost]["Post_Data"]);
                        PostBoxes[i].Image = new Bitmap(ms);
                    }
                    string postDate = AllPosts_Set.Tables[0].Rows[currPost]["Post_date"].ToString();
                    dateLabels[i].Text = postDate;

                    posId[i]= AllPosts_Set.Tables[0].Rows[currPost]["Post_id"].ToString();

                    likeLabels[i].Text = query.total_Postlikes(posId[i]).ToString();
                    bool alreadyLiked = query.user_alreadyLike(posId[i], id.ToString());
                    if (alreadyLiked)
                    {
                        likePic[i].BackColor = Color.FromArgb(33, 150, 243);
                    }
                    else
                    {
                        likePic[i].BackColor = Color.White;   
                    }

                }
               ProfileBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;


                if(Post_User_Table.Tables[0].Rows.Count > 0)
                {
                    if(Post_User_Table.Tables[0].Rows[0]["PictureData"].ToString() != "")
                    {
                        MemoryStream ms = new MemoryStream((byte[])Post_User_Table.Tables[0].Rows[0]["PictureData"]);
                        ProfileBoxes[i].Image = new Bitmap(ms);
                    }
                    string userNam = Post_User_Table.Tables[0].Rows[0]["First_Name"].ToString() + " " + Post_User_Table.Tables[0].Rows[0]["Last_Name"].ToString();
                    userNameLabels[i].Text = userNam;
                }
                currPost++;
            }
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            ActiveUser_Form ActiveUser = new ActiveUser_Form(DatSet);
            ActiveUser.Show();
        }

        private void fbNewsfeed1_Load(object sender, EventArgs e)
        {
         
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Panel5_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue == panel1.VerticalScroll.Maximum - panel1.VerticalScroll.LargeChange + 1)
            {
                if (e.NewValue != e.OldValue) // Checking when the scrollbar is at bottom and user clicks/scrolls the scrollbar      
                {
                    MessageBox.Show("Test"); // Some operation
                }
            }
            
        }

        public bool like_Post(string user_id,string post_id)
        {
            bool liked = query.like_thePost(post_id, user_id);
            if(!liked)
            {
                query.takeBack_Postlike(post_id, user_id);
                return true;
            }
            return false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel5.AutoScrollPosition = new Point(0, 0);
           
            if ((currPost+3) > AllPosts_Set.Tables[0].Rows.Count)
            {
                currPost = 0;
            }
            load_All_Posts();
        }

        private void likePoster1_Click(object sender, EventArgs e)
        {
            string usrId = DatSet.Tables[0].Rows[0]["ID"].ToString();
            string curPostId = posId[0];

            bool alreadyLiked = like_Post(usrId, curPostId);
            Like1.Text = query.total_Postlikes(curPostId).ToString();

            if(alreadyLiked)
            {
                likePoster1.BackColor = Color.White;
            }
            else
            {
                likePoster1.BackColor = Color.FromArgb(33, 150, 243);
            }

        }

        private void likePoster2_Click(object sender, EventArgs e)
        {
            string usrId = DatSet.Tables[0].Rows[0]["ID"].ToString();
            string curPostId = posId[1];

            bool alreadyLiked = like_Post(usrId, curPostId);
            Like2.Text = query.total_Postlikes(curPostId).ToString();

            if (alreadyLiked)
            {
                likePoster2.BackColor = Color.White;
            }
            else
            {
                likePoster2.BackColor = Color.FromArgb(33, 150, 243);
            }
        }
        private void likePoster3_Click(object sender, EventArgs e)
        {
            string usrId = DatSet.Tables[0].Rows[0]["ID"].ToString();
            string curPostId = posId[2];

            bool alreadyLiked = like_Post(usrId, curPostId);
            Like3.Text = query.total_Postlikes(curPostId).ToString();

            if (alreadyLiked)
            {
                likePoster3.BackColor = Color.White;
            }
            else
            {
                likePoster3.BackColor = Color.FromArgb(33, 150, 243);
            }
        }

        private void commen1_Click(object sender, EventArgs e)
        {
            string usrId = DatSet.Tables[0].Rows[0]["ID"].ToString();
            string curPostId = posId[0];
            Post_Comments post_cmnt = new Post_Comments(curPostId, usrId);
            post_cmnt.Show();
        }

        private void commen2_Click(object sender, EventArgs e)
        {
            string usrId = DatSet.Tables[0].Rows[0]["ID"].ToString();
            string curPostId = posId[1];
            Post_Comments post_cmnt = new Post_Comments(curPostId, usrId);
            post_cmnt.Show();
        }

        private void commen3_Click(object sender, EventArgs e)
        {
            string usrId = DatSet.Tables[0].Rows[0]["ID"].ToString();
            string curPostId = posId[2];
            Post_Comments post_cmnt = new Post_Comments(curPostId, usrId);
            post_cmnt.Show();
        }
    }
}
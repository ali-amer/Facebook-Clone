using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace dbProj4
{
    class sqlOperations_class
    {

        string connStr;
        SqlConnection cn;
        SqlDataAdapter dreader;
        public sqlOperations_class()
        {
            connStr = "Data Source=DESKTOP-N7HB27A;Initial Catalog=Facebook;Integrated Security=True";
        }
        public bool register_user(string fname, string lname, string passw, string email, string dob, string gender)
        {
            string query = "INSERT INTO [User](First_name, Last_name, Password, Email, DOB,Gender) Values('" + fname + "','" + lname + "', '" + passw + "','" + email + "','" + dob + "','" + gender + "')";
            using (cn = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                try
                {
                    cn.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Records inserted Successfully..!");
                    cn.Close();
                    return true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return false;
                }
            }
        }

        public bool Upload_Post(string Post_Name, string File_path, string Post_File, int user_id)
        {
            using (cn = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand("dbo.Upload_Post", cn))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@Post_Name", SqlDbType.NVarChar).Value = Post_Name;
                command.Parameters.Add("@imgFolderPath", SqlDbType.NVarChar).Value = File_path;
                command.Parameters.Add("@Post_Filename", SqlDbType.NVarChar).Value = Post_File;
                command.Parameters.Add("@User_ID", SqlDbType.Int).Value = user_id;
                try
                {
                    cn.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Post Uploaded Successfully..!");
                    cn.Close();
                    return true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return false;
                }
            }
        }

        public bool Upload_Profile(string File_path, string Post_File, int user_id)
        {
            using (cn = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand("dbo.Upload_Profile", cn))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@imgFolderPath", SqlDbType.NVarChar).Value = File_path;
                command.Parameters.Add("@Post_Filename", SqlDbType.NVarChar).Value = Post_File;
                command.Parameters.Add("@User_ID", SqlDbType.Int).Value = user_id;
                try
                {
                    cn.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Profile Picture Updated Successfully..!");
                    cn.Close();
                    return true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return false;
                }
            }
        }

        public DataSet search_User(string userName, string Passw)
        {
            DataSet ds = new DataSet();
            string query = "select * from [USER] where Email = '" + @userName + "' AND Password = '" + @Passw + "' ";
            using (cn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    cn.Open();

                    da.Fill(ds);

                    cn.Close();
                    return ds;

                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return ds;
                }
            }
        }

        public DataSet search_User_Post(int userID)
        {
            DataSet ds = new DataSet();
            string query = "select * from [POST] where User_ID = '" + @userID + "'";
            using (cn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    cn.Open();

                    da.Fill(ds);

                    cn.Close();
                    return ds;

                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return ds;
                }
            }
        }

        public DataSet All_Post(int ID)
        {
            DataSet ds = new DataSet();
            string query = "select * from [POST] ORDER BY Post_id DESC";
            using (cn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    cn.Open();

                    da.Fill(ds);

                    cn.Close();
                    return ds;

                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return ds;
                }
            }
        }


        public DataSet search_UserByID(int ID)
        {
            DataSet ds = new DataSet();
            string query = "select * from [USER] where ID = '" + @ID + "' ";
            using (cn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    cn.Open();

                    da.Fill(ds);

                    cn.Close();
                    return ds;

                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return ds;
                }
            }
        }


        public bool like_thePost(string post_id, string user_id)
        {

            string query = "INSERT INTO [Posts likes](Post_id, User_id) Values('" + post_id + "','" + user_id + "')";
            using (cn = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                try
                {
                    cn.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Post Liked Successfully..!");
                    cn.Close();
                    return true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return false;
                }
            }
        }

        public bool takeBack_Postlike(string post_id, string user_id)
        {
            string query = "delete from [Posts likes] where Post_id='" + post_id + "' AND User_id='" + user_id + "'";
            using (cn = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                try
                {
                    cn.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Post Like returned Successfully..!");
                    cn.Close();
                    return true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return false;
                }
            }
        }

        public int total_Postlikes(string post_id)
        {
            string query = "select count(*) from [Posts likes] where Post_id='" + post_id + "'";
            using (cn = new SqlConnection(connStr))
            {
                try
                {
                    cn.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = cn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    Int32 totalLikes = (Int32)command.ExecuteScalar();
                    Console.WriteLine("post likes counted Successfully..!");
                    cn.Close();
                    return totalLikes;
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return 0;
                }
            }
        }

        public bool user_alreadyLike(string post_id, string user_id)
        {
            string query = "select count(*) from [Posts likes] where Post_id='" + post_id + "' AND  User_id='" + user_id + "'";
            using (cn = new SqlConnection(connStr))
            {
                try
                {
                    cn.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = cn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    Int32 userExist = (Int32)command.ExecuteScalar();
                    cn.Close();
                    if (userExist > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return false;
                }
            }
        }

        public bool post_theComment(string post_id, string user_id, string content)
        {
            string query = "INSERT INTO [Comments](Post_id,User_id,Content) values('" + post_id + "', '" + user_id + "', '" + content + "')";
            using (cn = new SqlConnection(connStr))
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                try
                {
                    cn.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Comment Posted Successfully..!");
                    cn.Close();
                    return true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return false;
                }
            }
        }

        public DataSet All_Comments(string post_id)
        {
            DataSet ds = new DataSet();
            string query = "select * from [Comments] where Post_id= '" + post_id + "'";
            using (cn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    cn.Open();

                    da.Fill(ds);

                    cn.Close();
                    return ds;

                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    return ds;
                }
            }
        }

        //public void update(string id, string stdNam, string faNam, string age, string sems)
        //{
        //    string query = "UPDATE Student SET STD_NAME = '" + stdNam + "',FATHER_NAME ='" + faNam + "',AGE='" + age + "',SEMESTER='" + sems + "' WHERE ID= @id";
        //    using (cn = new SqlConnection(connStr))
        //    using (SqlCommand command = new SqlCommand(query, cn))
        //    {
        //        try
        //        {
        //            cn.Open();

        //            command.Parameters.AddWithValue("@ID", id);
        //            command.Parameters.AddWithValue("@STD_NAME", stdNam);
        //            command.Parameters.AddWithValue("@FATHER_NAME", faNam);
        //            command.Parameters.AddWithValue("@AGE", age);
        //            command.Parameters.AddWithValue("@SEMESTER", sems);

        //            int ret = command.ExecuteNonQuery();
        //            Console.WriteLine("Records Updated Successfully");
        //        }
        //        catch (SqlException e)
        //        {
        //            Console.WriteLine("Error Generated. Details  : " + e.ToString());
        //        }
        //    }

        //}

        //public void Delete(string id)
        //{
        //    string query = "DELETE FROM STUDENT WHERE ID=@id";

        //    using (cn = new SqlConnection(connStr))
        //    using (SqlCommand command = new SqlCommand(query, cn))
        //    {
        //        try
        //        {
        //            cn.Open();
        //            command.Parameters.AddWithValue("@ID", id);
        //            int ret = command.ExecuteNonQuery();
        //            Console.WriteLine("Record Deleted Successfully");
        //        }
        //        catch (SqlException e)
        //        {
        //            Console.WriteLine("Error Generated. Details  : " + e.ToString());
        //        }
        //    }
        //}
    }
}

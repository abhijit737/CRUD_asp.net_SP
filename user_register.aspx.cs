using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Sql;


namespace dotnet_crud
{
    public partial class user_register : System.Web.UI.Page
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);

       

        

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert_user_reg", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", txtemail.Text);
            cmd.Parameters.AddWithValue("@nm", txtname.Text);
            cmd.Parameters.AddWithValue("@pwd", txtpwd.Text);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Data added ...";
            con.Close();

        }


        protected void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("find_user_reg", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@nm", txtname.Text);
                cmd.Parameters.AddWithValue("@pwd", txtpwd.Text);

                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Access the columns using reader["ColumnName"]
                            string foundEmail = reader["Emailid"].ToString();
                            string foundName = reader["Name"].ToString();
                            // ... other columns as needed
                            lblmsg.Text = $"User found: Email - {foundEmail}, Name - {foundName}";
                        }
                    }
                    else
                    {
                        lblmsg.Text = "User not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (log, display error message, etc.)
                lblmsg.Text = "An error occurred: " + ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

        }



        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update_user_reg", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Use the Text property of TextBox controls to get the values
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@nm", txtname.Text);
                cmd.Parameters.AddWithValue("@pwd", txtpwd.Text);


                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    lblmsg.Text = "User information updated successfully.";
                }
                else
                {
                    lblmsg.Text = "No user found or no changes made.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (log, display error message, etc.)
                lblmsg.Text = "An error occurred: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }






        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete_user_reg", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", txtemail.Text);  // Use the Text property

                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    lblmsg.Text = "User deleted successfully.";
                }
                else
                {
                    lblmsg.Text = "No user found or deletion failed.";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = "An error occurred: " + ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

    }
}
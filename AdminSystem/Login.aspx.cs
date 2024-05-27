using System;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;

        string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("spLoginUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Session["UserID"] = reader["UserID"];
                Session["Username"] = reader["Username"];
                Session["Role"] = reader["Role"];
                Response.Redirect("Dashboard.aspx"); // Redirect to your dashboard or main page
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }
    }
}

using System;
using System.Data.SqlClient;
using System.Configuration;

public partial class Register : System.Web.UI.Page
{
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string email = txtEmail.Text;
        string password = txtPassword.Text;
        string role = ddlRole.SelectedValue;

        string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("spRegisterUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Role", role);

            conn.Open();
            cmd.ExecuteNonQuery();

            lblMessage.Text = "Registration successful!";
        }
    }
}

using System;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;

namespace MovieTrackerApp
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user is already logged in, redirect to home page
            if (Session["UserID"] != null)
            {
                Response.Redirect("~/Default.aspx");
            }

            // Clear any previous error messages
            lblMessage.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both username and password.";
                return;
            }

            try
            {

                // Query to check user credentials
                string query = "SELECT UserID, Username FROM Users WHERE Username = @Username AND Password = @Password";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password)
                };

                // Execute query
                DataTable result = DatabaseConnection.ExecuteQuery(query, parameters);

                // Check if user exists
                if (result.Rows.Count > 0)
                {
                    // Store user information in session
                    Session["UserID"] = result.Rows[0]["UserID"];
                    Session["Username"] = result.Rows[0]["Username"];


                    // Redirect to home page
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    // Invalid credentials
                    lblMessage.Text = "Invalid username or password.";
                }
            }
            catch (Exception ex)
            {
                // Log the error and show a generic message
                lblMessage.Text = "An error occurred during login. Please try again.";
                // In a real application, you would log the exception
                System.Diagnostics.Debug.WriteLine("Login error: " + ex.Message);
            }
        }
    }
}
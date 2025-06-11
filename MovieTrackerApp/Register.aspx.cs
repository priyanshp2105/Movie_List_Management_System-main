using System;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;

namespace MovieTrackerApp
{
    public partial class Register : Page
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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Validate input (additional validation beyond the validators in the ASPX page)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "Please fill in all required fields.";
                return;
            }

            if (password != confirmPassword)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            try
            {

                // Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                SqlParameter[] checkParameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username)
                };

                int userCount = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkQuery, checkParameters));
                if (userCount > 0)
                {
                    lblMessage.Text = "Username already exists. Please choose a different username.";
                    return;
                }

                // Check if email already exists
                string checkEmailQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlParameter[] checkEmailParameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email)
                };

                int emailCount = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkEmailQuery, checkEmailParameters));
                if (emailCount > 0)
                {
                    lblMessage.Text = "Email already registered. Please use a different email.";
                    return;
                }

                // Insert new user
                string insertQuery = "INSERT INTO Users (Username, Password, Email) VALUES (@Username, @Password, @Email)";
                SqlParameter[] insertParameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password),
                    new SqlParameter("@Email", email)
                };

                int result = DatabaseConnection.ExecuteNonQuery(insertQuery, insertParameters);
                if (result > 0)
                {
                    // Registration successful, redirect to login page
                    Response.Redirect("~/Login.aspx?registered=true");
                }
                else
                {
                    lblMessage.Text = "Registration failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                // Log the error and show a generic message
                lblMessage.Text = "An error occurred during registration. Please try again.";
                // In a real application, you would log the exception
                System.Diagnostics.Debug.WriteLine("Registration error: " + ex.Message);
            }
        }
    }
}
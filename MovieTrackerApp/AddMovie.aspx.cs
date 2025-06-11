using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace MovieTrackerApp
{
    public partial class AddMovie : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Set default values if needed
                txtReleaseYear.Text = DateTime.Now.Year.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Get form values
                string title = txtTitle.Text.Trim();
                string director = txtDirector.Text.Trim();
                string releaseYearText = txtReleaseYear.Text.Trim();
                string genre = ddlGenre.SelectedValue;
                string watchStatus = ddlWatchStatus.SelectedValue;
                string ratingText = ddlRating.SelectedValue;
                string notes = txtNotes.Text.Trim();

                // Validate required fields
                if (string.IsNullOrEmpty(title))
                {
                    lblMessage.Text = "Movie title is required.";
                    return;
                }

                // Parse numeric values
                int? releaseYear = null;
                if (!string.IsNullOrEmpty(releaseYearText))
                {
                    releaseYear = Convert.ToInt32(releaseYearText);
                }

                int? rating = null;
                if (!string.IsNullOrEmpty(ratingText))
                {
                    rating = Convert.ToInt32(ratingText);
                }

                // Get user ID from session
                int userId = Convert.ToInt32(Session["UserID"]);

                // Insert movie into database
                string query = @"INSERT INTO Movies 
                              (UserID, Title, Director, ReleaseYear, Genre, WatchStatus, Rating, Notes) 
                              VALUES 
                              (@UserID, @Title, @Director, @ReleaseYear, @Genre, @WatchStatus, @Rating, @Notes)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@Title", title),
                    new SqlParameter("@Director", director),
                    new SqlParameter("@ReleaseYear", releaseYear.HasValue ? (object)releaseYear.Value : DBNull.Value),
                    new SqlParameter("@Genre", string.IsNullOrEmpty(genre) ? DBNull.Value : (object)genre),
                    new SqlParameter("@WatchStatus", watchStatus),
                    new SqlParameter("@Rating", rating.HasValue ? (object)rating.Value : DBNull.Value),
                    new SqlParameter("@Notes", string.IsNullOrEmpty(notes) ? DBNull.Value : (object)notes)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    // Redirect to movie list page
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lblMessage.Text = "Failed to add movie. Please try again.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred. Please try again.";
                System.Diagnostics.Debug.WriteLine("Error adding movie: " + ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Return to movie list page
            Response.Redirect("~/Default.aspx");
        }
    }
}
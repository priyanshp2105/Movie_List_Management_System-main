using System;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;

namespace MovieTrackerApp
{
    public partial class EditMovie : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            // Check if movie ID is provided in the query string
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Load movie data on first page load
                LoadMovieData();
            }
        }

        private void LoadMovieData()
        {
            try
            {
                int movieId = Convert.ToInt32(Request.QueryString["id"]);
                int userId = Convert.ToInt32(Session["UserID"]);

                // Store movie ID in hidden field
                hfMovieID.Value = movieId.ToString();

                // Query to get movie details (ensure it belongs to the current user for security)
                string query = "SELECT * FROM Movies WHERE MovieID = @MovieID AND UserID = @UserID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MovieID", movieId),
                    new SqlParameter("@UserID", userId)
                };

                DataTable result = DatabaseConnection.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    // Populate form fields with movie data
                    DataRow row = result.Rows[0];

                    txtTitle.Text = row["Title"].ToString();
                    txtDirector.Text = row["Director"].ToString();

                    if (row["ReleaseYear"] != DBNull.Value)
                    {
                        txtReleaseYear.Text = row["ReleaseYear"].ToString();
                    }

                    if (row["Genre"] != DBNull.Value)
                    {
                        ddlGenre.SelectedValue = row["Genre"].ToString();
                    }

                    ddlWatchStatus.SelectedValue = row["WatchStatus"].ToString();

                    if (row["Rating"] != DBNull.Value)
                    {
                        ddlRating.SelectedValue = row["Rating"].ToString();
                    }

                    if (row["Notes"] != DBNull.Value)
                    {
                        txtNotes.Text = row["Notes"].ToString();
                    }
                }
                else
                {
                    // Movie not found or doesn't belong to current user
                    Response.Redirect("~/Default.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading movie data.";
                System.Diagnostics.Debug.WriteLine("Error loading movie data: " + ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Get form values
                int movieId = Convert.ToInt32(hfMovieID.Value);
                int userId = Convert.ToInt32(Session["UserID"]);
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

                // Update movie in database
                string query = @"UPDATE Movies 
                              SET Title = @Title, Director = @Director, ReleaseYear = @ReleaseYear, 
                              Genre = @Genre, WatchStatus = @WatchStatus, Rating = @Rating, Notes = @Notes 
                              WHERE MovieID = @MovieID AND UserID = @UserID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MovieID", movieId),
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
                    lblMessage.Text = "Failed to update movie. Please try again.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred. Please try again.";
                System.Diagnostics.Debug.WriteLine("Error updating movie: " + ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Return to movie list page
            Response.Redirect("~/Default.aspx");
        }
    }
}
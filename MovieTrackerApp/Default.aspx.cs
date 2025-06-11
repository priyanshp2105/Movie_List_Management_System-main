using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace MovieTrackerApp
{
    public partial class _Default : Page
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
                // Load movies on first page load
                LoadMovies();
                LoadStatistics();
            }
        }

        private void LoadMovies()
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                string query = "SELECT * FROM Movies WHERE UserID = @UserID";
                if (!string.IsNullOrEmpty(ddlFilter.SelectedValue))
                {
                    query += " AND WatchStatus = @WatchStatus";
                }
                query += " ORDER BY Title";

                SqlParameter[] parameters;

                if (!string.IsNullOrEmpty(ddlFilter.SelectedValue))
                {
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@UserID", userId),
                        new SqlParameter("@WatchStatus", ddlFilter.SelectedValue)
                    };
                }
                else
                {
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@UserID", userId)
                    };
                }

                DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);
                gvMovies.DataSource = dt;
                gvMovies.DataBind();

            }
            catch (Exception ex)
            {
                // Log the error

                System.Diagnostics.Debug.WriteLine("Error loading movies: " + ex.Message);
            }
        }

        private void LoadStatistics()
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserID"]);

                // Get count of watched movies
                string watchedQuery = "SELECT COUNT(*) FROM Movies WHERE UserID = @UserID AND WatchStatus = 'Watched'";
                SqlParameter[] watchedParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId)
                };
                object watchedCount = DatabaseConnection.ExecuteScalar(watchedQuery, watchedParams);
                lblWatchedCount.Text = watchedCount.ToString();

                // Get count of plan to watch movies
                string planToWatchQuery = "SELECT COUNT(*) FROM Movies WHERE UserID = @UserID AND WatchStatus = 'Plan to Watch'";
                SqlParameter[] planToWatchParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId)
                };
                object planToWatchCount = DatabaseConnection.ExecuteScalar(planToWatchQuery, planToWatchParams);
                lblPlanToWatchCount.Text = planToWatchCount.ToString();

                // Get count of unwatched movies
                string unwatchedQuery = "SELECT COUNT(*) FROM Movies WHERE UserID = @UserID AND WatchStatus = 'Unwatched'";
                SqlParameter[] unwatchedParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId)
                };
                object unwatchedCount = DatabaseConnection.ExecuteScalar(unwatchedQuery, unwatchedParams);
                lblUnwatchedCount.Text = unwatchedCount.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading statistics: " + ex.Message);
            }
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMovies();
        }

        protected void gvMovies_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditMovie")
            {
                int movieId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"~/EditMovie.aspx?id={movieId}");
            }
            else if (e.CommandName == "DeleteMovie")
            {
                try
                {
                    int movieId = Convert.ToInt32(e.CommandArgument);
                    int userId = Convert.ToInt32(Session["UserID"]);

                    string deleteQuery = "DELETE FROM Movies WHERE MovieID = @MovieID AND UserID = @UserID";
                    SqlParameter[] deleteParams = new SqlParameter[]
                    {
                        new SqlParameter("@MovieID", movieId),
                        new SqlParameter("@UserID", userId)
                    };

                    int result = DatabaseConnection.ExecuteNonQuery(deleteQuery, deleteParams);

                    if (result > 0)
                    {
                        LoadMovies();
                        LoadStatistics();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error deleting movie: " + ex.Message);
                }
            }
        }

        protected void btnAddMovie_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddMovie.aspx");
        }

        protected void gvMovies_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the data item
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                // Customize Status appearance
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                string status = rowView["WatchStatus"].ToString();

                // Display Rating as simple number
                Label lblRating = (Label)e.Row.FindControl("lblRating");
                if (lblRating != null && rowView["Rating"] != DBNull.Value)
                {
                    int rating = Convert.ToInt32(rowView["Rating"]);
                    lblRating.Text = rating.ToString() + "/5";
                }
            }
        }
    }
}
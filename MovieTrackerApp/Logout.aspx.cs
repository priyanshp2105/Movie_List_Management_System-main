using System;
using System.Web.UI;

namespace MovieTrackerApp
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear all session variables
            Session.Clear();
            Session.Abandon();

            // Clear authentication cookies
            if (Request.Cookies["UserInfo"] != null)
            {
                Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(-1);
            }

            // Redirect to login page
            Response.Redirect("~/Login.aspx");
        }
    }
}
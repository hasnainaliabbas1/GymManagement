using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CheckStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Email"] == null || Session["Password"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return; 
            }

            string email = Session["Email"].ToString();
            string password = Session["Password"].ToString();

            string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MembershipStatus FROM Members WHERE Email = @Email AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    ltlMemberStatus.Text = "Your current status: " + result.ToString();
                }
                else
                {
                    ltlMemberStatus.Text = "Status not found";
                }
            }
        }
    }


protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["Email"] = null;
        Session["Password"] = null;
        Response.Redirect("~/Default.aspx");
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MemberDashboard.aspx");
    }

}
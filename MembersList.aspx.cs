using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class MembersList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DisplayMembersList();
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["Email"] = null;
        Session["Password"] = null;
        Response.Redirect("~/Default.aspx");
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ManagerDashboard.aspx");
    }
    private void DisplayMembersList()
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        string query = "SELECT MemberID, FirstName, LastName, MembershipType, MembershipStatus FROM Members";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                GridViewMembers.DataSource = dt;
                GridViewMembers.DataBind();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string memberID = MemberID.Text.Trim();

        // Check if the entered MemberID is valid
        if (!IsMemberValid(memberID))
        {
            lblError.Text = "Invalid Member ID";
            GridViewCheckInHistory.DataSource = null;
            GridViewCheckInHistory.DataBind();
            return;
        }

        // Check if the member exists in the CheckIns table
        if (!IsMemberCheckedIn(memberID))
        {
            // If not checked in, show an empty table
            GridViewCheckInHistory.DataSource = null;
            GridViewCheckInHistory.DataBind();
            return;
        }

        // Retrieve check-in history from the CheckIns and Classes tables
        DisplayCheckInHistory(memberID);
    }

    private bool IsMemberValid(string memberID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        string query = "SELECT COUNT(*) FROM Members WHERE MemberID = @MemberID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", memberID);
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }

    private bool IsMemberCheckedIn(string memberID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        string query = "SELECT COUNT(*) FROM CheckIns WHERE MemberID = @MemberID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", memberID);
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }

    private void DisplayCheckInHistory(string memberID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        string query = @"SELECT CI.MemberID, CI.ClassID, C.ClassTime, CI.CheckInStatus
                     FROM CheckIns CI
                     INNER JOIN Classes C ON CI.ClassID = C.ClassID
                     WHERE CI.MemberID = @MemberID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", memberID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                GridViewCheckInHistory.DataSource = dt;
                GridViewCheckInHistory.DataBind();
            }
        }
    }


}
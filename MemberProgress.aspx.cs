using System;
using System.Data;
using System.Data.SqlClient;


public partial class MemberProgress : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string memberID = MemberID.Text.Trim();

        if (!IsMemberIDValid(memberID))
        {
            lblError.Text = "Member ID invalid.";
            ltlMemberProgress.Text = "";
            return;
        }
        DataTable progressData = GetMemberProgress(Convert.ToInt32(memberID));
        DisplayProgress(progressData);
        DataTable memberInfo = GetMemberInformation(memberID);
        GridViewMemberInfo.DataSource = memberInfo;
        GridViewMemberInfo.DataBind();
    }
    private DataTable GetMemberInformation(string memberID)
    {
        DataTable dt = new DataTable();
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        string query = "SELECT MemberID, FirstName, LastName, Email, Phone, Gender," +
            " BirthDate, MembershipType, MembershipStatus, Goals, Preferences" +
            " FROM Members WHERE MemberID = @MemberID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", memberID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
        }

        return dt;
    }
    private DataTable GetMemberProgress(int memberID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        DataTable dt = new DataTable();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetMemberProgress", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_MemberID", memberID);

            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        return dt;
    }
    private void DisplayProgress(DataTable dt)
    {
        string progressHtml = "<table class='table'><thead><tr><th>Session Date</th><th>Description</th><th>Training Status</th></tr></thead><tbody>";

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                progressHtml += "<tr>";
                progressHtml += "<td>" + row["SessionDate"] + "</td>";
                progressHtml += "<td>" + row["Description"] + "</td>";
                progressHtml += "<td>" + row["TrainingStatus"] + "</td>";
                progressHtml += "</tr>";
            }
        }
        else
        {
            progressHtml += "<tr>";
            progressHtml += "<td></td>";
            progressHtml += "<td></td>";
            progressHtml += "<td></td>";
            progressHtml += "</tr>";
        }

        progressHtml += "</tbody></table>";

        ltlMemberProgress.Text = progressHtml;
    }

    private bool IsMemberIDValid(string MemberID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        string query = "SELECT COUNT(*) FROM Members WHERE MemberID = @MemberID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", MemberID);
                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
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
        Response.Redirect("~/ManagerDashboard.aspx");
    }

}
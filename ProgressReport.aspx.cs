using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class ProgressReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int memberID = GetMemberIDFromDatabase(Session["Email"].ToString(), Session["Password"].ToString());
            if (memberID != -1)
            {
                DataTable dt = GetMemberProgress(memberID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                DisplayProgress(dt);
                }
                else
                {
                    
                ltlMemberProgress.Text = "No progress details available.";
                }
            }
            else
            {
                ltlMemberProgress.Text = "Error: MemberID not found.";
            }
        }
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
    private int GetMemberIDFromDatabase(string email, string password)
    {
        int memberID = -1;
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT MemberID FROM Members WHERE Email = @Email AND Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            object result = command.ExecuteScalar();

            if (result != null)
            {
                memberID = Convert.ToInt32(result);
            }
        }
        return memberID;
    }


}
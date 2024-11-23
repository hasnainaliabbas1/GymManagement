using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ClassAttendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        int classID = Convert.ToInt32(ClassID.Text);
        int memberID = Convert.ToInt32(MemberID.Text);
        string checkinStatus = CheckInStatus.Text;

        if (!IsClassValid(classID.ToString()))
        {
            lblError.Text = "Invalid class id";
            return;
        }
        if (!IsMemberIDValid(memberID.ToString()))
        {
            lblError.Text = "Invalid Member id";
            return;
        }
        if (AttendanceRecordExists(classID.ToString(), memberID.ToString()))
        {
            UpdateAttendance(classID, memberID, checkinStatus);
            lblError.Text = "Attendance record updated successfully";
        }
        else
        {
            AddAttendance(classID, memberID, checkinStatus);
            lblError.Text = "Attendance record added successfully";
        }
    }

    private bool IsClassValid(string classID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        string query = "SELECT COUNT(*) FROM Classes WHERE ClassID = @ClassID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ClassID", classID);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
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

    private bool AttendanceRecordExists(string classID, string memberID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        string query = "SELECT COUNT(*) FROM CheckIns WHERE ClassID = @ClassID AND MemberID = @MemberID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ClassID", classID);
                command.Parameters.AddWithValue("@MemberID", memberID);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }

    private void AddAttendance(int ClassID, int MemberID, string checkinStatus)
    {
       
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand cmd = new SqlCommand("sp_AddAttendance", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                int newCheckInID = GetNextCheckInID(connection);

                cmd.Parameters.AddWithValue("@CheckInID", newCheckInID);
                cmd.Parameters.AddWithValue("@p_ClassID", ClassID);
                cmd.Parameters.AddWithValue("@p_MemberID", MemberID);
                cmd.Parameters.AddWithValue("@p_CheckInStatus", checkinStatus);
                cmd.ExecuteNonQuery();
            }
        }
    }

    private int GetNextCheckInID(SqlConnection connection)
    {
        string query = "SELECT ISNULL(MAX(CheckInID), 0) + 1 FROM CheckIns";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            return (int)command.ExecuteScalar();
        }
    }

    private void UpdateAttendance(int ClassID, int MemberID, string checkinStatus)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE CheckIns SET CheckInStatus = @CheckInStatus WHERE ClassID = @ClassID AND MemberID = @MemberID", connection);
            cmd.Parameters.AddWithValue("@ClassID", ClassID);
            cmd.Parameters.AddWithValue("@MemberID", MemberID);
            cmd.Parameters.AddWithValue("@CheckInStatus", checkinStatus);
            cmd.ExecuteNonQuery();
        }
    }
}

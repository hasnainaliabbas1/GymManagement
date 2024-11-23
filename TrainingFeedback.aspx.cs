using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class TrainingFeedback : System.Web.UI.Page
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
        string memberID = MemberID.Text;
        string description = Description.Text;
        string trainingStatus = TrainingStatus.Text;
        string dateString = HiddenDate.Value;
        DateTime parsedDate = DateTime.Parse(dateString);
        DateTime sessionDate = parsedDate.Date;
        if (!IsMemberIDValid(memberID))
        {
            lblError.Text = "MemberID Invalid";
            return;
        }
        string email = Session["Email"] as string;
        string password = Session["Password"] as string;
        int trainerID = GetTrainerIDFromDatabase(email, password);

        if (trainerID == -1)
        {
            lblError.Text = "TrainerID not found";
            return;
        }
        int feedbackID = GetNextFeedbackID();
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        string query = "INSERT INTO Feedback (FeedbackID, MemberID, TrainerID, SessionDate, Description, TrainingStatus) VALUES (@FeedbackID, @MemberID, @TrainerID, @SessionDate, @Description, @TrainingStatus)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FeedbackID", feedbackID);
                command.Parameters.AddWithValue("@MemberID", memberID);
                command.Parameters.AddWithValue("@TrainerID", trainerID);
                command.Parameters.AddWithValue("@SessionDate", sessionDate);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@TrainingStatus", trainingStatus);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    lblError.Text = "Feedback added successfully";
                }
                else
                {
                    lblError.Text = "Failed to add feedback";
                }
            }
        }
    }

    private int GetNextFeedbackID()
    {
        int nextFeedbackID = 1;
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        string query = "SELECT MAX(FeedbackID) FROM Feedback";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    nextFeedbackID = Convert.ToInt32(result) + 1; // Increment the max ID by 1
                }
            }
        }

        return nextFeedbackID;
    }

    private bool IsMemberIDValid(string MemberID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        string query = "SELECT COUNT(*) FROM Members WHERE MemberID = @MemberID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", MemberID);
                connection.Open();
                int count = (int)command.ExecuteScalar();

                // If count is greater than 0, it means the class is valid
                return count > 0;
            }
        }
    }

    private int GetTrainerIDFromDatabase(string email, string password)
    {
        int trainerID = -1;
        // Database query to retrieve MemberID based on email and password
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT TrainerID FROM Trainers WHERE Email = @Email AND Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            object result = command.ExecuteScalar();

            if (result != null)
            {
                trainerID = Convert.ToInt32(result);
            }
        }
        return trainerID;
    }


}
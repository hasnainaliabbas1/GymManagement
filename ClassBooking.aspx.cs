using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ClassBooking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Fetch and display classes whose DateTime has not passed the current DateTime
            DisplayUpcomingClasses();

            // Fetch and display booked classes by the member
            DisplayBookedClasses();
        }
    }
    private void DisplayUpcomingClasses()
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        string query = "SELECT ClassID, ClassName, Description, ClassTime FROM Classes WHERE ClassTime > @CurrentDateTime";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CurrentDateTime", DateTime.Now);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                GridViewClasses.DataSource = dt;
                GridViewClasses.DataBind();
            }
        }
    }
    private void DisplayBookedClasses()
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        string query = @"SELECT B.ClassID
                         FROM Bookings B
                         INNER JOIN Classes C ON B.ClassID = C.ClassID
                         WHERE B.MemberID = @MemberID AND C.ClassTime > @CurrentDateTime";

        int MID = GetMemberIDFromDatabase(Session["Email"].ToString(), Session["Password"].ToString());
        string memberID = MID.ToString();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", memberID);
                command.Parameters.AddWithValue("@CurrentDateTime", DateTime.Now);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                GridViewBookedClasses.DataSource = dt;
                GridViewBookedClasses.DataBind();
            }
        }
    }
    private int GetMemberIDFromDatabase(string email, string password)
    {
        int memberID = -1;
        // Database query to retrieve MemberID based on email and password
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        int mID = GetMemberIDFromDatabase(Session["Email"].ToString(), Session["Password"].ToString());
        string memberID = mID.ToString();

        // Get the value of ClassID entered by the user
        string classID = ClassID.Text.Trim();

        // Check if the ClassID is already registered by the member
        if (IsClassAlreadyBooked(classID, memberID))
        {
            // Display message if the class is already registered
            lblError.Text = "Class already registered.";
        }
        else
        {
            // Check if the ClassID exists in the Classes table and has not passed the current DateTime
            if (IsClassValid(classID))
            {
                // Insert a new record into the Bookings table
                BookClass(classID, memberID);
                // Clear any previous error messages
                lblError.Text = "Class Registered";
                // Refresh the booked classes gridview
                DisplayBookedClasses();
            }
            else
            {
                // Display message if the class does not exist or has passed already
                lblError.Text = "Class does not exist or conducted already.";
            }
        }
    }

    private bool IsClassValid(string classID)
    {
        string connectionString = @"
Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        string query = "SELECT COUNT(*) FROM Classes WHERE ClassID = @ClassID AND ClassTime > @CurrentDateTime";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ClassID", classID);
                command.Parameters.AddWithValue("@CurrentDateTime", DateTime.Now);
                connection.Open();
                int count = (int)command.ExecuteScalar();

                // If count is greater than 0, it means the class is valid
                return count > 0;
            }
        }
    }

    private void BookClass(string classID, string memberID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";

        int newBookingID = GetNextBookingID(connectionString);

        string query = "INSERT INTO Bookings (BookingID, ClassID, MemberID) VALUES (@BookingID, @ClassID, @MemberID)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookingID", newBookingID);
                command.Parameters.AddWithValue("@ClassID", classID);
                command.Parameters.AddWithValue("@MemberID", memberID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    private int GetNextBookingID(string connectionString)
    {
        int newBookingID = 1; 

        string query = "SELECT MAX(BookingID) FROM Bookings";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    newBookingID = Convert.ToInt32(result) + 1;
                }
            }
        }

        return newBookingID;
    }


    private bool IsClassAlreadyBooked(string classID, string memberID)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        string query = "SELECT COUNT(*) FROM Bookings WHERE ClassID = @ClassID AND MemberID = @MemberID";

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

}
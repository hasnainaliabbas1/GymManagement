using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class ClassEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Fetch and display classes whose DateTime has not passed the current DateTime
            DisplayUpcomingClasses();

        }
    }
    private void DisplayUpcomingClasses()
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
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
        // Retrieve the date from the hidden field
        string dateString = HiddenDateTime.Value;

        // Convert the date string to a DateTime object
        string format = "MM/dd/yyyy HH:mm:ss";
        DateTime classTime = DateTime.Parse(dateString);

        //if (!DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out classTime))
        //{
        //    lblError.Text = "Invalid date format.";
        //    return;
        //}

        // Get other values from the form
        string className = ClassName.Text;
        string description = Description.Text;
        int trainerID;
        if (!int.TryParse(TrainerID.Text, out trainerID))
        {
            lblError.Text = "Invalid Trainer ID";
            return;
        }

        // Check if TrainerID exists in the Trainers table
        if (!IsTrainerValid(trainerID))
        {
            lblError.Text = "Trainer ID invalid";
            return;
        }

        // Add the class
        AddClass(className, description, trainerID, classTime);
        lblError.Text = "Class added!";
        DisplayUpcomingClasses();
    }
    private bool IsTrainerValid(int trainerID)
    {
        // Check if TrainerID exists in the Trainers table
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";
        string query = "SELECT COUNT(*) FROM Trainers WHERE TrainerID = @TrainerID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TrainerID", trainerID);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
    private void AddClass(string ClassName, string Description, int TrainerID, DateTime ClassTime)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True";

        int newClassID;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmdGetMaxID = new SqlCommand("SELECT ISNULL(MAX(ClassID), 0) FROM Classes", connection);
            newClassID = (int)cmdGetMaxID.ExecuteScalar() + 1; 
        }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Classes (ClassID, ClassName, Description, TrainerID, ClassTime) " +
                                             "VALUES (@ClassID, @ClassName, @Description, @TrainerID, @ClassTime)", connection);

            cmd.Parameters.AddWithValue("@ClassID", newClassID);
            cmd.Parameters.AddWithValue("@ClassName", ClassName);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@TrainerID", TrainerID);
            cmd.Parameters.AddWithValue("@ClassTime", ClassTime);

            cmd.ExecuteNonQuery();
        }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected void ButtonRegister_Click(object sender, EventArgs e)
    {
        string dateString = HiddenDate.Value;
        DateTime parsedDate = DateTime.Parse(dateString);
        DateTime dateOnly = parsedDate.Date;
        string memberstatus = "Suspended";

        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Get the current max MemberID
            SqlCommand cmdMaxID = new SqlCommand("SELECT MAX(MemberID) FROM Members", connection);
            int maxMemberID = (int)cmdMaxID.ExecuteScalar();
            int memberID = maxMemberID + 1;  // Increment to get the next MemberID

            // Insert new member with the new MemberID
            SqlCommand cmd = new SqlCommand("INSERT INTO Members (MemberID, FirstName, LastName, Email, Password, Phone, Gender, Birthdate, MembershipType, MembershipStatus, Goals, Preferences) VALUES (@MemberID, @FirstName, @LastName, @Email, @Password, @Phone, @Gender, @Birthdate, @MembershipType, @MembershipStatus, @Goals, @Preferences)", connection);

            cmd.Parameters.AddWithValue("@MemberID", memberID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName.Text);
            cmd.Parameters.AddWithValue("@LastName", LastName.Text);
            cmd.Parameters.AddWithValue("@Email", Email.Text);
            cmd.Parameters.AddWithValue("@Password", Password.Text);
            cmd.Parameters.AddWithValue("@Phone", Phone.Text);
            cmd.Parameters.AddWithValue("@Gender", Gender.Text);
            cmd.Parameters.AddWithValue("@Birthdate", dateOnly);
            cmd.Parameters.AddWithValue("@MembershipType", MembershipType.Text);
            cmd.Parameters.AddWithValue("@MembershipStatus", memberstatus);
            cmd.Parameters.AddWithValue("@Goals", Goals.Text);
            cmd.Parameters.AddWithValue("@Preferences", Preferences.Text);

            cmd.ExecuteNonQuery();
            Response.Redirect("default.aspx");
        }
    }

    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

}
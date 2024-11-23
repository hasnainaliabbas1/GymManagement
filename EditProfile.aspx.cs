using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class EditProfile : System.Web.UI.Page
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
        Response.Redirect("~/MemberDashboard.aspx");
    }


    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        
        string dateString = HiddenDate.Value;
         int memberID = GetMemberIDFromDatabase(Session["Email"].ToString(), Session["Password"].ToString());
  if (memberID != -1)
        {
            if(dateString != "" && dateString != null)
            {
               
                DateTime parsedDate = DateTime.Parse(dateString);
                DateTime dateOnly = parsedDate.Date;
          UpdateMemberProfile(memberID, FirstName.Text, LastName.Text, Email.Text, Password.Text, Phone.Text, Gender.Text, dateOnly, "", Goals.Text, Preferences.Text);
          Session["Email"] = null;
                Session["Password"] = null;
           Response.Redirect("Default.aspx");
            }
            else
            {
                UpdateMemberProfileNoDate(memberID, FirstName.Text, LastName.Text, Email.Text, Password.Text, Phone.Text, Gender.Text, "", Goals.Text, Preferences.Text);

                Session["Email"] = null;
                Session["Password"] = null;
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
        }
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

    private void UpdateMemberProfile(int memberID, string firstName, string lastName, string email, string password, string phone, string gender, DateTime birthdate, string membershipType, string goals, string preferences)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("UpdateMemberProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_MemberID", memberID);
            cmd.Parameters.AddWithValue("@p_FirstName", firstName);
            cmd.Parameters.AddWithValue("@p_LastName", lastName);
            cmd.Parameters.AddWithValue("@p_Email", email);
            cmd.Parameters.AddWithValue("@p_Pass", password);
            cmd.Parameters.AddWithValue("@p_Phone", phone);
            cmd.Parameters.AddWithValue("@p_Gender", gender);
            cmd.Parameters.AddWithValue("@p_Birthdate", birthdate);
            cmd.Parameters.AddWithValue("@p_MembershipType", membershipType);
            cmd.Parameters.AddWithValue("@p_Goals", goals);
            cmd.Parameters.AddWithValue("@p_Preferences", preferences);

            cmd.ExecuteNonQuery();
        }
    }

    private void UpdateMemberProfileNoDate(int memberID, string firstName, string lastName, string email, string password, string phone, string gender, string membershipType, string goals, string preferences)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("UpdateMemberProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_MemberID", memberID);
            cmd.Parameters.AddWithValue("@p_FirstName", firstName);
            cmd.Parameters.AddWithValue("@p_LastName", lastName);
            cmd.Parameters.AddWithValue("@p_Email", email);
            cmd.Parameters.AddWithValue("@p_Pass", password);
            cmd.Parameters.AddWithValue("@p_Phone", phone);
            cmd.Parameters.AddWithValue("@p_Gender", gender);
            cmd.Parameters.AddWithValue("@p_Birthdate", "");
            cmd.Parameters.AddWithValue("@p_MembershipType", membershipType);
            cmd.Parameters.AddWithValue("@p_Goals", goals);
            cmd.Parameters.AddWithValue("@p_Preferences", preferences);

            cmd.ExecuteNonQuery();
        }
    }

}
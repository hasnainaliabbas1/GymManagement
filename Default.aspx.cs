using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Form1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string email = UserName.Text;
        string password = Password.Text;
        if (CheckCredentials("Members", email, password))
        {
            Session["Email"] = email;
            Session["Password"] = password;
            Response.Redirect("MemberDashboard.aspx");
        }
     
        else if (CheckCredentials("Trainers", email, password))
        {
           
            Session["Email"] = email;
            Session["Password"] = password;
            Response.Redirect("TrainerDashboard.aspx");
        }
       
        else if (CheckCredentials("Managers", email, password))
        {
            
            Session["Email"] = email;
            Session["Password"] = password;
            Response.Redirect("ManagerDashboard.aspx");
        }
        else
        {
            lblError.Text = "Incorrect email or password.";
        }
    }

    protected void BtnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }

    private bool CheckCredentials(string tableName, string email, string password)
    {
        string connectionString = @"Data Source=HP\SQLEXPRESS;Initial Catalog=GymManagementSystem;Integrated Security=True;";
        string query = $"SELECT COUNT(*) FROM {tableName} WHERE Email = @Email AND Password = @Password";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
       

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MemberDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["Email"] != null && Session["Password"] != null)
        {
            //string email = Session["Email"].ToString();
            //string password = Session["Password"].ToString();

            //string script = "alert('Email: " + email + "\\nPassword: " + password + "');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "SessionValuesAlert", script, true);
        }
        else 
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["Email"] = null;
        Session["Password"] = null;
        Response.Redirect("~/Default.aspx");
    }
    protected void CheckStatusButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CheckStatus.aspx", false);
    }

    protected void EditProfileButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditProfile.aspx");
    }

    protected void ClassBookingButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClassBooking.aspx");
    }

    protected void ProgressReportButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProgressReport.aspx");
    }

}
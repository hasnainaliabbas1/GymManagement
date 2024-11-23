using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManagerDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Email"] != null && Session["Password"] != null)
        {

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

    protected void MembersListButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MembersList.aspx");
    }
    protected void ClassEntryButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClassEntry.aspx");
    }
    protected void ClassAttendanceButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClassAttendance.aspx");
    }
    protected void MemberProgressButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberProgress.aspx");
    }
}
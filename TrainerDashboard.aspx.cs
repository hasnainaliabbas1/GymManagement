using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TrainerDashboard : System.Web.UI.Page
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

    protected void TrainingFeedbackButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingFeedback.aspx");
    }
    protected void MemberProgressButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberProgressForTrainer.aspx");
    }
 

}
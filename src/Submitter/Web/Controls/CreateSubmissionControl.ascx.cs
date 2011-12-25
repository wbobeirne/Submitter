using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.BusinessLogic.BusinessObjects;
using Submitter.Infrastructure.ValueObjects;

namespace Submitter.Web.Controls {
    public partial class CreateSubmissionControl : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["login"] != null) {
                controlTitle.Text = "Create a submission";
            }
            else {
                controlTitle.Text = "You must be logged in to submit";
                createSubmissionPanel.Visible = false;
            }
        }


        protected void CreateSubmission_Submit(object sender, EventArgs e) {

            SubmissionManagementBO bo = new SubmissionManagementBO();

            try {
                if (createSubmissionTitle.Text.Equals("")) {
                    throw new Exception("You must enter a title");
                }
                if (createSubmissionLink.Text.Equals("")) {
                    throw new Exception("You must enter a link");
                }
                bo.CreateNewSubmission(createSubmissionTitle.Text, createSubmissionLink.Text, Session["login"].ToString());
                Response.Redirect(WebConstants.HOME_PAGE + "?sort=new");
            }
            catch (Exception exc) {
                submissionError.Text = exc.Message;
            }

        }
    }
}
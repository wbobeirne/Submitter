using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.BusinessLogic.BusinessObjects;
using Submitter.Infrastructure.ValueObjects;

namespace Submitter.Web.Controls {
    public partial class SubmissionViewControl : System.Web.UI.UserControl {

        protected const int NUMBER_OF_SUBMISSIONS = 25;

        protected void Page_Load(object sender, EventArgs e) {

        }

        public void GenerateSubmissionView(int numSubs) {

            SubmissionManagementBO bo = new SubmissionManagementBO();
            UserManagementBO userBO = new UserManagementBO();
            List<SubmissionVO> subList;
            string sortType = Request.QueryString["sort"];

            if (sortType == "top") {
                subList = bo.GetListOfSubmissionsTop(NUMBER_OF_SUBMISSIONS);
            }
            if (sortType == "popular") {
                subList = bo.GetListOfSubmissionsPopular(NUMBER_OF_SUBMISSIONS);
            }
            else {
                subList = bo.GetListOfSubmissionsNew(NUMBER_OF_SUBMISSIONS);
            }

            foreach (SubmissionVO sub in subList) {

                Submission submission = new Submission();
                submission.ID = "submission" + sub.SubmissionID;
                submission.submissionID = sub.SubmissionID;

                subPanel.Controls.Add(submission);
            }
        }

        public void GenerateSubmissionViewForUser(int userID) {
            SubmissionManagementBO bo = new SubmissionManagementBO();
            List<SubmissionVO> subList = bo.GetListOfSubmissionsByUser(5, userID);

            foreach (SubmissionVO sub in subList) {
                Submission submission = (Submission)Page.LoadControl("~/Controls/Submission.ascx");
                submission.ID = "submission" + sub.SubmissionID;
                submission.submissionID = sub.SubmissionID;

                subPanel.Controls.Add(submission);
            }
        }

        protected void UpArrow_Click(object sender, EventArgs e) {
            Response.Redirect(WebConstants.CREATE_USER_PAGE);
        }

        protected void DownArrow_Click(object sender, EventArgs e) {
            Response.Redirect(WebConstants.CREATE_USER_PAGE);
        }
    }
}
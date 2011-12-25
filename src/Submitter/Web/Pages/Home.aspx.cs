using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.Web.Controls;
using Submitter.BusinessLogic.BusinessObjects;
using Submitter.Infrastructure.ValueObjects;

namespace Submitter.Web.Pages {
    public partial class Home : System.Web.UI.Page {

        protected const int NUMBER_OF_SUBMISSIONS = 20;

        protected void Page_Load(object sender, EventArgs e) {

            GenerateSubmissionView(NUMBER_OF_SUBMISSIONS);

        }


        protected void GenerateSubmissionView(int numSubs) {

            SubmissionManagementBO bo = new SubmissionManagementBO();
            List<SubmissionVO> subList;
            string sortType = Request.QueryString["sort"];

            if (sortType == "top") {
                subList = bo.GetListOfSubmissionsTop(NUMBER_OF_SUBMISSIONS);
                submissionViewTitle.Text = "<h1>See what's on top</h1>";
            }
            else if (sortType == "popular") {
                subList = bo.GetListOfSubmissionsPopular(NUMBER_OF_SUBMISSIONS);
                submissionViewTitle.Text = "<h1>See what's popular</h1>";
            }
            else {
                submissionViewTitle.Text = "<h1>See what's new</h1>";
                subList = bo.GetListOfSubmissionsNew(NUMBER_OF_SUBMISSIONS);
            }

            foreach (SubmissionVO sub in subList) {

                Submission submission = (Submission)Page.LoadControl("~/Controls/Submission.ascx");
                submission.ID = "submission" + sub.SubmissionID;
                submission.submissionID = sub.SubmissionID;

                submissionsPanel.Controls.Add(submission);
            }
        }
    }
}
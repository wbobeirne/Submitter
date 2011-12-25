using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.Infrastructure.ValueObjects;
using Submitter.BusinessLogic.BusinessObjects;

namespace Submitter.Web.Controls {
    public partial class Submission : System.Web.UI.UserControl {

        public int submissionID;

        protected void Page_Load(object sender, EventArgs e) {

            GenerateSubmissionDetails();

        }

        protected void GenerateSubmissionDetails() {

            CommentManagementBO comBO = new CommentManagementBO();
            SubmissionManagementBO bo = new SubmissionManagementBO();
            SubmissionVO sub = bo.GetSubmission(submissionID);
            UserManagementBO userBO = new UserManagementBO();
            UserVO vo = userBO.GetUser(sub.UserID);

            submissionRating.Text = sub.Rating.ToString();
            submissionCommentLink.Text = (comBO.GetListOfSubmissionComments(submissionID).Count + " comments");

            Uri url;
            try {
                url = new Uri(sub.Link);
            }
            catch (Exception e) {
                try {
                    url = new Uri("http://" + sub.Link);
                }
                catch (Exception exc) {
                    url = new Uri("http://CouldntParseUrl");
                }
            }

            submissionTitle.Text = "<a href=\"" + url + "\">" + sub.Title + "</a> (" + url.Host.ToString() + ")";

            submissionDetails.Text = bo.FormatePostTime(sub.PostTime);

            userLink.Text = vo.Username;

            //Change arrow based on voting
            if(Session["login"] != null){
                int i = userBO.CheckIfVoted(submissionID, userBO.GetUser(Session["login"].ToString()).UserID);
                if (i == 1) {
                    upArrow.ImageUrl = "~/Images/uparrow_voted.png";
                }
                else if (i == -1) {
                    downArrow.ImageUrl = "~/Images/downarrow_voted.png";
                }
            }
        }

        protected void UpArrow_Click(object sender, EventArgs e) {
            SubmitVote(1);
        }

        protected void DownArrow_Click(object sender, EventArgs e) {
            SubmitVote(-1);
        }

        protected void SubmitVote(int vote) {
            UserManagementBO userBO = new UserManagementBO();
            UserVO userVO;
            if (Session["login"] != null) {
                userVO = userBO.GetUser(Session["login"].ToString());
                if (userBO.CheckIfVoted(submissionID, userVO.UserID) == 0) {
                    userBO.SubmitVote(submissionID, userVO.UserID, vote);
                    int i = int.Parse(submissionRating.Text);
                    i += vote;
                    submissionRating.Text = i.ToString();
                    if (vote == 1) {
                        upArrow.ImageUrl = "~/Images/uparrow_voted.png";
                    }
                    else if (vote == -1) {
                        downArrow.ImageUrl = "~/Images/downarrow_voted.png";
                    }
                }
            }
        }

        protected void SubmissionCommentLink_Click(object sender, EventArgs e) {
            Response.Redirect(WebConstants.SUBMISSION_COMMENT_PAGE + "?subid=" + submissionID);
        }

        protected void UserLink_Click(object sender, EventArgs e) {
            Response.Redirect(WebConstants.USER_PROFILE_PAGE + "?username=" + userLink.Text);
        }
    }
}
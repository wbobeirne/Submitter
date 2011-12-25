using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.Infrastructure.ValueObjects;
using Submitter.BusinessLogic.BusinessObjects;

namespace Submitter.Web.Controls{

    public partial class UserViewControl : System.Web.UI.UserControl{

        public string Username { get; set; }
        public int UserID { get; set; }

        protected void Page_Load(object sender, EventArgs e){

        }


        public void LoadUserDetails(){

            UserManagementBO bo = new UserManagementBO();
            UserVO vo = null;

            if (Username != null){
                vo = bo.GetUser(Username);
                UserID = vo.UserID;
            }
            else if (UserID != 0){
                vo = bo.GetUser(UserID);
                Username = vo.Username;
            }

            usernameLabel.Text = "<h1>" + vo.Username + "</h1>";
            ratingLabel.Text = vo.Rating + " collective positive rating";
            joinDateLabel.Text = "Joined on " + vo.RegisterDate.ToLongDateString();

            if (Session["login"] != null) {
                if (Session["login"].ToString() == Username) {
                    passwordChangePanel.Visible = true;
                }
            }

            //Load the content in the first time we visit
            if (!Page.IsPostBack) {
                GenerateRecentContent("submissions");
            }
        }

        private void GenerateRecentContent(string type) {

            if (type == "submissions") {

                //We need to add a table for submissions, since they are just trs and tds
                Literal lit1 = new Literal();
                lit1.Text = "<table width=\"100%\">";
                recentContentPanel.Controls.Add(lit1);

                SubmissionManagementBO bo = new SubmissionManagementBO();
                List<SubmissionVO> subList = bo.GetListOfSubmissionsByUser(5, UserID);

                foreach (SubmissionVO sub in subList) {

                    Submission submission = (Submission)Page.LoadControl("~/Controls/Submission.ascx");
                    submission.ID = "submission" + sub.SubmissionID;
                    submission.submissionID = sub.SubmissionID;

                    recentContentPanel.Controls.Add(submission);
                }

                //We need to close the table tag we added
                Literal lit2 = new Literal();
                lit2.Text = "</table>";
                recentContentPanel.Controls.Add(lit2);
            }
            else if(type == "comments") {

                CommentManagementBO bo = new CommentManagementBO();
                List<CommentVO> comList = bo.GetListOfCommentsByUserID(5, UserID);

                foreach (CommentVO comment in comList) {

                    Comment com = (Comment)Page.LoadControl("~/Controls/Comment.ascx");
                    com.ID = "comment" + comment.CommentID;
                    com.commentDepth = 0;
                    com.commentID = comment.CommentID;

                    recentContentPanel.Controls.Add(com);
                }
            }
        }

        protected void PasswordChange_Click(object sender, EventArgs e) {

            UserManagementBO bo = new UserManagementBO();
            UserVO vo = bo.GetUser(UserID);

            //Look for invalid inputs
            try {
                if (newPassword.Text != newPasswordConfirm.Text) {
                    throw new Exception("Both new password fields must match");
                }

                if (!bo.CheckIfValidLogin(Username, oldPassword.Text)) {
                    throw new Exception("Invalid entry for current password");
                }

                bo.ChangePassword(UserID, newPassword.Text);
                errorLabel.Text = "Password succesfully changed";
            }
            catch (Exception exc) {
                errorLabel.Text = exc.Message;
            }

        }

        protected void Submissions_Click(object sender, EventArgs e) {

            GenerateRecentContent("submissions");

        }

        protected void Comments_Click(object sender, EventArgs e) {

            GenerateRecentContent("comments");

        }
    }
}
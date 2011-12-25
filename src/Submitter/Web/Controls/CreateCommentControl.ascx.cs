using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.Infrastructure.ValueObjects;
using Submitter.BusinessLogic.BusinessObjects;

namespace Submitter.Web.Controls {
    public partial class CreateCommentControl : System.Web.UI.UserControl {

        public int submissionID;
        public int parentCommentID;

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void SubmitButton_Click(object sender, EventArgs e) {
            CommentManagementBO bo = new CommentManagementBO();

            try {
                if (Session["login"] == null) {
                    throw new Exception("You must be logged in to comment!");
                }
                else if (commentContents.Text.Equals("")) {
                    throw new Exception("A comment must be entered");
                }
                string comText = commentContents.Text;
                comText = Server.HtmlEncode(comText);
                comText = comText.Replace(Environment.NewLine, "<br/>");
                bo.CreateNewComment(Session["login"].ToString(), comText, submissionID, parentCommentID);
                Response.Redirect(WebConstants.SUBMISSION_COMMENT_PAGE + "?subid=" + submissionID);
            }
            catch (Exception exc) {
                errorLabel.Text = exc.Message;
            }
        }
    }
}
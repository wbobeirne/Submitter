using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.Infrastructure.ValueObjects;
using Submitter.BusinessLogic.BusinessObjects;

namespace Submitter.Web.Controls {
    public partial class CommentReplyControl : System.Web.UI.UserControl {

        protected void Page_Load(object sender, EventArgs e) {


        }

        //Much like edit, reply needs this hacky solution because properties get reset
        public void SetParentCommentID(int pcid) {
            parentCommentID.Text = pcid.ToString();
        }

        protected void SubmitReply_Click(object sender, EventArgs e) {
            CommentManagementBO bo = new CommentManagementBO();
            UserManagementBO userBO = new UserManagementBO();

            try {
                if (replyTextBox.Text == "") {
                    throw new Exception("You must enter text to reply");
                }

                bo.CreateNewComment(userBO.GetUser(Session["login"].ToString()).Username, replyTextBox.Text, 
                                    Convert.ToInt32(Request.QueryString["subid"]), Convert.ToInt32(parentCommentID.Text));

                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception exc) {
                errorLabel.Text = exc.Message;
            }

        }

        protected void CancelReply_Click(object sender, EventArgs e) {

            this.Visible = false;
        }
    }
}
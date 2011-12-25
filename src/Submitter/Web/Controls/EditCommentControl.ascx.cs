using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.BusinessLogic.BusinessObjects;
using Submitter.Infrastructure.ValueObjects;

namespace Submitter.Web.Controls {
    public partial class EditCommentControl : System.Web.UI.UserControl {

        public const string NO_TEXT_EXCEPTION = "You need to enter text";

        protected void Page_Load(object sender, EventArgs e) {

        }

        public void LoadInitialText(CommentVO vo) {
            string text = vo.CommentContents;
            text = text.Replace("<br/>", Environment.NewLine);
            text = text.Replace("&lt;", "<");
            text = text.Replace("&amp;", "&");
            text = text.Replace("&quot;", "\"");
            text = text.Replace("&gt;", ">");
            text = text.Replace("<p>", "");
            text = text.Replace("</p>", "");
            editTextBox.Text = text;
            //A stupid little hack I'm having to put in, because my properties keep getting reset to null
            commentID.Text = vo.CommentID.ToString();
        }

        protected void SubmitEdit_Click(object sender, EventArgs e) {
            CommentManagementBO bo = new CommentManagementBO();
            //A stupid little hack I'm having to put in, because my properties keep getting reset to null
            CommentVO commentVO = bo.GetComment(Convert.ToInt32(commentID.Text));
            try {
                //Check for no text
                if (editTextBox.Text.Equals("")) {
                    throw new Exception(NO_TEXT_EXCEPTION);
                }

                //Edit comment
                string comText = editTextBox.Text;
                comText = Server.HtmlEncode(comText);
                comText = comText.Replace(Environment.NewLine, "<br/>");
                commentVO.CommentContents = comText;
                bo.UpdateComment(commentVO);
                this.Visible = false;
                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception exc) {
                errorLabel.Text = exc.Message;
            }
        }

        protected void CancelEdit_Click(object sender, EventArgs e) {
            this.Visible = false;
        }
    }
}
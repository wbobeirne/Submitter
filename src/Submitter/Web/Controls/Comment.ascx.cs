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
    public partial class Comment : System.Web.UI.UserControl {

        public int commentID;
        public int commentDepth;
        private int commentRating;
        private CommentVO comVO;

        protected void Page_Load(object sender, EventArgs e) {
            LoadCommentDetails();
        }


        protected void LoadCommentDetails() {
            CommentManagementBO bo = new CommentManagementBO();
            UserManagementBO userBO = new UserManagementBO();
            CommentVO vo = bo.GetComment(commentID);
            comVO = vo;

            //Give comment space for its depth
            for (int i = 0; i < commentDepth; i++) {
                spacingLabel.Text = spacingLabel.Text + "<td width=\"25px\"></td>";
            }

            commentRating = vo.Rating;
            string username = userBO.GetUser(vo.UserID).Username;

            userLink.Text = username;

            commentInfo.Text = " rated at " +
                                commentRating + " points, posted " +
                                bo.FormatePostTime(vo.PostDate);

            commentContents.Text = "<p>" + vo.CommentContents + "</p>";

            //Choose which controls to display
            if(Session["login"] == null){
                editButton.Visible = false;
                deleteButton.Visible = false;
                replyButton.Visible = false;
            }
            else if (!username.Equals(Session["login"].ToString())) {
                editButton.Visible = false;
                deleteButton.Visible = false;
            }

            //Change vote image based on whether or not it's been voted on
            if(Session["login"] != null){
                int i = bo.CheckIfVoted(commentID, userBO.GetUser(Session["login"].ToString()).UserID);
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


        protected void DeleteButton_Click(object sender, EventArgs e) {
            CommentManagementBO bo = new CommentManagementBO();
            bo.DeleteComment(commentID);
            Response.Redirect(Request.Url.ToString());
        }


        protected void EditButton_Click(object sender, EventArgs e) {
            commentReplyControl.Visible = false;
            editCommentControl.Visible = true;
            editCommentControl.LoadInitialText(comVO);
        }


        protected void ReplyButton_Click(object sender, EventArgs e) {
            editCommentControl.Visible = false;
            commentReplyControl.Visible = true;
            commentReplyControl.SetParentCommentID(commentID);
        }


        protected void SubmitVote(int vote) {
            CommentManagementBO comBO = new CommentManagementBO();
            UserManagementBO userBO = new UserManagementBO();
            CommentVO comVO = comBO.GetComment(commentID);
            UserVO userVO;
            if (Session["login"] != null) {
                userVO = userBO.GetUser(Session["login"].ToString());
                if (comBO.CheckIfVoted(commentID, userVO.UserID) == 0) {
                    comBO.SubmitVote(commentID, userVO.UserID, vote);
                    commentRating += vote;
                    Response.Redirect(Request.Url.ToString());
                }
            }
        }

        protected void UserLink_Click(Object sender, EventArgs e) {
            Response.Redirect(WebConstants.USER_PROFILE_PAGE + "?username=" + userLink.Text);
        }
    }
}
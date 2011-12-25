using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.BusinessLogic.BusinessObjects;
using Submitter.Infrastructure.ValueObjects;

namespace Submitter.Web.Controls {
    public partial class CommentViewControl : System.Web.UI.UserControl {

        public int submissionID;
        public int userID;

        protected void Page_Load(object sender, EventArgs e) {
            AddComments();
        }

        protected void AddComments() {
            if (submissionID != 0) {
                CommentManagementBO bo = new CommentManagementBO();
                List<CommentVO> commentList = bo.GetListOfSubmissionComments(submissionID);
                commentList = bo.SortComments(commentList);

                CommentVO previousComment = new CommentVO();
                Stack<int> pcStack = new Stack<int>();
                int depth = 0;

                foreach (CommentVO comment in commentList) {
                    //This determines the depth of the comment
                    //We only want to be indenting once, not every postback
                    if (!Page.IsPostBack) {
                        if (comment.ParentCommentID == 0) {
                            pcStack.Push(comment.CommentID);
                            depth = 0;
                        }
                        else if (comment.ParentCommentID == pcStack.Peek()) {
                            if (comment.ParentCommentID == previousComment.CommentID) {
                                pcStack.Push(comment.CommentID);
                                depth++;
                            }
                        }
                        else {
                            pcStack.Pop();
                            depth--;
                        }
                    }

                    Comment com = (Comment)Page.LoadControl("~/Controls/Comment.ascx");
                    com.ID = "comment" + comment.CommentID;
                    com.commentDepth = depth;
                    com.commentID = comment.CommentID;

                    commentPanel.Controls.Add(com);

                    previousComment = comment;
                }
            }
            else if (userID != 0) {
                CommentManagementBO bo = new CommentManagementBO();
                List<CommentVO> commentList = bo.GetListOfCommentsByUserID(userID, 5);

                foreach (CommentVO comment in commentList) {

                    Comment com = (Comment)Page.LoadControl("~/Controls/Comment.ascx");
                    com.ID = "comment" + comment.CommentID;
                    com.commentID = comment.CommentID;

                    commentPanel.Controls.Add(com);
                }
            }
        }
    }
}
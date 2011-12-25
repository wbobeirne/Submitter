using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Submitter.Infrastructure.ValueObjects;
using Submitter.BusinessLogic;
using Submitter.DataAccess.DataAccessObjects;

namespace Submitter.BusinessLogic.BusinessObjects {
    public class CommentManagementBO : BaseBO {

        public CommentManagementBO()
            : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {

        }

        public CommentManagementBO(Type loggerClassType) 
            : base(loggerClassType) { 
    
        }


        public CommentVO GetComment(int cID) {
            CommentDAO dao = new CommentDAO();
            return dao.GetComment(cID);
        }

        public List<CommentVO> GetListOfSubmissionComments(int subID) {
            CommentDAO dao = new CommentDAO();
            List<CommentVO> commentList = dao.GetAllCommentsInASubmission(subID);

            return commentList;
        }

        public List<CommentVO> GetListOfCommentsByUserID(int size, int userID) {
            CommentDAO dao = new CommentDAO();
            List<CommentVO> commentList = dao.GetAllUsersComments(userID);

            TruncateList(commentList, size);

            return commentList;
        }

        public void DeleteComment(int cID) {
            CommentDAO dao = new CommentDAO();
            dao.DeleteComment(cID);
        }

        public void CreateNewComment(string username, string commentContents, int subID, int pcID) {
            CommentDAO dao = new CommentDAO();
            UserDAO userDAO = new UserDAO();
            CommentVO comment = new CommentVO();

            if (commentContents == null) {
                throw new Exception("You need to enter a comment");
            }

            comment.UserID = userDAO.GetUser(username).UserID;
            comment.CommentContents = commentContents;
            comment.PostDate = DateTime.Now;
            comment.Rating = 0;
            if (pcID != 0) {
                comment.ParentCommentID = pcID;
            }
            comment.SubmissionID = subID;

            dao.InsertComment(comment);
        }

        public List<CommentVO> SortComments(List<CommentVO> comList) {
            List<CommentVO> sortedList = new List<CommentVO>();

            //First get them in rating order...
            comList.Sort((x, y) => y.Rating.CompareTo(x.Rating));

            //Then, we find the highest rated no parent ID comment
            foreach (CommentVO com in comList) {
                if (com.ParentCommentID == 0) {
                    sortedList.Add(com);
                    AddChildrenComments(comList, sortedList, com.CommentID);
                }
            }

            return sortedList;
        }

        private void AddChildrenComments(List<CommentVO> comList, List<CommentVO> sortedList, int pcID) {
            foreach (CommentVO com in comList) {
                if (com.ParentCommentID == pcID) {
                    sortedList.Add(com);
                    AddChildrenComments(comList, sortedList, com.CommentID);
                }
            }
        }


        public void UpdateComment(CommentVO comment) {
            CommentDAO dao = new CommentDAO();

            dao.UpdateComment(comment);
        }

        public string FormatePostTime(DateTime time) {
            TimeSpan ts = DateTime.Now.Subtract(time);

            string returnString = "";

            int mins = ts.Minutes;
            int hours = ts.Hours;
            int days = ts.Days;

            if (days == 0) {
                if (hours == 0) {
                    if (mins == 0) {
                        returnString = "a few seconds ago";
                    }
                    else if (mins == 1) {
                        returnString = "1 minute ago";
                    }
                    else {
                        returnString = ts.Minutes.ToString() + " minutes ago";
                    }
                }
                else if (hours == 1) {
                    returnString = "1 hour ago";
                }
                else {
                    returnString = ts.Hours.ToString() + " hours ago";
                }
            }
            else if (days == 1) {
                returnString = "1 day ago";
            }
            else {
                returnString = ts.Days.ToString() + " days ago";
            }

            return returnString;
        }


        public int CheckIfVoted(int comID, int userID) {

            int voted;
            CommentDAO dao = new CommentDAO();

            voted = dao.CheckIfVoted(userID, comID);

            return voted;

        }

        public void SubmitVote(int comID, int uID, int vote) {
            UserManagementBO userBO = new UserManagementBO();

            CommentDAO dao = new CommentDAO();

            dao.SubmitVote(uID, comID, vote);
            userBO.ChangeRating(uID, vote);
        }


        private void TruncateList(List<CommentVO> list, int size) {
            try {
                list.RemoveRange(size, list.Count - size);
            }
            catch (Exception e) {

            }
        }

    }
}

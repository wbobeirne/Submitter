using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Submitter.Infrastructure.ValueObjects {
    public class CommentVO {

        private int commentID;
        private string commentContents;
        private DateTime postDate;
        private int rating;
        private int userID;
        private int submissionID;
        private int parentCommentID;

        public int CommentID { 
            get { return commentID;} 
            set {commentID = value;}
        }

        public string CommentContents { 
            get { return commentContents;} 
            set {commentContents = value;}
        }

        public DateTime PostDate {
            get { return postDate; }
            set { postDate = value; }
        }

        public int Rating { 
            get { return rating;} 
            set {rating = value;}
        }

        public int UserID {
            get { return userID; }
            set { userID = value; }
        }

        public int SubmissionID {
            get { return submissionID; }
            set { submissionID = value; }
        }

        public int ParentCommentID {
            get { return parentCommentID; }
            set { parentCommentID = value; }
        }

        public CommentVO() {

            commentID = 0;
            commentContents = String.Empty;
            rating = 0;
            userID = 0;
            submissionID = 0;
            parentCommentID = 0;

        }

        public CommentVO(int cID, string comConts, DateTime pDate, int rtng, int uID, int sID, int pcID) {

            commentID = cID;
            commentContents = comConts;
            postDate = pDate;
            rating = rtng;
            userID = uID;
            submissionID = sID;
            parentCommentID = pcID;

        }

    }
}

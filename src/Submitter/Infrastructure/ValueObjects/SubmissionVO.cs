using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Submitter.Infrastructure.ValueObjects {
    public class SubmissionVO {

        private int submissionID;
        private string title;
        private string link;
        private int rating;
        private int userID;
        private DateTime postTime;

        public int SubmissionID { 
            get { return submissionID;} 
            set {submissionID = value;}
        }

        public string Title { 
            get { return title;} 
            set {title = value;}
        }

        public string Link {
            get { return link; }
            set { link = value; }
        }

        public int Rating { 
            get { return rating;} 
            set {rating = value;}
        }

        public int UserID {
            get { return userID; }
            set { userID = value; }
        }

        public DateTime PostTime {
            get { return postTime; }
            set { postTime = value; }
        }

        public SubmissionVO() {

            submissionID = 0;
            title = String.Empty;
            link = String.Empty;
            rating = 0;
            userID = 0;

        }

        public SubmissionVO(int sID, string ttle, string lnk, int rtng, int uID, DateTime pstTime) {

            submissionID = sID;
            title = ttle;
            link = lnk;
            rating = rtng;
            userID = uID;
            postTime = pstTime;

        }

    }
}

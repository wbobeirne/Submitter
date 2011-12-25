using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Submitter.Infrastructure.ValueObjects;
using Submitter.BusinessLogic;
using Submitter.DataAccess.DataAccessObjects;

namespace Submitter.BusinessLogic.BusinessObjects {
    public class SubmissionManagementBO : BaseBO {

        public SubmissionManagementBO()
            : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {

        }

        public SubmissionManagementBO(Type loggerClassType) 
            : base(loggerClassType) { 
    
        }

        public SubmissionVO GetSubmission(int subID) {
            SubmissionDAO dao = new SubmissionDAO();
            return dao.GetSubmission(subID);
        }

        public List<SubmissionVO> GetListOfSubmissionsNew(int size) {
            List<SubmissionVO> subList = new List<SubmissionVO>();
            SubmissionDAO dao = new SubmissionDAO();
            subList = dao.GetAllSubmissions();

            subList.Sort((x, y) => DateTime.Compare(y.PostTime, x.PostTime));

            TruncateList(subList, size);

            return subList;
        }

        public List<SubmissionVO> GetListOfSubmissionsTop(int size) {
            List<SubmissionVO> subList;
            SubmissionDAO dao = new SubmissionDAO();
            subList = dao.GetAllSubmissions();

            subList.Sort((x, y) => y.Rating.CompareTo(x.Rating));

            TruncateList(subList, size);

            return subList;
        }

        public List<SubmissionVO> GetListOfSubmissionsPopular(int size) {
            List<SubmissionVO> subList;
            SubmissionDAO dao = new SubmissionDAO();
            subList = dao.GetAllSubmissions();

            subList.Sort((x, y) => y.Rating.CompareTo(x.Rating));

            List<SubmissionVO> newList = new List<SubmissionVO>();

            //Can't remove things from a list in a foreach
            foreach (SubmissionVO vo in subList) {
                if (!(vo.PostTime.Date < DateTime.Now.AddDays(-1d))) {
                    newList.Add(vo);
                }
            }

            subList = newList;

            TruncateList(subList, size);

            return subList;
        }

        public List<SubmissionVO> GetListOfSubmissionsByUser(int size, int userID) {
            List<SubmissionVO> subList;
            SubmissionDAO dao = new SubmissionDAO();
            subList = dao.GetAllUsersSubmissions(userID);

            subList.Sort((x, y) => DateTime.Compare(y.PostTime, x.PostTime));

            TruncateList(subList, size);

            return subList;
        }

        public void CreateNewSubmission(string title, string link, string username) {

            SubmissionDAO dao = new SubmissionDAO();
            UserDAO userDAO = new UserDAO();
            SubmissionVO newSub = new SubmissionVO();

            if (title == null) {
                throw new Exception("You need to enter a title");
            }

            if (link == null) {
                throw new Exception("You need to enter a link");
            }

            newSub.Title = title;
            newSub.Link = link;
            newSub.Rating = 0;
            newSub.PostTime = DateTime.Now;
            newSub.UserID = userDAO.GetUser(username).UserID;

            dao.InsertSubmission(newSub);

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
                else if(hours == 1){
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

        private void TruncateList(List<SubmissionVO> list, int size) {
            try {
                list.RemoveRange(size, list.Count - size);
            }
            catch (Exception e) {

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Submitter.Infrastructure.ValueObjects {
    public class UserVO {

        private int userID;
        private string username;
        private int rating;
        private string password;
        private DateTime registerDate;

        public int UserID { 
            get { return userID;} 
            set {userID = value;}
        }

        public string Username { 
            get { return username;} 
            set {username = value;}
        }

        public int Rating { 
            get { return rating;} 
            set {rating = value;}
        }

        public string Password {
            get { return password; }
            set { password = value; }
        }

        public DateTime RegisterDate {
            get { return registerDate; }
            set { registerDate = value; }
        }

        public UserVO() {

            userID = 0;
            username = string.Empty;
            rating = 0;
            password = string.Empty;
            registerDate = DateTime.MinValue;

        }

        public UserVO(int eID, string uname, int rtng, string pass, DateTime regDateTime) {

            userID = eID;
            username = uname;
            rating = rtng;
            password = pass;
            regDateTime = registerDate;

        }

    }
}

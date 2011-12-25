using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Security.Cryptography;

using Submitter.Infrastructure.ValueObjects;
using Submitter.BusinessLogic;
using Submitter.DataAccess.DataAccessObjects;

namespace Submitter.BusinessLogic.BusinessObjects {
    public class UserManagementBO : BaseBO {

        //Exception constants
        public const string USERNAME_TAKEN_EXCEPTION = "That username is already in use";
        public const string PASSWORD_INCORRECT_EXCEPTION = "The password you entered is incorrect";
        public const string USERNAME_DOES_NOT_EXIST_EXCEPTION = "That username does not exist";

        public UserManagementBO()
            : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {

        }

        public UserManagementBO(Type loggerClassType) 
            : base(loggerClassType) { 
    
        }


        public UserVO GetUser(int uID) {
            UserDAO dao = new UserDAO();
            return dao.GetUser(uID);
        }

        public UserVO GetUser(string username) {
            UserDAO dao = new UserDAO();
            return dao.GetUser(username);
        }

        //Hash the passed in password, then check it against the stored user's password
        public bool CheckIfValidLogin(string username, string password) {

            bool valid = false;
            UserDAO dao = new UserDAO();
            UserVO loginCompare;
            string hashedPass = HashPassword(password);

            System.Diagnostics.Debug.WriteLine("In checkifvalidlogin, username: " + username);

            //If you can't find a user under that name, throw the exception
            try {
                loginCompare = dao.GetUser(username);
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e);
                throw new Exception(USERNAME_DOES_NOT_EXIST_EXCEPTION);
            }

            //If the password is not equal, throw the exception
            if (hashedPass.Equals(loginCompare.Password)) {
                valid = true;
            }
            else {
                throw new Exception(PASSWORD_INCORRECT_EXCEPTION);
            }
            

            return valid;
        }

        public void CreateNewUser(string username, string password) {

            UserDAO dao = new UserDAO();
            UserVO newUser = new UserVO();

            //Check to see if there's already a user of that name
            if (CheckIfUsernameTaken(username)) {
                throw new Exception(USERNAME_TAKEN_EXCEPTION);
            }

            newUser.Username = username;
            newUser.Password = HashPassword(password);
            newUser.RegisterDate = DateTime.Now;
            newUser.Rating = 0;

            InsertUser(newUser);

        }

        public void SubmitVote(int subID, int uID, int vote) {

            UserDAO dao = new UserDAO();

            dao.SubmitVote(uID, subID, vote);
            ChangeRating(uID, vote);
        }

        public int CheckIfVoted(int subID, int uID) {

            int voted;
            UserDAO dao = new UserDAO();

            voted = dao.CheckIfVoted(uID, subID);

            return voted;

        }

        //True if taken, false if not taken
        private bool CheckIfUsernameTaken(string username) {

            bool taken = true;
            UserDAO dao = new UserDAO();
            UserVO vo;

            //Try to grab a vo with the name passed in. If it throws
            //an exception, that means it couldn't find a user with that name
            try {
                vo = dao.GetUser(username);
                Console.WriteLine("CheckIfUsername: " + vo.Username);
            }
            catch (Exception e) {
                taken = false;
            }

            return taken;
        }

        private UserVO InsertUser(UserVO newUser) {

            try {

                using(TransactionScope ts = new TransactionScope()){
                    UserDAO dao = new UserDAO();
                    LogDebug("Inserting Employee " + newUser.Username);
                    newUser = dao.InsertUser(newUser);
                    LogDebug("User succesfully inserted!");
                    ts.Complete();
                }

            }
            catch (Exception e) {
                LogError(e.ToString());
                throw e;
            }

            return newUser;

        }

        public void UpdateComment(UserVO user) {
            UserDAO dao = new UserDAO();

            dao.UpdateUser(user);
        }

        public void ChangePassword(int userID, string newPass) {
            UserDAO dao = new UserDAO();
            UserVO vo = GetUser(userID);

            vo.Password = HashPassword(newPass);
            dao.UpdateUser(vo);
        }


        public void ChangeRating(int userID, int rating) {
            UserDAO dao = new UserDAO();

            dao.ChangeRating(userID, rating);
        }

        //SHA256 hash algorithm method I found over yonder:
        //http://www.techlicity.com/blog/dotnet-hash-algorithms.html
        private string HashPassword(string password) {

            UnicodeEncoding ue = new UnicodeEncoding();
            string hashedPass = "";
            byte[] hashValue;
            byte[] pass = ue.GetBytes(password);
            SHA256Managed hash = new SHA256Managed();

            hashValue = hash.ComputeHash(pass);

            foreach (byte x in hashValue) {
                hashedPass += String.Format("{0:x2}", x);
            }

            return hashedPass;
        }

    }
}

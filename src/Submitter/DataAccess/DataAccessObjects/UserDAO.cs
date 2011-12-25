using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using Submitter.Infrastructure.ValueObjects;

namespace Submitter.DataAccess.DataAccessObjects {
    public class UserDAO : BaseDAO {

        //Constants for all the columns
        private const String USER_ID = "@userID";
        private const String USERNAME = "@username";
        private const String RATING = "@rating";
        private const String PASSWORD = "@password";
        private const String REGISTER_DATE = "@registerdate";

        //Sql command constants
        private const String SELECT_ALL_COLUMNS =
            "SELECT tbl_User.UserID, " +
            "Username, " +
            "Rating, " +
            "Password, " +
            "RegisterDate ";

        private const String SELECT_USER_BY_USERNAME =
            SELECT_ALL_COLUMNS +
            "FROM tbl_User " +
            "WHERE Username = " + USERNAME;

        private const String SELECT_ALL_USERS =
            SELECT_ALL_COLUMNS + 
            "FROM tbl_User ";

        private const String SELECT_USER_BY_USER_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_User " +
            "WHERE UserID = " + USER_ID;

        private const String SELECT_ALL_USER_VOTE_STATUSES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_User_Rating_Submission_xref " +
            "WHERE UserID = " + USER_ID;


        private const String INSERT_USER =
            "INSERT INTO tbl_User " +
            "(Username, Rating, Password, RegisterDate) " +
                "VALUES (" + 
                USERNAME + ", " + 
                RATING + ", " + 
                PASSWORD + ", " +
                REGISTER_DATE + ")" +
            "SELECT scope_identity()";

        private const String UPDATE_USER =
            "UPDATE tbl_User " +
            "SET Username = " + USERNAME + ", " +
                "Rating = " + RATING + ", " +
                "Password = " + PASSWORD + ", " +
                "RegisterDate = " + REGISTER_DATE + " " +
            "WHERE UserID = " + USER_ID;

        private const String DELETE_USER =
            "DELETE FROM tbl_User " +
            "WHERE UserID = " + USER_ID;


        //Constructor
        public UserDAO()
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {

        }

        //This method passes in an ID, creates a SQL command with that ID, and then calls the
        //private method that takes a SQL command as an argument
        public UserVO GetUser(int uID) {
            LogDebug("Retrieving user with a user ID of " + uID + "...");

            DbCommand command = Database.GetSqlStringCommand(SELECT_USER_BY_USER_ID);
            Database.AddInParameter(command, USER_ID, DbType.Int32, uID);

            return GetUser(command);

        }

        public UserVO GetUser(string username) {
            LogDebug("Retrieving user with a username of " + username + "...");

            DbCommand command = Database.GetSqlStringCommand(SELECT_USER_BY_USERNAME);
            Database.AddInParameter(command, USERNAME, DbType.String, username);

            return GetUser(command);
        }

        private UserVO GetUser(DbCommand command) {

            UserVO user = null;
            IDataReader reader = null;

            try {

                reader = Database.ExecuteReader(command);
                LogDebug("DataReader created, retrieving user VO...");

                if (reader.Read()) {
                    user = FillInUserVO(ref reader);
                }

                LogDebug("User VO retrieved!");
            }
            catch (Exception e) {
                LogError("Something exploded in trying to retrieve a user VO!", e);
                throw new Exception("Something exploded in trying to retrieve a user VO!", e);
            }
            finally {
                base.CloseReader(reader);
            }

            if (user == null) {
                throw new Exception("No user found with that name!");
            }

            return user;
        }


        private UserVO FillInUserVO(ref IDataReader reader) {

            UserVO user = new UserVO();

            user.UserID = reader.GetInt32(0);
            user.Username = reader.GetString(1);
            user.Rating = reader.GetInt32(2);
            user.Password = reader.GetString(3);
            user.RegisterDate = reader.GetDateTime(4);

            return user;

        }

        //Returns a list of all users
        public List<UserVO> GetAllUsers() {

            LogDebug("Retrieving and building list of all users...");

            DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_USERS);

            return GetUserList(command);
        }


        private List<UserVO> GetUserList(DbCommand command) {

            List<UserVO> userList = new List<UserVO>();
            IDataReader reader = null;

            try {

                reader = Database.ExecuteReader(command);
                LogDebug("DataReader created, retrieving requested UserVOs...");

                while(reader.Read()) {
                    UserVO user = new UserVO();

                    user.UserID = reader.GetInt32(0);
                    user.Username = reader.GetString(1);
                    user.Rating = reader.GetInt32(2);
                    user.Password = reader.GetString(3);
                    user.RegisterDate = reader.GetDateTime(4);

                    userList.Add(user);
                }

                LogDebug("List created!");
            }
            catch (Exception e) {
                LogError("Something exploded in trying to retrieve a user VO!", e);
                throw new Exception("Something exploded in trying to retrieve a user VO!", e);
            }
            finally {
                base.CloseReader(reader);
            }

            return userList;

        }


        public UserVO InsertUser(UserVO user) {

            int uID = 0;

            try {
                DbCommand command = Database.GetSqlStringCommand(INSERT_USER);


                Database.AddInParameter(command, USERNAME, DbType.String, user.Username);
                Database.AddInParameter(command, RATING, DbType.Int32, user.Rating);
                Database.AddInParameter(command, PASSWORD, DbType.String, user.Password);
                Database.AddInParameter(command, REGISTER_DATE, DbType.DateTime, user.RegisterDate);

                uID = Convert.ToInt32(Database.ExecuteScalar(command));
            }
            catch (Exception e) {
                LogError("Something exploded while trying to insert a user!", e);
                throw new Exception("Something exploded while trying to insert a user!", e);
            }

            return GetUser(uID);

        }


        public void DeleteUser(UserVO user) {

            int rowsAffected = 0;

            try {
                DbCommand command = Database.GetSqlStringCommand(DELETE_USER);

                Database.AddInParameter(command, USER_ID, DbType.Int32, user.UserID);

                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Something exploded while deleting a user from the database!", e);
                throw new Exception("Something exploded while deleting a user from the database!", e);
            }

            if (rowsAffected == 0) {
                LogError("No user found with an ID of " + user.UserID);
                throw new Exception("The delete command didn't delete anything!");
            }
        }

        public int CheckIfVoted(int userID, int submissionID) {
            //1 means positively voted, 0 means not voted, -1 means negatively voted
            int voted = 0;

            DbCommand command = Database.GetSqlStringCommand(
            "SELECT tbl_User_Rating_Submission_xref.UserID, " +
            "SubmissionID, " +
            "VoteType " +
            "FROM tbl_User_Rating_Submission_xref " +
            "WHERE UserID = " + userID + " " +
            "and SubmissionID = " + submissionID);

            IDataReader reader = null;

            try {

                reader = Database.ExecuteReader(command);

                if (reader.Read()) {
                    voted = reader.GetInt32(2);
                }
            }
            catch (Exception e) {
                LogError("Something exploded in trying to retrieve a vote status!", e);
                throw new Exception("Something exploded in trying to retrieve a vote status!", e);
            }
            finally {
                base.CloseReader(reader);
            }

            System.Diagnostics.Debug.WriteLine(voted);

            return voted;
        }

        public void SubmitVote(int userID, int submissionID, int voteType) {

            SubmissionDAO subDAO = new SubmissionDAO();

            String submitVote =
            "INSERT INTO tbl_User_Rating_Submission_xref " +
            "(UserID, SubmissionID, VoteType) " +
                "VALUES (" +
                userID + ", " +
                submissionID + ", " +
                voteType + ")" +
            "SELECT scope_identity()";

            try {
                DbCommand command = Database.GetSqlStringCommand(submitVote);

                Database.ExecuteScalar(command);

                subDAO.AdjustRating(submissionID, voteType);
            }
            catch (Exception e) {
                LogError("Something exploded while trying to submit a vote!", e);
                throw new Exception("Something exploded while trying to submit a vote!", e);
            }

        }


        public void ChangeRating(int userID, int rating) {
            UserVO vo = GetUser(userID);

            vo.Rating += rating;

            UpdateUser(vo);
        }

        public void UpdateUser(UserVO user) {

            DbCommand command = Database.GetSqlStringCommand(UPDATE_USER);

            try {

                Database.AddInParameter(command, USER_ID, DbType.Int32, user.UserID);
                Database.AddInParameter(command, USERNAME, DbType.String, user.Username);
                Database.AddInParameter(command, RATING, DbType.Int32, user.Rating);
                Database.AddInParameter(command, PASSWORD, DbType.String, user.Password);
                Database.AddInParameter(command, REGISTER_DATE, DbType.DateTime, user.RegisterDate);

                Database.ExecuteScalar(command);
            }
            catch (Exception e) {
                LogError("Something exploded while updating user " + user.UserID + " " + e.Message);
                throw new Exception("Something exploded while updating user " + user.UserID + " " + e.Message);
            }

        }

    }
}

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
    public class SubmissionDAO : BaseDAO {

        //Constants for all the columns
        private const String SUBMISSION_ID = "@submissionID";
        private const String TITLE = "@title";
        private const String LINK = "@link";
        private const String RATING = "@rating";
        private const String USER_ID = "@userID";
        private const String POST_TIME = "@posttime";

        //SQL query constants
        private const String SELECT_ALL_COLUMNS =
            "SELECT tbl_Submission.SubmissionID, " +
            "Title, " +
            "Link, " +
            "Rating, " +
            "UserID, " +
            "PostTime ";

        private const String SELECT_BY_TITLE =
            SELECT_ALL_COLUMNS +
            "WHERE Title = " + TITLE;

        private const String SELECT_ALL_SUBMISSIONS =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Submission ";

        private const String SELECT_SUBMISSION_BY_SUBMISSION_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Submission " +
            "WHERE SubmissionID = " + SUBMISSION_ID;

        private const String SELECT_SUBMISSIONS_BY_USER_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Submission " +
            "WHERE UserID = " + USER_ID;

        private const String INSERT_SUBMISSION =
            "INSERT INTO tbl_Submission " +
            "(Title, Link, Rating, UserID, PostTime) " +
                "VALUES (" + 
                TITLE + ", " + 
                LINK + ", " + 
                RATING + ", " +
                USER_ID + ", " +
                POST_TIME + ")" +
            "SELECT scope_identity()";

        private const String UPDATE_SUBMISSION =
            "UPDATE tbl_Submission " +
            "SET Title = " + TITLE + ", " +
                "Link = " + LINK + ", " +
                "Rating = " + RATING + ", " +
                "UserID = " + USER_ID + ", " +
                "PostTime = " + POST_TIME + " " +
            "WHERE SubmissionID = " + SUBMISSION_ID;

        private const String DELETE_SUBMISSION =
            "DELETE FROM tbl_Submission " +
            "WHERE SubmissionID = " + SUBMISSION_ID;


        public SubmissionDAO()
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {

        }

        //Pass in an ID, make a sql command with it, get back a submission VO
        public SubmissionVO GetSubmission(int sID){
            LogDebug ("Retrieving submission of submissionID " + sID + "...");

            DbCommand command = Database.GetSqlStringCommand(SELECT_SUBMISSION_BY_SUBMISSION_ID);
            Database.AddInParameter(command, SUBMISSION_ID, DbType.Int32, sID);

            return GetSubmission(command);

        }

        private SubmissionVO GetSubmission(DbCommand command) {

            SubmissionVO sub = null;
            IDataReader reader = null;

            try {

                reader = Database.ExecuteReader(command);
                LogDebug("DataReader created, retrieving sbumission VO...");

                if (reader.Read()) {
                    sub = FillInSubmissionVO(ref reader);
                }

                LogDebug("Submission VO retrieved!");
            }
            catch (Exception e) {
                LogError("Something exploded in trying to retrieve a submission VO!", e);
                throw new Exception("Something exploded in trying to retrieve a submission VO!", e);
            }
            finally {
                base.CloseReader(reader);
            }

            if (sub == null) {
                throw new Exception("No submission found under submission ID!");
            }

            return sub;
        }


        private SubmissionVO FillInSubmissionVO(ref IDataReader reader) {

            SubmissionVO sub = new SubmissionVO();

            sub.SubmissionID = reader.GetInt32(0);
            sub.Title = reader.GetString(1);
            sub.Link = reader.GetString(2);
            sub.Rating = reader.GetInt32(3);
            sub.UserID = reader.GetInt32(4);
            sub.PostTime = reader.GetDateTime(5);

            return sub;

        }

        public List<SubmissionVO> GetAllSubmissions(){

            LogDebug("Retrieving and building list of all submissions...");

            DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_SUBMISSIONS);

            return GetSubmissionList(command);

        }

        private List<SubmissionVO> GetSubmissionList(DbCommand command){

            List<SubmissionVO> subList = new List<SubmissionVO>();
            IDataReader reader = null;

            try{

                reader = Database.ExecuteReader(command);
                LogDebug("DataReader created, retrieving requested submission VO's...");

                while(reader.Read()){
                    SubmissionVO sub = new SubmissionVO();

                    sub.SubmissionID = reader.GetInt32(0);
                    sub.Title = reader.GetString(1);
                    sub.Link = reader.GetString(2);
                    sub.Rating = reader.GetInt32(3);
                    sub.UserID = reader.GetInt32(4);
                    sub.PostTime = reader.GetDateTime(5);

                    subList.Add(sub);
                }

                LogDebug("List created!");
            }
            catch (Exception e) {
                LogError("Something exploded in trying to retrieve a submission VO!", e);
                throw new Exception("Something exploded in trying to retrieve a submission VO!", e);
            }
            finally {
                base.CloseReader(reader);
            }

            return subList;
        }

        public List<SubmissionVO> GetAllUsersSubmissions(int userID) {

            DbCommand command = Database.GetSqlStringCommand(SELECT_SUBMISSIONS_BY_USER_ID);

            Database.AddInParameter(command, USER_ID, DbType.Int32, userID);

            return GetSubmissionList(command);

        }


        public SubmissionVO InsertSubmission(SubmissionVO sub){

            int sID = 0;

            try{
                DbCommand command = Database.GetSqlStringCommand(INSERT_SUBMISSION);

                Database.AddInParameter(command, TITLE, DbType.String, sub.Title);
                Database.AddInParameter(command, LINK, DbType.String, sub.Link);
                Database.AddInParameter(command, RATING, DbType.Int32, sub.Rating);
                Database.AddInParameter(command, USER_ID, DbType.Int32, sub.UserID);
                Database.AddInParameter(command, POST_TIME, DbType.DateTime, sub.PostTime);

                sID = Convert.ToInt32(Database.ExecuteScalar(command));

            }
            catch(Exception e){
                LogError("Something exploded while trying to insert a submission!", e);
                throw new Exception("Something exploded while trying to insert a submission!", e);
            }

            return GetSubmission(sID);

        }

        public void DeleteSubmission(SubmissionVO sub){

            int rowsAffected = 0;

            try {
                DbCommand command = Database.GetSqlStringCommand(DELETE_SUBMISSION);

                Database.AddInParameter(command, SUBMISSION_ID, DbType.Int32, sub.SubmissionID);

                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Something exploded while deleting a submission from the database!", e);
                throw new Exception("Something exploded while deleting a submission from the database!", e);
            }

            if (rowsAffected == 0) {
                LogError("No submission found with an ID of " + sub.SubmissionID);
                throw new Exception("The delete command didn't delete anything!");
            }

        }

        public void AdjustRating(int subID, int rating) {

            SubmissionVO vo = GetSubmission(subID);

            vo.Rating += rating;

            UpdateSubmission(vo);
        }

        public void UpdateSubmission(SubmissionVO sub) {

            DbCommand command = Database.GetSqlStringCommand(UPDATE_SUBMISSION);

            try {

                Database.AddInParameter(command, TITLE, DbType.String, sub.Title);
                Database.AddInParameter(command, LINK, DbType.String, sub.Link);
                Database.AddInParameter(command, RATING, DbType.Int32, sub.Rating);
                Database.AddInParameter(command, USER_ID, DbType.Int32, sub.UserID);
                Database.AddInParameter(command, POST_TIME, DbType.DateTime, sub.PostTime);
                Database.AddInParameter(command, SUBMISSION_ID, DbType.Int32, sub.SubmissionID);

                Database.ExecuteScalar(command);
            }
            catch (Exception e) {
                LogError("Something exploded while updating submission " + sub.SubmissionID);
                throw new Exception("Something exploded while updating submission " + sub.SubmissionID);
            }

        }

    }
}

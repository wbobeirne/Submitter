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
    public class CommentDAO : BaseDAO {

        //Column constants
        private const String COMMENT_ID = "@commentID";
        private const String COMMENT_CONTENTS = "@commentcontents";
        private const String POST_DATE = "@postdate";
        private const String RATING = "@rating";
        private const String SUBMISSION_ID = "@submissionID";
        private const String USER_ID = "@userID";
        private const String PARENT_COMMENT_ID = "@parentCommentID";

        //Sql command constants
        private const String SELECT_ALL_COLUMNS =
            "SELECT tbl_Comment.CommentID, " +
            "CommentContents, " +
            "PostDate, " +
            "Rating, " +
            "SubmissionID, " +
            "UserID, " + 
            "ParentCommentID ";

        private const String SELECT_COMMENT_BY_COMMENT_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Comment " +
            "WHERE CommentID = " + COMMENT_ID;

        private const String SELECT_ALL_COMMENTS =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Comment ";

        private const String SELECT_ALL_COMMENTS_BY_SUBMISSION_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Comment " +
            "WHERE SubmissionID = " + SUBMISSION_ID;

        private const String SELECT_COMMENTS_BY_USER_ID =
            SELECT_ALL_COLUMNS +
            "FROM tbl_Comment " +
            "WHERE UserID = " + USER_ID;

        private const String SELECT_ALL_USER_VOTE_STATUSES =
            SELECT_ALL_COLUMNS +
            "FROM tbl_User_Rating_Comment_xref " +
            "WHERE UserID = " + USER_ID;

        private const String INSERT_COMMENT =
            "INSERT INTO tbl_Comment " +
            "(CommentContents, PostDate, Rating, SubmissionID, UserID, ParentCommentID) " +
                "VALUES (" +
                COMMENT_CONTENTS + ", " +
                POST_DATE + ", " +
                RATING + ", " +
                SUBMISSION_ID + ", " +
                USER_ID + ", " +
                PARENT_COMMENT_ID + ")" +
            "SELECT scope_identity()";

        private const String UPDATE_COMMENT =
            "UPDATE tbl_Comment " +
            "SET CommentContents = " + COMMENT_CONTENTS + ", " +
                "PostDate = " + POST_DATE + ", " +
                "Rating = " + RATING + ", " +
                "SubmissionID = " + SUBMISSION_ID + ", " +
                "UserID = " + USER_ID + ", " +
                "ParentCommentID = " + PARENT_COMMENT_ID + " " +
            "WHERE CommentID = " + COMMENT_ID;

        private const String DELETE_COMMENT =
            "DELETE FROM tbl_Comment " +
            "WHERE CommentID = " + COMMENT_ID;

        private const String DELETE_VOTES_FOR_COMMENT =
            "DELETE FROM tbl_User_Rating_Comment_xref " +
            "WHERE CommentID = " + COMMENT_ID;

        public CommentDAO()
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {

        }

        //Pass in ID, get comment VO
        public CommentVO GetComment(int cID) {

            LogDebug("Retrieving comment with a comment ID of " + cID + "...");

            DbCommand command = Database.GetSqlStringCommand(SELECT_COMMENT_BY_COMMENT_ID);
            Database.AddInParameter(command, COMMENT_ID, DbType.Int32, cID);

            return GetComment(command);

        }

        private CommentVO GetComment(DbCommand command) {

            CommentVO com = null;
            IDataReader reader = null;

            try {

                reader = Database.ExecuteReader(command);
                LogDebug("DataReader created, retrieving comment VO...");

                if (reader.Read()) {
                    com = FillInCommentVO(ref reader);
                }

                LogDebug("Comment VO retrieved!");
            }
            catch (Exception e) {
                LogError("Something exploded in trying to retrieve a comment VO!", e);
                throw new Exception("Something exploded in trying to retrieve a comment VO!", e);
            }
            finally {
                base.CloseReader(reader);
            }

            if (com == null) {
                throw new Exception("No comment found under that ID! " + command.CommandText);
            }

            return com;

        }


        private CommentVO FillInCommentVO(ref IDataReader reader) {

            CommentVO com = new CommentVO();

            com.CommentID = reader.GetInt32(0);
            com.CommentContents = reader.GetString(1);
            com.PostDate = reader.GetDateTime(2);
            com.Rating = reader.GetInt32(3);
            com.SubmissionID = reader.GetInt32(4);
            com.UserID = reader.GetInt32(5);
            com.ParentCommentID = reader.GetInt32(6);

            return com;

        }


        public List<CommentVO> GetAllComments() {

            LogDebug("Retrieving and building list of all comments...");

            DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_COMMENTS);

            return GetCommentList(command);

        }


        public List<CommentVO> GetAllCommentsInASubmission(int subID) {
            LogDebug("Retrieving and building a list of all comments in submission " + subID);

            DbCommand command = Database.GetSqlStringCommand(SELECT_ALL_COMMENTS_BY_SUBMISSION_ID);
            Database.AddInParameter(command, SUBMISSION_ID, DbType.Int32, subID);

            return GetCommentList(command);
        }

        private List<CommentVO> GetCommentList(DbCommand command) {

            List<CommentVO> comList = new List<CommentVO>();
            IDataReader reader = null;

            try {

                reader = Database.ExecuteReader(command);
                LogDebug("DataReader created, retrieving requested comment VO's...");

                while (reader.Read()) {
                    CommentVO com = new CommentVO();

                    com.CommentID = reader.GetInt32(0);
                    com.CommentContents = reader.GetString(1);
                    com.PostDate = reader.GetDateTime(2);
                    com.Rating = reader.GetInt32(3);
                    com.SubmissionID = reader.GetInt32(4);
                    com.UserID = reader.GetInt32(5);
                    com.ParentCommentID = reader.GetInt32(6);

                    comList.Add(com);
                }

                LogDebug("List created!");
            }
            catch (Exception e) {
                LogError("Something exploded in trying to retrieve a comment VO!", e);
                throw new Exception("Something exploded in trying to retrieve a comment VO!", e);
            }
            finally {
                base.CloseReader(reader);
            }

            return comList;

        }

        public List<CommentVO> GetAllUsersComments(int userID) {

            DbCommand command = Database.GetSqlStringCommand(SELECT_COMMENTS_BY_USER_ID);

            Database.AddInParameter(command, USER_ID, DbType.Int32, userID);

            return GetCommentList(command);

        }


        public CommentVO InsertComment(CommentVO com) {

            int cID = 0;

            try {
                DbCommand command = Database.GetSqlStringCommand(INSERT_COMMENT);

                Database.AddInParameter(command, COMMENT_CONTENTS, DbType.String, com.CommentContents);
                Database.AddInParameter(command, POST_DATE, DbType.DateTime, com.PostDate);
                Database.AddInParameter(command, RATING, DbType.Int32, com.Rating);
                Database.AddInParameter(command, SUBMISSION_ID, DbType.Int32, com.SubmissionID);
                Database.AddInParameter(command, USER_ID, DbType.Int32, com.UserID);
                Database.AddInParameter(command, PARENT_COMMENT_ID, DbType.Int32, com.ParentCommentID);

                cID = Convert.ToInt32(Database.ExecuteScalar(command));

            }
            catch (Exception e) {
                LogError("Something exploded while trying to insert a comment!", e);
                throw new Exception("Something exploded while trying to insert a comment!", e);
            }

            return GetComment(cID);

        }


        public void DeleteComment(CommentVO com) {

            int rowsAffected = 0;

            try {
                DeleteComment(com.CommentID);
            }
            catch (Exception e) {
                LogError("Something exploded while deleting a comment from the database!", e);
                throw new Exception("Something exploded while deleting a comment from the database!", e);
            }

            if (rowsAffected == 0) {
                LogError("No comment found with an ID of " + com.CommentID);
                throw new Exception("The delete command didn't delete anything!");
            }
        }


        public void DeleteComment(int cID) {

            int rowsAffected = 0;

            try {
                //First we need to delete all the votes, because of the FK restriction
                DbCommand ratingDeleteCommand = Database.GetSqlStringCommand(DELETE_VOTES_FOR_COMMENT);

                Database.AddInParameter(ratingDeleteCommand, COMMENT_ID, DbType.Int32, cID);

                Database.ExecuteNonQuery(ratingDeleteCommand);

                //Then we delete the comment
                DbCommand command = Database.GetSqlStringCommand(DELETE_COMMENT);

                Database.AddInParameter(command, COMMENT_ID, DbType.Int32, cID);

                rowsAffected = Database.ExecuteNonQuery(command);
            }
            catch (Exception e) {
                LogError("Something exploded while deleting a comment from the database!", e);
                throw new Exception("Something exploded while deleting a comment from the database!", e);
            }

            if (rowsAffected == 0) {
                LogError("No comment found with an ID of " + cID);
                throw new Exception("The delete command didn't delete anything!");
            }

        }


        public void UpdateComment(CommentVO com) {

            DbCommand command = Database.GetSqlStringCommand(UPDATE_COMMENT);

            try {

                Database.AddInParameter(command, COMMENT_CONTENTS, DbType.String, com.CommentContents);
                Database.AddInParameter(command, POST_DATE, DbType.DateTime, com.PostDate);
                Database.AddInParameter(command, RATING, DbType.Int32, com.Rating);
                Database.AddInParameter(command, SUBMISSION_ID, DbType.Int32, com.SubmissionID);
                Database.AddInParameter(command, USER_ID, DbType.Int32, com.UserID);
                Database.AddInParameter(command, PARENT_COMMENT_ID, DbType.Int32, com.ParentCommentID);
                Database.AddInParameter(command, COMMENT_ID, DbType.Int32, com.CommentID);

                Database.ExecuteScalar(command);
            }
            catch (Exception e) {
                LogError("Something exploded while updating comment " + com.CommentID);
                throw new Exception("Something exploded while updating comment " + com.CommentID);
            }

        }


        public int CheckIfVoted(int userID, int commentID) {
            //1 means positively voted, 0 means not voted, -1 means negatively voted
            int voted = 0;

            DbCommand command = Database.GetSqlStringCommand(
            "SELECT tbl_User_Rating_Comment_xref.UserID, " +
            "CommentID, " +
            "VoteType " +
            "FROM tbl_User_Rating_Comment_xref " +
            "WHERE UserID = " + userID + " " +
            "and CommentID = " + commentID);

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

            return voted;
        }

        public void SubmitVote(int userID, int commentID, int voteType) {

            String submitVote =
            "INSERT INTO tbl_User_Rating_Comment_xref " +
            "(UserID, CommentID, VoteType) " +
                "VALUES (" +
                userID + ", " +
                commentID + ", " +
                voteType + ")" +
            "SELECT scope_identity()";

            try {
                DbCommand command = Database.GetSqlStringCommand(submitVote);

                Database.ExecuteScalar(command);

                AdjustRating(commentID, voteType);
            }
            catch (Exception e) {
                LogError("Something exploded while trying to submit a vote! " + e.Message, e);
                throw new Exception("Something exploded while trying to submit a vote! " + e.Message, e);
            }
        }

        public void AdjustRating(int comID, int rating) {

            CommentVO vo = GetComment(comID);

            vo.Rating += rating;

            UpdateComment(vo);
        }

    }
}

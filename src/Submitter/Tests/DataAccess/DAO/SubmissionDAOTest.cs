using System;
using System.Collections.Generic;

using NUnit.Framework;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

using Submitter.DataAccess.DataAccessObjects;
using Submitter.Infrastructure;
using Submitter.Infrastructure.ValueObjects;

namespace Submitter.Tests.DataAccess.DAO {

    [TestFixture]
    class SubmissionDAOTest : BaseObject {

        private const string TITLE = "I'M A LINK!";
        private const string LINK = "http://Google.com";
        private const int RATING = 100;
        private const int USER_ID = 1;
        private DateTime POST_TIME = DateTime.Now;

        private const string COMMENT_CONTENTS = "This is a comment, blah blah blah.";
        private DateTime POST_DATE = DateTime.Now;
        private const int SUBMISSION_ID = 1;

        private const string USERNAME = "Yousername";
        private const String PASSWORD = "password";
        private DateTime REGISTER_DATE = DateTime.Now;

        private SubmissionVO sub1;
        private SubmissionDAO subDAO = new SubmissionDAO();

        private UserVO user1;
        private UserDAO userDAO = new UserDAO();

        private CommentVO com1;
        private CommentDAO comDAO = new CommentDAO();

        private Database db = DatabaseFactory.CreateDatabase();

        public SubmissionDAOTest()
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {

        }


        [SetUp]
        public void SetUp() {

        }

        [Test]
        public void SubmissionInsertTest() {

        }

        [TearDown]
        public void TearDown() {
            

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

using NUnit.Framework;

using Submitter.BusinessLogic.BusinessObjects;
using Submitter.Infrastructure;
using Submitter.Infrastructure.ValueObjects;

namespace Submitter.Tests.BusinessLogic.BO {
    [TestFixture]
    class CommentBOTest : BaseObject {

        List<CommentVO> list = new List<CommentVO>();
        int[] pcIDs = { 0, 0, 1, 3, 4, 0, 2, 5, 0 };
        int[] ratings = { 12, 25, 16, 82, 99, 23, 6, 22, 18 };

        UserManagementBO userBO = new UserManagementBO();

        public CommentBOTest() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }

        [SetUp]
        public void SetUp() {
            for (int i = 0; i < pcIDs.Length; i++) {
                CommentVO vo = new CommentVO();
                vo.Rating = ratings[i];
                vo.ParentCommentID = pcIDs[i];
                list.Add(vo);
            }
        }

        [Test]
        public void Test() {
            CommentManagementBO bo = new CommentManagementBO();
            bo.SortComments(list);

            foreach (CommentVO com in list) {
                System.Diagnostics.Debug.WriteLine(com.CommentID + " " + com.ParentCommentID + " " + com.Rating);
            }
        }


    }
}

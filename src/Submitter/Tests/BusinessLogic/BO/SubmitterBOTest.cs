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
    class SubmitterBOTest : BaseObject {

        string username = "xyz";
        string password = "password";

        UserManagementBO userBO = new UserManagementBO();

        public SubmitterBOTest() : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }

        [SetUp]
        public void SetUp() {
        }

        [Test]
        public void Test() {
        }

        
    }
}

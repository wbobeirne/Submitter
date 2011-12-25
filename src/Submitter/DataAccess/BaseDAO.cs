using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using Submitter.Infrastructure;

namespace Submitter.DataAccess {

    public class BaseDAO : BaseObject {

        private Database db;

        public BaseDAO()
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {
        }

        protected BaseDAO(Type loggerClassType)
            : base(loggerClassType) {
        }

        protected Database Database {
            get {
                if (db == null) {
                    try {
                        db = DatabaseFactory.CreateDatabase("Submitter");
                    }
                    catch (System.Configuration.ConfigurationException ce) {
                        LogError("Couldn't create the database object, caught a ConfigurationException", ce);
                        throw new Exception("Could not obtain a handle on the database", ce);
                    }
                    catch (System.Reflection.TargetInvocationException tie) {
                        LogError("Couldn't create the database object, caught a TargetInvocationException", tie);
                        throw new Exception("Could not obtain a handle on the database", tie);
                    }
                }
                return db;
            }
        }

        protected void CloseReader(IDataReader reader) {
            try {
                reader.Close();
            }
            catch (Exception e) {
                LogWarn("Caught an exception in trying to close the reader, logging only.", e);
            }
        }

    }
}

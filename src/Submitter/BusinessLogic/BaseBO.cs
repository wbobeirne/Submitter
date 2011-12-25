using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Submitter.Infrastructure;
using Submitter.DataAccess;

namespace Submitter.BusinessLogic {
    public class BaseBO : BaseObject {

        public BaseBO() : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {
        
        }

        public BaseBO(Type loggerClassType) : base(loggerClassType) {
        
        }

    }
}

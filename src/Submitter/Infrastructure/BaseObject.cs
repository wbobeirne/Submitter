using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace Submitter.Infrastructure{

    public class BaseObject{

        private ILog logger;

        public BaseObject()
            : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {

        }

        public BaseObject(Type loggingClassType) {

            logger = LogManager.GetLogger(loggingClassType);
            log4net.Config.XmlConfigurator.Configure();

        }

        //Various logging methods

        public static void LogDebug(System.Type type, String message) {

            ILog newLogger = LogManager.GetLogger(type);
            newLogger.Debug(message);

        }

        public void LogDebug(String msg) {

            logger.Debug(msg);

        }

        public void LogDebug(String msg, Exception e) {

            logger.Debug(msg, e);

        }

        public void LogWarn(String msg) {

            logger.Warn(msg);

        }

        public void LogWarn(String msg, Exception e) {

            logger.Warn(msg, e);

        }

        public void LogError(String msg) {

            logger.Error(msg);

        }

        public void LogError(String msg, Exception e) {

            logger.Error(msg, e);

        }

    }
}

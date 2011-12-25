using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

//log4net
using log4net;
using log4net.Config;

using Submitter.Web;

namespace Submitter.Web {

    public class BasePage : System.Web.UI.Page {

        //logging utility
        private ILog _logger;

        public BasePage()
            : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {

        }

        public BasePage(Type loggingClassType) {
            _logger = LogManager.GetLogger(loggingClassType);

        }


        public static void LogDebug(System.Type type, string message) {
            ILog logger = LogManager.GetLogger(type);
            logger.Debug(message);
        }


        protected void LogDebug(String msg) {
            _logger.Debug(msg);
        }

        protected void LogDebug(String msg, Exception e) {
            _logger.Debug(msg, e);
        }

        protected void LogWarn(String msg) {
            _logger.Warn(msg);
        }

        protected void LogWarn(String msg, Exception e) {
            _logger.Warn(msg, e);
        }

        protected void LogError(String msg) {
            _logger.Error(msg);
        }

        protected void LogError(String msg, Exception e) {
            _logger.Error(msg, e);
        }

        public void NavigateToPage(String page) {

            Response.Redirect(page, false);
            Page.Visible = false;

        }

        public void NavigateToErrorPage() {

            Response.Redirect(WebConstants.GENERIC_ERROR_PAGE, false);
            Page.Visible = false;

        }

    }
}
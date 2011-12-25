using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Submitter.Web;
using Submitter.BusinessLogic.BusinessObjects;
using Submitter.Infrastructure.ValueObjects;

namespace Submitter.Web.Controls {
    public partial class LoginControl : System.Web.UI.UserControl {

        protected void Page_Load(object sender, EventArgs e) {

            //If user is logged in, don't show all the login stuff, hide
            if (Session["login"] != null) {
                loginPanel.Visible = false;
                loggedInPanel.Visible = true;
                GenerateLoggedInText();
            }
            else {
                loginPanel.Visible = true;
                loggedInPanel.Visible = false;
            }

        }

        protected void CreateUser_Click(object sender, EventArgs e) {

            Response.Redirect(WebConstants.CREATE_USER_PAGE);

        }

        protected void Login_Submit(object sender, EventArgs e) {


            UserManagementBO umbo = new UserManagementBO();

            try {
                if (umbo.CheckIfValidLogin(loginUsername.Text, loginPassword.Text)) {
                    Session["login"] = loginUsername.Text;
                    System.Diagnostics.Trace.WriteLine(Session["login"]);
                    Session.Timeout = 1000;
                    Response.Redirect(Request.Url.ToString());
                }
            }
            catch (Exception exc) {
                loginErrorMessage.Visible = true;

                loginErrorMessage.Text = exc.Message;
            }

        }

        protected void Logout_Click(object sender, EventArgs e) {

            Session["login"] = null;
            Response.Redirect(WebConstants.HOME_PAGE);

        }


        protected void ViewProfile_Click(object sender, EventArgs e) {

            Response.Redirect(WebConstants.USER_PROFILE_PAGE + "?username=" + Session["login"].ToString());

        }

        protected void GenerateLoggedInText() {

            UserManagementBO userBO = new UserManagementBO();
            UserVO userVO = userBO.GetUser(Session["login"].ToString());

            viewProfile.Text = userVO.Username;
            loggedInLabel.Text = " [" + userVO.Rating.ToString() + "] | "; 
        }

    }
}
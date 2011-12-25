using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.BusinessLogic.BusinessObjects;

namespace Submitter.Web.Controls {
    public partial class CreateUserControl : System.Web.UI.UserControl {

        public const string MISSING_INFO_EXCEPTION = "All fields are required, please enter a username and password";
        public const string PASSWORD_MISMATCH_EXCEPTION = "The passwords entered don't match";
        public const string USERNAME_ALREADY_TAKEN_EXCEPTION = "That username is already in use";
        public const string ILLEGAL_CHARACTER_EXCEPTION = "You have entered an illegal character in one of the fields, please only use alphanumerics, -, _, or .";
        public char[] ILLEGAL_CHAR_ARRAY = {'~', '`', '|', '<', '>', '!', '@', '#',
                                            '$', '%', '^', '&', '*', '(', ')', '+',
                                            '=', '[', ']', '\\', '/', '\"', '\'',
                                            ',', '?' };

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void CreateUser_Submit(object sender, EventArgs e) {

            UserManagementBO userBO = new UserManagementBO();

            //Make sure username isn't already taken
            try {
                //For starters, make sure they've entered info in all the fields.
                if (createUserUsername.Text.Equals("") || createUserPassword.Text.Equals("")) {
                    throw new Exception(MISSING_INFO_EXCEPTION);
                }
                //Check if there are any illegal characters.
                if(createUserUsername.Text.IndexOfAny(ILLEGAL_CHAR_ARRAY) >= 0){
                    throw new Exception(ILLEGAL_CHARACTER_EXCEPTION);
                }

                //Check if name taken
                try {
                    userBO.GetUser(createUserUsername.Text);
                    //If it's found in the db, it won't throw the exception, causing it
                    //to hit this exception
                    throw new Exception(USERNAME_ALREADY_TAKEN_EXCEPTION);
                }
                catch (Exception exce) {
                    //If the exception we caught was the username taken, throw it again
                    if(exce.Message.Equals(USERNAME_ALREADY_TAKEN_EXCEPTION)){
                        throw new Exception(USERNAME_ALREADY_TAKEN_EXCEPTION);
                    }
                }

                //Make sure the passwords are the same
                if (createUserPassword.Text != createUserPassword2.Text) {
                    throw new Exception(PASSWORD_MISMATCH_EXCEPTION);
                }

                //And if their info passed all of that, we make their account
                userBO.CreateNewUser(createUserUsername.Text, createUserPassword.Text);
                Session["login"] = createUserUsername.Text;
                Response.Redirect(WebConstants.HOME_PAGE);
            }
            catch (Exception exc) {
                errorMessage.Text = exc.Message;
            }
        }
    }
}
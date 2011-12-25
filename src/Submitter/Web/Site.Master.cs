using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.Web;

namespace Web{

    public partial class SiteMaster : System.Web.UI.MasterPage{

        protected void Page_Load(object sender, EventArgs e){

        }

        protected void Logo_Click(object sender, EventArgs e) {

            Response.Redirect(WebConstants.HOME_PAGE);

        }

        protected void SubmissionsNew_Click(object sender, EventArgs e) {

            Response.Redirect(WebConstants.HOME_PAGE + "?sort=new");

        }

        protected void SubmissionsPopular_Click(object sender, EventArgs e) {

            Response.Redirect(WebConstants.HOME_PAGE + "?sort=popular");

        }

        protected void SubmissionsTop_Click(object sender, EventArgs e) {

            Response.Redirect(WebConstants.HOME_PAGE + "?sort=top");

        }

        protected void SubmissionsSubmit_Click(object sender, EventArgs e) {

            Response.Redirect(WebConstants.CREATE_SUBMISSION_PAGE);

        }
    }
}

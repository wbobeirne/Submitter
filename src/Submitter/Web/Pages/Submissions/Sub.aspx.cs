using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Submitter.Web.Pages.Submissions {
    public partial class Sub : System.Web.UI.Page {

        int submissionID;

        protected void Page_Load(object sender, EventArgs e) {
            submissionID = Convert.ToInt32(Request.QueryString["subid"]);
            submission.submissionID = submissionID;

            createCommentControl.submissionID = submissionID;
            commentViewControl.submissionID = submissionID;
        }
    }
}
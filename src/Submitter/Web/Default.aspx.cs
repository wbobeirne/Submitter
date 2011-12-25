using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Submitter.Web;

namespace Web
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NavigateToPage(WebConstants.HOME_PAGE);
        }
    }
}

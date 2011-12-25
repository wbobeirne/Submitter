<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Submission.ascx.cs" Inherits="Submitter.Web.Controls.Submission" %>

<tr>
    <td width="25px" style="text-align:center; font: normal x-small verdana, arial, helvetica, sans-serif; font-weight:bold;">
        <asp:ImageButton ID="upArrow" runat="server" OnClick="UpArrow_Click" ImageUrl="~/images/uparrow.png" />
        <div>
        <asp:Label ID="submissionRating" runat="server" style="text-align:center; font-size:small;" />
        </div>
        <asp:ImageButton ID="downArrow" runat="server" OnClick="DownArrow_Click" ImageUrl="~/images/downarrow.png" />
    </td>
    <td>
        <span class="subTitle">
            <asp:Label ID="submissionTitle" runat="server" />
        </span>
        <br />
        <span class="subDetails">
            Submitted by 
            <asp:LinkButton ID="userLink" runat="server" OnClick="UserLink_Click" />
            <asp:Label ID="submissionDetails" runat="server" Text="" />
             | 
            <asp:LinkButton ID="submissionCommentLink" runat="server" OnClick="SubmissionCommentLink_Click" />
        </span>
    </td>
</tr>
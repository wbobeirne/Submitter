<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserViewControl.ascx.cs" Inherits="Submitter.Web.Controls.UserViewControl" %>

<%@ Register Src="~/Controls/SubmissionViewControl.ascx" TagName="SubmissionViewControl" TagPrefix="svc" %>
<%@ Register Src="~/Controls/CommentViewControl.ascx" TagName="CommentViewControl" TagPrefix="cvc" %>

<div class="userDetails">
<table width="100%">
    <tr>
        <td valign="top">
            <h1>Recent Activity</h1>
            <asp:LinkButton ID="submissionsButton" runat="server" Text="Submissions" OnClick="Submissions_Click" />
                |
            <asp:LinkButton ID="commentsButton" runat="server" Text="Comments" OnClick="Comments_Click" />
            <div class="submissions">
                <asp:Panel ID="recentContentPanel" runat="server" />
            </div>
        </td>
        <td valign="top">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="usernameLabel" runat="server" />
                        <br />
                        <asp:Label ID="joinDateLabel" runat="server" />
                        <br />
                        <asp:Label ID="ratingLabel" runat="server" />
                        <br />
                    </td>
                </tr>
                <asp:Panel ID="passwordChangePanel" runat="server" Visible="false" DefaultButton="passwordChangeButton">
                    <tr>
                        <td>
                            <h1>Password Change</h1>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>Current Password:</p>
                            <asp:TextBox ID="oldPassword" runat="server" TextMode="Password" AutoPostBack="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>New Password:</p>
                            <asp:TextBox ID="newPassword" runat="server" TextMode="Password" AutoPostBack="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>Confirm Password:</p>
                            <asp:TextBox ID="newPasswordConfirm" runat="server" TextMode="Password" AutoPostBack="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="passwordChangeButton" runat="server" OnClick="PasswordChange_Click" Text="Submit" />
                        </td>
                    </tr>
                    <asp:Label ID="errorLabel" runat="server" style="color: #FF0000;" />
                </asp:Panel>
            </table>
        </td>
    </tr>
</table>
</div>
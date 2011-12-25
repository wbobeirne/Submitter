<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateUserControl.ascx.cs" Inherits="Submitter.Web.Controls.CreateUserControl" %>
    
<h1>Create a new user</h1>
<br />

<asp:Panel ID="createUserPanel" runat="server" DefaultButton="createUserSubmitButton">
    <table>
        <tr>
            <td>Username:</td>
            <td><asp:TextBox ID="createUserUsername" runat="server" Width="120px" TextMode="SingleLine" MaxLength="20" /></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><asp:TextBox ID="createUserPassword" runat="server" Width="120px" TextMode="Password" MaxLength="20" /></td>
        </tr>
        <tr>
            <td>Confirm Password:</td>
            <td><asp:TextBox ID="createUserPassword2" runat="server" Width="120px" TextMode="Password" MaxLength="20"  /></td>
        </tr>
    </table>
    <br />
    <asp:Button ID="createUserSubmitButton" runat="server" OnClick="CreateUser_Submit" Text="Submit" />
    <br />
    <asp:Label ID="errorMessage" runat="server" style="color: #FF0000;" />
</asp:Panel>
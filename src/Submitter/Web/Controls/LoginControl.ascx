<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="LoginControl.ascx.cs" Inherits="Submitter.Web.Controls.LoginControl" %>


<asp:Panel ID="loginPanel" runat="server" DefaultButton="loginSubmitButton">
    <asp:Label ID="loginErrorMessage" runat="server" style="color: #FF0000;" visible="false" />
        Login:
    <asp:TextBox ID="loginUsername" runat="server" Width="80px" TextMode="SingleLine" MaxLength="20" BackColor="#888888" BorderColor="#AAAAAA"  />
    <asp:TextBox ID="loginPassword" runat="server" Width="80px" TextMode="Password" MaxLength="20" BackColor="#888888" BorderColor="#AAAAAA" />
    <asp:Button ID="loginSubmitButton" runat="server" OnClick="Login_Submit" Text="Submit" BackColor="#888888" BorderColor="#AAAAAA" />
    or 
    <asp:LinkButton ID="createUser" runat="server" Text="Sign Up" OnClick="CreateUser_Click" />
</asp:Panel>

<asp:Panel ID="loggedInPanel" runat="server">

    <asp:LinkButton ID="viewProfile" runat="server" OnClick="ViewProfile_Click" />
    <asp:Label ID="loggedInLabel" runat="server" />
    <asp:LinkButton ID="logoutButton" runat="server" Text="Log Out" OnClick="Logout_Click" />

</asp:Panel>
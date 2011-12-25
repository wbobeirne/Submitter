<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="Submitter.Web.Pages.User.CreateUser" %>

<%@ Register Src="~/Controls/CreateUserControl.ascx" TagName="CreateUserControl" TagPrefix="cuc" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="createUser">
        <cuc:CreateUserControl ID="createUserControl" runat="server" />
    </div>

</asp:Content>

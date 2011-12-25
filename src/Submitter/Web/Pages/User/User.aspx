<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Submitter.Web.Pages.User.User" %>

<%@ Register Src="~/Controls/UserViewControl.ascx" TagName="UserViewControl" TagPrefix="uvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--For some reason, the enter key is refreshing the page. We don't want that, do we? -->
    <script language="javascript" src="/Scripts/entercapture.js" ></script>

    <uvc:UserViewControl ID="userViewControl" runat="server" />
</asp:Content>

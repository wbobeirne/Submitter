<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Submitter.Web.Pages.Home" %>

<%@ Register Src="~/Controls/Submission.ascx" TagName="Submission" TagPrefix="sub" %>

<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Submitter - Home Page</title>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="submissionViewTitle" runat="server" />
    <div class="submissions">
        <table width="100%">
            <asp:Panel ID="submissionsPanel" runat="server" />
        </table>
    </div>
</asp:Content>
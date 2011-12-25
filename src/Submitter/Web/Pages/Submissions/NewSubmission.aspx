<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSubmission.aspx.cs" Inherits="Submitter.Web.Pages.Submissions.NewSubmission" %>

<%@ Register Src="~/Controls/CreateSubmissionControl.ascx" TagName="CreateSubmissionControl" TagPrefix="csc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <csc:CreateSubmissionControl ID="createSubmissionControl" runat="server" />

</asp:Content>

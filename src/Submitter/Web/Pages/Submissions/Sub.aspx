<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sub.aspx.cs" Inherits="Submitter.Web.Pages.Submissions.Sub" validateRequest="false" %>

<%@ Register Src="~/Controls/Submission.ascx" TagName="Submission" TagPrefix="sub" %>
<%@ Register Src="~/Controls/CreateCommentControl.ascx" TagName="CreateCommentControl" TagPrefix="ccc" %>
<%@ Register Src="~/Controls/CommentViewControl.ascx" TagName="CommentViewControl" TagPrefix="cvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="submissions">
        <table width="100%">
            <sub:Submission ID="submission" runat="server" />
        </table>
    </div>

    <ccc:CreateCommentControl ID="createCommentControl" runat="server" />
    <br />
    <cvc:CommentViewControl ID="commentViewControl" runat="server" />
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentViewControl.ascx.cs" Inherits="Submitter.Web.Controls.CommentViewControl" %>

<%@ Register Src="~/Controls/Comment.ascx" TagName="Comment" TagPrefix="com" %>

<div class="comments">
    <asp:Panel ID="commentPanel" runat="server"></asp:Panel>
</div>
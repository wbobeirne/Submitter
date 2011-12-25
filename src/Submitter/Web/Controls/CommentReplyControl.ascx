<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentReplyControl.ascx.cs" Inherits="Submitter.Web.Controls.CommentReplyControl" %>

<asp:Panel ID="commentReplyPanel" runat="server" >
    <asp:TextBox ID="replyTextBox" runat="server" TextMode="MultiLine" Width="400px" Height="80px" />
    <asp:Label ID="parentCommentID" runat="server" Visible="false" />
    <br />
    <asp:Button ID="submitEditButton" runat="server" OnClick="SubmitReply_Click" Text="submit" />
    <asp:Button ID="cancelEditButton" runat="server" OnClick="CancelReply_Click" Text="cancel" />
    <br />
    <asp:Label ID="errorLabel" runat="server" />
</asp:Panel>
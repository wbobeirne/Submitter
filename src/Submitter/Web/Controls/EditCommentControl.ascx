<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCommentControl.ascx.cs" Inherits="Submitter.Web.Controls.EditCommentControl" %>

<asp:Panel ID="editCommentPanel" runat="server" >
    <asp:TextBox ID="editTextBox" runat="server" TextMode="MultiLine" Width="400px" Height="80px" />
    <asp:Label ID="commentID" runat="server" Visible="false" />
    <br />
    <asp:Button ID="submitEditButton" runat="server" OnClick="SubmitEdit_Click" Text="submit" />
    <asp:Button ID="cancelEditButton" runat="server" OnClick="CancelEdit_Click" Text="cancel" />
    <br />
    <asp:Label ID="errorLabel" runat="server" />
</asp:Panel>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateCommentControl.ascx.cs" Inherits="Submitter.Web.Controls.CreateCommentControl"  %>


<asp:Panel ID="commentCreatePanel" runat="server" DefaultButton="submitButton">

    <h1>Submit a comment</h1>
    <asp:TextBox ID="commentContents" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine" Width="400px" Height="80px" />
    <br />
    <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
    <asp:Label ID="errorLabel" runat="server" style="color: #FF0000;" />

</asp:Panel>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateSubmissionControl.ascx.cs" Inherits="Submitter.Web.Controls.CreateSubmissionControl" %>

<h1><asp:Label ID="controlTitle" runat="server" /></h1>

<asp:Panel ID="createSubmissionPanel" runat="server" DefaultButton="createSubmissionSubmitButton">
    
    <table>
        <tr>
            <td>Title:</td>
            <td><asp:TextBox ID="createSubmissionTitle" runat="server" Width="200px" TextMode="SingleLine" MaxLength="140" /></td>
        </tr>
        <tr>
            <td>Link:</td>
            <td><asp:TextBox ID="createSubmissionLink" runat="server" Width="200px" TextMode="SingleLine" MaxLength="500" /></td>
        </tr>
    </table>
    <br />
    <asp:Button ID="createSubmissionSubmitButton" runat="server" OnClick="CreateSubmission_Submit" Text="Submit" />
</asp:Panel>

<br />

<asp:Label ID="submissionError" runat="server" Text="" style="color: #FF0000;" />
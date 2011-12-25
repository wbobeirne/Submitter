<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comment.ascx.cs" Inherits="Submitter.Web.Controls.Comment" %>

<%@ Register Src="~/Controls/EditCommentControl.ascx" TagName="EditCommentControl" TagPrefix="ecc" %>
<%@ Register Src="~/Controls/CommentReplyControl.ascx" TagName="CommentReplyControl" TagPrefix="crc" %>

<table width="100%" class="comments">
    <tr>
        <!-- This label is for spacing when the comment is a reply -->
        <asp:Label ID="spacingLabel" runat="server" Text=""/>
        <td style="text-align:center; vertical-align:top; font: normal x-small verdana, arial, helvetica, sans-serif; font-weight:bold; width: 25px !important;">
            <asp:ImageButton ID="upArrow" runat="server" OnClick="UpArrow_Click" ImageUrl="~/images/uparrow.png" />
            <asp:ImageButton ID="downArrow" runat="server" OnClick="DownArrow_Click" ImageUrl="~/images/downarrow.png" />
        </td>
        <td>
            <asp:Panel ID="commentPanel" runat="server">
                <asp:LinkButton ID="userLink" runat="server" OnClick="UserLink_Click" />
                <asp:Label ID="commentInfo" runat="server" Text="" />
                <br />
                <asp:Label ID="commentContents" runat="server" Text="" />
                <ecc:EditCommentControl ID="editCommentControl" runat="server" Visible="false" />
                <crc:CommentReplyControl ID="commentReplyControl" runat="server" Visible="false" />
                <asp:LinkButton ID="deleteButton" runat="server" Text="delete " OnClick="DeleteButton_Click" />
                <asp:LinkButton ID="editButton" runat="server" Text="edit " OnClick="EditButton_Click" />
                <asp:LinkButton ID="replyButton" runat="server" Text="reply " OnClick="ReplyButton_Click" />
            </asp:Panel>
        </td>
    </tr>
</table>
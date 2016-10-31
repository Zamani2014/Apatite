<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HtmlContent.ascx.cs" Inherits="SectionControls_HtmlContent" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<%@ Register Src="~/SectionControls/HtmlEditor.ascx" TagPrefix="uc" TagName="HtmlEditor" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td><uc:HtmlEditor runat="server" ID="txtHtml" Height="500" ImageGalleryPath="~/App_Data/UserImages/Images" ImageGalleryUrl="~/ftb.imagegallery.aspx" /></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false" />&nbsp;
                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="false"/>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
        <asp:literal runat="server" ID="litContent"></asp:literal>
    </asp:View>
</asp:MultiView>
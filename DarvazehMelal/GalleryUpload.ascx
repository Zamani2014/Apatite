<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GalleryUpload.ascx.cs"
    Inherits="GalleryUpload" %>
<tr>
    <td class="fieldlabel">
        <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:stringsRes, ctl_Gallery_Title%>"></asp:Localize></td>
    <td class="field">
        <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox></td>
</tr>
    <tr>
        <td class="fieldlabel">
        </td>
        <td class="field">
            <asp:RequiredFieldValidator runat="server" ID="valFileRequired" ControlToValidate="fileData"
                Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Gallery_ErrorMessageImage%>"
                SetFocusOnError="true"></asp:RequiredFieldValidator></td>
    </tr>
<tr>
    <td class="fieldlabel">
        <asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_Gallery_File%>"></asp:Localize></td>
    <td class="field">
        <asp:FileUpload runat="server" ID="fileData" /></td>
</tr>
<tr>
    <td class="fieldlabel">
        <asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_Gallery_Comment%>"></asp:Localize></td>
    <td class="field">
        <asp:TextBox runat="server" ID="txtComment"></asp:TextBox></td>
</tr>

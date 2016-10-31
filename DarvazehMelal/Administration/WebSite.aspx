<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebSite.aspx.cs" Inherits="Administration_WebSite" MasterPageFile="~/Site.master" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:Content ID="contentPane" ContentPlaceHolderID="mainContent" runat="Server">
    <img src="../Images/info.gif" alt="Info" onclick="window.open('../Documentation/<%=lang%>/quick_guide.html#set-up','InfoPopUp', 'height=780,width=800,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no');return false;" style="height:16px;width:16px;border-width:0px;cursor:hand;" />
    <table>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_Title%>"></asp:Localize></td>
            <td class="field"><asp:TextBox runat="server" ID="txtWebSiteTitle"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_Language%>"></asp:Localize></td>
            <td class="field"><cc1:CultureDropdown runat="server" ID="cmbLanguage"></cc1:CultureDropdown></td>
        </tr>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_MailSender%>"></asp:Localize></td>
            <td class="field"><asp:TextBox runat="server" ID="txtSenderMail"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_SmtpServer%>"></asp:Localize></td>
            <td class="field"><asp:TextBox runat="server" ID="txtSmtpServer"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_SmtpUser%>"></asp:Localize></td>
            <td class="field"><asp:TextBox runat="server" ID="txtSmtpUser"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_SmtpPassword%>"></asp:Localize></td>
            <td class="field"><asp:TextBox runat="server" ID="txtSmtpPassword"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_SmtpDomain%>"></asp:Localize></td>
            <td class="field"><asp:TextBox runat="server" ID="txtSmtpDomain"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_FooterLine%>"></asp:Localize></td>
            <td class="field"><asp:TextBox runat="server" ID="txtFooter"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_WebsiteDescription%>"></asp:Localize></td>
            <td class="field"><asp:TextBox runat="server" ID="txtDescription" TextMode="multiline"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_WebsiteKeywords%>"></asp:Localize></td>
            <td class="field"><asp:TextBox runat="server" ID="txtKeywords" TextMode="multiline"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td class="field"><asp:Checkbox runat="server" ID="chkSectionRss" Text="<%$ Resources:stringsRes, adm_Website_EnableSectionRSS%>"></asp:Checkbox></td>
        </tr>
        <tr>
            <td></td>
            <td class="field"><asp:Checkbox runat="server" ID="chkIE8Compatibility" Text="<%$ Resources:stringsRes, adm_Website_EnableIE8CompatibilityMetatag%>"></asp:Checkbox></td>
        </tr>
        <tr>
			<td></td>
			<td class="field"><asp:CheckBox runat="server" ID="chkAllowUserSelfRegistration" Text="<%$ Resources:stringsRes, adm_Website_AllowUserSelfRegistration%>" /></td>
        </tr>
        <tr>
			<td></td>
			<td class="field"><asp:CheckBox runat="server" ID="chkEnableVersionChecking" Text="<%$ Resources:stringsRes, adm_Website_EnableVersionChecking%>" /><br />
			<a href="Default.aspx"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_ShowVersionInfo%>" /></a>
			</td>
        </tr>
        <tr>
            <td colspan="2" align="right"><asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="<%$ Resources:stringsRes, glb__Save%>"></asp:Button></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="right"><asp:Button runat="server" ID="btnReset" OnClick="btnReset_Click" Text="<%$ Resources:stringsRes, adm_Website_Reset%>"></asp:Button></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2"><hr /></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2"><h2><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Website_SelectTheme%>"></asp:Localize></h2></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DataList runat="server" ID="lstThemes" DataKeyField="Name" OnItemCommand="lstThemes_ItemCommand" SkinID="gallery">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnSelect" CommandName="SelectTheme">
                            <cc1:ResizedImage runat="server" ID="imgThumbnail" ImageUrl='<%#Eval("Thumbnail")%>' SkinID="galleryThumbnail"></cc1:ResizedImage>
                        </asp:LinkButton>
                        <%#Eval("Name")%>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>

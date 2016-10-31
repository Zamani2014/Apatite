<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Navigation.aspx.cs"
    Inherits="Administration_Navigation" MasterPageFile="~/Site.master" %>

<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="contentPane" ContentPlaceHolderID="mainContent" runat="Server">
    <img src="../Images/info.gif" alt="Info" onclick="window.open('../Documentation/<%=lang%>/quick_guide.html#manage-pages','InfoPopUp', 'height=780,width=800,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no');return false;"
        style="height: 16px; width: 16px; border-width: 0px; cursor: hand;" />
    <table width="100%" border="0">
        <tr>
            <td>
                <asp:ListBox runat="server" ID="lstPages" OnSelectedIndexChanged="lstPages_SelectedIndexChanged"
                    AutoPostBack="true" CssClass="sel" Rows="20"></asp:ListBox></td>
            <td>
                <table border="0">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnNewPage" OnClick="btnNewPage_Click" Text="<%$ Resources:stringsRes, adm_Navigation_NewSite%>">
                            </asp:Button></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Localize ID="LocalizeMoveTitle" runat="server" Visible="false" Text="<%$ Resources:stringsRes, adm_Navigation_Move%>"></asp:Localize>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table border="0" runat="server" id="tblMoveIcons" visible="false">
                                <tr>
                                    <td style="width: 33%;">
                                    </td>
                                    <td style="width: 33%; text-align: center; vertical-align: middle;">
                                        <asp:LinkButton ID="btnMoveUp" runat="server" CommandName="up" OnCommand="movePage_Command"
                                            SkinID="Up" /></td>
                                    <td style="width: 33%;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; vertical-align: middle;">
                                        <asp:LinkButton ID="btnLevelUp" runat="server" CommandName="left" OnCommand="movePage_Command"
                                            SkinID="Left" /></td>
                                    <td>
                                    </td>
                                    <td style="text-align: center; vertical-align: middle;">
                                        <asp:LinkButton ID="btnLevelDown" runat="server" CommandName="right" OnCommand="movePage_Command"
                                            SkinID="Right" /></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align: center; vertical-align: middle;">
                                        <asp:LinkButton ID="btnMoveDown" runat="server" CommandName="down" OnCommand="movePage_Command"
                                            SkinID="Down" /></td>
                                    <td>
                                    </td>
                                </tr>
                              </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" runat="server" id="tblPageDetails" visible="false">
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Navigation_PageTitle%>"></asp:Localize></td>
                        <td class="field">
                            <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:stringsRes, adm_Navigation_Description%>" />
                        </td>
                        <td class="field">
                            <asp:TextBox runat="server" ID="txtDescription" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:stringsRes, adm_Navigation_Keywords%>" />
                        </td>
                        <td class="field">
                            <asp:TextBox runat="server" ID="txtKeywords" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Navigation_Navigation%>"></asp:Localize></td>
                        <td class="field">
                            <asp:TextBox runat="server" ID="txtNavigation"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td class="field">
                            <asp:CustomValidator runat="server" ID="valVirtualPath" OnServerValidate="valVirtualPath_ServerValidate"
                                Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, adm_Navigation_VirtualPathErrorMessage%>"></asp:CustomValidator></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Navigation_VirtualPath%>"></asp:Localize></td>
                        <td class="field">
                            <asp:TextBox runat="server" ID="txtVirtualPath"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Navigation_AllowAnonymous%>"></asp:Localize></td>
                        <td class="field">
                            <asp:CheckBox runat="server" ID="chkAnonymousAccess" /></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:stringsRes, adm_Navigation_EditPowerUser%>"></asp:Localize></td>
                        <td class="field">
                            <asp:CheckBox runat="server" ID="chkEditPowerUser" /></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:stringsRes, adm_Navigation_Visible%>"></asp:Localize></td>
                        <td class="field">
                            <asp:CheckBox runat="server" ID="chkVisible" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="<%$ Resources:stringsRes, glb__Save%>">
                                        </asp:Button></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button runat="Server" ID="btnSetHomepage" OnClick="btnSetHomepage_Click" Text="<%$ Resources:stringsRes, adm_Navigation_Homepage%>" /></td>
                                    <td>
                                        <asp:Button runat="Server" ID="btnDelete" OnClick="btnDelete_Click" Text="<%$ Resources:stringsRes, glb__Delete%>" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

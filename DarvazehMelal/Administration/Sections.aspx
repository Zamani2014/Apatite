<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Sections.aspx.cs" Inherits="Administration_Sections" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
        <table width="100%">
            <tr runat="server" id="trMessage">
                <td>
                    <asp:Label ID="lblMessage" CssClass="error" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                   <fieldset runat="server" id="fieldset1">
                    <legend runat="server" id="legend1"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Sections_MoveSection%>"></asp:Localize></legend>
                    <table width="100%">
                    <tr><td>&nbsp;</td></tr>
                    <tr><td width="33%"></td><td width="33%"></td><td width="33%"></td></tr>
                        <tr>
                            <th><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Sections_SourcePage%>"></asp:Localize></th>
                            <th runat="server" id="thSection"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Sections_Section%>"></asp:Localize></th>
                            <th runat="server" id="thTargetPage"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Sections_TargetPage%>"></asp:Localize></th>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox runat="server" ID="lstSourcePage" OnSelectedIndexChanged="lstSourcePage_SelectedIndexChanged" AutoPostBack="true"  Rows="7" Width="95%"></asp:ListBox>
                            </td>
                            <td>
                                <asp:ListBox runat="server" ID="lstPageSections" OnSelectedIndexChanged="lstPageSections_SelectedIndexChanged" AutoPostBack="true" Rows="7" Width="95%"></asp:ListBox>
                             </td>
                            <td>
                                <asp:ListBox runat="server" ID="lstTargetPage" OnSelectedIndexChanged="lstTargetPage_SelectedIndexChanged" AutoPostBack="true" Rows="7" Width="95%"></asp:ListBox>
                             </td>
                          </tr>
                          <tr>
                             <td>
                                <asp:Button runat="server" ID="btnMove" OnClick="btnMoveSection_Click" Text="<%$ Resources:stringsRes, adm_Sections_Move%>"></asp:Button>
                             </td>
                          </tr>
                    </table>
                </fieldset>
                </td>
            </tr>
        </table>
</asp:Content>


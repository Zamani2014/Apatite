<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LinkList.ascx.cs" Inherits="SectionControls_LinkList" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td colspan="2" align="right"><asp:Button runat="server" ID="btnNewEntry" OnClick="btnNewEntry_Click" Text="<%$ Resources:stringsRes, glb__add %>" CausesValidation="false"></asp:Button></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView runat="server" ID="gvLinkListEdit" DataKeyNames="Guid" AutoGenerateColumns="false" OnRowCommand="gvLinkListEdit_RowCommand" EnableViewState="false">
                        <Columns>
                            <asp:TemplateField><ItemTemplate><a href="<%#Eval("Url")%>" target="_blank"><%#buildTitle(Eval("Title", "{0}"), Eval("Url", "{0}"))%></a></ItemTemplate></asp:TemplateField>
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnEdit" CommandName="entry_edit" CausesValidation="false" />
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnDelete" CommandName="entry_delete" CausesValidation="false" />
                        </Columns>
                        <RowStyle Height="25px" />
                        <HeaderStyle Height="30px" />
                    </asp:GridView>
                    <br />
                    <cc1:Pager runat="server" ID="editPager" ControlToPage="gvLinkListEdit"></cc1:Pager>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="editDetailsView">
        <table width="100%">
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_LinkList_Title %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" ID="txtTitle"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel"></td>
                <td class="field">
                    <asp:RequiredFieldValidator runat="server" ID="valUrlRequired" ControlToValidate="txtUrl" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_LinkList_ErrorMessageUrl %>" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="valUrlRegex" ControlToValidate="txtUrl" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_LinkList_ErrorMessageValidUrl %>" SetFocusOnError="true"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_LinkList_Url %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" ID="txtUrl"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_LinkList_Target %>"></asp:Localize></td>
                <td class="field"><asp:DropDownList runat="server" ID="cmbTarget"><asp:ListItem Text="_blank"></asp:ListItem><asp:ListItem Text="_self"></asp:ListItem></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_LinkList_Comment %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" id="txtComment"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnCancelDetails" OnClick="btnCancelDetails_Click" Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false"></asp:Button>
                    <asp:Button runat="server" ID="btnSaveDetails" OnClick="btnSaveDetails_Click" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="true"></asp:Button>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
        <asp:GridView runat="server" ID="gvLinkList" DataKeyNames="Guid" AutoGenerateColumns="false" OnRowCommand="gvLinkListEdit_RowCommand" EnableViewState="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="linkitem">
                            <a href="<%#Eval("Url")%>" target="<%#Eval("Target")%>"><%#buildTitle(Eval("Title", "{0}"), Eval("Url", "{0}"))%></a><br />
                            <%#Eval("Comment")%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <cc1:Pager runat="server" ID="readonlyPager" ControlToPage="gvLinkList"></cc1:Pager>
    </asp:View>
</asp:MultiView>
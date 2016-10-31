<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DownloadList.ascx.cs" Inherits="SectionControls_DownloadList" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td colspan="2" align="right"><asp:Button runat="server" ID="btnNewEntry" OnClick="btnNewEntry_Click" Text="<%$ Resources:stringsRes, glb__add %>" CausesValidation="false"></asp:Button></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView runat="server" ID="gvDownloadListEdit" DataKeyNames="Guid" AutoGenerateColumns="false" OnRowCommand="gvDownloadListEdit_RowCommand" EnableViewState="false">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="25px"><ItemTemplate><%#getFiletypeIcon(Eval("Filename", "{0}"))%></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:stringsRes, ctl_Downloadlist_Title%>"><ItemTemplate><a href="<%#getDownloadLink(Eval("Filename", "{0}"))%>"><%#buildTitle(Eval("Title", "{0}"), Eval("Filename", "{0}")) + " (" + Eval("Size") + ")"%></a></ItemTemplate></asp:TemplateField>
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnEdit" CommandName="entry_edit" CausesValidation="false" />
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnDelete" CommandName="entry_delete" CausesValidation="false" />
                        </Columns>
                        <RowStyle Height="25px" />
                        <HeaderStyle Height="30px" />
                    </asp:GridView>
                    <br />
                    <cc1:Pager runat="server" ID="editPager" ControlToPage="gvDownloadListEdit"></cc1:Pager>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="editDetailsView">
        <table width="100%">
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_Downloadlist_Title%>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" ID="txtTitle"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td class="field"><asp:RequiredFieldValidator runat="server" ID="valDataRequired" ControlToValidate="filData" ErrorMessage="<%$ Resources:stringsRes, ctl_ErrorMessageMissingFile%>" Display="dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_Downloadlist_File%>"></asp:Localize></td>
                <td class="field"><asp:FileUpload runat="server" ID="filData" /></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_Downloadlist_Comment%>"></asp:Localize></td>
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
        <asp:GridView runat="server" ID="gvDownloadList" DataKeyNames="Guid" AutoGenerateColumns="false" OnRowCommand="gvDownloadListEdit_RowCommand" EnableViewState="false">
            <Columns>
                <asp:TemplateField ItemStyle-Width="25px">
                    <ItemTemplate>
                        <div class="downloaditem"><%#getFiletypeIcon(Eval("Filename", "{0}"))%></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="downloaditem">
                            <a href="<%#getDownloadLink(Eval("Filename", "{0}"))%>"><%#buildTitle(Eval("Title", "{0}"), Eval("Filename", "{0}")) + " (" + Eval("Size") + ")"%></a><br />
                            <%#Eval("Comment")%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <cc1:Pager runat="server" ID="readonlyPager" ControlToPage="gvDownloadList"></cc1:Pager>
    </asp:View>
</asp:MultiView>
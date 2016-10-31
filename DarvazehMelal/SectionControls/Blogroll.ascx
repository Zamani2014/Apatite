<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Blogroll.ascx.cs" Inherits="SectionControls_Blogroll" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnEditConfiguration" Text="<%$ Resources:stringsRes, ctl_Blogroll_Configuration %>" OnClick="btnEditConfiguration_Click" />
                    <asp:Button runat="server" ID="btnNewEntry" OnClick="btnNewEntry_Click" Text="<%$ Resources:stringsRes, glb__add %>"
                        CausesValidation="false"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView runat="server" ID="gvBlogrollEdit" DataKeyNames="Guid" AutoGenerateColumns="false"
                        OnRowCommand="gvBlogrollEdit_RowCommand" EnableViewState="false">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:stringsRes, ctl_Blog_RssTitle %>">
                                <ItemTemplate>
                                    <a href="<%#Eval("Url")%>" target="_blank">
                                        <%#buildTitle(Eval("Title", "{0}"), Eval("Url", "{0}"))%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:stringsRes, ctl_Blogroll_Description %>">
                                <ItemTemplate>
                                    <%#Eval("Description")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:stringsRes, ctl_Blogroll_Order %>">
                                <ItemTemplate>
                                    <%#Eval("Order")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnEdit"
                                CommandName="entry_edit" CausesValidation="false" />
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnDelete"
                                CommandName="entry_delete" CausesValidation="false" />
                        </Columns>
                        <RowStyle Height="25px" />
                        <HeaderStyle Height="30px" />
                    </asp:GridView>
                    <br />
                    <cc1:Pager runat="server" ID="editPager" ControlToPage="gvBlogrollEdit">
                    </cc1:Pager>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="editDetailsView">
        <table width="100%">
            <tr>
                <td class="fieldlabel">
                    <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:stringsRes, ctl_Blogroll_Title %>"></asp:Localize></td>
                <td class="field">
                    <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel">
                </td>
                <td class="field">
                    <asp:RequiredFieldValidator runat="server" ID="valUrlRequired" ControlToValidate="txtUrl"
                        Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Blogroll_ErrorMessageUrl %>"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="valUrlRegex" ControlToValidate="txtUrl"
                        Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Blogroll_ErrorMessageValidUrl %>"
                        SetFocusOnError="true"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="fieldlabel">
                    <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:stringsRes, ctl_Blogroll_Url %>"></asp:Localize></td>
                <td class="field">
                    <asp:TextBox runat="server" ID="txtUrl"></asp:TextBox></td>
            </tr>
               <tr>
                <td class="fieldlabel">
                </td>
                <td class="field">
                    <asp:RegularExpressionValidator runat="server" ID="valFeedUrlRegex" ControlToValidate="txtFeedUrl"
                        Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Blogroll_ErrorMessageValidUrl %>"
                        SetFocusOnError="true"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="fieldlabel">
                    <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:stringsRes, ctl_Blogroll_FeedUrl %>"></asp:Localize></td>
                <td class="field">
                    <asp:TextBox runat="server" ID="txtFeedUrl"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel">
                    <asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:stringsRes, ctl_Blogroll_Description %>"></asp:Localize></td>
                <td class="field">
                    <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel">
                    <asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:stringsRes, ctl_Blogroll_Order %>"></asp:Localize></td>
                <td class="field">
                    <asp:TextBox runat="server" ID="txtOrder"></asp:TextBox>
                    <asp:RangeValidator runat="server" ID="valOrder" ControlToValidate="txtOrder" Display="Dynamic"
                        MinimumValue="0" MaximumValue="9999" Type="Integer" ErrorMessage="<%$ Resources:stringsRes, ctl_Blogroll_ErrorMessageValidOrder %>"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnCancelDetails" OnClick="btnCancelDetails_Click"
                        Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false"></asp:Button>
                    <asp:Button runat="server" ID="btnSaveDetails" OnClick="btnSaveDetails_Click" Text="<%$Resources:StringsRes, glb__Save %>"
                        CausesValidation="true"></asp:Button>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
        <asp:GridView runat="server" ID="gvBlogroll" DataKeyNames="Guid" AutoGenerateColumns="false"
            OnRowCommand="gvBlogrollEdit_RowCommand" EnableViewState="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="blogrollitem">
                            <a href="<%#Eval("Url")%>" target="_blank">
                                <%#buildTitle(Eval("Title", "{0}"), Eval("Url", "{0}"))%>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <cc1:Pager runat="server" ID="readonlyPager" ControlToPage="gvBlogroll">
        </cc1:Pager>
    </asp:View>
    <asp:View runat="server" ID="editConfigurationView">
        <br />
        <fieldset>
            <legend><asp:Localize ID="Localize8" runat="server" Text="<%$Resources:StringsRes, ctl_Blogroll_Config %>"></asp:Localize></legend>
                <table width="100%">
                    <tr>
                        <td></td>
                        <td><asp:Label runat="server" ID="lbConfigSaved" ForeColor="Red"><asp:Localize ID="Localize12" runat="server" Text="<%$Resources:StringsRes, ctl_Blogroll_ConfigSafed %>"></asp:Localize></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize ID="Localize7" runat="server" Text="<%$Resources:StringsRes, ctl_Blogroll_ConfigName %>"></asp:Localize></td>
                        <td class="field">
                            <asp:TextBox runat="server" ID="txtBlogrollTitle"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize ID="Localize6" runat="server" Text="<%$Resources:StringsRes, ctl_Blogroll_ConfigVisible %>"></asp:Localize></td>
                        <td class="field">
                            <asp:CheckBox runat="server" ID="ckbVisible" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right"><asp:Button runat="server" ID="btnBack" Text="<%$Resources:StringsRes, glb__Back %>" CausesValidation="false" OnClick="btnBack_Click" />
                            <asp:Button runat="server" ID="btnSaveConfiguration" Text="<%$Resources:StringsRes, glb__Save %>"
                                CausesValidation="true" OnClick="btnSaveConfiguration_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
        </fieldset>
    </asp:View>
</asp:MultiView>

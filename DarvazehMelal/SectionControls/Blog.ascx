<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Blog.ascx.cs" Inherits="SectionControls_Blog" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<%@ Register Src="~/SectionControls/HtmlEditor.ascx" TagPrefix="uc" TagName="HtmlEditor" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <div style="padding-top: 4px">
        </div>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnConfig" Text="<%$ Resources:stringsRes, ctl_Blog_Configuration %>" OnClick="btnConfig_Click" />
                    <asp:Button runat="server" ID="btnEditTags" Text="<%$ Resources:stringsRes, ctl_Blog_Tags %>" OnClick="btnEditTags_Click" />
                    <asp:Button runat="server" ID="btnNewEntry" OnClick="btnNewEntry_Click" Text="<%$ Resources:stringsRes, glb__add %>"
                        CausesValidation="false"></asp:Button></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:DataList runat="server" ID="lstBlogEntriesEdit" DataKeyField="Guid" OnItemCommand="lstBlogEdit_ItemCommand"
                        SkinID="blogEdit" Width="100%">
                        <ItemTemplate>
                            <table width="100%" border="0" class="entry">
                                <tr>
                                    <td class="title">
                                        <%#Eval("Title", "{0}")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="content">
                                        <%#Eval("Content", "{0}")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="footer">
                                        {<%#System.Convert.ToDateTime(Eval("Created", "{0}")).ToString("g")%>}&nbsp;
                                        {<%# getCommentsFooter(DataBinder.Eval(Container.DataItem,"Comments"))%>}
                                         <%# getTagsFooter(DataBinder.Eval(Container.DataItem, "Tags"))%>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="4">
                                <tr>
                                    <td style="width: 100%;">
                                    </td>
                                    <td align="right">
                                        <asp:LinkButton ID="Button1" runat="server" CssClass="btnEdit" CommandName="entry_edit"
                                            CausesValidation="false" /></td>
                                    <td style="width: 30px;">
                                        &nbsp;</td>
                                    <td align="right">
                                        <asp:LinkButton ID="Button2" runat="server" CssClass="btnDelete" CommandName="entry_delete"
                                            CausesValidation="false" /></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    <br />
                    <cc1:Pager runat="server" ID="pagBlogEdit" ControlToPage="lstBlogEntriesEdit" SkinID="blog" PageSize="5">
                    </cc1:Pager>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="editDetailsView">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="blogTitle">
                    <asp:Localize ID="Localize9" runat="server" Text="<%$ Resources:stringsRes, ctl_Blog_Title %>"></asp:Localize></td>
            </tr>
            <tr>
                <td class="blogField">
                    <asp:RequiredFieldValidator runat="server" ID="valTitleRequired" ControlToValidate="txtTitle"
                        Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Blog_ErrorMessageTitle %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" SkinID="blog"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="blogTitle">
                    <asp:Localize ID="Localize10" runat="server" Text="<%$ Resources:stringsRes, ctl_Blog_Entry %>"></asp:Localize></td>
            </tr>
            <tr>
                <td class="blogField">
                    <asp:CustomValidator runat="server" ID="valContentRequired" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Blog_ErrorMessageEntry %>"
                        SetFocusOnError="true" OnServerValidate="valContentRequired_ServerValidate"></asp:CustomValidator></td>
            </tr>
            <tr>
                <td class="blogField">
					<uc:HtmlEditor runat="server" ID="txtBlogPostHtml" Height="500" />
                </td>
            </tr>
            <tr>
                <td align="right">
                   
                    <asp:Button runat="server" ID="btnCancelDetails" OnClick="btnCancelDetails_Click" Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false" />
                    <asp:Button runat="server" ID="btnTags" Text="<%$ Resources:stringsRes, ctl_Blog_Tags %>" OnClientClick="return openTags();" />
                    <asp:Button runat="server" ID="btnSaveDetails" OnClick="btnSaveDetails_Click" Text="<%$Resources:StringsRes, glb__Save %>"
                        CausesValidation="true" />
                </td>
            </tr>
            <asp:Panel ID="pnEditComments" runat="server">
                <tr>
                    <td style="height: 5px;">
                    </td>
                </tr>
                <tr>
                    <td>
                        <b> <asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:stringsRes, ctl_Blog_Comments %>"></asp:Localize>:</b></td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView runat="server" ID="gvEntryCommentsEdit" DataKeyNames="Guid" CellPadding="5"
                            AutoGenerateColumns="false" OnRowCommand="gvEntryCommentsEdit_RowCommand" EnableViewState="false">
                            <Columns>
                                <asp:BoundField DataField="Created" DataFormatString="{0:d}" HtmlEncode="false"
                                    HeaderText="<%$ Resources:stringsRes, ctl_Blog_CommentsDate %>" SortExpression="Created" />
                                <asp:BoundField DataField="Author" HeaderText="<%$ Resources:stringsRes, ctl_Blog_CommentsAuthor %>" SortExpression="Author" />
                                <asp:TemplateField HeaderText="<%$ Resources:stringsRes, ctl_Blog_CommentsTitle %>">
                                    <ItemTemplate>
                                        <%#((string)Eval("Title")).Replace(Environment.NewLine, "<br>")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:stringsRes, ctl_Blog_Comment %>">
                                    <ItemTemplate>
                                        <%#((string)Eval("Content")).Replace(Environment.NewLine, "<br>")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="btnDelete" CommandName="delete_comment"
                                    CausesValidation="false" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <cc1:Pager runat="server" ID="editPager" PageSize="5" ControlToPage="gvEntryCommentsEdit">
                        </cc1:Pager>
                    </td>
                </tr>
            </asp:Panel>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
        <div style="padding-top: 4px"></div>
       <asp:Calendar ID="calendarBlog" runat="server" SkinID="blogCalendar" OnDayRender="calendarBlog_DayRender" OnSelectionChanged="calendarBlog_SelectionChanged"></asp:Calendar>
        <div style="padding-top: 4px"></div>
        <asp:DataList runat="server" ID="lstBlogEntries" DataKeyField="Guid" SkinID="blog"
            Width="100%" OnItemCommand="lstBlog_ItemCommand" OnItemDataBound="lstBlog_ItemBound">
            <ItemTemplate>
                <table border="0" width="100%" class="entry">
                    <tr>
                        <td class="title">
                            <asp:LinkButton runat="server" ID="btnDetails" CausesValidation="false" CommandName="ShowDetails"> <%#Eval("Title", "{0}")%>
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="content">
                            <%#Eval("Content", "{0}")%>
                        </td>
                    </tr>
                    <tr>
                        <td class="footer">
                            {<%#System.Convert.ToDateTime(Eval("Created", "{0}")).ToString("g")%>}&nbsp;{<asp:LinkButton
                                runat="server" ID="btnShowComments" CausesValidation="false" CommandName="ShowDetails"></asp:LinkButton>}
                            <%# getTagsFooter(DataBinder.Eval(Container.DataItem, "Tags"))%>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <cc1:Pager runat="server" ID="pagBlog" ControlToPage="lstBlogEntries" SkinID="blog" PageSize="5">
        </cc1:Pager>
    </asp:View>
    <asp:View runat="server" ID="readonlyDetailsView">
        <div style="padding-top: 4px"></div>
        <asp:Calendar ID="calendarBlogDetail" runat="server" SkinID="blogCalendar" OnDayRender="calendarBlogDetail_DayRender" OnSelectionChanged="calendarBlogDetail_SelectionChanged"></asp:Calendar>
        <div style="padding-top: 4px"></div>
        <table border="0" width="100%" class="blogDetail">
            <tr>
                <td class="title">
                    <asp:Label ID="lbTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="content">
                    <asp:Literal ID="ltContent" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="footer">
                    <asp:Label ID="lbFooter" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="CommentTitle">
            <asp:Label runat="server" ID="lbCommentsTitle"></asp:Label></div>
        <asp:Panel runat="server" ID="pnComments">
            <div class="roundedTop">
            </div>
            <asp:DataList runat="server" ID="lstEntryComments" Width="100%" SkinID="comments">
                <ItemTemplate>
                    <table border="0" width="100%" class="even">
                        <tr>
                            <td class="title">
                                <%#Eval("Title", "{0}")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="content">
                                <%#Eval("Content", "{0}")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="footer">
                                <%# String.Format(Resources.StringsRes.ctl_Blog_CommentsFooter,System.Convert.ToDateTime(Eval("Created", "{0}")).ToString("g"),Eval("Author", "{0}")) %>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <table border="0" width="100%" class="odd">
                        <tr>
                            <td class="title">
                                <%#Eval("Title", "{0}")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="content">
                                <%#Eval("Content", "{0}")%>
                            </td>
                        </tr>
                        <tr>
                             <td class="footer">
                                <%# String.Format(Resources.StringsRes.ctl_Blog_CommentsFooter,System.Convert.ToDateTime(Eval("Created", "{0}")).ToString("g"),Eval("Author", "{0}")) %>
                            </td>
                        </tr>
                    </table>
                </AlternatingItemTemplate>
            </asp:DataList>
            <div class="roundedBottom">
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnNewComment">
            <br />
            <div class="roundedTop">
            </div>
            <div class="framed">
                <table width="100%">
                    <tr>
                        <td class="fieldlabel">
                        </td>
                        <td class="field">
                            <asp:RequiredFieldValidator runat="server" ID="valCommentTitleRequired" ControlToValidate="txtCommentTitle"
                                Display="dynamic" ErrorMessage="<%$Resources:StringsRes, ctl_Blog_CommentsErrorMessageTitle %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize ID="Localize1" runat="server" Text="<%$Resources:StringsRes, ctl_Blog_CommentsTitle %>"></asp:Localize></td>
                        <td class="field">
                            <asp:TextBox runat="server" ID="txtCommentTitle"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                        </td>
                        <td class="field">
                            <asp:RequiredFieldValidator runat="server" ID="valCommentAuthorRequired" ControlToValidate="txtCommentAuthor"
                                Display="dynamic" ErrorMessage="<%$Resources:StringsRes, ctl_Blog_CommentsErrorMessageAuthor %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize ID="Localize4" runat="server" Text="<%$Resources:StringsRes, ctl_Blog_CommentsAuthor %>"></asp:Localize></td>
                        <td class="field">
                            <asp:TextBox runat="server" ID="txtCommentAuthor"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                        </td>
                        <td class="field">
                            <asp:RequiredFieldValidator runat="server" ID="valCommentTextRequired" ControlToValidate="txtCommentText"
                                Display="dynamic" ErrorMessage="<%$Resources:StringsRes, ctl_Blog_ErrorMessageComment %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize ID="Localize2" runat="server" Text="<%$Resources:StringsRes, ctl_Blog_Comment %>"></asp:Localize></td>
                        <td class="field">
                            <asp:TextBox runat="server" ID="txtCommentText" TextMode="multiline" Rows="8" Style="height: auto;"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                        </td>
                        <td class="field">
                            <asp:RequiredFieldValidator runat="server" ID="valAntiBotImageRequired" ControlToValidate="txtAntiBotImage"
                                Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibot %>" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:CustomValidator runat="server" ID="valAntiBotImage" OnServerValidate="valAntiBotImage_ServerValidate"
                               Text="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibotInvalid %>" Display="dynamic"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:stringsRes, ctl_Guestbook_AntiBotImage%>"></asp:Localize></td>
                                    <td>
                                        &nbsp;&nbsp;</td>
                                    <td>
                                        <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAntiBotImage" MaxLength="4" CssClass="textbox"
                                            Width="75px"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button runat="server" ID="btnNewComment" OnClick="btnNewComment_Click" Text="<%$ Resources:stringsRes, glb__Submit%>"
                                CausesValidation="true" /></td>
                    </tr>
                </table>
            </div>
            <div class="roundedBottom">
            </div>
        </asp:Panel>
    </asp:View>
    <asp:View runat="server" ID="editTagsView">
        <table width="100%">
            <tr>
                <td align="right"><asp:Button runat="server" ID="btnBack" OnClick="btnBack_Click" Text="<%$Resources:StringsRes, glb__Back %>" CausesValidation="false" />
                <asp:Button runat="server" ID="btnNewTag" OnClick="btnNewTag_Click" Text="<%$Resources:stringsRes, glb__add %>" CausesValidation="false"></asp:Button></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView runat="server" ID="gvTagsEdit" DataKeyNames="Guid" CellPadding="5"
                        AutoGenerateColumns="false" OnRowCommand="gvTagsEdit_RowCommand" EnableViewState="false">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="<%$Resources:stringsRes, ctl_Blog_Tag %>" SortExpression="Name" />
                            <asp:TemplateField HeaderText="<%$Resources:stringsRes, ctl_Blog_TagsDescription %>">
                                <ItemTemplate>
                                    <%#((string)Eval("Description")).Replace(Environment.NewLine, "<br>")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Number" HeaderText="<%$Resources:stringsRes, ctl_Blog_TagsNumber %>" SortExpression="Number" />
                            <asp:ButtonField ItemStyle-Width="30px" ButtonType="Link" ControlStyle-CssClass="btnEdit" CommandName="edit_tag" CausesValidation="false" />
                            <asp:ButtonField ItemStyle-Width="30px" ButtonType="Link" ControlStyle-CssClass="btnDelete" CommandName="delete_tag" CausesValidation="false" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:View>
     <asp:View runat="server" ID="editTagsDetailsView">
        <table width="100%">
            <tr>
                <td class="fieldlabel"></td>
                <td class="field"><asp:RequiredFieldValidator runat="server" ID="valTagRequired" ControlToValidate="txtTag" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Blog_TagsErrorMessage %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
            </tr>
             <tr>
                <td class="fieldlabel"><asp:Localize ID="Localize5" runat="server" Text="<%$Resources:stringsRes, ctl_Blog_Tag %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" id="txtTag"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize ID="Localize6" runat="server" Text="<%$Resources:stringsRes, ctl_Blog_TagsDescription %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" id="txtTagDescription" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnCancelTag" OnClick="btnCancelTag_Click" Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false" />
                    <asp:Button runat="server" ID="btnSaveTag" OnClick="btnSaveTag_Click" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="true" />
                </td>
            </tr>
        </table>
     </asp:View>
      <asp:View runat="server" ID="editConfigurationView">
        <br />
        <fieldset>
            <legend><asp:Localize runat="server" Text="<%$Resources:StringsRes, ctl_Blog_Config %>"></asp:Localize></legend>
                <table width="100%">
             <tr>
                 <td></td>
                  <td><asp:Label runat="server" ID="lbConfigSaved" ForeColor="Red"><asp:Localize ID="Localize12" runat="server" Text="<%$Resources:StringsRes, ctl_Blog_ConfigSafed %>"></asp:Localize></asp:Label></td>
             </tr>
             <tr>
                <td class="fieldlabel"><asp:Localize ID="Localize7" runat="server" Text="<%$Resources:StringsRes, ctl_Blog_ConfigBlogName %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" id="txtBlogName"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize ID="Localize8" runat="server" Text="<%$Resources:StringsRes, ctl_Blog_ConfigPing %>"></asp:Localize></td>
                <td class="field"><asp:CheckBox runat="server" ID="ckbSendPings" /></td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                   <asp:Button runat="server" ID="btnBackConfig" Text="<%$Resources:StringsRes, glb__Back %>" CausesValidation="false" OnClick="btnBackConfig_Click" />
                    <asp:Button runat="server" ID="btnSaveConfiguration" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="true" OnClick="btnSaveConfiguration_Click" />
                </td>
            </tr>
        </table>
        </fieldset>
     </asp:View>
</asp:MultiView>
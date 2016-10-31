<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Guestbook.ascx.cs" Inherits="SectionControls_Guestbook" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td colspan="2">
                    <asp:GridView runat="server" ID="gvGuestbookEdit"  CellPadding="5"  DataKeyNames="Guid" AutoGenerateColumns="false" OnRowCommand="gvGuestbookEdit_RowCommand" EnableViewState="false">
                        <Columns>
                            <asp:BoundField DataField="EntryDate" DataFormatString="{0:d}" HtmlEncode="false" HeaderText="<%$ Resources:stringsRes, ctl_Guestbook_Date %>" SortExpression="EntryDate" />
                            <asp:BoundField DataField="Author" HeaderText="<%$ Resources:stringsRes, ctl_Guestbook_Author %>" SortExpression="Author" />
                            <asp:TemplateField HeaderText="<%$ Resources:stringsRes, ctl_Guestbook_Input %>">
                                <ItemTemplate>
                                <%#((string)Eval("Text")).Replace(Environment.NewLine, "<br>")%> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="btnDelete" CommandName="entry_delete" CausesValidation="false" />
                        </Columns>
                    </asp:GridView> 
                    <br />
                    <cc1:Pager runat="server" ID="editPager" ControlToPage="gvGuestbookEdit"></cc1:Pager>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
        <asp:Panel runat="server" ID="pnlEntries">
        <div class="roundedTop"></div>
        <div id="guestbookentries">
            <asp:Repeater runat="server" ID="repGuestbook" EnableViewState="false">
                <ItemTemplate>
                    <div class="odd">
                        <div class="pad">
                            <a id="<%#Eval("Guid").ToString()%>"></a><span class="date"><%#((DateTime)Eval("EntryDate")).ToShortDateString()%></span><span class="author"><%#Eval("Author")%></span><br />
                            <div class="entry"><%#((string)Eval("Text")).Replace(Environment.NewLine, "<br>")%></div>
                        </div>
                    </div>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <div class="even">
                        <div class="pad">
                             <a id="<%#Eval("Guid").ToString()%>"></a><span class="date"><%#((DateTime)Eval("EntryDate")).ToShortDateString()%></span><span class="author"><%#Eval("Author")%></span><br />
                            <div class="entry"><%#((string)Eval("Text")).Replace(Environment.NewLine, "<br>")%></div>
                        </div>
                    </div>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </div>
        <div class="roundedBottom"></div>
        <br />
        <br />
        </asp:Panel>
        <asp:PlaceHolder runat="server" ID="phEntryForm">
            <div class="roundedTop"></div>
            <div class="framed">
                <table width="100%">
                    <tr>
                        <td class="fieldlabel"></td>
                        <td class="field"><asp:RequiredFieldValidator runat="server" ID="valAuthorRequired" ControlToValidate="txtAuthor" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAuthorName %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_Guestbook_Name%>"></asp:Localize></td>
                        <td class="field"><asp:TextBox runat="server" ID="txtAuthor"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel"></td>
                        <td class="field"><asp:RequiredFieldValidator runat="server" ID="valTextRequired" ControlToValidate="txtText" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageText %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_Guestbook_Input%>"></asp:Localize></td>
                        <td class="field"><asp:TextBox runat="server" ID="txtText" TextMode="multiline" Rows="8" style="height: auto;"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="fieldlabel"></td>
                        <td class="field">
                            <asp:RequiredFieldValidator runat="server" ID="valAntiBotImageRequired" ControlToValidate="txtAntiBotImage" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibot %>" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:CustomValidator runat="server" ID="valAntiBotImage" OnServerValidate="valAntiBotImage_ServerValidate" Text="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibotInvalid %>" Display="dynamic"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldlabel"></td>
                        <td>
                            <table>
                                <tr>
                                    <td><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_Guestbook_AntiBotImage%>"></asp:Localize></td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td><asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td><asp:TextBox runat="server" ID="txtAntiBotImage" MaxLength="4" CssClass="textbox" Width="75px"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right"><asp:Button runat="server" ID="btnNewEntry" OnClick="btnNewEntry_Click"  Text="<%$ Resources:stringsRes, glb__Submit%>" CausesValidation="true" /></td>
                    </tr>
                </table>
            </div>
            <div class="roundedBottom"></div>
        </asp:PlaceHolder>
    </asp:View>
</asp:MultiView>
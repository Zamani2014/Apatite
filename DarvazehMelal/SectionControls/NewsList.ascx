<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsList.ascx.cs" Inherits="SectionControls_NewsList" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar" %>
<%@ Register Src="~/SectionControls/HtmlEditor.ascx" TagPrefix="uc" TagName="HtmlEditor" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td align="right"><asp:Button runat="server" ID="btnNewEntry" OnClick="btnNewEntry_Click" Text="<%$ Resources:stringsRes, glb__add %>" CausesValidation="false"></asp:Button></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView runat="server" DataKeyNames="Guid" ID="gvNewsListEdit" AutoGenerateColumns="false" ShowHeader="true" OnRowCommand="gvNewsListEdit_RowCommand">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="100px" DataField="NewsDate" DataFormatString="{0:d}" HeaderText="<%$ Resources:stringsRes, ctl_NewsList_Date %>" HtmlEncode="false" SortExpression="NewsDate"/>
                            <asp:BoundField DataField="Headline" HeaderText="<%$ Resources:stringsRes, ctl_NewsList_News %>" SortExpression="Headline"/>
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnEdit" CommandName="edit_entry" CausesValidation="false" />
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnDelete" CommandName="delete_entry" CausesValidation="false" />
                        </Columns>
                        <RowStyle Height="25px" />
                        <HeaderStyle Height="30px" />
                    </asp:GridView>
                    <br />
                    <cc1:Pager runat="server" ID="editPager" ControlToPage="gvNewsListEdit"></cc1:Pager>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="editDetailsView">
        <table width="100%">
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_NewsList_ShowFrom %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" ID="txtDateFrom" SkinID="datepicker"></asp:TextBox><RJS:PopCalendar runat="server" Control="txtDateFrom" ID="dteDateFrom" /></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_NewsList_ShowUntil %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" ID="txtDateUntil" SkinID="datepicker"></asp:TextBox><RJS:PopCalendar runat="server" Control="txtDateUntil" ID="dteDateUntil" /></td>
            </tr>
            <tr>
                <td class="fieldlabel"></td>
                <td class="field"><asp:RequiredFieldValidator runat="server" ID="valHeadlineRequired" ControlToValidate="txtHeadline" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_NewsList_ErrorMessageHeadline %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_NewsList_Headline %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" id="txtHeadline"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_NewsList_Content %>"></asp:Localize></td>
                <td class="field"><uc:HtmlEditor runat="server" ID="txtNewsHtml" Height="300" /></td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnCancelDetails" OnClick="btnCancelDetails_Click" Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false"></asp:Button>&nbsp;
                    <asp:Button runat="server" ID="btnSaveDetails" OnClick="btnSaveDetails_Click" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="true"></asp:Button>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
        <asp:GridView runat="server" DataKeyNames="Guid" ID="gvNewsList" AutoGenerateColumns="false" ShowHeader="true" OnRowCommand="gvNewsList_RowCommand">
            <Columns>
                <asp:BoundField DataField="NewsDate" ItemStyle-Width="100px" DataFormatString="{0:d}" HeaderText="<%$ Resources:stringsRes, ctl_NewsList_Date %>" HtmlEncode="false" SortExpression="NewsDate" />
                <asp:ButtonField ButtonType="Link" DataTextField="Headline" HeaderText="<%$ Resources:stringsRes, ctl_NewsList_News %>" CommandName="show_details" SortExpression="Headline" />
            </Columns>
            <RowStyle Height="25px" />
            <HeaderStyle Height="30px" />
        </asp:GridView>
        <br />
        <cc1:Pager runat="server" ID="readonlyPager" ControlToPage="gvNewsList"></cc1:Pager>
        <div runat="server" id="divNewsDetails" class="newscontent" visible="false">
            <span class="date"><asp:Literal runat="server" ID="litNewsDate"></asp:Literal></span>
            <h1><asp:Literal runat="server" ID="litNewsTitle"></asp:Literal></h1>
            <asp:Literal runat="server" ID="litNewsContent"></asp:Literal>
        </div>
    </asp:View>
</asp:MultiView>
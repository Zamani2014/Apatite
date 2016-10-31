<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" Title="<%$ Resources:stringsRes, glb__Search%>" ValidateRequest="false" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
<style type="Text/css">
div#breadcrumbs
{
    display: none;
}
</style>
 <div class="roundedTop"></div>
    <div class="framed">
        <asp:Panel runat="server" ID="pnlSearchform" DefaultButton="btnSearch">
        <table width="100%">
            <tr>
                <td class="fieldlabel"></td>
                <td class="field"><asp:RequiredFieldValidator runat="server" ID="valKeywordRequired" ControlToValidate="txtSearch" ErrorMessage="<%$ Resources:stringsRes, pge_Search_ErrorMessageKeyword %>" Display="dynamic" ValidationGroup="SearchForm"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize ID="lclSearchTerm" runat="server" Text="<%$ Resources:stringsRes, pge_Search_SearchQuery %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" ID="txtSearch"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="right"><asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="<% $ Resources:stringsRes, pge_Search_StartSearch %>" CausesValidation="true" ValidationGroup="SearchForm" /></td>
            </tr>
        </table>
        </asp:Panel>
    </div>
     <div class="roundedBottom"></div>
    <p>&nbsp;</p>
    <asp:Localize ID="locNoresults" runat="server" Text="<%$ Resources:stringsRes, glb_Search_NoSearchResults %>"></asp:Localize>
    <asp:GridView runat="server" ID="gvResults" Visible="false" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Title" HtmlEncode="false" HeaderText="<%$ Resources:stringsRes, pge_Search_Rowtitle_Title %>" />
            <asp:BoundField DataField="Excerpt" HtmlEncode="false" HeaderText="<%$ Resources:stringsRes, pge_Search_Rowtitle_Excerpt %>" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="lnkView" NavigateUrl='<%# Eval("Url") %>' runat="server"><asp:Localize ID="lclSearchTerm" runat="server" Text="<%$ Resources:stringsRes, pge_Search_View %>"></asp:Localize></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <cc1:Pager runat="server" ID="resultsPager" ControlToPage="gvResults"></cc1:Pager>
</asp:Content>

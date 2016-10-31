<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BlogSearch.aspx.cs" Inherits="TagResults" ValidateRequest="false" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
<style type="Text/css">
div#breadcrumbs
{
    display: none;
}
</style>

<asp:DataList runat="server" ID="lstBlogEntries" DataKeyField="Guid" SkinID="blog" Width="100%">
            <HeaderTemplate>
            <table class="search">
                <tr>
                    <td class="separator">&gt;</td>
                    <td class="searchtitle"><%= String.Format(Resources.StringsRes.pge_BlogSearch_Title,Request.QueryString["tag"]) %></td>
                </tr>
            </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table border="0" width="100%" class="entry">
                    <tr>
                        <td class="title">
                            <a href="Default.aspx?pg=<%#Eval("PageId", "{0}")%>&detail=<%#Eval("Guid", "{0}")%>#<%#Eval("SectionId", "{0}")%>"><%#Eval("Title", "{0}")%></a>
                        </td>
                    </tr>
                    <tr>
                        <td class="content">
                            <%#Eval("Content", "{0}")%>
                        </td>
                    </tr>
                    <tr>
                        <td class="footer">
                            {<%#System.Convert.ToDateTime(Eval("Created", "{0}")).ToString("g")%>}&nbsp;{<a href="Default.aspx?pg=<%#Eval("PageId", "{0}")%>&detail=<%#Eval("Guid", "{0}")%>#<%#Eval("SectionId", "{0}")%>"><%# countComments(DataBinder.Eval(Container.DataItem, "Comments"))%></a>}
                            <%# getTagsFooter(DataBinder.Eval(Container.DataItem, "Tags"))%>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <cc1:Pager runat="server" ID="pagBlog" ControlToPage="lstBlogEntries" SkinID="blog" PageSize="5">
        </cc1:Pager>
        <br />
        <asp:Label runat="server" ID="lbNoResults"></asp:Label>
</asp:Content>
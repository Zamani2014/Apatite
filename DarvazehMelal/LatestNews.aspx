<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LatestNews.aspx.cs" Inherits="LatestNews" MasterPageFile="~/Site.master"%>

<asp:Content ID="contentPane" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:GridView runat="server" ID="gvResults" AutoGenerateColumns="False" 
        DataKeyNames="ID" 
        ForeColor="Black" GridLines="None" AllowPaging="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
        <asp:TemplateField> 
        <ItemTemplate>
            <table style="width: 100%; font-size:12px">
                <tr>
                    <td style=" padding-right:5px">
                        <asp:Label ID="Label2" runat="server" Text="عنوان خبر :  " Font-Bold="true"></asp:Label><asp:Label ID="Label1" runat="server" Text='<%# Bind("Subject") %>'></asp:Label><br />
                        <asp:Label ID="Label9" runat="server" Text="تاریخ خبر :  " Font-Bold="true"></asp:Label><asp:Label ID="Label10"
                            runat="server" Text='<%# Bind("DateTime") %>'></asp:Label><br />
                    </td>
                </tr>
                </table>
                <table style="width: 100%;font-size:12px">
                <tr>
                    <td style=" padding-right:5px">
                        <asp:Label ID="Label16" runat="server" Text="متن خبر :  " Font-Bold="true"></asp:Label><asp:Label ID="Label17"
                            runat="server" Text='<%# Bind("News") %>'></asp:Label>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#FEF5CC" Font-Bold="True" ForeColor="Black"  BorderColor="#4E1714" BorderStyle="Solid" BorderWidth="1px"/>
<%--        <HeaderStyle BackColor="#FEF5CC" Font-Bold="True" ForeColor="Black" BorderColor="#4E1714" BorderStyle="Solid" BorderWidth="1px"/>
--%>        <PagerStyle BackColor="#FEF5CC" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#000000"/>
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        <RowStyle BorderColor="#F2E9C4" BorderStyle="Solid" BorderWidth="1px"/>
    </asp:GridView>
</asp:Content>

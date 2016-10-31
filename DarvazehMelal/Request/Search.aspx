<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Request_Search" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:Label ID="Label65" runat="server" Text="موردی برای جستحوی شما یافت نشد !" Visible="false"></asp:Label>
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
                        <asp:Label ID="Label2" runat="server" Text="نوع ملک :" Font-Bold="true"></asp:Label><asp:Label ID="Label1" runat="server" Text='<%# Bind("BuildingType") %>'></asp:Label><br />
                        <asp:Label ID="Label7" runat="server" Text="نوع معامله :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label8" runat="server" Text='<%# Bind("TransactionType") %>'></asp:Label><br />
                        <asp:Label ID="Label9" runat="server" Text="قیمت کل :" Font-Bold="true"></asp:Label><asp:Label ID="Label10"
                            runat="server" Text='<%# Bind("Price") %>'></asp:Label><br />
                        <asp:Label ID="Label11" runat="server" Text="استان و شهر :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label12" runat="server" Text='<%# Bind("Province") %>'></asp:Label>&nbsp;-&nbsp;<asp:Label ID="Label13" runat="server"
                                Text='<%# Bind("State") %>'></asp:Label>&nbsp;-&nbsp;<asp:Label ID="Label28" runat="server" Text='<%# Bind("City") %>'></asp:Label><br />
                        <asp:Label ID="Label14" runat="server" Text="منطقه :" Font-Bold="true"></asp:Label><asp:Label ID="Label15"
                            runat="server" Text='<%# Bind("Region") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="نوع سند / کاربری :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label4" runat="server" Text='<%# Bind("DocType") %>'></asp:Label><br />
                        <asp:Label ID="Label5" runat="server" Text="حدود متراژ :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label6" runat="server" Text='<%# Bind("Range") %>'></asp:Label><br />
                        <asp:Label ID="Label29" runat="server" Text="کد تقاضا :" Font-Bold="true"></asp:Label><asp:Label ID="Label30"
                            runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="نام متقاضی :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label19" runat="server" Text='<%# Bind("Name") %>'></asp:Label><br />
                        <asp:Label ID="Label20" runat="server" Text="پست الکترونیکی :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label21" runat="server" Text='<%# Bind("EMail") %>'></asp:Label><br />
                        <asp:Label ID="Label22" runat="server" Text="تلفن ثابت :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label23" runat="server" Text='<%# Bind("Tel") %>'></asp:Label><br />
                        <asp:Label ID="Label24" runat="server" Text="تلفن همراه :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label25" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label><br />
                        <asp:Label ID="Label26" runat="server" Text="زمان ثبت :" Font-Bold="true"></asp:Label><asp:Label ID="Label27"
                            runat="server" Text='<%# Bind("DateTime") %>'></asp:Label>
                    </td>
                </tr>
                </table>
                <table style="width: 100%;font-size:12px">
                <tr>
                    <td style=" padding-right:5px">
                        <asp:Label ID="Label16" runat="server" Text="توضیحات :" Font-Bold="true"></asp:Label><asp:Label ID="Label17"
                            runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
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
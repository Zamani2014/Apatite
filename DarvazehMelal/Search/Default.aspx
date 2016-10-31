<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Search_Default"  MasterPageFile="~/Site.master"%>


<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:Label ID="Label65" runat="server" Text="موردی برای جستحوی شما یافت نشد !" Visible="false"></asp:Label>
    <asp:GridView runat="server" ID="gvResults" AutoGenerateColumns="False" 
        DataKeyNames="ID"
        ForeColor="Black" GridLines="None" AllowPaging="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
        <asp:TemplateField> 
        <ItemTemplate>
            <table style="width: 100%;font-size:12px">
                <tr >
                    <td >
                        <div style=" background-color:#F3CE5A; margin:10px; padding:3px; border:1px solid #C49527; border-radius:5px; font-size:13px; font-weight:bold">
                        <asp:Label ID="Label43" runat="server" Text='<%# Bind("TransactionType") %>'></asp:Label> - <asp:Label ID="Label44" runat="server" Text='<%# Bind("BuildingType") %>'></asp:Label> - <asp:Label ID="Label48" runat="server" Text='<%# Bind("Province") %>'></asp:Label> - <asp:Label ID="Label45" runat="server" Text='<%# Bind("City") %>'></asp:Label> - <asp:Label ID="Label46" runat="server" Text='<%# Bind("Region") %>'></asp:Label> - <asp:Label ID="Label47" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; font-size:12px">
                <tr>
                    <td style=" padding-right:5px">
                        <asp:Label ID="Label2" runat="server" Text="متراژ بنا :" Font-Bold="true"></asp:Label><asp:Label ID="Label1" runat="server" Text='<%# Bind("Range") %>'></asp:Label><br />
                        <asp:Label ID="Label7" runat="server" Text="مساحت زمین :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label8" runat="server" Text='<%# Bind("Area") %>'></asp:Label><br />
                        <asp:Label ID="Label9" runat="server" Text="تعداد اتاق :" Font-Bold="true"></asp:Label><asp:Label ID="Label10"
                            runat="server" Text='<%# Bind("RoomNo") %>'></asp:Label><br />
                        <asp:Label ID="Label11" runat="server" Text="شماره طبقه :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label12" runat="server" Text='<%# Bind("ClassNo") %>'></asp:Label><br />
                        <asp:Label ID="Label14" runat="server" Text="تعداد کل طبقات :" Font-Bold="true"></asp:Label><asp:Label ID="Label15"
                            runat="server" Text='<%# Bind("ClassTot") %>'></asp:Label><br />
                        <asp:Label ID="Label13" runat="server" Text="تعداد واحد در طبقه :" Font-Bold="true"></asp:Label><asp:Label ID="Label28"
                            runat="server" Text='<%# Bind("UnitsInClass") %>'></asp:Label><br />
                        <asp:Label ID="Label31" runat="server" Text="تعداد کل واحد ها :" Font-Bold="true"></asp:Label><asp:Label ID="Label32"
                            runat="server" Text='<%# Bind("UnitsTot") %>'></asp:Label><br />
                        <asp:Label ID="Label33" runat="server" Text="نمای ساختمان :" Font-Bold="true"></asp:Label><asp:Label ID="Label34"
                            runat="server" Text='<%# Bind("BuildingView") %>'></asp:Label><br />
                        <asp:Label ID="Label35" runat="server" Text="وضعیت سکونت :" Font-Bold="true"></asp:Label><asp:Label ID="Label36"
                            runat="server" Text='<%# Bind("ResidentType") %>'></asp:Label><br />
                        <asp:Label ID="Label37" runat="server" Text="سن بنا :" Font-Bold="true"></asp:Label><asp:Label ID="Label38"
                            runat="server" Text='<%# Bind("YoursOld") %>'></asp:Label><br />
                        <asp:Label ID="Label39" runat="server" Text="چند ساله :" Font-Bold="true"></asp:Label><asp:Label ID="Label40"
                            runat="server" Text='<%# Bind("Old") %>'></asp:Label><br />
                        <asp:Label ID="Label41" runat="server" Text="موقعیت جغرافیائی :" Font-Bold="true"></asp:Label><asp:Label ID="Label42"
                            runat="server" Text='<%# Bind("GeoPosition") %>'></asp:Label><br />
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="کابینت آشپزخانه :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label4" runat="server" Text='<%# Bind("Cabinet") %>'></asp:Label><br />
                        <asp:Label ID="Label5" runat="server" Text="سرویس بهداشتی :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label6" runat="server" Text='<%# Bind("Sanitary") %>'></asp:Label><br />
                        <asp:Label ID="Label29" runat="server" Text="کف پوش :" Font-Bold="true"></asp:Label><asp:Label ID="Label30"
                            runat="server" Text='<%# Bind("Floor") %>'></asp:Label><br />
                        <asp:Label ID="Label49" runat="server" Text="قیمت :" Font-Bold="true"></asp:Label><asp:Label ID="Label50"
                            runat="server" Text='<%# Bind("TotPrice") %>'></asp:Label><br />
                        <asp:Label ID="Label51" runat="server" Text="قیمت متری :" Font-Bold="true"></asp:Label><asp:Label ID="Label52"
                            runat="server" Text='<%# Bind("Price") %>'></asp:Label><br />
                        <asp:Label ID="Label55" runat="server" Text="واحد پول :" Font-Bold="true"></asp:Label><asp:Label ID="Label56"
                            runat="server" Text='<%# Bind("Currency") %>'></asp:Label><br />
                        <asp:Label ID="Label59" runat="server" Text="تعداد پارکینگ :" Font-Bold="true"></asp:Label><asp:Label ID="Label60"
                            runat="server" Text='<%# Bind("Parking") %>'></asp:Label><br />
                        <asp:Label ID="Label61" runat="server" Text="تعداد تلفن :" Font-Bold="true"></asp:Label><asp:Label ID="Label62"
                            runat="server" Text='<%# Bind("TelsNo") %>'></asp:Label><br />
                        <asp:Label ID="Label57" runat="server" Text="دیگر امکانات :" Font-Bold="true"></asp:Label><asp:Label ID="Label58"
                            runat="server" Text='<%# Bind("Other") %>'></asp:Label><br />
                    </td>
                    <td>
                        <asp:Label ID="Label53" runat="server" Text="نوع سند / کاربری :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label54" runat="server" Text='<%# Bind("DocType") %>'></asp:Label><br />
                        <asp:Label ID="Label18" runat="server" Text="نام مالک :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label19" runat="server" Text='<%# Bind("OwnerName") %>'></asp:Label><br />
                        <asp:Label ID="Label20" runat="server" Text="پست الکترونیکی :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label21" runat="server" Text='<%# Bind("EMail") %>'></asp:Label><br />
                        <asp:Label ID="Label22" runat="server" Text="تلفن ثابت :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label23" runat="server" Text='<%# Bind("Tel1") %>'></asp:Label><br />
                        <asp:Label ID="Label24" runat="server" Text="تلفن همراه :" Font-Bold="true"></asp:Label><asp:Label
                            ID="Label25" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label><br />
                        <asp:Label ID="Label26" runat="server" Text="زمان ثبت :" Font-Bold="true"></asp:Label><asp:Label ID="Label27"
                            runat="server" Text='<%# Bind("DateTime") %>'></asp:Label>
                    </td>
                </tr>
                </table>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <div style=" background-color:#FFD829; margin:10px; padding:3px; border:1px solid #C49527; border-radius:5px; font-size:13px; font-weight:bold">
                            <asp:Label ID="Label63" runat="server" Text="امکانات :" Font-Bold="true"></asp:Label><asp:Label ID="Label64"
                            runat="server" Text='<%# Bind("Facilities") %>'></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
                <table style="width: 100%;font-size:12px">
                <tr>
                    <td>
                        <div style=" background-color:#EFDC9C; margin:10px; padding:3px; border:1px solid #C49527; border-radius:5px; font-size:13px; font-weight:bold">
                        <asp:Label ID="Label16" runat="server" Text="توضیحات :" Font-Bold="true"></asp:Label><asp:Label ID="Label17"
                            runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                            </div>
                    </td>
                    </div>
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
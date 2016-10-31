<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterList.aspx.cs" Inherits="Register_RegisterList" MasterPageFile="~/Site.master" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:Label ID="Label65" runat="server" Text="موردی برای جستحوی شما یافت نشد !" Visible="false"></asp:Label>
    <webdiyer:aspnetpager id="AspNetPager1" runat="server" PageSize="10" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged" ShowCustomInfoSection="Left" CustomInfoSectionWidth="30%" ShowPageIndexBox="always" PageIndexBoxType="DropDownList"
    CustomInfoHTML="صفحه ی:<font color='red'><b>%currentPageIndex%</b></font> از %PageCount% تعداد %PageSize% مورد در هر صفحه"></webdiyer:aspnetpager>
    <asp:GridView runat="server" ID="gvResults" AutoGenerateColumns="False" 
        DataKeyNames="ID" 
        ForeColor="Black" GridLines="None" AllowPaging="True"  OnSelectedIndexChanged="openLinkClick">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
        <asp:TemplateField> 
        <ItemTemplate>
            <asp:Label ID="OwnerIDlbl" Visible="false" runat="server" Text='<%# Bind("OwnerID") %>'></asp:Label>
            <asp:Label ID="IDlbl" Visible="false" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            <table style="width: 100%;font-size:12px; ">
                <tr >
                    <td >
                        <div style=" background-color:#F3CE5A; margin:10px; padding:5px; border:1px solid #C49527; border-radius:5px; font-size:13px; font-weight:bold">
                        <asp:Label ID="Label43" runat="server" Text='<%# Bind("TransactionType") %>'></asp:Label><asp:Label ID="Label44" runat="server" Text='<%# Bind("BuildingType") %>'></asp:Label> در استان <asp:Label ID="Label48" runat="server" Text='<%# Bind("Province") %>'></asp:Label> و در شهر <asp:Label ID="Label45" runat="server" Text='<%# Bind("City") %>'></asp:Label> و در منطقه <asp:Label ID="Label46" runat="server" Text='<%# Bind("Region") %>'></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; font-size:12px">
                <tr>
                    <td style=" padding-right:5px">
                       <div style="text-align:center"> <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Very-Basic-Home-icon.png" Height="120" Width="120" /></div>
                    </td>
                    <td style=" padding-right:5px">
                        <asp:Label ID="Label2" runat="server" Text="متراژ بنا :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000" ID="Label1" runat="server" Text='<%# Bind("Range") %>'></asp:Label><br />
                        <asp:Label ID="Label7" runat="server" Text="مساحت زمین :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000"
                            ID="Label8" runat="server" Text='<%# Bind("Area") %>'></asp:Label><br />
                        <asp:Label ID="Label9" runat="server" Text="تعداد اتاق :" Font-Bold="true"></asp:Label><asp:Label ID="Label10" ForeColor="#800000"
                            runat="server" Text='<%# Bind("RoomNo") %>'></asp:Label><br />
                        <asp:Label ID="Label11" runat="server" Text="شماره طبقه :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000"
                            ID="Label12" runat="server" Text='<%# Bind("ClassNo") %>'></asp:Label><br />
                        <asp:Label ID="Label14" runat="server" Text="تعداد کل طبقات :" Font-Bold="true"></asp:Label><asp:Label ID="Label15" ForeColor="#800000"
                            runat="server" Text='<%# Bind("ClassTot") %>'></asp:Label><br />
                        <asp:Label ID="Label13" runat="server" Text="تعداد واحد در طبقه :" Font-Bold="true"></asp:Label><asp:Label ID="Label28" ForeColor="#800000"
                            runat="server" Text='<%# Bind("UnitsInClass") %>'></asp:Label>

                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="کابینت آشپزخانه :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000"
                            ID="Label4" runat="server" Text='<%# Bind("Cabinet") %>'></asp:Label><br />
                        <asp:Label ID="Label5" runat="server" Text="سرویس بهداشتی :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000"
                            ID="Label6" runat="server" Text='<%# Bind("Sanitary") %>'></asp:Label><br />
                        <asp:Label ID="Label29" runat="server" Text="کف پوش :" Font-Bold="true"></asp:Label><asp:Label ID="Label30" ForeColor="#800000"
                            runat="server" Text='<%# Bind("Floor") %>'></asp:Label><br />
                        <asp:Label ID="Label49" runat="server" Text="قیمت :" Font-Bold="true"></asp:Label><asp:Label ID="Label50" ForeColor="#800000"
                            runat="server" Text='<%# Bind("TotPrice") %>'></asp:Label><br />
                        <asp:Label ID="Label51" runat="server" Text="قیمت متری :" Font-Bold="true"></asp:Label><asp:Label ID="Label52" ForeColor="#800000"
                            runat="server" Text='<%# Bind("Price") %>'></asp:Label><br />
                        <asp:Label ID="Label55" runat="server" Text="واحد پول :" Font-Bold="true"></asp:Label><asp:Label ID="Label56" ForeColor="#800000"
                            runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label53" runat="server" Text="نوع سند / کاربری :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000"
                            ID="Label54" runat="server" Text='<%# Bind("DocType") %>'></asp:Label><br />
                        <asp:Label ID="Label18" runat="server" Text="نام مالک :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000"
                            ID="Label19" runat="server" Text='<%# Bind("OwnerName") %>'></asp:Label><br />
                        <asp:Label ID="Label20" runat="server" Text="پست الکترونیکی :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000"
                            ID="Label21" runat="server" Text='<%# Bind("EMail") %>'></asp:Label><br />
                        <asp:Label ID="Label22" runat="server" Text="تلفن ثابت :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000"
                            ID="Label23" runat="server" Text='<%# Bind("Tel1") %>'></asp:Label><br />
                        <asp:Label ID="Label24" runat="server" Text="تلفن همراه :" Font-Bold="true"></asp:Label><asp:Label ForeColor="#800000"
                            ID="Label25" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label><br />
                        <asp:Label ID="Label26" runat="server" Text="زمان ثبت :" Font-Bold="true"></asp:Label><asp:Label ID="Label27" ForeColor="#800000"
                            runat="server" Text='<%# Bind("DateTime") %>'></asp:Label>
                    </td>
                </tr>
                </table>
            <div style="text-align:center; padding:5px">
            <asp:LinkButton ID="viewLink" runat="server" CommandName="Select" Style=" float:right">
             <img class="pdficon" src="../Images/Icon-Document.png" width="50" height="50" alt="مشاهده جزئیات" /><br />نمایش جزئیات ملک
            </asp:LinkButton>      
            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" Style=" float:left">
             <img class="pdficon" src="../Images/Icon-Document.png" width="50" height="50" alt="ذخیره در فهرست مقایسه" /><br />ذخیره در فهرست مقایسه
            </asp:LinkButton>   
            </div>    
            <br />   
            <br /><br /><br />                    
            <hr /><hr /><hr />
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
        SelectCommand="SELECT * FROM [BuildingTbl], [OwnerTbl] WHERE BuildingTbl.OwnerID = OwnerTbl.ID"></asp:SqlDataSource>
</asp:Content>
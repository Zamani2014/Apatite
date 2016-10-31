<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HouseTabs.ascx.cs" Inherits="EasyControls_HouseTabs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="DNA.UI.JQuery" Namespace="DNA.UI.JQuery" TagPrefix="DotNetAge" %>
<%@ Register TagPrefix="LatestNews" TagName="LNews" src="~/EasyControls/LatestNews.ascx" %>
     <link rel="stylesheet" type="text/css" href="../App_Themes/DarvazehMelal/CSS/sagscroller.css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="js/sagscroller.js"></script>
    <script type="text/javascript">

        var sagscroller1 = new sagscroller({
            id: 'mysagscroller',
            mode: 'manual'
        })



        var sagscroller2 = new sagscroller({
            id: 'mysagscroller2',
            mode: 'auto',
            pause: 2500,
            animatespeed: 400
        })


    </script>
    <style type="text/css">
     .fieldset1
       {
           background-color: #D09046;
           position:relative; right:5px; margin-top:0px;
       }
       
       .legend1
       {
       }
        .style1
        {
            width: 115px;
        }
    </style>

<DotNetAge:Tabs ID="Tabs1" runat="server" Height="250px" Width="550px" style=" float:right">
<Animations>
    <DotNetAge:AnimationAttribute AnimationType="opacity" Value="toggle" />
    <DotNetAge:AnimationAttribute AnimationType="height" Value="toggle" />
</Animations>
<Views>
                <DotNetAge:View ID="View3" runat="server" Text="خرید ملک">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="استان :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" Width="120px" runat="server" DataSourceID="SqlDataSource1" DataTextField="ProvinceName" DataValueField="ProvinceName">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="شهر :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="100px" Height="20px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="منطقه :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" Width="100px" Height="20px"></asp:TextBox><br /><br />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="نوع ملک:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" Width="120px" runat="server" DataSourceID="SqlDataSource2" DataTextField="BuildingName" DataValueField="BuildingName">
                                </asp:DropDownList>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="قیمت/حداقل:"></asp:Label>
                            </td>
                           <td>
                               <asp:TextBox ID="TextBox4" runat="server" Width="100px" Height="20px" style=" text-align:center"></asp:TextBox>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="قیمت/حداکثر:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server" Width="100px"  Height="20px" style=" text-align:center"></asp:TextBox><br /><br />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="متراژ :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox10" runat="server" Width="100px"  Height="20px" style=" text-align:center"></asp:TextBox>
                               </td>
                            <td>
                            </td>
                             <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="SearchBtn" runat="server" Text="جستجو" OnClick="SearchBtn_Click"  style="float:left"/>
                            </td>

                        </tr>
                    </table>
                    انتخاب استان ، شهر و نوع ملک الزامی است.
                </DotNetAge:View>
                <DotNetAge:View ID="View5" runat="server" Text="رهن و اجاره ملک">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="استان :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList4" Width="120px" runat="server" DataSourceID="SqlDataSource1" DataTextField="ProvinceName" DataValueField="ProvinceName">
                                </asp:DropDownList>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="شهر :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server" Width="100px" Height="20px"></asp:TextBox>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="منطقه :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" Width="100px" Height="20px"></asp:TextBox><br /><br />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="نوع ملک:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList5" Width="120px" runat="server" DataSourceID="SqlDataSource2" DataTextField="BuildingName" DataValueField="BuildingName">
                                </asp:DropDownList>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="اجاره/حداقل :"></asp:Label>
                            </td>
                           <td>
                               <asp:TextBox ID="TextBox8" runat="server" Width="100px" Height="20px" style=" text-align:center"></asp:TextBox>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="اجاره/حداکثر :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox9" runat="server" Width="100px"  Height="20px" style=" text-align:center"></asp:TextBox><br /><br />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="متراژ :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox11" runat="server" Width="100px"  Height="20px" style=" text-align:center"></asp:TextBox>
                               </td>
                            <td>
                            </td>
                                                        <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="SearchBtn2" runat="server" Text="جستجو"  OnClick="SearchBtn2_Click" style="float:left"/>
                            </td>

                        </tr>
                    </table>
                    انتخاب استان ، شهر و نوع ملک الزامی است.
                </DotNetAge:View>
                <DotNetAge:View ID="View2" runat="server" Text="جستجو با شناسه ملک">
                <div style=" margin-top:50px; margin-right:140px">
                    <asp:TextBox ID="TextBox1" ValidationGroup="1" runat="server" style=" text-align:left"></asp:TextBox><asp:Button ID="SearchBtn3"
                        runat="server" Text="جستجو" OnClick="SearchBtn3_Click" ValidationGroup="1"/>
                        </div>
                </DotNetAge:View>
</Views>
</DotNetAge:Tabs>

<fieldset class="fieldset1">
<legend class="legend1">آخرین خبرهای گروه :</legend>
<div id="mysagscroller" class="sagscroller">
                <table id="sagTable" width="100%">
                <tr>
                    <td><LatestNews:LNews id="LNews1" runat="server"></LatestNews:LNews></td>
                </tr>
                </table>            
</div>
</fieldset>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                    SelectCommand="SELECT * FROM [ProvinceTbl]"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                    SelectCommand="SELECT * FROM [BuildingTypes]"></asp:SqlDataSource>
                                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                    SelectCommand="SELECT * FROM [AreaRange]"></asp:SqlDataSource>

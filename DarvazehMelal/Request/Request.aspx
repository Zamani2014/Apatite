<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Request.aspx.cs" Inherits="Request_Request" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<fieldset>
<legend>ثبت تقاضای ملکی</legend>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ValidationGroup="1" DisplayMode="List" HeaderText="لطفا به خطاهای زیر توجه فرمائید :" />
    <table style="width:100%;">
        <tr>
            <td>&nbsp;
                <span style="color: #FF0000">*</span><asp:Label ID="Label2" runat="server" Text="نام و نام خانوادگی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
            <td><span style="color: #FF0000">*</span>
                <asp:Label ID="Label1" runat="server" Text="پست الکترونیکی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" style="direction: ltr"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
                <asp:Label ID="Label3" runat="server" Text="شماره تماس | ثابت :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" style="direction: ltr"></asp:TextBox>
            </td>
            <td><span style="color: #FF0000">*</span>
                <asp:Label ID="Label4" runat="server" Text="شماره تماس | همراه :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" style="direction: ltr"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;
                <asp:Label ID="Label5" runat="server" Text="استان مورد نظر :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList6" runat="server" 
                    DataSourceID="SqlDataSource6" DataTextField="ProvinceName" 
                    DataValueField="ProvinceName" CssClass="aspcontrols">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                    SelectCommand="SELECT [ProvinceName] FROM [ProvinceTbl]">
                </asp:SqlDataSource>
            </td>
            <td><span style="color: #FF0000">*</span>
                <asp:Label ID="Label6" runat="server" Text="شهرستان مورد نظر :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;<span style="color: #FF0000">*</span>
                <asp:Label ID="Label7" runat="server" Text="شهر مورد نظر :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            </td>
            <td><span style="color: #FF0000">*</span>
                <asp:Label ID="Label8" runat="server" Text="منطقه مورد نظر :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;
                <asp:Label ID="Label9" runat="server" Text="نوع ملک :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="BuildingName" 
                    DataValueField="BuildingName" CssClass="aspcontrols">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                    SelectCommand="SELECT [BuildingName] FROM [BuildingTypes]">
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="نوع سند :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" 
                    DataSourceID="SqlDataSource2" DataTextField="DocName" DataValueField="DocName" CssClass="aspcontrols">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                    SelectCommand="SELECT [DocName] FROM [DocTypes]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
                <asp:Label ID="Label11" runat="server" Text="نوع معامله :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList3" runat="server" 
                    DataSourceID="SqlDataSource3" DataTextField="TransactionName" 
                    DataValueField="TransactionName" CssClass="aspcontrols">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                    SelectCommand="SELECT [TransactionName] FROM [TransactionTypes]">
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:Label ID="Label12" runat="server" Text="محدوده متراژ :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList4" runat="server" 
                    DataSourceID="SqlDataSource4" DataTextField="Area" DataValueField="Area" CssClass="aspcontrols">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                    SelectCommand="SELECT [Area] FROM [AreaRange]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
                <asp:Label ID="Label13" runat="server" Text="حدود قیمت کل :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList5" runat="server" 
                    DataSourceID="SqlDataSource5" DataTextField="TotPrice" 
                    DataValueField="TotPrice" CssClass="aspcontrols">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
                    SelectCommand="SELECT [TotPrice] FROM [TotPriceRange]"></asp:SqlDataSource>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table>
        <tr>
            <td>&nbsp;
                <asp:Label ID="Label14" runat="server" Text="توضیحات :"></asp:Label>&nbsp;
            </td>
            <td>
                <asp:TextBox ID="TextBox9" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>&nbsp;<span style="color: #FF0000">*</span>
                <asp:Label ID="Label15" runat="server" Text="تصویر ضد بات :"></asp:Label>
            </td>
            <td>
                <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true"  />&nbsp;<asp:ImageButton ID="CAPTCHARefresh" runat="server" ImageUrl="~/Images/Button-Refresh32.png" OnClick="CAPTCHARefresh_Click"/><br /><br />
                <asp:TextBox runat="server" ID="txtAntiBotImage" ValidationGroup="1" MaxLength="4" 
                    CssClass="textbox" Width="75px" style="direction: ltr"></asp:TextBox>
                </td>
        </tr>
    </table>
                <asp:Button ID="submitBtn" runat="server" Text="ارسال فرم" ValidationGroup="1" 
                    onclick="submitBtn_Click" style=" float:left"/>
    </fieldset>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="وارد کردن نام و نام خانوادگی ضروری است ." ControlToValidate="TextBox2" ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="وارد کردن نشانی پست الکترونیکی ضروری است ." ControlToValidate="TextBox1" ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="وارد کردن شماره تلفن همراه ضروری است ." ControlToValidate="TextBox4" ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="وارد کردن نام شهرستان ضروری است ." ControlToValidate="TextBox6" ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="وارد کردن نام شهر ضروری است ." ControlToValidate="TextBox7" ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="وارد کردن نام منطقه مورد نظر ضروری است ." ControlToValidate="TextBox8" ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="وارد کردن کد ضد بات ضروری است ." ControlToValidate="txtAntiBotImage" ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBox3" FilterType="Numbers">
    </asp:FilteredTextBoxExtender>
    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TextBox4" FilterType="Numbers">
    </asp:FilteredTextBoxExtender>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="نشانی پست الکترونیکی نادرست است ." ValidationGroup="1" Display="None" ControlToValidate="TextBox1" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"></asp:RegularExpressionValidator>
</asp:Content>
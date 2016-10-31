﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageSlider.aspx.cs" Inherits="Administration_ImageSlider" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<fieldset>
<legend>فهرست تصاویر</legend>
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateDeleteButton="true"  AutoGenerateColumns="False" AllowPaging="True"  GridLines="None" ShowHeader="false" OnRowDeleting="GridView1_OnRowDeleting" DataKeyNames="id, url, target, alt, imageURL, comments" OnDataBinding="GridView1_OnDataBinding">
                <Columns>
        <asp:TemplateField> 
        <ItemTemplate>
        <div style=" background-color:#F3CE5A; margin:10px; padding:3px; border:1px solid #C49527; border-radius:5px; font-size:13px; font-weight:bold">
        <table>
        <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="عنوان : "></asp:Label> <asp:Label ID="Label5" runat="server" Text='<%# Bind("alt") %>'></asp:Label>
        </td>
        </tr>
        </table>
        <table>
        <tr>
        <td>
            <asp:Label ID="Label7" runat="server" Text="پیوند تصویر : "></asp:Label> <asp:Label ID="Label8" runat="server" Text='<%# Bind("url") %>'></asp:Label>
        </td>
        </tr>
        </table>
        <table>
        <tr>
        <td>
            <asp:Label ID="Label9" runat="server" Text="آدرس تصویر : "></asp:Label> <asp:Label ID="Label10" runat="server" Text='<%# Bind("imageURL") %>'></asp:Label>
        </td>
        </tr>
        </table>
        <table>
        <tr>
        <td>
            <asp:Label ID="Label11" runat="server" Text="توضیحات : "></asp:Label> <asp:Label ID="Label12" runat="server" Text='<%# Bind("comments") %>'></asp:Label>
        </td>
        </tr>
        </table>
        </div>
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>

        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>

</fieldset>
<br /><br />
<fieldset>
<legend>درج تصویر جدید</legend>
نکته اینکه تصویر مورد نظر حتما باید با عرض 250 و با ارتفاع 285 در واحد پیکسل باشد ، لطفا پیش از بالاگذاری تصویر مورد نظر اندازه آن را بررسی نمائید . | برای پیوند تصویر میتوانید از یک پیوند داخلی وبسایت و یا یک پیوند خارجی از وبسایتی دیگر انتخاب نمائید ، درصورتیکه از صفحات داخلی سایت برای صفحه مربوط به توضیحات تصویر استفاده می کنید پیش از درج تصویر باید صفحه و پیوند آنرا آماده کنید .
    <table style="width: 100%; height: 461px;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="عنوان : "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" ValidationGroup="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا عنوان تصویر را انتخاب کنید !" ControlToValidate="TextBox1" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="انتخاب تصویر : "></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="پیوند : "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" ValidationGroup="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="لطفا پیوند تصویر را وارد کنید !" ControlToValidate="TextBox2" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="توضیحات : "></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
        <td>
            <asp:Button ID="Button1" runat="server" Text="درج شود" 
                onclick="Button1_Click" ValidationGroup="1" />
        </td>
        <td></td>
        </tr>
    </table>
</fieldset>
</asp:Content>
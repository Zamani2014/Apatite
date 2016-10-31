<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LatestNewsManagement.aspx.cs" Inherits="Administration_LatestNewsManagement" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<fieldset>
<legend>فهرست آخرین خبرها</legend>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="ID" DataSourceID="SqlDataSource1" ForeColor="#333333" 
        GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ButtonType="Button" CancelText="لغو" DeleteText="حذف" 
                EditText="ویرایش" InsertText="درج" NewText="جدید" SelectText="انتخاب" 
                ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" 
                UpdateText="بروزرسانی" />
            <asp:BoundField DataField="ID" HeaderText="شناسه خبر" ReadOnly="True" 
                SortExpression="ID" />
            <asp:BoundField DataField="Subject" HeaderText="موضوع خبر" 
                SortExpression="Subject" />
            <asp:BoundField DataField="News" HeaderText="متن خبر" SortExpression="News" />
            <asp:BoundField DataField="DateTime" HeaderText="تاریخ و زمان" 
                SortExpression="DateTime" />
        </Columns>
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
        DeleteCommand="DELETE FROM [NewsTbl] WHERE [ID] = @original_ID AND [Subject] = @original_Subject AND [News] = @original_News AND [DateTime] = @original_DateTime" 
        InsertCommand="INSERT INTO [NewsTbl] ([ID], [Subject], [News], [DateTime]) VALUES (@ID, @Subject, @News, @DateTime)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [NewsTbl]" 
        UpdateCommand="UPDATE [NewsTbl] SET [Subject] = @Subject, [News] = @News, [DateTime] = @DateTime WHERE [ID] = @original_ID AND [Subject] = @original_Subject AND [News] = @original_News AND [DateTime] = @original_DateTime">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Subject" Type="String" />
            <asp:Parameter Name="original_News" Type="String" />
            <asp:Parameter Name="original_DateTime" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Subject" Type="String" />
            <asp:Parameter Name="News" Type="String" />
            <asp:Parameter Name="DateTime" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Subject" Type="String" />
            <asp:Parameter Name="News" Type="String" />
            <asp:Parameter Name="DateTime" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Subject" Type="String" />
            <asp:Parameter Name="original_News" Type="String" />
            <asp:Parameter Name="original_DateTime" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</fieldset>
<br /><br />
<fieldset>
<legend>افزودن خبر جدید</legend>
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="عنوان : "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="متن خبر : "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="افزودن" onclick="Button1_Click" />
                            </td>
        </tr>
    </table>
</fieldset>
</asp:Content>
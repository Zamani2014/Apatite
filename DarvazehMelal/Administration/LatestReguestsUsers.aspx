<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LatestReguestsUsers.aspx.cs" Inherits="Administration_LatestReguestsUsers" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<fieldset>
<legend>آخرین کاربران تقاضاهای ملکی</legend>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="ID" DataSourceID="SqlDataSource1" ForeColor="#333333" 
        GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="کد تقاضا" ReadOnly="True" 
                SortExpression="ID" />
            <asp:BoundField DataField="CompName" HeaderText="نام رایانه" 
                SortExpression="CompName" />
            <asp:BoundField DataField="CompIP" HeaderText="نشانی اینترنتی" 
                SortExpression="CompIP" />
            <asp:BoundField DataField="CompDate" HeaderText="تاریخ رایانه" 
                SortExpression="CompDate" />
            <asp:BoundField DataField="CompTime" HeaderText="زمان رایانه" 
                SortExpression="CompTime" />
            <asp:BoundField DataField="CompBrowser" HeaderText="مرورگر کاربر" 
                SortExpression="CompBrowser" />
            <asp:BoundField DataField="Referer" HeaderText="ارجاع دهنده" 
                SortExpression="Referer" />
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
        DeleteCommand="DELETE FROM [RequestTblUsers] WHERE [ID] = @original_ID AND [CompName] = @original_CompName AND [CompIP] = @original_CompIP AND [CompDate] = @original_CompDate AND [CompTime] = @original_CompTime AND [CompBrowser] = @original_CompBrowser AND (([Referer] = @original_Referer) OR ([Referer] IS NULL AND @original_Referer IS NULL))" 
        InsertCommand="INSERT INTO [RequestTblUsers] ([ID], [CompName], [CompIP], [CompDate], [CompTime], [CompBrowser], [Referer]) VALUES (@ID, @CompName, @CompIP, @CompDate, @CompTime, @CompBrowser, @Referer)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [RequestTblUsers]" 
        UpdateCommand="UPDATE [RequestTblUsers] SET [CompName] = @CompName, [CompIP] = @CompIP, [CompDate] = @CompDate, [CompTime] = @CompTime, [CompBrowser] = @CompBrowser, [Referer] = @Referer WHERE [ID] = @original_ID AND [CompName] = @original_CompName AND [CompIP] = @original_CompIP AND [CompDate] = @original_CompDate AND [CompTime] = @original_CompTime AND [CompBrowser] = @original_CompBrowser AND (([Referer] = @original_Referer) OR ([Referer] IS NULL AND @original_Referer IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_CompName" Type="String" />
            <asp:Parameter Name="original_CompIP" Type="String" />
            <asp:Parameter Name="original_CompDate" Type="String" />
            <asp:Parameter Name="original_CompTime" Type="String" />
            <asp:Parameter Name="original_CompBrowser" Type="String" />
            <asp:Parameter Name="original_Referer" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="CompName" Type="String" />
            <asp:Parameter Name="CompIP" Type="String" />
            <asp:Parameter Name="CompDate" Type="String" />
            <asp:Parameter Name="CompTime" Type="String" />
            <asp:Parameter Name="CompBrowser" Type="String" />
            <asp:Parameter Name="Referer" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CompName" Type="String" />
            <asp:Parameter Name="CompIP" Type="String" />
            <asp:Parameter Name="CompDate" Type="String" />
            <asp:Parameter Name="CompTime" Type="String" />
            <asp:Parameter Name="CompBrowser" Type="String" />
            <asp:Parameter Name="Referer" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_CompName" Type="String" />
            <asp:Parameter Name="original_CompIP" Type="String" />
            <asp:Parameter Name="original_CompDate" Type="String" />
            <asp:Parameter Name="original_CompTime" Type="String" />
            <asp:Parameter Name="original_CompBrowser" Type="String" />
            <asp:Parameter Name="original_Referer" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</fieldset>
</asp:Content>
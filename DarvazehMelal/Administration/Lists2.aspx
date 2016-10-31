<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lists2.aspx.cs" Inherits="Administration_Lists2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <fieldset>
<legend>فهرست تقاضاهای ملکی</legend>
    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="ID" DataSourceID="SqlDataSource2" ForeColor="#333333" 
        GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="شناسه مالک" ReadOnly="True" 
                SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="نام" SortExpression="Name" />
            <asp:BoundField DataField="EMail" HeaderText="پست الکترونیکی" 
                SortExpression="EMail" />
            <asp:BoundField DataField="Tel" HeaderText="تلفن ثابت" SortExpression="Tel" />
            <asp:BoundField DataField="Mobile" HeaderText="تلفن همراه" 
                SortExpression="Mobile" />
            <asp:BoundField DataField="Province" HeaderText="استان" 
                SortExpression="Province" />
            <asp:BoundField DataField="State" HeaderText="شهرستان" SortExpression="State" />
            <asp:BoundField DataField="City" HeaderText="شهر" SortExpression="City" />
            <asp:BoundField DataField="Region" HeaderText="منظقه" 
                SortExpression="Region" />
            <asp:BoundField DataField="BuildingType" HeaderText="نوع ساختمان" 
                SortExpression="BuildingType" />
            <asp:BoundField DataField="DocType" HeaderText="نوع سند" 
                SortExpression="DocType" />
            <asp:BoundField DataField="TransactionType" HeaderText="نوع معامله" 
                SortExpression="TransactionType" />
            <asp:BoundField DataField="Range" HeaderText="متراژ" SortExpression="Range" />
            <asp:BoundField DataField="Price" HeaderText="قیمت" SortExpression="Price" />
            <asp:BoundField DataField="Comments" HeaderText="توضیحات" 
                SortExpression="Comments" />
            <asp:BoundField DataField="DateTime" HeaderText="زمان ثبت در سیستم" 
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
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
        DeleteCommand="DELETE FROM [RequestTbl] WHERE [ID] = @original_ID AND [Name] = @original_Name AND [EMail] = @original_EMail AND (([Tel] = @original_Tel) OR ([Tel] IS NULL AND @original_Tel IS NULL)) AND [Mobile] = @original_Mobile AND [Province] = @original_Province AND [State] = @original_State AND [City] = @original_City AND [Region] = @original_Region AND [BuildingType] = @original_BuildingType AND [DocType] = @original_DocType AND [TransactionType] = @original_TransactionType AND [Range] = @original_Range AND [Price] = @original_Price AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL)) AND [DateTime] = @original_DateTime" 
        InsertCommand="INSERT INTO [RequestTbl] ([ID], [Name], [EMail], [Tel], [Mobile], [Province], [State], [City], [Region], [BuildingType], [DocType], [TransactionType], [Range], [Price], [Comments], [DateTime]) VALUES (@ID, @Name, @EMail, @Tel, @Mobile, @Province, @State, @City, @Region, @BuildingType, @DocType, @TransactionType, @Range, @Price, @Comments, @DateTime)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [RequestTbl]" 
        UpdateCommand="UPDATE [RequestTbl] SET [Name] = @Name, [EMail] = @EMail, [Tel] = @Tel, [Mobile] = @Mobile, [Province] = @Province, [State] = @State, [City] = @City, [Region] = @Region, [BuildingType] = @BuildingType, [DocType] = @DocType, [TransactionType] = @TransactionType, [Range] = @Range, [Price] = @Price, [Comments] = @Comments, [DateTime] = @DateTime WHERE [ID] = @original_ID AND [Name] = @original_Name AND [EMail] = @original_EMail AND (([Tel] = @original_Tel) OR ([Tel] IS NULL AND @original_Tel IS NULL)) AND [Mobile] = @original_Mobile AND [Province] = @original_Province AND [State] = @original_State AND [City] = @original_City AND [Region] = @original_Region AND [BuildingType] = @original_BuildingType AND [DocType] = @original_DocType AND [TransactionType] = @original_TransactionType AND [Range] = @original_Range AND [Price] = @original_Price AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL)) AND [DateTime] = @original_DateTime">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Name" Type="String" />
            <asp:Parameter Name="original_EMail" Type="String" />
            <asp:Parameter Name="original_Tel" Type="String" />
            <asp:Parameter Name="original_Mobile" Type="String" />
            <asp:Parameter Name="original_Province" Type="String" />
            <asp:Parameter Name="original_State" Type="String" />
            <asp:Parameter Name="original_City" Type="String" />
            <asp:Parameter Name="original_Region" Type="String" />
            <asp:Parameter Name="original_BuildingType" Type="String" />
            <asp:Parameter Name="original_DocType" Type="String" />
            <asp:Parameter Name="original_TransactionType" Type="String" />
            <asp:Parameter Name="original_Range" Type="String" />
            <asp:Parameter Name="original_Price" Type="String" />
            <asp:Parameter Name="original_Comments" Type="String" />
            <asp:Parameter Name="original_DateTime" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="EMail" Type="String" />
            <asp:Parameter Name="Tel" Type="String" />
            <asp:Parameter Name="Mobile" Type="String" />
            <asp:Parameter Name="Province" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="Region" Type="String" />
            <asp:Parameter Name="BuildingType" Type="String" />
            <asp:Parameter Name="DocType" Type="String" />
            <asp:Parameter Name="TransactionType" Type="String" />
            <asp:Parameter Name="Range" Type="String" />
            <asp:Parameter Name="Price" Type="String" />
            <asp:Parameter Name="Comments" Type="String" />
            <asp:Parameter Name="DateTime" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="EMail" Type="String" />
            <asp:Parameter Name="Tel" Type="String" />
            <asp:Parameter Name="Mobile" Type="String" />
            <asp:Parameter Name="Province" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="Region" Type="String" />
            <asp:Parameter Name="BuildingType" Type="String" />
            <asp:Parameter Name="DocType" Type="String" />
            <asp:Parameter Name="TransactionType" Type="String" />
            <asp:Parameter Name="Range" Type="String" />
            <asp:Parameter Name="Price" Type="String" />
            <asp:Parameter Name="Comments" Type="String" />
            <asp:Parameter Name="DateTime" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Name" Type="String" />
            <asp:Parameter Name="original_EMail" Type="String" />
            <asp:Parameter Name="original_Tel" Type="String" />
            <asp:Parameter Name="original_Mobile" Type="String" />
            <asp:Parameter Name="original_Province" Type="String" />
            <asp:Parameter Name="original_State" Type="String" />
            <asp:Parameter Name="original_City" Type="String" />
            <asp:Parameter Name="original_Region" Type="String" />
            <asp:Parameter Name="original_BuildingType" Type="String" />
            <asp:Parameter Name="original_DocType" Type="String" />
            <asp:Parameter Name="original_TransactionType" Type="String" />
            <asp:Parameter Name="original_Range" Type="String" />
            <asp:Parameter Name="original_Price" Type="String" />
            <asp:Parameter Name="original_Comments" Type="String" />
            <asp:Parameter Name="original_DateTime" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</fieldset>
    </div>
    </form>
</body>
</html>

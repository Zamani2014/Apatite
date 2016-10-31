<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lists.aspx.cs" Inherits="Administration_Lists" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <fieldset>
<legend>فهرست سپرده های ملکی</legend>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="ID" DataSourceID="SqlDataSource1" ForeColor="#333333" 
        GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="شناسه ملک" ReadOnly="True" 
                SortExpression="ID" />
            <asp:BoundField DataField="OwnerID" HeaderText="شناسه مالک" 
                SortExpression="OwnerID" />
            <asp:BoundField DataField="Range" HeaderText="متراژ" SortExpression="Range" />
            <asp:BoundField DataField="Area" HeaderText="مساحت" 
                SortExpression="Area" />
            <asp:BoundField DataField="RoomNo" HeaderText="تعداد اتاق" 
                SortExpression="RoomNo" />
            <asp:BoundField DataField="ClassNo" HeaderText="شماره طبقه" 
                SortExpression="ClassNo" />
            <asp:BoundField DataField="ClassTot" HeaderText="تعداد طبقات" 
                SortExpression="ClassTot" />
            <asp:BoundField DataField="UnitsInClass" HeaderText="تعداد واحد در طبقه" 
                SortExpression="UnitsInClass" />
            <asp:BoundField DataField="UnitsTot" HeaderText="تعداد کل واحدها" 
                SortExpression="UnitsTot" />
            <asp:BoundField DataField="BuildingView" HeaderText="نمای ساختمان" 
                SortExpression="BuildingView" />
            <asp:BoundField DataField="ResidentType" HeaderText="وضعیت سکونت" 
                SortExpression="ResidentType" />
            <asp:BoundField DataField="YoursOld" HeaderText="وضعیت ساختمان" 
                SortExpression="YoursOld" />
            <asp:BoundField DataField="Old" HeaderText="سن بنا" SortExpression="Old" />
            <asp:BoundField DataField="GeoPosition" HeaderText="موقعیت جغرافیایی" 
                SortExpression="GeoPosition" />
            <asp:BoundField DataField="Comments" HeaderText="توضیحات" 
                SortExpression="Comments" />
            <asp:BoundField DataField="Cabinet" HeaderText="کابینت" 
                SortExpression="Cabinet" />
            <asp:BoundField DataField="Sanitary" HeaderText="سرویس بهداشتی" 
                SortExpression="Sanitary" />
            <asp:BoundField DataField="Floor" HeaderText="کف پوش" SortExpression="Floor" />
            <asp:BoundField DataField="Price" HeaderText="قیمت متری" 
                SortExpression="Price" />
            <asp:BoundField DataField="TotPrice" HeaderText="قیمت کل" 
                SortExpression="TotPrice" />
            <asp:BoundField DataField="Currency" HeaderText="واحد پول" 
                SortExpression="Currency" />
            <asp:BoundField DataField="Parking" HeaderText="تعداد پارکینگ" 
                SortExpression="Parking" />
            <asp:BoundField DataField="TelsNo" HeaderText="تعداد تلفن" 
                SortExpression="TelsNo" />
            <asp:BoundField DataField="Other" HeaderText="دیگر امکانات" 
                SortExpression="Other" />
            <asp:BoundField DataField="Facilities" HeaderText="امکانات ساختمان" 
                SortExpression="Facilities" />
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:DarvazehConnectionString %>" 
        DeleteCommand="DELETE FROM [BuildingTbl] WHERE [ID] = @original_ID AND [OwnerID] = @original_OwnerID AND [Range] = @original_Range AND [Area] = @original_Area AND [RoomNo] = @original_RoomNo AND [ClassNo] = @original_ClassNo AND [ClassTot] = @original_ClassTot AND [UnitsInClass] = @original_UnitsInClass AND (([UnitsTot] = @original_UnitsTot) OR ([UnitsTot] IS NULL AND @original_UnitsTot IS NULL)) AND (([BuildingView] = @original_BuildingView) OR ([BuildingView] IS NULL AND @original_BuildingView IS NULL)) AND (([ResidentType] = @original_ResidentType) OR ([ResidentType] IS NULL AND @original_ResidentType IS NULL)) AND [YoursOld] = @original_YoursOld AND (([Old] = @original_Old) OR ([Old] IS NULL AND @original_Old IS NULL)) AND (([GeoPosition] = @original_GeoPosition) OR ([GeoPosition] IS NULL AND @original_GeoPosition IS NULL)) AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL)) AND (([Cabinet] = @original_Cabinet) OR ([Cabinet] IS NULL AND @original_Cabinet IS NULL)) AND (([Sanitary] = @original_Sanitary) OR ([Sanitary] IS NULL AND @original_Sanitary IS NULL)) AND (([Floor] = @original_Floor) OR ([Floor] IS NULL AND @original_Floor IS NULL)) AND [Price] = @original_Price AND [TotPrice] = @original_TotPrice AND (([Currency] = @original_Currency) OR ([Currency] IS NULL AND @original_Currency IS NULL)) AND (([Parking] = @original_Parking) OR ([Parking] IS NULL AND @original_Parking IS NULL)) AND (([TelsNo] = @original_TelsNo) OR ([TelsNo] IS NULL AND @original_TelsNo IS NULL)) AND (([Other] = @original_Other) OR ([Other] IS NULL AND @original_Other IS NULL)) AND (([Facilities] = @original_Facilities) OR ([Facilities] IS NULL AND @original_Facilities IS NULL)) AND [DateTime] = @original_DateTime" 
        InsertCommand="INSERT INTO [BuildingTbl] ([ID], [OwnerID], [Range], [Area], [RoomNo], [ClassNo], [ClassTot], [UnitsInClass], [UnitsTot], [BuildingView], [ResidentType], [YoursOld], [Old], [GeoPosition], [Comments], [Cabinet], [Sanitary], [Floor], [Price], [TotPrice], [Currency], [Parking], [TelsNo], [Other], [Facilities], [DateTime]) VALUES (@ID, @OwnerID, @Range, @Area, @RoomNo, @ClassNo, @ClassTot, @UnitsInClass, @UnitsTot, @BuildingView, @ResidentType, @YoursOld, @Old, @GeoPosition, @Comments, @Cabinet, @Sanitary, @Floor, @Price, @TotPrice, @Currency, @Parking, @TelsNo, @Other, @Facilities, @DateTime)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [BuildingTbl]" 
        UpdateCommand="UPDATE [BuildingTbl] SET [OwnerID] = @OwnerID, [Range] = @Range, [Area] = @Area, [RoomNo] = @RoomNo, [ClassNo] = @ClassNo, [ClassTot] = @ClassTot, [UnitsInClass] = @UnitsInClass, [UnitsTot] = @UnitsTot, [BuildingView] = @BuildingView, [ResidentType] = @ResidentType, [YoursOld] = @YoursOld, [Old] = @Old, [GeoPosition] = @GeoPosition, [Comments] = @Comments, [Cabinet] = @Cabinet, [Sanitary] = @Sanitary, [Floor] = @Floor, [Price] = @Price, [TotPrice] = @TotPrice, [Currency] = @Currency, [Parking] = @Parking, [TelsNo] = @TelsNo, [Other] = @Other, [Facilities] = @Facilities, [DateTime] = @DateTime WHERE [ID] = @original_ID AND [OwnerID] = @original_OwnerID AND [Range] = @original_Range AND [Area] = @original_Area AND [RoomNo] = @original_RoomNo AND [ClassNo] = @original_ClassNo AND [ClassTot] = @original_ClassTot AND [UnitsInClass] = @original_UnitsInClass AND (([UnitsTot] = @original_UnitsTot) OR ([UnitsTot] IS NULL AND @original_UnitsTot IS NULL)) AND (([BuildingView] = @original_BuildingView) OR ([BuildingView] IS NULL AND @original_BuildingView IS NULL)) AND (([ResidentType] = @original_ResidentType) OR ([ResidentType] IS NULL AND @original_ResidentType IS NULL)) AND [YoursOld] = @original_YoursOld AND (([Old] = @original_Old) OR ([Old] IS NULL AND @original_Old IS NULL)) AND (([GeoPosition] = @original_GeoPosition) OR ([GeoPosition] IS NULL AND @original_GeoPosition IS NULL)) AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL)) AND (([Cabinet] = @original_Cabinet) OR ([Cabinet] IS NULL AND @original_Cabinet IS NULL)) AND (([Sanitary] = @original_Sanitary) OR ([Sanitary] IS NULL AND @original_Sanitary IS NULL)) AND (([Floor] = @original_Floor) OR ([Floor] IS NULL AND @original_Floor IS NULL)) AND [Price] = @original_Price AND [TotPrice] = @original_TotPrice AND (([Currency] = @original_Currency) OR ([Currency] IS NULL AND @original_Currency IS NULL)) AND (([Parking] = @original_Parking) OR ([Parking] IS NULL AND @original_Parking IS NULL)) AND (([TelsNo] = @original_TelsNo) OR ([TelsNo] IS NULL AND @original_TelsNo IS NULL)) AND (([Other] = @original_Other) OR ([Other] IS NULL AND @original_Other IS NULL)) AND (([Facilities] = @original_Facilities) OR ([Facilities] IS NULL AND @original_Facilities IS NULL)) AND [DateTime] = @original_DateTime">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_OwnerID" Type="Int32" />
            <asp:Parameter Name="original_Range" Type="String" />
            <asp:Parameter Name="original_Area" Type="String" />
            <asp:Parameter Name="original_RoomNo" Type="Int32" />
            <asp:Parameter Name="original_ClassNo" Type="Int32" />
            <asp:Parameter Name="original_ClassTot" Type="Int32" />
            <asp:Parameter Name="original_UnitsInClass" Type="Int32" />
            <asp:Parameter Name="original_UnitsTot" Type="Int32" />
            <asp:Parameter Name="original_BuildingView" Type="String" />
            <asp:Parameter Name="original_ResidentType" Type="String" />
            <asp:Parameter Name="original_YoursOld" Type="String" />
            <asp:Parameter Name="original_Old" Type="String" />
            <asp:Parameter Name="original_GeoPosition" Type="String" />
            <asp:Parameter Name="original_Comments" Type="String" />
            <asp:Parameter Name="original_Cabinet" Type="String" />
            <asp:Parameter Name="original_Sanitary" Type="String" />
            <asp:Parameter Name="original_Floor" Type="String" />
            <asp:Parameter Name="original_Price" Type="Int32" />
            <asp:Parameter Name="original_TotPrice" Type="Int32" />
            <asp:Parameter Name="original_Currency" Type="String" />
            <asp:Parameter Name="original_Parking" Type="Int32" />
            <asp:Parameter Name="original_TelsNo" Type="Int32" />
            <asp:Parameter Name="original_Other" Type="String" />
            <asp:Parameter Name="original_Facilities" Type="String" />
            <asp:Parameter Name="original_DateTime" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="OwnerID" Type="Int32" />
            <asp:Parameter Name="Range" Type="String" />
            <asp:Parameter Name="Area" Type="String" />
            <asp:Parameter Name="RoomNo" Type="Int32" />
            <asp:Parameter Name="ClassNo" Type="Int32" />
            <asp:Parameter Name="ClassTot" Type="Int32" />
            <asp:Parameter Name="UnitsInClass" Type="Int32" />
            <asp:Parameter Name="UnitsTot" Type="Int32" />
            <asp:Parameter Name="BuildingView" Type="String" />
            <asp:Parameter Name="ResidentType" Type="String" />
            <asp:Parameter Name="YoursOld" Type="String" />
            <asp:Parameter Name="Old" Type="String" />
            <asp:Parameter Name="GeoPosition" Type="String" />
            <asp:Parameter Name="Comments" Type="String" />
            <asp:Parameter Name="Cabinet" Type="String" />
            <asp:Parameter Name="Sanitary" Type="String" />
            <asp:Parameter Name="Floor" Type="String" />
            <asp:Parameter Name="Price" Type="Int32" />
            <asp:Parameter Name="TotPrice" Type="Int32" />
            <asp:Parameter Name="Currency" Type="String" />
            <asp:Parameter Name="Parking" Type="Int32" />
            <asp:Parameter Name="TelsNo" Type="Int32" />
            <asp:Parameter Name="Other" Type="String" />
            <asp:Parameter Name="Facilities" Type="String" />
            <asp:Parameter Name="DateTime" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="OwnerID" Type="Int32" />
            <asp:Parameter Name="Range" Type="String" />
            <asp:Parameter Name="Area" Type="String" />
            <asp:Parameter Name="RoomNo" Type="Int32" />
            <asp:Parameter Name="ClassNo" Type="Int32" />
            <asp:Parameter Name="ClassTot" Type="Int32" />
            <asp:Parameter Name="UnitsInClass" Type="Int32" />
            <asp:Parameter Name="UnitsTot" Type="Int32" />
            <asp:Parameter Name="BuildingView" Type="String" />
            <asp:Parameter Name="ResidentType" Type="String" />
            <asp:Parameter Name="YoursOld" Type="String" />
            <asp:Parameter Name="Old" Type="String" />
            <asp:Parameter Name="GeoPosition" Type="String" />
            <asp:Parameter Name="Comments" Type="String" />
            <asp:Parameter Name="Cabinet" Type="String" />
            <asp:Parameter Name="Sanitary" Type="String" />
            <asp:Parameter Name="Floor" Type="String" />
            <asp:Parameter Name="Price" Type="Int32" />
            <asp:Parameter Name="TotPrice" Type="Int32" />
            <asp:Parameter Name="Currency" Type="String" />
            <asp:Parameter Name="Parking" Type="Int32" />
            <asp:Parameter Name="TelsNo" Type="Int32" />
            <asp:Parameter Name="Other" Type="String" />
            <asp:Parameter Name="Facilities" Type="String" />
            <asp:Parameter Name="DateTime" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_OwnerID" Type="Int32" />
            <asp:Parameter Name="original_Range" Type="String" />
            <asp:Parameter Name="original_Area" Type="String" />
            <asp:Parameter Name="original_RoomNo" Type="Int32" />
            <asp:Parameter Name="original_ClassNo" Type="Int32" />
            <asp:Parameter Name="original_ClassTot" Type="Int32" />
            <asp:Parameter Name="original_UnitsInClass" Type="Int32" />
            <asp:Parameter Name="original_UnitsTot" Type="Int32" />
            <asp:Parameter Name="original_BuildingView" Type="String" />
            <asp:Parameter Name="original_ResidentType" Type="String" />
            <asp:Parameter Name="original_YoursOld" Type="String" />
            <asp:Parameter Name="original_Old" Type="String" />
            <asp:Parameter Name="original_GeoPosition" Type="String" />
            <asp:Parameter Name="original_Comments" Type="String" />
            <asp:Parameter Name="original_Cabinet" Type="String" />
            <asp:Parameter Name="original_Sanitary" Type="String" />
            <asp:Parameter Name="original_Floor" Type="String" />
            <asp:Parameter Name="original_Price" Type="Int32" />
            <asp:Parameter Name="original_TotPrice" Type="Int32" />
            <asp:Parameter Name="original_Currency" Type="String" />
            <asp:Parameter Name="original_Parking" Type="Int32" />
            <asp:Parameter Name="original_TelsNo" Type="Int32" />
            <asp:Parameter Name="original_Other" Type="String" />
            <asp:Parameter Name="original_Facilities" Type="String" />
            <asp:Parameter Name="original_DateTime" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</fieldset>
    </div>
    </form>
</body>
</html>

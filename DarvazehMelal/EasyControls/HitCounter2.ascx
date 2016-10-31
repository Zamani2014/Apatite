<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HitCounter2.ascx.cs" Inherits="EasyControls_HitCounter2" %>
<fieldset>
<legend>آمار و اطلاعات بازدیدکنندگان</legend>
<div>
    <asp:Panel ID="panelAdmin" runat="server" Visible="false">
        <asp:Label ID="lblAdmin" runat="server" Text="<b>آمار و اطلاعات بازدیدکنندگان - حالت مدیر :</b><br/>"/>
        
        <br />
        <asp:CheckBox ID="chkViewSpacer" runat="server" Text="View Spacer" />
        <br />
        <asp:CheckBox ID="chkViewVisitorCount" runat="server" 
            Text="View Visitor Count" />
        <br />
        <asp:CheckBox ID="chkViewHitCount" runat="server" Text="View Hit Count" />
        <br />
        <asp:CheckBox ID="chkViewLastVisit" runat="server" Text="View Last Visit" />
        <br />
        <asp:CheckBox ID="chkViewIP" runat="server" Text="View Client IP" />
        <br />
        <asp:CheckBox ID="chkViewAgent" runat="server" Text="View Client Agent" />
        <br />
        <br />
        <asp:Button ID="btnSaveSettings" runat="server" onclick="btnSaveSettings_Click" 
            Text="اعمال تغییرات" />
        
    </asp:Panel>
    <asp:Panel ID="panelSpacer" runat="server">
        <asp:Label ID="spacer1" runat="server" Text="&nbsp;<br/>"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="panelVisitors" runat="server">            
        <asp:Label ID="lblNumberOfVisitorsLabel" runat="server" Text="تعداد کل بازدیدکنندگان :"></asp:Label>
        <asp:Label ID="lblNumberOfVisitors" runat="server" Text="#"></asp:Label>
        <br />
    </asp:Panel>
    <asp:Panel ID="panelHits" runat="server">            
        <asp:Label ID="lblNumberOfHitsLabel" runat="server" Text="تعداد بازدید این صفحه :"></asp:Label>
        <asp:Label ID="lblNumberOfHits" runat="server" Text="#"></asp:Label>
        <br />
    </asp:Panel>
    <asp:Panel ID="panelLastVisit" runat="server">                
        <asp:Label ID="lblLastVisitorLabel" runat="server" Text="آخرین بازدید :"></asp:Label>
        <asp:Label ID="lblLastVisit" runat="server" Text="date & time"></asp:Label>    
        <br />   
    </asp:Panel>
    <asp:Panel ID="panelYourIP" runat="server">            
        <asp:Label ID="lblYourIPLabel" runat="server" Text="نشانی اینترنتی شما برابر است با :"></asp:Label>
        <asp:Label ID="lblYourIP" runat="server" Text="#"></asp:Label>
        <br />
    </asp:Panel>
    <asp:Panel ID="panelAgent" runat="server" Visible="false">            
        <asp:Label ID="lblAgentLabel" runat="server" Text="مرورگر کاربران : "></asp:Label>
        <asp:Label ID="lblAgent" runat="server" Text="#"></asp:Label>
        <br />
    </asp:Panel>
 </div>

</fieldset>
 


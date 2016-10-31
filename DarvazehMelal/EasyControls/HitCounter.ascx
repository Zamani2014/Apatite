<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HitCounter.ascx.cs" Inherits="EasyControls_HitCounter" %>
<fieldset>
<legend>آمار بازدید کنندگان</legend>
<div>
<asp:Label ID="lblNumberOfHitsLabel" runat="server" Text="تعداد بازدید :"></asp:Label>
<asp:Label ID="lblNumberOfHitsCounter" runat="server" Text="#"></asp:Label>
    <br />
    <asp:Label ID="lblLastVisitorLabel" runat="server" Text="آخرین بازدید :"></asp:Label>
    <asp:Label ID="lblLastVisit" runat="server" Text="تاریخ و زمان"></asp:Label></div>

</fieldset>

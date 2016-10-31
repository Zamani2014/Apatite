<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuildingList.aspx.cs" Inherits="Administration_BuildingList"  MasterPageFile="~/Site.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<fieldset>
<legend>فهرست سپرده های ملکی</legend>
<iframe src="Lists.aspx" height="600px" width="100%" runat="server" scrolling="yes" >
</iframe>
</fieldset>
<br />
<br />
<fieldset>
<legend>فهرست تقاضاهای ملکی</legend>
<iframe height="600px" scrolling="yes" width="100%" runat="server" src="Lists2.aspx">
</iframe>
</fieldset>
</asp:Content>
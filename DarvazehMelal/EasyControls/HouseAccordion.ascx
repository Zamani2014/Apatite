<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HouseAccordion.ascx.cs" Inherits="EasyControls_HouseAccordion" %>
<%@ Register Assembly="DNA.UI.JQuery" Namespace="DNA.UI.JQuery" TagPrefix="DotNetAge" %>
<%@ Register src="~/EasyControls/SlideShow.ascx" tagname="SlideShow" tagprefix="uc1" %>
<%@ Register TagPrefix="Scrolling" TagName="News" src="~/EasyControls/Scrolling_News.ascx" %>
<%@ Register TagPrefix="ScrollingRequests" TagName="Requests" src="~/EasyControls/ScrollingRequests.ascx" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<style type="text/css">
#SlideShow 
{
    float:left; margin-top:3px; margin-bottom:3px; margin-left:3px;
}
#Accordion
{
}

.fieldset1
{
    margin-top:12px;
           background-color: #D09046;
}
</style>
<div id="Accordion">
<DotNetAge:Accordion ID="Accordion1" runat="server" Width="325px" AutoSizeMode="None" Height="290" style="float:right; margin-top:10px; margin-bottom:10px;">
<Views>
            <DotNetAge:View ID="View3" runat="server" Text="آخرین سپرده های ملکی">
                <table width="100%">
                <tr>
                    <td><Scrolling:News id="scroller" runat="server"></Scrolling:News></td>
                </tr>
                </table>            
                </DotNetAge:View>
            <DotNetAge:View ID="View5" runat="server" Text="آخرین تقاضاهای ملکی">
                <table width="100%">
                <tr>
                    <td><ScrollingRequests:Requests id="Requests1" runat="server"></ScrollingRequests:Requests></td>
                </tr>
                </table>            
            </DotNetAge:View>
        </Views>
</DotNetAge:Accordion>
</div>
<fieldset class="fieldset1">
<div id="SlideShow">
<uc1:SlideShow ID="SlideShow7" runat="server" XPath="site500x281"  Width="450" Height="285"
            ShowNavigation="true"  
             XMLSource="~/xml/500x281.xml"/>
</div>
</fieldset>
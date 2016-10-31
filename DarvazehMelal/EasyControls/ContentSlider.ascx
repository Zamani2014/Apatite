<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentSlider.ascx.cs" Inherits="EasyControls_ContentSlider" %>
<%@ Register src="~/EasyControls/SlideShow.ascx" tagname="SlideShow" tagprefix="uc1" %>
<style>
.fieldset3
{
      clear:both;
           background-color: #D09046;
           right: 0px; 
}
#SlideShow
{
    margin-top:3px; margin-bottom:3px; margin-left:3px; margin-right:3px; float:right
}

</style>
<fieldset class="fieldset3">
<div id="SlideShow">
         <uc1:SlideShow ID="SlideShow5" runat="server" BtnNext="~/images/nav-arrow-right.gif" 
        BtnPrevious="~/images/nav-arrow-left.gif" AutoPlay="false" Width="780" Height="100" XMLSource="~/xml/Ads.xml" xPath="ads" />
        </div>
        </fieldset>



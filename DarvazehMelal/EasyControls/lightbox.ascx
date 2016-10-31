<%@ Control Language="C#" AutoEventWireup="true" CodeFile="lightbox.ascx.cs" Inherits="EasyControls_lightbox" %>
<%@ Register Assembly="DNA.UI.JQuery" Namespace="DNA.UI.JQuery" TagPrefix="DotNetAge" %>

    <style type="text/css">
        .gallery { background: #f3f3f3; padding: 5px; width: 450px; }
        .gallery ul { list-style: none; }
        .gallery ul li { display: inline; }
        .gallery ul img { border: 5px solid #3e3e3e; border-width: 5px 5px 20px; }
        .gallery ul a:hover img { border: 5px solid #ffffff; border-width: 5px 5px 20px; color: #ffffff; }
        .gallery ul a:hover { color: #ffffff; }
        #jquery-overlay { position: absolute; top: 0; left: 0; z-index: 90; width: 100%; height: 500px; }
        #jquery-lightbox { position: absolute; top: 0; left: 0; width: 100%; z-index: 100; text-align: center; line-height: 0; }
        #jquery-lightbox a img { border: none; }
        #lightbox-container-image-box { position: relative; background-color: #fff; width: 250px; height: 250px; margin: 0 auto; }
        #lightbox-container-image { padding: 10px; }
        #lightbox-loading { position: absolute; top: 40%; left: 0%; height: 25%; width: 100%; text-align: center; line-height: 0; }
        #lightbox-nav { position: absolute; top: 0; left: 0; height: 100%; width: 100%; z-index: 10; }
        #lightbox-container-image-box > #lightbox-nav { left: 0; }
        #lightbox-nav a { outline: none; }
        #lightbox-nav-btnPrev, #lightbox-nav-btnNext { width: 49%; height: 100%; zoom: 1; display: block; }
        #lightbox-nav-btnPrev { left: 0; float: left; }
        #lightbox-nav-btnNext { right: 0; float: right; }
        #lightbox-container-image-data-box { font: 10px Verdana, Helvetica, sans-serif; background-color: #fff; margin: 0 auto; line-height: 1.4em; overflow: auto; width: 100%; padding: 0 10px 0; }
        #lightbox-container-image-data { padding: 0 10px; color: #666; }
        #lightbox-container-image-data #lightbox-image-details { width: 70%; float: left; text-align: left; }
        #lightbox-image-details-caption { font-weight: bold; }
        #lightbox-image-details-currentNumber { display: block; clear: left; padding-bottom: 1.0em; }
        #lightbox-secNav-btnClose { width: 66px; float: right; padding-bottom: 0.7em; }
    </style>

<fieldset>
       <asp:Panel ID="Panel1" runat="server" CssClass="gallery">
        <DotNetAge:SimpleListView ID="SimpleListView1" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("NavigateUrl") %>'
                    ImageUrl='<%# Eval("ImageUrl") %>'></asp:HyperLink>
            </ItemTemplate>
            <Items>
                <DotNetAge:SimpleListItem ImageUrl="~/Images/photos/thumb_image1.jpg"
                    NavigateUrl="~/Images/photos/image1.jpg" Text="test" />
                <DotNetAge:SimpleListItem ImageUrl="~/Images/photos/thumb_image2.jpg"
                    NavigateUrl="~/Images/photos/image2.jpg" />
                <DotNetAge:SimpleListItem ImageUrl="~/Images/photos/thumb_image3.jpg"
                    NavigateUrl="~/Images/photos/image3.jpg" />
                <DotNetAge:SimpleListItem ImageUrl="~/Images/photos/thumb_image4.jpg"
                    NavigateUrl="~/Images/photos/image4.jpg" />
                <DotNetAge:SimpleListItem ImageUrl="~/Images/photos/thumb_image5.jpg"
                    NavigateUrl="~/Images/photos/image5.jpg" />
            </Items>
        </DotNetAge:SimpleListView>
    </asp:Panel>
    <br />
    <DotNetAge:JQueryPlugin ID="JQueryPlugin2" runat="server" Name="lightBox">
        <Target TargetID="SimpleListView1 a" />
        <PlugInScripts>
            <asp:ScriptReference Assembly="jQuery" Name="jQuery.plugins.lightbox.js" />
        </PlugInScripts>
    </DotNetAge:JQueryPlugin>
</fieldset>
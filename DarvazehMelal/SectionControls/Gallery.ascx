<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Gallery.ascx.cs" Inherits="SectionControls_Gallery" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<%@ Register TagPrefix="cc2" TagName="Upload" Src="~/GalleryUpload.ascx" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td colspan="2" align="right">
                    <!-- robs: Import button -->
	                <asp:Button Visible="false" runat="server" ID="btnImportEntries" OnClick="btnImportEntries_Click" Text="Import" CausesValidation="false"></asp:Button>
                    <asp:Button runat="server" ID="btnNewEntry" OnClick="btnNewEntry_Click" Text="<%$ Resources:stringsRes, glb__add %>" CausesValidation="false"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DataList runat="server" ID="lstGalleryEdit" DataKeyField="Guid" OnItemCommand="lstGalleryEdit_ItemCommand" SkinID="galleryEdit">
                        <ItemTemplate>
                            <table width="100%" border="0">
                                <tr>
                                    <td colspan="2" class="imagecell">
                                        <cc1:ResizedImage runat="server" ID="imgThumbnail" ImageUrl='<%#Eval("Filename", "{0}")%>' SectionId='<%#getSectionId()%>' PageId='<%#getPageId()%>' SkinID="galleryThumbnail" ToolTip='<%#Eval("Comment", "{0}")%>'></cc1:ResizedImage> 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="buttoncell" align="right"><asp:LinkButton ID="Button1" runat="server" CssClass="btnEdit" CommandName="entry_edit" CausesValidation="false" /></td>
                                    <td class="buttoncell" align="left"><asp:LinkButton ID="Button2" runat="server" CssClass="btnDelete" CommandName="entry_delete" CausesValidation="false" /></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    <br />
                    <cc1:Pager runat="server" ID="pagGalleryEdit" ControlToPage="lstGalleryEdit" SkinID="gallery"></cc1:Pager>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="editDetailsView">
        <table width="100%">
            <cc2:Upload runat="server" ID="Upload1" UploadRequired="true" />
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
           <cc2:Upload runat="server" ID="Upload2" UploadRequired="true" />
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
           <cc2:Upload runat="server" ID="Upload3" UploadRequired="true" />
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
           <cc2:Upload runat="server" ID="Upload4" UploadRequired="true" />
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
           <cc2:Upload runat="server" ID="Upload5" UploadRequired="true" />
            <tr>
                <td colspan="2">
                    <asp:Button runat="server" ID="btnCancelDetails" OnClick="btnCancelDetails_Click" Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false"></asp:Button>
                    <asp:Button runat="server" ID="btnSaveDetails" OnClick="btnSaveDetails_Click" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="true"></asp:Button>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
        <asp:MultiView runat="server" ID="mvHtmlSilverlight">
            <asp:View runat="server" ID="vwHtml">
                <div style="padding-top:4px"></div> 
                <asp:DataList runat="server" ID="lstGallery" DataKeyField="Guid" OnItemCommand="lstGallery_ItemCommand" SkinID="gallery">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnDetails" CommandName="ShowDetails" CausesValidation="false">
                            <cc1:ResizedImage runat="server" ID="imgThumbnail" ImageUrl='<%#Eval("Filename", "{0}")%>' SectionId='<%#getSectionId()%>' PageId='<%#getPageId()%>' SkinID="galleryThumbnail"></cc1:ResizedImage>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:DataList>
                <br />
                <cc1:Pager runat="server" ID="pagGallery" ControlToPage="lstGallery" SkinID="gallery"></cc1:Pager>
                <asp:LinkButton runat="server" ID="btnShowSilverlightVersion" Text="<%$Resources:StringsRes, ctl_Gallery_SilverlightVersion %>" OnClick="btnShowSilverlightVersion_Click"></asp:LinkButton>
            </asp:View>
            <asp:View runat="server" ID="vwSilverlight">
				<object type="application/x-silverlight-2" data="data:application/x-silverlight-2," width="100%" height="400">
					<param name="background" value="#000" /> 
					<param name="source" value="<%=ResolveUrl("~/Silverlight/Gallery/Vertigo.SlideShow.xap") %>" /> 
					<param name="initParams" value="ConfigurationProvider=XmlConfigurationProvider;Path=<%= SilverlightConfigurationUrl %>,DataProvider=XmlDataProvider;Path=<%= SilverlightDataUrl %>"/>
					<a href="http://go.microsoft.com/fwlink/?LinkID=124807" style="text-decoration: none;">
						<img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style: none" />
					</a>
				</object>
                <asp:LinkButton runat="server" ID="btnShowHtmlVersion" Text="<%$Resources:StringsRes, ctl_Gallery_HTMLVersion %>" OnClick="btnShowHtmlVersion_Click"></asp:LinkButton>
            </asp:View>
        </asp:MultiView>
    </asp:View>
    <asp:View runat="server" ID="readonlyDetailsView">
    <div style="padding-top:4px"></div>
        <div class="gallerydetails">
            <h1><asp:Literal runat="server" ID="litTitle"></asp:Literal></h1>
            <cc1:ResizedImage runat="server" ID="imgGalleryBigView" SkinID="galleryBigView" />
            <p><asp:Literal runat="server" ID="litComment"></asp:Literal></p>
            <p><asp:HyperLink runat="server" Text="Download" ID="lnkDownload"></asp:HyperLink></p>
        </div>
        <table class="pager" width="100%">
            <tr>
                <td style="width:33%;" align="left"><asp:LinkButton SkinID="Left" runat="server" ID="btnPrevious" CommandName="move" OnCommand="DetailView_Command" CausesValidation="false" /></td>
                <td style="width:33%;" align="center"><asp:LinkButton SkinID="Up" runat="server" ID="btnBackToGallery" CommandName="up" OnCommand="DetailView_Command" CausesValidation="false" /></td>
                <td style="width:33%;" align="right"><asp:LinkButton SkinID="Right" runat="server" ID="btnNext" CommandName="move" OnCommand="DetailView_Command" CausesValidation="false" /></td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>
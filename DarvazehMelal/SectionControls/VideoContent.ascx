<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoContent.ascx.cs" Inherits="SectionControls_VideoContent" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td><asp:Label runat="server" ID="lblKeywords" Text="<%$Resources:StringsRes, ctl_VideoContent_AdminKeywords %>" /></td>
                <td><asp:Textbox runat="server" ID="txtKeywords" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="Label1" Text="<%$Resources:StringsRes, ctl_VideoContent_AdminAutoplay %>" /></td>
                <td><asp:Checkbox runat="server" ID="cbxAutoPlay" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblMuted" Text="<%$Resources:StringsRes, ctl_VideoContent_AdminMuted %>" /></td>
                <td><asp:Checkbox runat="server" ID="cbxMuted" /></td>
            </tr>
             <tr>
                <td><asp:Label runat="server" ID="lblFile" Text="<%$Resources:StringsRes, ctl_VideoContent_AdminUploadedFile %>" /></td>
                <td><asp:Hyperlink runat="server" ID="hlVideoFile" /></td>
            </tr>
               <tr>
                <td><asp:Label runat="server" ID="lblVideoFile" Text="<%$Resources:StringsRes, ctl_VideoContent_AdminNewFile %>" /></td>
                <td><asp:FileUpload runat="server" ID="filData" /></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false" />&nbsp;
                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="false"/>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
		<div id="silverlightControlHost">
			<object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="320" height="240">
				<param name="source" value="<%=ResolveUrl("~/Silverlight/Videoplayer/VideoPlayerM.xap")%>"/>
				<param name="background" value="white" />
				<param name="initParams" value="m=<%=getDownloadLink()%>,autostart=<%=getAutoPlay()%>,muted=<%=getMuted()%>" />
				<param name="minruntimeversion" value="2.0.31005.0" />
				<a href="http://go.microsoft.com/fwlink/?LinkId=124807" style="text-decoration: none;"><img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style: none"/></a>
			</object>
		</div>
    </asp:View>
</asp:MultiView>
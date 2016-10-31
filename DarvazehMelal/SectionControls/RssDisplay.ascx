<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RssDisplay.ascx.cs" Inherits="SectionControls_RssDisplay" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<%@ Register Assembly="RssToolkit" Namespace="RssToolkit.Web.WebControls" TagPrefix="RssToolkit" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td>
					<table border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td><asp:Localize runat="server" Text="<%$Resources:StringsRes, ctl_RssDisplay_AdminFeedUrl %>" /></td>
							<td><asp:TextBox runat="server" ID="txtFeedUrl" /></td>
						</tr>
						<tr>
							<td><asp:Localize ID="Localize1" runat="server" Text="<%$Resources:StringsRes, ctl_RssDisplay_AdminMaxItems %>" /></td>
							<td><asp:TextBox runat="server" ID="txtMaxItems" Width="30" /></td>
						</tr>
					</table>
                </td>
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
		<RssToolkit:RssDataSource runat="server" ID="rssDataSource"/>
		<asp:DataList runat="server" ID="lstRssContent" DataSourceID="rssDataSource" SkinID="RssDisplay">
			<ItemTemplate>
				<h2><a href="<%#Eval("link") %>" target="_blank"><%#Eval("title") %></a></h2>
				<p><asp:Literal runat="server" ID="litDescription" Text='<%#Eval("description") %>' /></p>
			</ItemTemplate>
		</asp:DataList>
    </asp:View>
    <asp:View runat="server" ID="mediumTrustErrorView">
		<div style="border: solid 1px Red; padding: 10px;">
			<asp:Localize runat="server" Text="<%$Resources:StringsRes, glb__mediumTrustError %>" />
		</div>
	</asp:View>
</asp:MultiView>
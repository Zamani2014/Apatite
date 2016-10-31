<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TwitterStatusFeed.ascx.cs" Inherits="SectionControls_TwitterStatusFeed" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:MultiView runat="server" ID="multiview">
	<asp:View runat="server" ID="editView">
		<table width="100%">
			<tr>
				<td>
					<table border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td><asp:Localize runat="server" Text="<%$Resources:StringsRes, ctl_TwitterStatusFeed_AdminTwitterUsername %>" /></td>
							<td>
								<asp:TextBox runat="server" ID="txtUsername" /></td>
						</tr>
						<tr>
							<td><asp:Localize ID="Localize1" runat="server" Text="<%$Resources:StringsRes, ctl_TwitterStatusFeed_AdminMaxItems %>" /></td>
							<td>
								<asp:TextBox runat="server" ID="txtMaxItems" Width="30" /></td>
						</tr>
						<tr>
							<td><asp:Localize ID="Localize2" runat="server" Text="<%$Resources:StringsRes, ctl_TwitterStatusFeed_AdminTemplate %>" /></td>
							<td>
								<asp:DropDownList runat="server" ID="cmbTemplate" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td align="right">
					<asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false" />&nbsp;
					<asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="false" />
				</td>
			</tr>
		</table>
	</asp:View>
	<asp:View runat="server" ID="readonlyView">
		<asp:Xml runat="server" ID="xmlTwitter" />
	</asp:View>
	<asp:View runat="server" ID="mediumTrustErrorView">
		<div style="border: solid 1px Red; padding: 10px;">
			<asp:Localize runat="server" Text="<%$Resources:StringsRes, glb__mediumTrustError %>" />
		</div>
	</asp:View>
</asp:MultiView>
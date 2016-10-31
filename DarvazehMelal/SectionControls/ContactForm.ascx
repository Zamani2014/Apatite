<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactForm.ascx.cs" Inherits="SectionControls_ContactForm" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<%@ Register Src="~/SectionControls/HtmlEditor.ascx" TagPrefix="uc" TagName="HtmlEditor" %>
<asp:MultiView runat="server" ID="multiview">
	<asp:View runat="server" ID="noMailserverView">
		<asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_IntroPart1%>"></asp:Localize>
		<asp:HyperLink runat="server" ID="lnkAdmin" NavigateUrl="~/Administration/WebSite.aspx"><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_IntroPart2%>"></asp:Localize></asp:HyperLink>.
	</asp:View>
	<asp:View runat="server" ID="editView">
		<table width="100%">
			<tr>
				<td></td>
				<td class="field">
					<asp:RequiredFieldValidator runat="server" ID="valEmailToRequired" ControlToValidate="txtEmailTo" ErrorMessage="<%$ Resources:stringsRes, ctl_ContactForm_ErrorMessageEmail%>" Display="dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator runat="server" ID="valEmailToRegex" ControlToValidate="txtEmailTo" ErrorMessage="<%$ Resources:stringsRes, ctl_ContactForm_ErrorMessageValidEmail%>" Display="dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td class="fieldlabel"><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_RecipientEMail%>"></asp:Localize></td>
				<td class="field"><asp:TextBox runat="server" ID="txtEmailTo"></asp:TextBox></td>
			</tr>
			<tr>
				<td></td>
				<td class="field"><asp:RegularExpressionValidator runat="server" ID="valEmailCcRegex" ControlToValidate="txtEmailCc" ErrorMessage="<%$ Resources:stringsRes, ctl_ContactForm_ErrorMessageValidEmail%>" Display="dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator></td>
			</tr>
			<tr>
				<td class="fieldlabel"><asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_CopyTo%>"></asp:Localize></td>
				<td class="field"><asp:TextBox runat="server" ID="txtEmailCc"></asp:TextBox></td>
			</tr>
			<tr>
				<td></td>
				<td class="field"><asp:RequiredFieldValidator runat="server" ID="valSubjectRequired" ControlToValidate="txtSubject" ErrorMessage="<%$ Resources:stringsRes, ctl_ContactForm_ErrorMessageSubject%>" Display="dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
			</tr>
			<tr>
				<td class="fieldlabel"><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_Subject%>"></asp:Localize></td>
				<td class="field"><asp:TextBox runat="server" ID="txtSubject"></asp:TextBox></td>
			</tr>
			<tr>
				<td class="fieldlabel"><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_Introtext%>"></asp:Localize></td>
				<td class="field"><uc:HtmlEditor runat="server" ID="txtIntroTextHtml" Height="250" /></td>
			</tr>
			<tr>
				<td class="fieldlabel"><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_ThankYouMessage%>"></asp:Localize></td>
				<td class="field"><uc:HtmlEditor runat="server" ID="txtThankYouMessageHtml" Height="250" /></td>
			</tr>
			<tr>
				<td colspan="2" align="right">
					<asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="<%$Resources:StringsRes, glb__Save %>" />
				</td>
			</tr>
		</table>
	</asp:View>
	<asp:View runat="server" ID="formView">
		<asp:Literal runat="server" ID="litIntrotext"></asp:Literal>
		<div class="roundedTop">
		</div>
		<div class="framed">
			<table width="100%">
				<tr>
					<td></td>
					<td class="field">
						<asp:RequiredFieldValidator runat="server" ID="valEmailFromRequired" ControlToValidate="txtEmailFrom" ErrorMessage="<%$ Resources:stringsRes, ctl_ContactForm_ErrorMessageEmail%>" Display="dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator runat="server" ID="valEmailFromRegex" ControlToValidate="txtEmailFrom" ErrorMessage="<%$ Resources:stringsRes, ctl_ContactForm_ErrorMessageValidEmail%>" Display="dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
					</td>
				</tr>
				<tr>
					<td class="fieldlabel"><asp:Localize ID="Localize8" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_EMailAddress%>"></asp:Localize></td>
					<td class="field"><asp:TextBox runat="server" ID="txtEmailFrom"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="fieldlabel"><asp:Localize ID="Localize9" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_Name%>"></asp:Localize></td>
					<td class="field"><asp:TextBox runat="server" ID="txtName"></asp:TextBox></td>
				</tr>
				<tr>
					<td></td>
					<td class="field"><asp:RequiredFieldValidator runat="server" ID="valMessageRequired" ControlToValidate="txtMessage" ErrorMessage="<%$ Resources:stringsRes, ctl_ContactForm_ErrorMessageMessage%>" Display="dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="fieldlabel"><asp:Localize ID="Localize10" runat="server" Text="<%$ Resources:stringsRes, ctl_ContactForm_Message%>"></asp:Localize></td>
					<td class="field"><asp:TextBox runat="server" ID="txtMessage" TextMode="multiLine"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="fieldlabel"></td>
					<td class="field">
						<asp:RequiredFieldValidator runat="server" ID="valAntiBotImageRequired" ControlToValidate="txtAntiBotImage" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibot %>" SetFocusOnError="true"></asp:RequiredFieldValidator>
						<asp:CustomValidator runat="server" ID="valAntiBotImage" OnServerValidate="valAntiBotImage_ServerValidate" Text="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibotInvalid %>" Display="dynamic"></asp:CustomValidator>
					</td>
				</tr>
				<tr>
					<td class="fieldlabel"></td>
					<td>
						<table>
							<tr>
								<td><asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:stringsRes, ctl_Guestbook_AntiBotImage%>"></asp:Localize></td>
								<td>&nbsp;&nbsp;</td>
								<td><asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /></td>
							</tr>
							<tr>
								<td></td>
								<td>&nbsp;&nbsp;</td>
								<td><asp:TextBox runat="server" ID="txtAntiBotImage" MaxLength="4" CssClass="textbox" Width="75px"></asp:TextBox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" align="right"><asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="<%$Resources:StringsRes, glb__Submit %>" /></td>
				</tr>
			</table>
		</div>
		<div class="roundedBottom">
		</div>
	</asp:View>
	<asp:View runat="server" ID="thankyouView">
		<asp:Literal runat="server" ID="litThankyoutext"></asp:Literal>
	</asp:View>
</asp:MultiView>
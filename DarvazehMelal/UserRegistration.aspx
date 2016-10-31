<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="UserRegistration" Title="<%$ Resources:stringsRes, pge_UserRegistration_Title%>" %>

<asp:Content runat="server" ContentPlaceHolderID="mainContent">
	<div class="roundedTop"></div>
	<div class="framed">
		<asp:CreateUserWizard 
		runat="server" 
		ID="createUserWizard" 
		DisableCreatedUser="True"
		DuplicateUserNameErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_DuplicateUserName%>"
		CreateUserButtonText="<%$ Resources:stringsRes, ctl_CreateUserWizard_CreateUser%>"
		ToolTip="<%$ Resources:stringsRes, ctl_CreateUserWizard_CreateUser%>"
		DuplicateEmailErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_DuplicateEmail%>"
		RequireEmail="true"
		ContinueDestinationPageUrl="~/Default.aspx"
		CreateUserButtonStyle-CssClass="button"
		>
			<WizardSteps>
				<asp:CreateUserWizardStep runat="server" ID="createUserStep">
					<ContentTemplate>
						<table border="0">
							<tr>
								<td colspan="2"><h2><asp:Localize runat="server" Text="<%$Resources:stringsRes, pge_UserRegistration_WizardTitle %>" /></h2></td>
							</tr>
							<tr>
								<td></td>
								<td><asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ValidationGroup="createUserWizard" Display="Dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_UserNameRequired%>"/></td>
							</tr>
							<tr>
								<td class="fieldlabel"><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Text="<%$ Resources:stringsRes, ctl_CreateUserWizard_UserName%>"/></td>
								<td class="field"><asp:TextBox ID="UserName" runat="server"/></td>
							</tr>
							<tr>
								<td></td>
								<td><asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ValidationGroup="createUserWizard" Display="Dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_PasswordRequired%>"/></td>
							</tr>
							<tr>
								<td class="fieldlabel"><asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Text="<%$ Resources:stringsRes, ctl_CreateUserWizard_Password%>"/></td>
								<td class="field"><asp:TextBox ID="Password" runat="server" TextMode="Password"/></td>
							</tr>
							<tr>
								<td></td>
								<td>
									<asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ValidationGroup="createUserWizard" ErrorMessage="<%$Resources:stringsRes, ctl_CreateUserWizard_ComparePassword %>"/>
									<asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ValidationGroup="createUserWizard" Display="Dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_ConfirmPasswordRequired%>"/>
								</td>
							</tr>
							<tr>
								<td class="fieldlabel"><asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword" Text="<%$ Resources:stringsRes, ctl_CreateUserWizard_ConfirmPassword%>"/></td>
								<td class="field"><asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"/></td>
							</tr>
							<tr>
								<td></td>
								<td>
									<asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ValidationGroup="createUserWizard" Display="Dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_EmailRequired%>" />
									<asp:RegularExpressionValidator ID="EmailRegexValidator" runat="server" ControlToValidate="Email" ValidationGroup="createUserWizard" Display="Dynamic" ErrorMessage="<%$Resources:stringsRes, pge_UserRegistration_EmailRegex %>" />
								</td>
							</tr>
							<tr>
								<td class="fieldlabel"><asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" Text="<%$ Resources:stringsRes, ctl_CreateUserWizard_Email%>"/></td>
								<td class="field"><asp:TextBox ID="Email" runat="server"/></td>
							</tr>
							<tr>
								<td></td>
								<td class="error"><asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"/></td>
							</tr>
						</table>
					</ContentTemplate>
				</asp:CreateUserWizardStep>
				<asp:CompleteWizardStep runat="server">
					<ContentTemplate>
						<h2><asp:Localize ID="Localize1" runat="server" Text="<%$Resources:stringsRes, pge_UserRegistration_WizardTitle %>" /></h2>
						<p><strong><asp:Localize runat="server" Text="<%$Resources:stringsRes, pge_UserRegistration_ConfirmationTitle %>" /></strong></p>
						<p><asp:Localize runat="server" Text="<%$Resources:stringsRes, pge_UserRegistration_ConfirmationText %>" /></p>
						<p><asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue" Text="<%$Resources:stringsRes, pge_UserRegistration_ContinueButton %>" ValidationGroup="createUserWizard" /></p>
					</ContentTemplate>
				</asp:CompleteWizardStep>
			</WizardSteps>
		</asp:CreateUserWizard>
	</div>
	<div class="roundedBottom"></div>
</asp:Content>

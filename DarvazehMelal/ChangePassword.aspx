<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePasswordN" Title="<%$ Resources:stringsRes, pge_ChangePassword_Title%>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:ChangePassword ID="ChangePassword1" runat="server" DisplayUserName="true"  ChangePasswordFailureText="<%$ Resources:stringsRes, pge_ChangePassword_PasswordFailureText%>" CancelDestinationPageUrl="~/Default.aspx" ContinueDestinationPageUrl="~/Default.aspx" SuccessPageUrl="~/Default.aspx">
        <ChangePasswordTemplate>
			<div class="roundedTop"></div>
            <div class="framed">
                <table>
                    <tr>
                        <td colspan="2"><h2> <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:stringsRes, pge_ChangePassword_Title%>"></asp:Localize></h2></td>
                    </tr>
                    <tr>
                        <td colspan="2">
							<asp:Panel runat="server" ID="pnlFirstUse" Visible="false" BorderColor="Red" BorderWidth="1px" BorderStyle="Solid" style="padding: 10px; margin-top: 10px; margin-bottom: 10px; margin-right: 30px;">
								<asp:Localize runat="server" Text="<%$ Resources:stringsRes, pge_ChangePassword_FirstUse%>"></asp:Localize>
							</asp:Panel>
							<asp:CompareValidator ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="<%$ Resources:stringsRes, pge_ChangePassword_NotMatching%>" ID="NewPasswordCompare" runat="server" ValidationGroup="ChangePassword1"></asp:CompareValidator>
						</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="color: Red;"><asp:Literal EnableViewState="False" ID="FailureText" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:stringsRes, pge_ChangePassword_UserName%>"></asp:Localize>:
                        <asp:RequiredFieldValidator ControlToValidate="UserName" ErrorMessage="<%$ Resources:stringsRes, pge_ChangePassword_UserNameRequired%>" ToolTip="<%$ Resources:stringsRes, pge_ChangePassword_UserNameRequired%>" ID="UserNameRequired" runat="server" ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator></td>
                        <td><asp:TextBox Width="150px" ID="UserName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:stringsRes, pge_ChangePassword_Password%>"></asp:Localize>:
                        <asp:RequiredFieldValidator ControlToValidate="CurrentPassword" ID="CurrentPasswordRequired" runat="server" ErrorMessage="<%$ Resources:stringsRes, pge_ChangePassword_PasswordRequired%>" ToolTip="<%$ Resources:stringsRes, pge_ChangePassword_PasswordRequired%>" ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator></td>
                        <td><asp:TextBox Width="150px" ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:stringsRes, pge_ChangePassword_NewPassword%>"></asp:Localize>:
                        <asp:RequiredFieldValidator ControlToValidate="NewPassword" ErrorMessage="<%$ Resources:stringsRes, pge_ChangePassword_NewPasswordRequired%>" ID="NewPasswordRequired" runat="server" ToolTip="<%$ Resources:stringsRes, pge_ChangePassword_NewPasswordRequired%>" ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator></td>
                        <td><asp:TextBox Width="150px" ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:stringsRes, pge_ChangePassword_ConfirmNewPassword%>"></asp:Localize>:
                        <asp:RequiredFieldValidator ControlToValidate="ConfirmNewPassword" ErrorMessage="<%$ Resources:stringsRes, pge_ChangePassword_ConfirmNewPasswordRequired%>" ID="ConfirmNewPasswordRequired" runat="server" ToolTip="<%$ Resources:stringsRes, pge_ChangePassword_ConfirmNewPasswordRequired%>" ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator></td>
                        <td><asp:TextBox Width="150px" ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td align="right" colspan="2"><asp:Button CausesValidation="False" CommandName="Cancel" ID="CancelPushButton" runat="server" Text="<%$ Resources:stringsRes, glb__Cancel%>" />&nbsp;
                        <asp:Button CommandName="ChangePassword" ID="ChangePasswordPushButton" runat="server" Text="<%$ Resources:stringsRes, pge_ChangePassword_Button%>" ValidationGroup="ChangePassword1" /></td>
                    </tr>
                </table>
            </div>
            <div class="roundedBottom"></div>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
</asp:Content>

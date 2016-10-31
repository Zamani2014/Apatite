<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="NLogin" Title="<%$ Resources:stringsRes, pge_Login_Title%>" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Login ID="Login1" runat="server" DisplayRememberMe="true" Width="100%" FailureText="<%$ Resources:stringsRes, pge_Login_Failure%>" OnLoggingIn="OnLoggingIn" OnLoggedIn="OnLoggedIn">
       <LayoutTemplate>
        <div class="roundedTop"></div>
            <div class="framed">
                <table width="100%" id="TABLE1" border="0">
					<tr>
                        <td colspan="2"><h2><asp:Localize runat="server" Text="<%$ Resources:stringsRes, pge_Login_Title%>"></asp:Localize></h2></td>
                    </tr>
                    <tr runat="server" id="trActivationSuccess" visible="false">
						<td></td>
						<td><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:stringsRes, pge_Login_ActivationSuccess%>" /></td>
					</tr>
                    <tr>
                        <td></td>
                        <td class="error"><asp:Localize ID="FailureText" runat="server" EnableViewState="False"></asp:Localize></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="<%$ Resources:stringsRes, pge_Login_UserNameRequired%>" ToolTip="<%$ Resources:stringsRes, pge_Login_UserNameRequired%>" ValidationGroup="Login1" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize runat="server" Text="<%$ Resources:stringsRes, pge_Login_UserName%>"></asp:Localize>:
                        </td>
                        <td class="field">
                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="<%$ Resources:stringsRes, pge_Login_PasswordRequired%>" ToolTip="<%$ Resources:stringsRes, pge_Login_PasswordRequired%>" ValidationGroup="Login1" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldlabel">
                            <asp:Localize runat="server" Text="<%$ Resources:stringsRes, pge_Login_Password%>"></asp:Localize>:
                        </td>
                        <td class="field">
                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="RememberMe" runat="server" Text="<%$ Resources:stringsRes, pge_Login_RememberMe%>" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="<%$ Resources:stringsRes, pge_Login_LogIn%>" ValidationGroup="Login1" />
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td colspan="2"><asp:HyperLink runat="server" ID="lnkChangePassword" Text="<%$ Resources:stringsRes, pge_Login_ChangePassword%>" NavigateUrl="~/ChangePassword.aspx"></asp:HyperLink></td>
                    </tr>
                    <tr runat="server" id="trCreateUser">
                        <td colspan="2"><asp:HyperLink runat="server" ID="lnkCreateUser" Text="<%$ Resources:stringsRes, pge_Login_CreateUser%>" NavigateUrl="~/UserRegistration.aspx"></asp:HyperLink></td>
                    </tr>
                </table>
            </div>
           <div class="roundedBottom"></div>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>

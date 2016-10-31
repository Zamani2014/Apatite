<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Users.aspx.cs"
    Inherits="Administration_Users"%>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="contentPane" ContentPlaceHolderID="mainContent" runat="Server">
    <img src="../Images/info.gif" alt="Info" onclick="window.open('../Documentation/<%=lang%>/quick_guide.html#user-management','InfoPopUp', 'height=780,width=800,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no');return false;" style="height:16px;width:16px;border-width:0px;cursor:hand;" />
    <table width="100%">
    <tr runat="server" id="trMessage">
        <td>
            <asp:Label ID="lblMessage" CssClass="error" runat="server"></asp:Label>
        </td>
    </tr>
        <tr>
            <td>
            <fieldset runat="server" id="fieldset1">
                <legend runat="server" id="legend1"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Users_ChangeUser%>"></asp:Localize></legend>
                <asp:GridView ID="gvUsers" runat="server" DataSourceID="srcUsersGrid" DataKeyNames="UserName"
                    AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnRowCommand="gvUsers_RowCommand" OnRowDataBound="gvUsers_RowDataBound">
                   <Columns>
                        <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:stringsRes, adm_Users_UserName%>" SortExpression="UserName"/>
                        <asp:BoundField DataField="Comment" HeaderText="<%$ Resources:stringsRes, adm_Users_Comment%>" />
                        <asp:TemplateField HeaderText="<%$ Resources:stringsRes, adm_Users_PowerUser%>" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%#ShowPowerUser((string)DataBinder.Eval(Container.DataItem, "UserName"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:stringsRes, adm_Users_Admin%>" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <%#ShowAdmin((string)DataBinder.Eval(Container.DataItem, "UserName"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:stringsRes, adm_Users_LastLogin%>"  ItemStyle-HorizontalAlign="left">
                            <ItemTemplate><%# ((DateTime)DataBinder.Eval(Container.DataItem,  "LastLoginDate")).ToShortDateString()%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Link" CommandName="edit_details" ControlStyle-CssClass="btnEdit"/>
                        <asp:ButtonField ButtonType="Link" CommandName="delete_user" ItemStyle-Width="40px" ControlStyle-CssClass="btnDelete"  />
                    </Columns>
                    <RowStyle CssClass="odd" />
                    <AlternatingRowStyle CssClass="even" />
                </asp:GridView>
                 </fieldset>
                
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr runat="server" id="trUpdateUser">
            <td>
            <fieldset runat="server" id="fieldsetEditUser">
                <legend runat="server" id="legendEditUser"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Users_ChangeUser%>"></asp:Localize></legend>
                <asp:Table id="tblEditUser" BorderWidth="0" runat="server" OnPreRender="EditUser_PreRender" width="100%">
                    <asp:TableRow>
                        <asp:TableCell CssClass="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Users_UserName%>"></asp:Localize></asp:TableCell>
                        <asp:TableCell CssClass="field"><asp:Literal ID="litUsername" runat="Server" Text="<%# SelectedUser.UserName %>"/></asp:TableCell>
                    </asp:TableRow>
                     <asp:TableRow Height="10px">
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Users_Email%>"></asp:Localize></asp:TableCell>
                        <asp:TableCell CssClass="field"><asp:TextBox ID="txtEmail" runat="Server" SkinID="admin"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Users_Comment%>"></asp:Localize></asp:TableCell>
                        <asp:TableCell CssClass="field"><asp:TextBox ID="txtComment" runat="Server" SkinID="admin"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="fieldlabel"><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:stringsRes, adm_Users_IsApproved%>"></asp:Localize></asp:TableCell>
                        <asp:TableCell CssClass="field"><asp:CheckBox ID="chkIsApproved" runat="Server" ></asp:Checkbox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Users_IsAdministrator%>"></asp:Localize></asp:TableCell>
                        <asp:TableCell CssClass="field"><asp:CheckBox ID="chkAdmin" runat="Server" ></asp:Checkbox></asp:TableCell>
                    </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableCell CssClass="fieldlabel"><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:stringsRes, adm_Users_IsPowerUser%>"></asp:Localize></asp:TableCell>
                        <asp:TableCell CssClass="field"><asp:CheckBox ID="chkPowerUser" runat="Server" ></asp:Checkbox></asp:TableCell>
                    </asp:TableRow>   
                    <asp:TableRow>
						<asp:TableCell></asp:TableCell>
                        <asp:TableCell><asp:Button ID="btnCancel" runat="Server" Text="<%$ Resources:stringsRes, glb__Cancel%>" OnClick="btnUpdateCancel_Click" CausesValidation="false"></asp:Button>&nbsp;<asp:Button ID="btnSave" runat="Server" Text="<%$ Resources:stringsRes, glb__Save%>" OnClick="btnUpdateSave_Click" CausesValidation="false"></asp:Button></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </fieldset>
            </td>
        </tr>
        <tr runat="server" id="trCreateUser">
            <td>
                <fieldset><legend><asp:Localize runat="server" Text="<%$ Resources:stringsRes, adm_Users_CreateUser%>"></asp:Localize>&nbsp;</legend>
                <Table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td height="20"></td>
                    </tr>
                    <tr>
                        <td>
                            <% // In order to have designer WYSIWYG support, remote the attributes refering to Resources %>
                            <asp:CreateUserWizard ID="CreateUserWizard1" runat="server"
                            MembershipProvider="CustomXmlMembershipProvider"
                            LoginCreatedUser="false"
                            EnableTheming="true" OnCreatedUser="CreateUserWizard1_Done"
                            RequireEmail="true" CompleteSuccessTextStyle-Width="100%"
                            UserNameRequiredErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_UserNameRequired%>"
                            PasswordRequiredErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_PasswordRequired%>"
                            ConfirmPasswordRequiredErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_ConfirmPasswordRequired%>"
                            EmailRequiredErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_EmailRequired%>"
                            ConfirmPasswordCompareErrorMessage="<%$ Resources:stringsRes,ctl_CreateUserWizard_ComparePassword%>"
                            DuplicateUserNameErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_DuplicateUserName%>"
                            UserNameLabelText="<%$ Resources:stringsRes, ctl_CreateUserWizard_UserName%>"
                            PasswordLabelText="<%$ Resources:stringsRes, ctl_CreateUserWizard_Password%>"
                            ConfirmPasswordLabelText="<%$ Resources:stringsRes, ctl_CreateUserWizard_ConfirmPassword%>"
                            EmailLabelText="<%$ Resources:stringsRes, ctl_CreateUserWizard_Email%>"
                            CreateUserButtonText="<%$ Resources:stringsRes, ctl_CreateUserWizard_CreateUser%>"
                            ToolTip="<%$ Resources:stringsRes, ctl_CreateUserWizard_CreateUser%>"
                            DuplicateEmailErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_DuplicateEmail%>"
                            ContinueDestinationPageUrl="~/Administration/Users.aspx"
                            >
                            <TextBoxStyle CssClass="textboxAdmin" />
                            <CancelButtonStyle CssClass="button"/>
                            <CreateUserButtonStyle CssClass="button" />
                            <ContinueButtonStyle CssClass="button" />
                            <FinishCompleteButtonStyle CssClass="button" />
                            <StepStyle />
                            <TextBoxStyle />
                            <WizardSteps>
                                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" Title="">
                                </asp:CreateUserWizardStep>
                                 <asp:CompleteWizardStep runat="server" ID="CompleteWizardStep1" >
                                <ContentTemplate>
                                    <table width="100%" border="0">
                                        <tr>
                                            <td align="center" ><asp:Localize ID="Localize1" runat="server" Text=" <%$ Resources:stringsRes, ctl_CreateUserWizard_CreateUserSucessful %>"></asp:Localize></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:Button id="ContinueButton" commandname="Continue" runat="server" Text="<%$ Resources:stringsRes, ctl_CreateUserWizard_CreateUserButtonDone %>" ValidationGroup="CreateUserWizard"/>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                </asp:CompleteWizardStep>
                            </WizardSteps>
                        </asp:CreateUserWizard>
                        </td>
                    </tr>
                </Table>
                </fieldset>
            </td>
        </tr>
        <tr runat="server" id="trSendUserPassword">
            <td>
            <fieldset><legend><asp:Label ID="lblApplicationName" runat="server" 
    Text="<%$ Resources:StringsRes, ctl_RecoverPassword_Legend %>" /> &nbsp;</legend>
            <Table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td height="20"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Wizard ID="RecoverPasswordWizard" runat="server" EnableTheming="true" OnNextButtonClick="RecoverPasswordWizard_OnNext"
                                OnFinishButtonClick="RecoverPasswordWizard_OnFinish" DisplaySideBar="false" NavigationButtonStyle-CssClass=""  FinishCompleteButtonText="<%$ Resources:StringsRes, ctl_RecoverPassword_FinishButton %>" >
                                <CancelButtonStyle CssClass="button"/>
                            <NavigationButtonStyle CssClass="button" />
                            <FinishCompleteButtonStyle CssClass="button" />
                                <WizardSteps>
                                    <asp:WizardStep runat="server" Title="Username" ID="stpUsername">
                                        <asp:ValidationSummary DisplayMode="List" EnableClientScript="true" EnableTheming="True" ID="ValidationSummary1" runat="server">
                                        </asp:ValidationSummary>
                                        <asp:TextBox ID="txtUsername" runat="server" ValidationGroup="RecoverPasswordWizard"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredUsernameValidator" runat="server" ErrorMessage="<%$ Resources:StringsRes, ctl_CreateUserWizard_PasswordRequired %>" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                                    </asp:WizardStep>
                                    <asp:WizardStep StepType="Complete"><asp:Label ID="lblResult" runat="Server"></asp:Label></asp:WizardStep>
                                </WizardSteps>
                            </asp:Wizard>
                       </td>
                    </tr>
               </table>
            </fieldset>
            </td>
         </tr>
    </table>
    <asp:ObjectDataSource runat="server" ID="srcUsersGrid" TypeName="System.Web.Security.Membership" SelectMethod="GetAllUsers"></asp:ObjectDataSource>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" MaintainScrollPositionOnPostback="true" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<%@ Register src="~/EasyControls/SlideShow.ascx" tagname="SlideShow" tagprefix="uc1" %>

<asp:Content ID="contentPane" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:PlaceHolder runat="server" ID="phSections" EnableViewState="true"></asp:PlaceHolder>
    <div runat="server" id="divPageSettings" class="sectionsettings">
        <div class="roundedTop"></div>
            <table width="100%">
                <tr>
                    <td><asp:DropDownList runat="server" ID="cmbSections" CssClass="aspcontrols"></asp:DropDownList>&nbsp;<asp:Button runat="server" ID="btnNewSection" OnClick="btnNewSection_Click" Text="<%$ Resources:stringsRes, pge_Default_AddSection%>" CausesValidation="false" UseSubmitBehavior="false"/></td>
                </tr>
            </table>
        <div class="roundedBottom"></div>
    </div>
</asp:Content>

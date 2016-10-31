<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionAdmin.ascx.cs" Inherits="SectionControls_SectionAdmin" %>
<div runat="server" id="adminDiv" class="sectionsettings">
    <div class="roundedTop"></div>
    <table width="100%">
        <tr>
            <td rowspan="2">
                <h2 style="margin-bottom: 5px;"><asp:Literal runat="server" ID="litSectionName"></asp:Literal>&nbsp;<asp:ImageButton runat="server" ID="btnInfo" AlternateText="<%$ Resources:stringsRes, ctl_SectionAdmin_Info %>" ImageUrl="~/Images/info.gif" Width="16" Height="16" /> </h2>
                <asp:Button runat="server" ID="btnDeleteSection" OnClick="btnDeleteSection_Click" Text="<%$ Resources:stringsRes, glb__DeleteSection%>" CausesValidation="false" UseSubmitBehavior="false" />
                <asp:Button runat="server" ID="btnSendToSidebar" OnClick="btnSendToSidebar_Click" Text="<%$ Resources:stringsRes, glb__SendToSidebar%>" CausesValidation="false" UseSubmitBehavior="false" />
                <asp:Button runat="server" ID="btnRemoveFromSidebar" OnClick="btnRemoveFromSidebar_Click" Text="<%$ Resources:stringsRes, glb__RemoveFromSidebar%>" CausesValidation="false" UseSubmitBehavior="false" />
                <asp:Button runat="server" ID="btnToggleViewMode" OnClick="btnToggleViewMode_Click" Text="<%$ Resources:stringsRes, glb__EditMode%>" CausesValidation="false" UseSubmitBehavior="false" />
            </td>
            <td align="right"><asp:LinkButton runat="server" ID="btnMoveUp" OnClick="btnMoveUp_Click" SkinID="Up" CausesValidation="false"/></td>
        </tr>
        <tr>
            <td align="right"><asp:LinkButton runat="server" ID="btnMoveDown" OnClick="btnMoveDown_Click" SkinID="Down" CausesValidation="false" /></td>
        </tr>
    </table>
    <div class="roundedBottom"></div>
</div>
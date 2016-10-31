<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EasyControl.ascx.cs" Inherits="SectionControls_EasyControl" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td></td>
                <td class="field"><asp:RequiredFieldValidator ID="valControlsRequired" runat="server" ControlToValidate="cmbControls" ErrorMessage="<%$ Resources:stringsRes, ctl_EasyControl_SelectControl%>" Display="dynamic"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_EasyControl_Choose%>"></asp:Localize></td>
                <td class="field"><asp:DropDownList runat="server" ID="cmbControls"></asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" Text="<%$Resources:StringsRes, glb__Delete %>" CausesValidation="false" />
                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="true"/>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
        <asp:PlaceHolder runat="server" ID="phControlPlaceholder" />
    </asp:View>
</asp:MultiView>
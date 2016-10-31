<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagCloud.ascx.cs" Inherits="SectionControls_TagCloud" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <br />
        <fieldset>
            <legend><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:stringsRes, ctl_TagCloud_ConfigTitle %>"></asp:Localize></legend>
                <table width="100%">
             <tr>
                 <td></td>
                  <td><asp:Label runat="server" ID="lbConfigSaved" ForeColor="Red"><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:stringsRes, ctl_TagCloud_ConfigSafed %>"></asp:Localize></asp:Label></td>
             </tr>
             <tr>
                <td></td>
                <td>  <asp:RequiredFieldValidator runat="server" ID="valNameRequired" ControlToValidate="txtTagCloudTitle"
                        Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_TagCloud_ErrorMessageTitle %>"
                        SetFocusOnError="true"></asp:RequiredFieldValidator></td>
             </tr>
             <tr>
                <td class="fieldlabel"><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:stringsRes, ctl_TagCloud_Title %>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" id="txtTagCloudTitle"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnSaveConfiguration" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="true" OnClick="btnSaveConfiguration_Click" />
                </td>
            </tr>
        </table>
        </fieldset>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
        <div style="padding-top: 4px">
        </div>
        <cc1:TagCloud runat="server" ID="tagCloud" AverageCloudSize="5" CloudSizeRange="10" />
    </asp:View>
</asp:MultiView>

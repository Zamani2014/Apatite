<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventList.ascx.cs" Inherits="SectionControls_EventList" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar" %>
<%@ Register Src="~/SectionControls/HtmlEditor.ascx" TagPrefix="uc" TagName="HtmlEditor" %>
<asp:MultiView runat="server" ID="multiview">
    <asp:View runat="server" ID="editView">
        <table width="100%">
            <tr>
                <td align="right"><asp:Button runat="server" ID="btnNewEntry" OnClick="btnNewEntry_Click" Text="<%$ Resources:stringsRes, glb__add %>" CausesValidation="False"></asp:Button></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView runat="server" DataKeyNames="Guid" ID="gvEventListEdit" AutoGenerateColumns="false" ShowHeader="true" OnRowCommand="gvEventListEdit_RowCommand" OnRowDataBound="gvEventListEdit_RowDataBound">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="100px" DataField="EventDate" DataFormatString="{0:g}" HeaderText="<%$ Resources:stringsRes, ctl_EventList_Date %>" HtmlEncode="false" SortExpression="EventDate" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="EventName" HeaderText="<%$ Resources:stringsRes, ctl_EventList_Event %>" SortExpression="EventName"/>
                            <asp:BoundField DataField="EventLocation" HeaderText="<%$ Resources:stringsRes, ctl_EventList_Location %>" SortExpression="EventLocation"/>
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnEdit" CommandName="edit_entry" CausesValidation="false" />
                            <asp:ButtonField ItemStyle-Width="40px" ButtonType="Link" ControlStyle-CssClass="btnDelete" CommandName="delete_entry" CausesValidation="false" />
                        </Columns>
                        <RowStyle Height="25px" />
                        <HeaderStyle Height="30px" />
                    </asp:GridView>
                    <br />
                    <cc1:Pager runat="server" ID="editPager" ControlToPage="gvEventListEdit"></cc1:Pager>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="editDetailsView">
        <table width="100%">
            <tr>
                <td class="fieldlabel"></td>
                <td class="field"><asp:RequiredFieldValidator runat="server" ID="valEventDateRequired" ControlToValidate="txtEventDate" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_EventList_ErrorMessageEventDate %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_EventList_EventDate%>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" ID="txtEventDate" Readonly="true" SkinID="datepicker"></asp:TextBox><RJS:PopCalendar runat="server" Control="txtEventDate" ID="dteEventDate" /></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_EventList_EventTime%>"></asp:Localize></td>
                <td class="field">
                <asp:DropDownList runat="server" ID="txtEventHours" SkinID="timepicker" />:<asp:DropDownList runat="server" ID="txtEventMinutes" SkinID="timepicker" />
                </td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_EventList_ShowFrom%>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" ID="txtDisplayDate" Readonly="true" SkinID="datepicker"></asp:TextBox><RJS:PopCalendar runat="server" Control="txtDisplayDate" ID="dteDisplayDate" /></td>
            </tr>
            <tr>
                <td class="fieldlabel"></td>
                <td class="field"><asp:RequiredFieldValidator runat="server" ID="valEventNameRequired" ControlToValidate="txtEventName" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_EventList_ErrorMessageEventName %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_EventList_EventName%>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" id="txtEventName"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel"></td>
                <td class="field"><asp:RequiredFieldValidator runat="server" ID="valEventLocationRequired" ControlToValidate="txtEventLocation" Display="dynamic" ErrorMessage="<%$ Resources:stringsRes, ctl_EventList_ErrorMessageEventLocation %>" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_EventList_Location%>"></asp:Localize></td>
                <td class="field"><asp:TextBox runat="server" id="txtEventLocation"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldlabel"><asp:Localize runat="server" Text="<%$ Resources:stringsRes, ctl_EventList_Description%>"></asp:Localize></td>
                <td class="field"><uc:HtmlEditor runat="server" ID="txtEventDescriptionHtml" Height="300" /></td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnCancelDetails" OnClick="btnCancelDetails_Click" Text="<%$Resources:StringsRes, glb__Cancel %>" CausesValidation="false" />
                    <asp:Button runat="server" ID="btnSaveDetails" OnClick="btnSaveDetails_Click" Text="<%$Resources:StringsRes, glb__Save %>" CausesValidation="true" />
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="readonlyView">
		<table>
			<tr>
				<td>
			<asp:CheckBox runat="server" ID="chkShowPastEvents" Text="<%$ Resources:stringsRes, ctl_EventList_ShowPastEvents %>" Checked="false" AutoPostBack="true" />
			<asp:GridView runat="server" DataKeyNames="Guid" ID="gvEventList" AutoGenerateColumns="false" OnRowCommand="gvEventList_RowCommand" OnRowDataBound="gvEventList_RowDataBound">
				<Columns>
					<asp:BoundField ItemStyle-Width="100px" DataField="EventDate" DataFormatString="{0:g}" HeaderText="<%$ Resources:stringsRes, ctl_EventList_Date %>"  SortExpression="EventDate" HtmlEncode="true" ItemStyle-Wrap="false" />
					<asp:ButtonField ItemStyle-Width="150px" ButtonType="Link" DataTextField="EventName" HeaderText="<%$ Resources:stringsRes, ctl_EventList_Event %>"  SortExpression="EventName" CommandName="show_details" CausesValidation="false" />
					<asp:BoundField DataField="EventLocation" HeaderText="<%$ Resources:stringsRes, ctl_EventList_Location %>" SortExpression="EventLocation" />
				</Columns>
				<RowStyle Height="25px" />
				<HeaderStyle Height="30px" />
			</asp:GridView>
			<br />
			<cc1:Pager runat="server" ID="readonlyPager" ControlToPage="gvEventList"></cc1:Pager>
			<div runat="server" id="divEventDetails" class="eventcontent" visible="false">
				<span class="date"><asp:Literal runat="server" ID="litEventDate"></asp:Literal></span>
				<h1><asp:Literal runat="server" ID="litEventName"></asp:Literal></h1>
				<asp:Literal runat="server" ID="litEventText"></asp:Literal>
			</div>
			 </td>
			</tr>
		</table>
    </asp:View>
</asp:MultiView>
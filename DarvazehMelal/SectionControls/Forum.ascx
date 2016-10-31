<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Forum.ascx.cs" Inherits="SectionControls_Forum" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<asp:MultiView ID="mvForum" runat="server">
	<asp:View ID="vwThreads" runat="server">
		<asp:Button ID="btnCreateNewThread" runat="server" Text="<%$Resources:Forum, CreateNewThread %>" OnClick="btnCreateNewThread_Click" />
		<br />
		<asp:Panel ID="pnlNewThread" Visible="false" runat="server">
			<div class="roundedTop"></div>
			<div class="framed">
				<table width="100%">
					<tr>
						<td class="fieldlabel"><asp:Label runat="server" Text="<%$Resources:Forum, ThreadSubject %>" AssociatedControlID="txtSubject"/></td>
						<td class="field">
							<asp:RequiredFieldValidator runat="server" ID="valSubject" ControlToValidate="txtSubject" ErrorMessage="<%$Resources:Forum, ThreadErrorSubjectMissing %>" Display="Dynamic" ValidationGroup="NewThread" />
							<asp:TextBox ID="txtSubject" runat="server"/>
						</td>
					</tr>
					<tr>
						<td class="fieldlabel"><asp:Label runat="server" Text="<%$Resources:Forum, ThreadBody %>" AssociatedControlID="txtBody"/></td>
						<td class="field">
							<asp:RequiredFieldValidator runat="server" ID="valBody" ControlToValidate="txtBody" ErrorMessage="<%$Resources:Forum, ThreadErrorBodyMissing %>" Display="Dynamic" ValidationGroup="NewThread" />
							<asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine"/>
						</td>
					</tr>
					<tr>
						<td colspan="2" align="right">
							<asp:Button ID="btnNewThreadCancel" runat="server" OnClick="btnNewThreadCancel_Click" CausesValidation="false" Text="<%$Resources:StringsRes, glb__Cancel %>" />
							<asp:Button ID="btnNewThreadSave" runat="server" OnClick="btnNewThreadSave_Click" ValidationGroup="NewThread" Text="<%$Resources:StringsRes, glb__Save %>" />
						</td>
					</tr>
				</table>
			</div>
			<div class="roundedBottom"></div>
			<br />
		</asp:Panel>
		<cc1:Pager runat="server" ID="pagThreads" ControlToPage="gvThreads" PageSize="10" />
		<asp:GridView 
			runat="server"
			ID="gvThreads" 
			AutoGenerateColumns="false" 
			CellPadding="5" 
			OnRowDeleting="gvThreads_RowDeleting" 
			OnRowCreated="gvThreads_RowCreated"
			DataKeyNames="ThreadID"
		>
			<RowStyle CssClass="odd" />
			<AlternatingRowStyle CssClass="even" />
			<Columns>
				<asp:TemplateField HeaderText="<%$Resources:Forum, ThreadSubject %>" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<a href="<%# createThreadDeeplink(Eval("Permalink")) %>"><strong><%#Eval("Subject") %></strong></a><br />
						<%#Eval("CreatedBy") %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<%$Resources:Forum, ThreadLastEntry %>" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<strong><%#Eval("lastEntry", "{0:g}") %></strong><br />
						<%#Eval("lastAuthor", "{0}") %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:BoundField DataField="MessageCount" HeaderText="<%$Resources:Forum, ThreadReplies %>" HeaderStyle-HorizontalAlign="Left" />
				<asp:BoundField DataField="HitCount" HeaderText="<%$Resources:Forum, ThreadHits %>" HeaderStyle-HorizontalAlign="Left" />
			</Columns>
		</asp:GridView>
	</asp:View>
	<asp:View ID="vwPosts" runat="server">
		<h2><asp:Literal runat="server" ID="litThreadTitle"/></h2>
		<asp:Button runat="server" ID="btnBackToThreads1" Text="<%$Resources:Forum, PostBackToThreadButton %>" />
		<asp:GridView 
			runat="server"
			ID="gvPosts" 
			AutoGenerateColumns="false" 
			CellPadding="5"
			OnRowDeleting="gvPosts_RowDeleting" 
			OnRowCreated="gvPosts_RowCreated"
			DataKeyNames="PostID"
		>
			<RowStyle CssClass="odd" />
			<AlternatingRowStyle CssClass="even" />
			<Columns>
				<asp:TemplateField HeaderText="<%$Resources:Forum, PostAuthor %>" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<a name="<%#Eval("PostID") %>"></a>
						<strong><%#Eval("Author", "{0}") %></strong><br />
						<%#Eval("Created", "{0:g}")%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<%$Resources:Forum, PostBody %>" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<%#formatBody((string)Eval("Body")) %>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<cc1:Pager runat="server" ID="pagPosts" ControlToPage="gvPosts" SkinID="blog" PageSize="10"/>
		<br />
		<div>
			<div style="float: left;"><asp:Button runat="server" ID="btnBackToThreads2" Text="<%$Resources:Forum, PostBackToThreadButton %>" /></div>
			<div style="float: right;"><asp:Button runat="server" ID="btnReply" Text="<%$Resources:Forum, PostReply %>" OnClick="btnReply_Click" /></div>
			<div style="clear: both;"></div>
		</div>
		
		<asp:Panel ID="pnlNewPost" Visible="false" runat="server">
			<table>
				<tr>
					<td class="fieldlabel"><asp:Label ID="Label1" runat="server" Text="<%$Resources:Forum, PostBody %>" AssociatedControlID="txtNewPostBody"/></td>
					<td class="field">
						<asp:RequiredFieldValidator runat="server" ID="valNewPostBody" ControlToValidate="txtNewPostBody" ErrorMessage="<%$Resources:Forum, PostErrorBodyMissing %>" Display="Dynamic" ValidationGroup="NewPost" />
						<asp:TextBox ID="txtNewPostBody" runat="server" TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td colspan="2" align="right">
						<asp:Button ID="btnCancelNewPost" runat="server" Text="<%$Resources:StringsRes, glb__Cancel %>" OnClick="btnCancelNewPost_Click" CausesValidation="false" />
						<asp:Button ID="btnSaveNewPost" runat="server" Text="<%$Resources:StringsRes, glb__Save %>" OnClick="btnSaveNewPost_Click" ValidationGroup="NewPost" />
					</td>
				</tr>
			</table>
		</asp:Panel>
	</asp:View>
	<asp:View ID="vwEdit" runat="server">
		<table border="0">
			<tr>
				<td class="field"><asp:CheckBox runat="server" ID="chkAllowAnonymousPosts" Text="<%$Resources:Forum, AdminAllowAnonymousPosts %>" /></td>
			</tr>
			<tr>
				<td class="field"><asp:CheckBox runat="server" ID="chkEnableModeration" Text="<%$Resources:Forum, AdminEnableModeration %>" /></td>
			</tr>
			<tr>
				<asp:Button runat="server" ID="btnSaveSettings" Text="<%$Resources:StringsRes, glb__Save %>" OnClick="btnSaveSettings_Click" />
			</tr>
		</table>
		<asp:Panel runat="server" ID="pnlApproval">
			<p>&nbsp;</p>
			<h2><asp:Localize runat="server" Text="<%$Resources:Forum, AdminThreadsToApprove %>"/></h2>
			<asp:GridView runat="server" ID="gvThreadsToApprove" AutoGenerateColumns="false">
				<RowStyle CssClass="odd" />
				<AlternatingRowStyle CssClass="even" />
				<EmptyDataTemplate>
					<asp:Localize runat="server" Text="<%$Resources:Forum, AdminNothingToApprove %>" />
				</EmptyDataTemplate>
				<Columns>
					<asp:TemplateField HeaderText="Approve" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="70px">
						<ItemTemplate>
							<asp:CheckBox runat="server" ID="chkApprove" />
							<asp:HiddenField runat="server" ID="hdfThreadID" Value='<%#Eval("ThreadID") %>' />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:BoundField DataField="Subject" HeaderText="<%$Resources:Forum, ThreadSubject %>" HeaderStyle-HorizontalAlign="Left" />
				</Columns>
			</asp:GridView>
			<asp:Button runat="server" ID="btnApproveThreads" Text="<%$Resources:Forum, AdminApproveSelectedThreads %>" OnClick="btnApproveThreads_Click" />
			<p>&nbsp;</p>
			<h2><asp:Localize runat="server" Text="<%$Resources:Forum, AdminPostsToApprove %>"/></h2>
			<asp:GridView runat="server" ID="gvPostsToApprove" AutoGenerateColumns="false">
				<RowStyle CssClass="odd" />
				<AlternatingRowStyle CssClass="even" />
				<EmptyDataTemplate>
					<asp:Localize runat="server" Text="<%$Resources:Forum, AdminNothingToApprove %>" />
				</EmptyDataTemplate>
				<Columns>
					<asp:TemplateField HeaderText="<%$Resources:Forum, Approve %>" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="70px">
						<ItemTemplate>
							<asp:CheckBox runat="server" ID="chkApprove" />
							<asp:HiddenField runat="server" ID="hdfPostID" Value='<%#Eval("PostID") %>' />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:BoundField DataField="Body" HeaderText="<%$Resources:Forum, PostBody %>" HeaderStyle-HorizontalAlign="Left" />
				</Columns>
			</asp:GridView>
			<asp:Button runat="server" ID="btnApprovePosts" Text="<%$Resources:Forum, AdminApproveSelectedPosts %>" OnClick="btnApprovePosts_Click" />
		</asp:Panel>
	</asp:View>
</asp:MultiView>
<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MetablogPreview.aspx.cs" Inherits="MetablogPreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
	<table border="0" width="100%" class="blogDetail">
		<tr>
			<td class="title">{post-title}</td>
		</tr>
		<tr>
			<td class="content">{post-body}</td>
		</tr>
	</table>
</asp:Content>
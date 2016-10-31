<%@ Page Language="C#" AutoEventWireup="true" CodeFile="keywords.aspx.cs" Inherits="keywords" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% = _title %></title>

    <script type="text/javascript"><!--

function getRefToDivMod( divID, oDoc ) {
	if( !oDoc ) { oDoc = document; }
	if( document.layers ) {
		if( oDoc.layers[divID] ) { return oDoc.layers[divID]; } else {
			for( var x = 0, y; !y && x < oDoc.layers.length; x++ ) {
				y = getRefToDivNest(divID,oDoc.layers[x].document); }
			return y; } }
	if( document.getElementById ) { return oDoc.getElementById(divID); }
	if( document.all ) { return oDoc.all[divID]; }
	return oDoc[divID];
}

function resizeWinTo( idOfDiv ) {
	var oH = getRefToDivMod( idOfDiv ); if( !oH ) { return false; }
	var x = window; x.resizeTo( screen.availWidth, screen.availWidth );
	var oW = oH.clip ? oH.clip.width : oH.offsetWidth;
	var oH = oH.clip ? oH.clip.height : oH.offsetHeight; if( !oH ) { return false; }
	x.resizeTo( oW + 200, oH + 200 );
	var myW = 0, myH = 0, d = x.document.documentElement, b = x.document.body;
	if( x.innerWidth ) { myW = x.innerWidth; myH = x.innerHeight; }
	else if( d && d.clientWidth ) { myW = d.clientWidth; myH = d.clientHeight; }
	else if( b && b.clientWidth ) { myW = b.clientWidth; myH = b.clientHeight; }
	if( window.opera && !document.childNodes ) { myW += 16; }
	//second sample, as the table may have resized
	var oH2 = getRefToDivMod( idOfDiv );
	var oW2 = oH2.clip ? oH2.clip.width : oH2.offsetWidth;
	var oH2 = oH2.clip ? oH2.clip.height : oH2.offsetHeight;
	x.resizeTo( oW2 + ( ( oW + 200 ) - myW ), oH2 + ( (oH + 200 ) - myH ) );
}

//--></script>

</head>
<body dir="<%= _direction %>" id="body1" onload="resizeWinTo('divKeys');" style="padding: 0; margin: 0;">
    <form id="form1" runat="server">
        <div style="position: absolute; left: 0px; top: 0px;" id="divKeys" class="keys">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td colspan="2">
                        <table border="0" cellpadding="6" cellspacing="0" class="tagFrame" width="240">
                            <tr>
                                <td colspan="2" align="left" class="tagTitle">
                                    <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:stringsRes, pge_Keywords_Title %>"></asp:Localize></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:CheckBoxList runat="server" ID="cblTags" DataTextField="Name" 
                                        DataValueField="Guid" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="tagListItem">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 4px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left"><asp:Button runat="server" ID="btNewTag" Text="<%$ Resources:stringsRes, glb__add %>" OnClick="btNewTag_Click" />&nbsp;<asp:TextBox runat="server"
                                        ID="tbNewTag" Width="80px"></asp:TextBox></td>
                                <td align="right"><asp:Button runat="server" ID="btSave" Text="<%$ Resources:stringsRes, glb__Save %>" OnClick="btSave_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 4px;">
                    </td>
                </tr>
                <tr>
                    <td align="left"><nobr><asp:Label ID="lbSaved" runat="server" ForeColor="red"></asp:Label>
                      <asp:CustomValidator  Display="Dynamic" ID="valTags" runat="server" ErrorMessage="<%$ Resources:stringsRes, pge_Keywords_ErrorMessageTagExists %>" OnServerValidate="valTags_ServerValidate"></asp:CustomValidator></nobr>
                    </td>
                    <td class="close">
                        <a href="#" onclick="javascript:window.close();"><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:stringsRes, glb__Close %>"></asp:Localize></a>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

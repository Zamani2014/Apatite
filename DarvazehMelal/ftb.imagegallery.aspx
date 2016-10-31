<%@ Page Language="C#" ValidateRequest=false Trace="false" %>
<%@ Register TagPrefix="cc1" Namespace="MyWebPagesStarterKit.Controls" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<script runat="server">
	protected override void OnLoad(EventArgs e)
	{
        string rif = Request.QueryString["rif"].ToLower();
        string cif = Request.QueryString["cif"].ToLower();
        string allowedBasePath = ResolveUrl("~/App_Data/UserImages/Image").ToLower();
        
        if(!rif.StartsWith(allowedBasePath) || !cif.StartsWith(allowedBasePath) || rif.Contains("..") || rif.Contains(".."))
        {
            Response.Redirect("~/Default.aspx");   
        }
		base.OnLoad(e);
	}
</script>

<html>
<head>
	<title>Image Gallery</title>
</head>
<body>

    <form id="Form1" runat="server" enctype="multipart/form-data">  
		<FTB:ImageGallery
			runat="server"
			ID="ImageGallery1" 
			AllowImageDelete="true" 
			AllowImageUpload="true" 
			AllowDirectoryCreate="true" 
			AllowDirectoryDelete="true"
		/>
	</form>

</body>
</html>

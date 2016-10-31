<%@ Control Language="C#" AutoEventWireup="false" CodeFile="HtmlEditor.ascx.cs" Inherits="SectionControls_HtmlEditor" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<FTB:FreeTextBox 
	runat="server" 
	ID="ftbEditor" 
	Width="100%"
	ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImageFromGallery|Cut,Copy,Paste,Delete;Undo,Redo,Print|SymbolsMenu|InsertRule,InsertDate,InsertTime;InsertTable"
/>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" MasterPageFile="~/Site.master" %>
<%@ Register Assembly="GoogleMap" Namespace="GoogleMap" TagPrefix="GoogleMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">

        <fieldset>
    <div>
        <GoogleMap:GoogleMap ID="GoogleMap1" runat="server">
        </GoogleMap:GoogleMap>
    </div>
    </fieldset>
    </asp:Content>
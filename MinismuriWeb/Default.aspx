<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <h1>Willkommen</h1>
    <hr />

    <asp:Literal ID="welcomeTextLiteral" runat="server" />

    <br />
    <br />

    <asp:Image ID="titelImage" ImageUrl="/Images/HomeImage.jpg" runat="server" style="max-height:800px; max-width:980px;" />
    
</asp:Content>

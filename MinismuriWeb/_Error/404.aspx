<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="MinismuriWeb._Error._404" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <h1>Uuuups....</h1>
    <hr />

    Irgendetwas ist wohl falsch gelaufen. Diese Seite ist wohl nicht vorhanden.
    <br />
    <a href="javascript:history.back();">Zur&uuml;ck</a> zur vorherigen Seite.
    

</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Generic.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #contentDiv img {
            max-width:960px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ToolkitScriptManager runat="server" />

    <h1>
        <%= Titel %>
    </h1>

    <hr style="clear:both" />

    <div id="contentDiv">
        <%= Server.HtmlDecode(Content) %>
    </div>
    <div style="clear:both">
    </div>
   
    <div style="text-align:right; font-size:80%">

        Letzte &Auml;nderung <%= Datum.ToString("dd.MM.yyyy HH:mm") %> 

        <span style="clear:both;"></span>
    </div>

    <div style="clear:both">
    </div>

</asp:Content>

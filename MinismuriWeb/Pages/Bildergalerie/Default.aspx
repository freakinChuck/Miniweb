<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Bildergalerie.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />    
    
    <div style="float:right" runat="server" id="linkDivs">
        <a href="Default.aspx">alle Galerien</a> <br />
        <a id="downloadLink" runat="server">Bilder herunterladen (.zip)</a>
    </div>


    <h1>Bildergalerie <%= !string.IsNullOrWhiteSpace(Galerie) ? string.Format("({0})", Galerie) : string.Empty %> </h1>

    <hr style="clear:both" />

    <asp:PlaceHolder ID="galerieItemsPlaceholder" runat="server" />

    <div style="clear:both">
    </div>

</asp:Content>

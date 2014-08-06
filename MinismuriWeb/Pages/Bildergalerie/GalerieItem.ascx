<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalerieItem.ascx.cs" Inherits="MinismuriWeb.Pages.Bildergalerie.GalerieItem" %>

<div class="ImageGalerieItem">
   
    <a class="ImageLink" href="Default.aspx?Galerie=<%= GalerieName %>">
        <img src="<%= string.Format("{0}/{1}/thumb/{2}", GalerieRootFolder, GalerieName, GalerieDisplayItemName) %>" alt="<%= GalerieName %>" />
    </a>
    
    <a href="Default.aspx?Galerie=<%= GalerieName %>">
        <p><%= GalerieName %></p>
    </a>

    <div style="clear:both"></div>
</div>
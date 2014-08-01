<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="MinismuriWeb.Admin.EventAnmeldung.Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

        .displayLabel
        {
            width:250px;
            display:inline-block;
            margin-bottom:10px; 
            vertical-align:top;
                              
        }
        .displayContent
        {
            width:350px;
            display:inline-block;    
            margin-bottom:10px;       
        }
        .displayContent input,
        .displayContent textarea
        {
            width:100%;
        }
        .displayContainer
        {
            width:605px;
            vertical-align:top;
        }

        .errorcontainer
        {
            background:#FFB8B8;
            border: 1px solid red;
            margin-bottom:10px;
            padding:10px;
        }

    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="Default.aspx" style="float:right; margin-right:20px">Eventverwaltung</a>

    <asp:ScriptManager runat="server" />    

    <h2><%= !IsNew ? "Details '" + Eventname + "'" : "neuer Event" %></h2>
    <hr />

    <div class="displayContainer" id="displayDiv" runat="server">
        
        <span class="displayLabel">Event</span>
        <span class="displayContent"><%= Eventname %></span>

        <span class="displayLabel">Anmeldefrist</span>
        <span class="displayContent"><%= string.Format("{0:dd.MM.yyyy}", Anmeldeschluss) %></span>

        <span class="displayLabel">Emaillink</span>
        <span class="displayContent"><a href="<%= LinkUrl %>"><%= LinkUrl %></a></span>


        <span class="displayLabel">Beschreibung</span>
        <span class="displayContent"><%= Beschreibung %></span>

        <asp:LinkButton Text="Löschen" runat="server" ID="loeschenLinkButton" OnClick="loeschenLinkButton_Click" style="float:right;" OnClientClick="return confirm('Sind sie sicher, dass sie diesen Event löschen möchten?')" />
        <a href="Detail.aspx?Event=<%=EventId %>&Edit=True" style="float:right; margin-right:10px">Bearbeiten</a>


    </div>

    <div class="displayContainer" id="editDiv" runat="server">
        
        <span class="displayLabel">Event</span>
        <span class="displayContent"><asp:TextBox ID="eventNameTextBox" runat="server" /></span>

        
        <span class="displayLabel">Anmeldefrist</span>
        <span class="displayContent"><asp:TextBox ID="anmeldefristTextBox" runat="server" /></span>

        <span class="displayLabel">Beschreibung</span>
        <span class="displayContent"><asp:TextBox ID="beschreibungTextBox" TextMode="MultiLine" Rows="10" runat="server" /></span>

        <a style="float:right;" href='<%= IsEdit ? "Detail.aspx?Event=" + EventId : "Default.aspx" %>'>Abbrechen</a>
        <asp:LinkButton Text="Speichern" runat="server" ID="speichernButton" style="float:right; margin-right:20px;" OnClick="speichernButton_Click" />
    
    </div>

</asp:Content>

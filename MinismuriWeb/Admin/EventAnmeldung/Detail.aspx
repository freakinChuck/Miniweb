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

        pre
        {
            font-family:inherit;
            line-height:150%;
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

        <span class="displayLabel">Email Verantwortl.</span>
        <span class="displayContent"><%= VerantwortlicherMail %></span>

        <span class="displayLabel">Emaillink</span>
        <span class="displayContent"><a href="<%= LinkUrl %>"><%= LinkUrl %></a></span>


        <span class="displayLabel">Beschreibung</span>
        <span class="displayContent"><pre><%= Beschreibung %></pre></span>

        <asp:LinkButton Text="Löschen" runat="server" ID="loeschenLinkButton" OnClick="loeschenLinkButton_Click" style="float:right;" OnClientClick="return confirm('Sind sie sicher, dass sie diesen Event löschen möchten?')" />
        <a href="Detail.aspx?Event=<%=EventId %>&Edit=True" style="float:right; margin-right:10px">Bearbeiten</a>


        <h2>Anmeldungen</h2>
        <hr />

        <asp:Repeater ID="anmeldungenRepeater" runat="server">
            <ItemTemplate>
                <div style="font-size:80%">
                    <span style="width:200px;display:inline-block"><a href="mailto:<%# Eval("Email") %>"><%# Eval("Name") %></a></span>
                    <span style="width:120px;display:inline-block"><%# Eval("Zeitpunkt", "{0:dd.MM.yyyy HH:mm}") %></span>
                    <span style="width:260px;display:inline-block"><%# Eval("Bemerkung") %></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <h2>Abmeldungen</h2>
        <hr />

        <asp:Repeater ID="abmeldungenRepeater" runat="server">
            <ItemTemplate>
                <div style="font-size:80%">
                    <span style="width:200px;display:inline-block"><a href="mailto:<%# Eval("Email") %>"><%# Eval("Name") %></a></span>
                    <span style="width:120px;display:inline-block"><%# Eval("Zeitpunkt", "{0:dd.MM.yyyy HH:mm}") %></span>
                    <span style="width:260px;display:inline-block"><%# Eval("Bemerkung") %></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>


        <asp:LinkButton Text="Anmeldung exportieren" ID="exportButton" runat="server" style="float:right;" OnClick="exportButton_Click" />


        <div style="clear:both"></div>
    </div>

    <div class="displayContainer" id="editDiv" runat="server">
        
        <span class="displayLabel">Event</span>
        <span class="displayContent"><asp:TextBox ID="eventNameTextBox" runat="server" /></span>
        


        <span class="displayLabel">Anmeldefrist</span>
        <span class="displayContent"><asp:TextBox ID="anmeldefristTextBox" runat="server" /></span>        
        
        <span class="displayLabel">Email Verantwortl.</span>
        <span class="displayContent"><asp:TextBox ID="verantwortlicherTextBox" runat="server" /></span>


        <span class="displayLabel">Beschreibung</span>
        <span class="displayContent"><asp:TextBox ID="beschreibungTextBox" TextMode="MultiLine" Rows="10" runat="server" /></span>

        <a style="float:right;" href='<%= IsEdit ? "Detail.aspx?Event=" + EventId : "Default.aspx" %>'>Abbrechen</a>
        <asp:LinkButton Text="Speichern" runat="server" ID="speichernButton" style="float:right; margin-right:20px;" OnClick="speichernButton_Click" />
    
    </div>

</asp:Content>

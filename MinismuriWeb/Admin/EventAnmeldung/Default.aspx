<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Admin.EventAnmeldung.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <h1>Eventverwaltung</h1>
    <hr />

        <h3>offene Events</h3>
        <hr />

    <asp:Repeater ID="openEventRepeater" runat="server">
        <ItemTemplate>


            <div style="border-bottom:1px dotted #ddd">
                <span style="display:inline-block;width:400px;"><%# Eval("Name") %></span>
                <span style="display:inline-block;width:500px; font-style:italic; font-size:80%">Anmeldeschluss <%# Eval("AnmeldefristEnde", "{0:dd.MM.yyyy}") %></span>
                <a href="Detail.aspx?Event=<%# Eval("Id") %>">Details</a>
            </div>

        </ItemTemplate>
    </asp:Repeater>

        <h3>Events mit abgelaufener Anmeldefrist</h3>
        <hr />

    <asp:Repeater ID="closedEventRepeater" runat="server">
        <ItemTemplate>


            <div style="border-bottom:1px dotted #ddd">
                <span style="display:inline-block;width:400px;"><%# Eval("Name") %></span>
                <span style="display:inline-block;width:500px; font-style:italic; font-size:80%">Anmeldeschluss <%# Eval("AnmeldefristEnde", "{0:dd.MM.yyyy}") %></span>
                <a href="Detail.aspx?Event=<%# Eval("Id") %>">Details</a>
            </div>

        </ItemTemplate>
    </asp:Repeater>

    <asp:LinkButton Text="neuer Event" runat="server" style="float:right;margin-top:20px;margin-right:20px" ID="newEventLinkButton" OnClick="newEventLinkButton_Click"/>


</asp:Content>

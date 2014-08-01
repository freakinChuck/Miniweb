<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Admin.Benutzer.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <h2>Benutzerverwaltung</h2>
    <hr />

    <asp:Repeater ID="userRepeater" runat="server">
        <ItemTemplate>


            <div style="border-bottom:1px dotted #ddd">
                <span style="display:inline-block;width:400px;"><%# Eval("Benutzername") %> <%# !(bool)Eval("Aktiv") ? "(Deaktiviert)" : string.Empty %></span>
                <span style="display:inline-block;width:500px; font-style:italic; font-size:80%">Erstellt von: <%# Eval("Ersteller") %></span>
                <a href="Detail.aspx?User=<%# Eval("Benutzername") %>">Details</a>
            </div>

        </ItemTemplate>
    </asp:Repeater>

    <asp:LinkButton Text="neuer Benutzer" runat="server" style="float:right;margin-top:20px" OnClick="newBenutzerLinkButton_Click" ID="newBenutzerLinkButton"/>

</asp:Content>

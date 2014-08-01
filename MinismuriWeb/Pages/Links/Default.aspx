<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Links.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />    

    <h1>Links</h1>
    <hr />

    <asp:Repeater ID="linksRepeater" runat="server">
        <ItemTemplate>

            <h4><%# Eval("Titel") %></h4>
            <%# !string.IsNullOrWhiteSpace((string)Eval("Beschreibung")) ? string.Format("<i>{0}</i> <br />", Eval("Beschreibung")) : string.Empty%>
            <a target="_blank" href="<%# Eval("Link") %>"><%# Eval("Link") %></a>

        </ItemTemplate>
    </asp:Repeater>

</asp:Content>

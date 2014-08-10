<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="MasterPageTest.aspx.cs" Inherits="MinismuriWeb.Trial.MasterPageTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <h1>Test Hallo :-)</h1>
    <hr />
    <asp:Literal id="testLiteral" runat="server" />
    <br />
    Utc Now: <%= DateTime.UtcNow.ToString() %>
    <hr />
    Last Run: <%= MinismuriWeb.AnmeldungslisteVersendenTask.Instance.LastRunTimestamp %> 
    <br />
    Next Run: <%= MinismuriWeb.AnmeldungslisteVersendenTask.Instance.NextRunAb %> 
    <br />
    Run Status: <%= MinismuriWeb.AnmeldungslisteVersendenTask.Instance.LastRunStatus %> 

</asp:Content>

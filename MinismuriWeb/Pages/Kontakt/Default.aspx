<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Kontakt.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function Kontakt() {
            window.location = 'mailto:' + 'admin' + '@' + 'minismuri.ch';
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" /> 

    <h1>Kontakt</h1>

    <hr />

    Bei Fragen wenden Sie sich an den Webmaster:

    <br />
    <br />

    <a href="javascript:Kontakt()">Email an den Webmaster</a>

</asp:Content>

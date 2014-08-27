<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="MinismuriWeb.Pages.Anmeldung.Success" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />    

    <h1><%= Request["Anmeldung"] == bool.TrueString ? "Anmeldung" : "Abmeldung" %> "<%= Request["Name"] %>" erfolgreich</h1>
    <hr />
    Vielen Dank für Ihre <%= Request["Anmeldung"] == bool.TrueString ? "Anmeldung" : "Abmeldung" %>. <br />
    Sie werden in K&uuml;rze ein Bestätigungsemail erhalten. <br />
    <i style="font-size:80%">
        Es bestehen aktuell <b>technische Probleme</b> beim Versenden des Bestätigungsmails an Domains wie <b>hotmail.com</b> oder <b>outlook.com</b>. <br />
        Falls Sie daher <b>kein Bestätigungsmail</b> erhalten, versichern wir Ihnen, dass Ihre Angaben <b>trotzdem</b> bei uns <b>eingetroffen</b> sind!
    </i>
    <br />
    <a href="/">Hier zurück zur Startseite.</a>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditText.aspx.cs" Inherits="MinismuriWeb.Admin.Bildergalerie.EditText" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/_Global/Master.css" rel="stylesheet" type="text/css" />
    <link href="/_Content/Content.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterCustomContentDiv" style="height:250px; min-height:250px;">
        <h1>Begleittext <%= Galerie %></h1>    
        <hr />

        <asp:TextBox ID="contentTextBox" runat="server" TextMode="MultiLine"
            Rows="8" Width="99%" />

        <asp:LinkButton Text="Speichern" ID="speichernButton" OnClick="speichernButton_Click" runat="server" style="float:right" />

    </div>
    </form>
</body>
</html>

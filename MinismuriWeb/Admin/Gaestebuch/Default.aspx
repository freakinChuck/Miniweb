<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Admin.Gaestebuch.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
    
        pre 
        {
         white-space: pre-wrap;       /* css-3 */
         white-space: -moz-pre-wrap;  /* Mozilla, since 1999 */
         white-space: -pre-wrap;      /* Opera 4-6 */
         white-space: -o-pre-wrap;    /* Opera 7 */
         word-wrap: break-word;       /* Internet Explorer 5.5+ */
        }

        .GaestebucheintragFieldset
        {
            padding-left:10px;
            color:Black;
            font-size:0.8em;
            margin-bottom:10px;
            margin-top:10px;
        }
        .GaestebucheintragFieldset pre
        {
            font-family:inherit;
            line-height:150%;
        }
        .Gaestebucheintraglegend
        {
            padding-left:5px;
            padding-right:5px;
        }
    
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />    

    <h1>G&auml;stebuch</h1>
    <hr />

    <div style="float:right; width:120px;">
    
        <a href="../../Pages/Gaestebuch/NeuerEintrag.aspx">Eintrag schreiben</a>        

    </div>
    
    <div style="clear:both"> </div>

    <asp:Repeater ID="gaestebuchEintraegeRepeater" runat="server">
        <ItemTemplate>

            <fieldset class="GaestebucheintragFieldset">
            
                <legend><%# Eval("Name") %> (<%# ((DateTime)Eval("Datum")).ToString("dd.MM.yyyy HH:mm") %>)</legend>

                <pre><%# Eval("Nachricht")  %></pre>

                <asp:LinkButton 
                    Text="Entfernen" 
                    OnClientClick="return confirm('Sind sie sich sicher, dass Sie diesen Eintrag löschen möchten?')" 
                    runat="server" CommandArgument='<%# Eval("Id") %>' 
                    OnCommand="entfernenButton_Command" 
                    ID="entfernenButton" />

            </fieldset>

        </ItemTemplate>
    </asp:Repeater>

    

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Admin.Aktuelles.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        
        .displayLabel
        {
            width:150px;
            display:inline-block;
            margin-bottom:10px; 
            vertical-align:top;
                              
        }
        .inputBox
        {
            width:350px;
            display:inline-block;    
            margin-bottom:10px;       
        }
        .inputBox input, .inputBox textarea
        {
            width:100%;
        }
        .inputContainer
        {
            width:505px;
            vertical-align:top;
        }
        
        .invalidInput
        {
            border:1px solid red;
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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <h1>Termine</h1>
    <hr />


    <asp:Repeater ID="terminRepeater" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="background-image:url('/Images/Kalender.png'); width:85px; height:73px;text-align:center">
                    <b style="color:White; font-size:8pt; padding-top:10px; display:block;"><%# Eval("Monat") %></b>
                    <b style="color:Black; font-size:15pt; padding-top:5px; display:block"><%# Eval("Tag") %></b>
                </td>
                <td style="vertical-align:middle">
                    <p style="margin-left:20px; font-weight:bold"><%# Eval("Titel") %></p>
                    <i style="margin-left:20px;">Angezeigt: <%# Eval("ZeigenAb", "{0:d}")%> - <%# Eval("ZeigenBis", "{0:d}")%> </i>
                </td>
                <td style="padding-left:20px;">
                    <asp:LinkButton 
                        Text="Entfernen" 
                        OnClientClick="return confirm('Sind sie sich sicher, dass Sie diesen Eintrag löschen möchten?')" 
                        runat="server" CommandArgument='<%# Eval("Id") %>' 
                        OnCommand="entfernenButton_Command" 
                        ID="entfernenButton" />
                </td>
            </tr>
            <tr>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>


    <h2>Ereignis erstellen</h2>
    <hr />

    <div class="errorcontainer" <%= string.IsNullOrWhiteSpace(errorLiteral.Text) ? "style=\"display:none\"" : string.Empty %>> 
        <span>
            <asp:Literal id="errorLiteral" runat="server" />
        </span>
    </div>

    <div class="inputContainer">

        <span class="displayLabel">Titel</span>
        <span class="inputBox">
            <asp:TextBox ID="titelTextBox" runat="server" />
        </span>

        <span class="displayLabel">Monat</span>
        <span class="inputBox">
            <asp:TextBox ID="monatTextBox" runat="server" />
        </span>

        <span class="displayLabel">Tag oder Jahr</span>
        <span class="inputBox">
            <asp:TextBox ID="tagTextBox" runat="server" />
        </span>

        <span class="displayLabel">Anzeigen ab</span>
        <span class="inputBox">
            <asp:TextBox ID="zeigenAbTextBox" runat="server" />
        </span>

        <span class="displayLabel">Anzeigen bis</span>
        <span class="inputBox">
            <asp:TextBox ID="zeigenBisTextBox" runat="server" />
        </span>       

        <asp:LinkButton ID="speichernButton" Text="Speichern" runat="server" 
            style="float:right;" onclick="speichernButton_Click"  />

        <div style="clear:both"> </div>
    </div>


</asp:Content>

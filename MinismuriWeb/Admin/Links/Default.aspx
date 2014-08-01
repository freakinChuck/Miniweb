<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Admin.Links.Default" %>
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
    
    <asp:ScriptManager runat="server" />    

    <h1>Links</h1>
    <hr />

    <asp:Repeater ID="linksRepeater" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="width:80%">
                    <h4><%# Eval("Titel") %></h4>
                    <%# !string.IsNullOrWhiteSpace((string)Eval("Beschreibung")) ? string.Format("<i>{0}</i> <br />", Eval("Beschreibung")) : string.Empty%>
                    <a target="_blank" href="<%# Eval("Link") %>"><%# Eval("Link") %></a>
                </td>
                <td style="padding-left:20px;">
                        <asp:LinkButton 
                            Text="Entfernen" 
                            OnClientClick="return confirm('Sind sie sich sicher, dass Sie diesen Eintrag löschen möchten?')" 
                            runat="server" CommandArgument='<%# Eval("Link").ToString().ToLower() %>' 
                            OnCommand="entfernenButton_Command" 
                            ID="entfernenButton" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <h2>Link erstellen</h2>
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

        <span class="displayLabel">Beschreibung</span>
        <span class="inputBox">
            <asp:TextBox ID="bechreibungTextBox" runat="server" />
        </span>

        <span class="displayLabel">Link</span>
        <span class="inputBox">
            <asp:TextBox ID="linkTextBox" runat="server" />
        </span>

        <asp:LinkButton ID="speichernButton" Text="Speichern" runat="server" 
            style="float:right;" onclick="speichernButton_Click"  />

        <div style="clear:both"> </div>
    </div>

</asp:Content>

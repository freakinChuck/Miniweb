<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="NeuerEintrag.aspx.cs" Inherits="MinismuriWeb.Pages.Gaestebuch.NeuerEintrag" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:ScriptManager runat="server" />--%>    

    <asp:NoBot ID="noBot" runat="server" ResponseMinimumDelaySeconds="5" />

    <h1>G&auml;stebucheintrag erstellen</h1>
    <hr />

    <div class="errorcontainer" <%= string.IsNullOrWhiteSpace(errorLiteral.Text) ? "style=\"display:none\"" : string.Empty %>> 
        <span>
            <asp:Literal id="errorLiteral" runat="server" />
        </span>
    </div>

    <div class="inputContainer">

        <span class="displayLabel">Name</span>
        <span class="inputBox">
            <asp:TextBox ID="nameTextBox" runat="server" />
        </span>


        <%--<span class="displayLabel">Email</span>
        <span class="inputBox">
            <asp:TextBox ID="emailTextBox" runat="server" />
        </span>--%>

        <span class="displayLabel">Nachricht</span>
        <span class="inputBox">
            <asp:TextBox ID="nachtichtTextBox" TextMode="MultiLine" runat="server" Rows="10" />
        </span>

        <asp:LinkButton ID="speichernButton" Text="Speichern" runat="server" 
            style="float:right;" onclick="speichernButton_Click"  />

        <div style="clear:both"> </div>
    </div>

</asp:Content>

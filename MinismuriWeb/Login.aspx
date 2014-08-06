<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MinismuriWeb.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
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

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <h2>Administrationslogin</h2>
    <hr />

    
    <div class="errorcontainer" <%= string.IsNullOrWhiteSpace(errorLiteral.Text) ? "style=\"display:none\"" : string.Empty %>> 
        <span>
            <asp:Literal id="errorLiteral" runat="server" />
        </span>
    </div>

    <asp:Panel runat="server" DefaultButton="loginLinkButton">

        <div class="inputContainer">

            <span class="displayLabel">
                Benutzername
            </span>
            <span class="inputBox">
                <asp:TextBox ID="benutzernameTextBox" runat="server" />
            </span>

            <span class="displayLabel">
                Passwort
            </span>
            <span class="inputBox">
                <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" />
            </span>

            <asp:LinkButton Text="Login" runat="server" 
                style="float:right;" ID="loginLinkButton" OnClick="loginLinkButton_Click" />

            <div style="clear:both"> </div>
        </div>
    </asp:Panel>
</asp:Content>

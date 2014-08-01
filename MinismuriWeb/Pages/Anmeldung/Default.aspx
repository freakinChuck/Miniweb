<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Anmeldung.Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


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

    <h1>Anmeldung <%= EventName %></h1>
    <hr />

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:ScriptManager runat="server" />--%>    

    <asp:NoBot ID="noBot" runat="server" ResponseMinimumDelaySeconds="5" />

    <div class="errorcontainer" runat="server" id="nichtExistentDiv"> 
        <span>
            <asp:Literal runat="server" Text="Der von Ihnen ausgewählte Event existiert leider nicht mehr oder hat nie existiert." />
        </span>
    </div>

    <div class="errorcontainer" runat="server" id="anmeldefristAbgelaufenDiv"> 
        <span>
            <asp:Literal runat="server" Text="Für den von Ihnen ausgewählten Event ist die Anmeldefrist leider schon abgelaufen." />
        </span>
    </div>

    
    <div id="anmeldungDiv" runat="server">

        

    </div>

</asp:Content>

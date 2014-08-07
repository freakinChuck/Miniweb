<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Anmeldung.Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style type="text/css">
        
        .displayLabel
        {
            width:200px;
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
        .inputBox input:not([type=radio]), .inputBox textarea
        {
            width:100%;
        }
        .inputContainer
        {
            width:555px;
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
        
        pre
        {
            font-family:inherit;
            line-height:150%;
            font-style:italic;
        }
    
    </style>

    <h1><%= EventName %></h1>
    <hr />

    <pre id="beschreibungText" runat="server"></pre>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:ScriptManager runat="server" />--%>    

    <asp:NoBot ID="noBot" runat="server" ResponseMinimumDelaySeconds="5" />

    <div class="errorcontainer" <%= string.IsNullOrWhiteSpace(genericErrorLiteral.Text) ? "style=\"display:none\"" : string.Empty %>> 
        <span>
            <asp:Literal id="genericErrorLiteral" runat="server" />
        </span>
    </div>

    <div class="errorcontainer" runat="server" id="nichtExistentDiv"> 
        <span>
            <asp:Literal runat="server" Text="Der von Ihnen ausgewählte Event existiert leider nicht mehr oder hat nie existiert." />
        </span>
    </div>

    <div class="errorcontainer" runat="server" id="anmeldefristAbgelaufenDiv"> 
        <span>
            <asp:Literal ID="Literal1" runat="server" Text="Für den von Ihnen ausgewählten Event ist die Anmeldefrist leider schon abgelaufen." />
            <br />
            <asp:Literal runat="server" Text="Es besteht die Möglichkeit, dass bei einer verspäteten Eingabe die Anmeldung nicht mehr berücksichtigt wird." />
        </span>
    </div>

    
    <div id="anmeldungDiv" runat="server" class="inputContainer">

        <span class="displayLabel"></span>
        <span class="inputBox">
            <asp:RadioButton ID="anmeldungRadioButton" Text="Anmeldung" runat="server" Checked="true" GroupName="AnAbmeldung" />            
            <asp:RadioButton ID="abmeldungRadioButton" Text="Abmeldung" runat="server" GroupName="AnAbmeldung" />
        </span>

        <span class="displayLabel">Name</span>
        <span class="inputBox"><asp:TextBox ID="nameTextBox" runat="server" /></span>

        <span class="displayLabel">Email</span>
        <span class="inputBox"><asp:TextBox ID="emailTextBox" runat="server" /></span>

        <asp:Repeater ID="zusatzInfoRepeater" runat="server">
            <ItemTemplate>
                <span class="displayLabel"><%# Eval("Feldname") %></span>
                <span class="inputBox">
                    <asp:HiddenField ID="feldnameHiddenField" runat="server" Value='<%# Eval("Feldname") %>' />
                    <asp:HiddenField ID="idHiddenField" runat="server" Value='<%# Eval("Id") %>' />
                    <asp:HiddenField ID="typHiddenField" runat="server" Value='<%# Eval("Typ") %>' />
                    <asp:TextBox ID="freitextTextField" runat="server" Visible='<%# (int)Eval("Typ") == 0 %>' />
                    <asp:CheckBox ID="janeinCheckBox" runat="server" Visible='<%# (int)Eval("Typ") == 1 %>' />
                </span>
            </ItemTemplate>
        </asp:Repeater>

        <span class="displayLabel">Bemerkung</span>
        <span class="inputBox"><asp:TextBox ID="bemerkungTextBox" runat="server" /></span>

        <asp:LinkButton Text="Absenden" ID="speichernButton" runat="server" style="float:right" OnClick="speichernButton_Click" />

    </div>

        <i style="font-size:80%">Anmeldeschluss: <%= string.Format("{0:dd.MM.yyyy}", Anmeldeschluss) %></i>    

</asp:Content>

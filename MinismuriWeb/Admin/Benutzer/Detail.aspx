<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="MinismuriWeb.Admin.Benutzer.Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

        .displayLabel
        {
            width:200px;
            display:inline-block;
            margin-bottom:10px; 
            vertical-align:top;
                              
        }
        .displayContent
        {
            width:250px;
            display:inline-block;    
            margin-bottom:10px;       
        }
        .displayContainer
        {
            width:455px;
            vertical-align:top;
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

    <a href="Default.aspx" style="float:right; margin-right:20px">Benutzerverwaltung</a>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <h2><%= !IsNew ? "Details '" + Benutzername + "'" : "neuer Benutzer" %></h2>
    <hr />

    <div class="errorcontainer" <%= string.IsNullOrWhiteSpace(errorLiteral.Text) ? "style=\"display:none\"" : string.Empty %>> 
        <span>
            <asp:Literal id="errorLiteral" runat="server" />
        </span>
    </div>

    <div class="displayContainer" runat="server" id="detailDiv">

        <span class="displayLabel">Benutzername</span>
        <span class="displayContent"><%= Benutzername %></span>

        <span class="displayLabel">Aktiv</span>
        <span class="displayContent">
            <input type="checkbox" <%= (Aktiv ? "checked='checked'" : string.Empty) %> disabled="disabled" />
        </span>

        <div id="rollenDisplayDiv" runat="server">

            <span class="displayLabel">Benutzeradmin</span>
            <span class="displayContent">
                <asp:CheckBox id="benutzeradminDisplayCheckBox" runat="server" Enabled="false" />
            </span>

            <span class="displayLabel">Blogschreiber</span>
            <span class="displayContent">
                <asp:CheckBox id="blogschreiberDisplayCheckBox" runat="server" Enabled="false" />
            </span>

            <span class="displayLabel">Galerie Freigeber</span>
            <span class="displayContent">
                <asp:CheckBox id="bildergalerieFreigeberDisplayCheckBox" runat="server" Enabled="false" />
            </span>

            <span class="displayLabel">Galerie Uploader</span>
            <span class="displayContent">
                <asp:CheckBox id="bildergalerieUploaderDisplayCheckBox" runat="server" Enabled="false" />
            </span>

            <span class="displayLabel">G&auml;stebuchadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="gaestebuchAdminDisplayCheckBox" runat="server" Enabled="false" />
            </span>

            <span class="displayLabel">Linkadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="linkadminDisplayCheckBox" runat="server" Enabled="false" />
            </span>

            <span class="displayLabel">Terminadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="terminadminDisplayCheckBox" runat="server" Enabled="false" />
            </span>

            <span class="displayLabel">Eventanmeldungsadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="eventDisplayCheckBox" runat="server" Enabled="false" />
            </span>

            <span class="displayLabel">Videoadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="videoadminDisplayCheckBox" runat="server" Enabled="false" />
            </span>

            <span class="displayLabel">Seitenadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="seitenadminDisplayCheckBox" runat="server" Enabled="false" />
            </span>

        </div>

        <asp:LinkButton Text="Löschen" runat="server" ID="deleteLinkButton"
            style="float:right;" OnClick="deleteLinkButton_Click" />
        <asp:LinkButton Text="Bearbeiten" runat="server" ID="editLinkButton"
            style="float:right;margin-right:10px;" OnClick="editLinkButton_Click" />
        

    </div>
    
    <div class="displayContainer" runat="server" id="editDiv">

        <span class="displayLabel">Benutzername</span>
        <span class="displayContent"><%= Benutzername %></span>

        <span class="displayLabel">Aktiv</span>
        <span class="displayContent">
            <asp:CheckBox runat="server" ID="aktivCheckBox" />
        </span>

        <span class="displayLabel">Passwort</span>
        <span class="displayContent">
            <asp:TextBox runat="server" ID="passwortTextBox" TextMode="Password" />
        </span>

        <div id="rollenEditDiv" runat="server">

            <span class="displayLabel">Benutzeradmin</span>
            <span class="displayContent">
                <asp:CheckBox id="benutzeradminEditCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Blogschreiber</span>
            <span class="displayContent">
                <asp:CheckBox id="blogschreiberEditCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Galerie Freigeber</span>
            <span class="displayContent">
                <asp:CheckBox id="bildergalerieFreigeberEditCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Galerie Uploader</span>
            <span class="displayContent">
                <asp:CheckBox id="bildergalerieUploaderEditCheckBox" runat="server" />
            </span>

            <span class="displayLabel">G&auml;stebuchadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="gaestebuchAdminEditCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Linkadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="linkadminEditCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Terminadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="terminadminEditCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Eventanmeldungsadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="eventEditCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Videoadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="videoadminEditCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Seitenadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="seitenadminEditCheckBox" runat="server" />
            </span>

        </div>

        <span style="font-style:italic">Falls das Passwortfeld nicht leer ist, wird das aktuelle Passwort überschrieben.</span>



        <asp:LinkButton Text="Abbrechen" runat="server" ID="editAbbrechenLinkButton" 
            style="float:right;" OnClick="editAbbrechenLinkButton_Click" />
        <asp:LinkButton Text="Speichern" runat="server" ID="speichernLinkButton"
            style="float:right; margin-right:10px;" OnClick="speichernLinkButton_Click" />
        

    </div>

    <div class="displayContainer" runat="server" id="newDiv">

        <span class="displayLabel">Benutzername</span>
        <span class="displayContent">
            <asp:TextBox runat="server" ID="newBenutzernameTextBox" />
        </span>

        <span class="displayLabel">Aktiv</span>
        <span class="displayContent">
            <asp:CheckBox runat="server" ID="newAktivCheckBox" Checked="true" />
        </span>

        <span class="displayLabel">Passwort</span>
        <span class="displayContent">
            <asp:TextBox runat="server" ID="newPasswordTextBox" TextMode="Password" />
        </span>

        <div id="rollenNewDiv" runat="server">

            <span class="displayLabel">Benutzeradmin</span>
            <span class="displayContent">
                <asp:CheckBox id="benutzeradminNewCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Blogschreiber</span>
            <span class="displayContent">
                <asp:CheckBox id="blogschreiberNewCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Galerie Freigeber</span>
            <span class="displayContent">
                <asp:CheckBox id="bildergalerieFreigeberNewCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Galerie Uploader</span>
            <span class="displayContent">
                <asp:CheckBox id="bildergalerieUploaderNewCheckBox" runat="server" />
            </span>

            <span class="displayLabel">G&auml;stebuchadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="gaestebuchAdminNewCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Linkadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="linkadminNewCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Terminadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="terminadminNewCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Eventanmeldungsadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="eventNewCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Videoadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="videoadminNewCheckBox" runat="server" />
            </span>

            <span class="displayLabel">Seitenadmin</span>
            <span class="displayContent">
                <asp:CheckBox id="seitenadminNewCheckBox" runat="server" />
            </span>

        </div>


        <asp:LinkButton Text="Abbrechen" runat="server" ID="newAbbrechenLinkButton" 
            style="float:right;" OnClick="newAbbrechenLinkButton_Click" />
        <asp:LinkButton Text="Speichern" runat="server" ID="newSpeichernLinkButton"
            style="float:right; margin-right:10px;" OnClick="newSpeichernLinkButton_Click"/>
        
        <div style="clear:both"></div>

    </div>

</asp:Content>

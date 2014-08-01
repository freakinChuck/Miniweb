<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Admin.Blog.Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

        .invalidInput 
        {
            border:1px solid red;
        }
        .inputLabel 
        {
            width:300px;
            display:inline-block;
            vertical-align:top;
            margin-bottom:10px;
        }
        .inputBox 
        {
            width:600px;
            display:inline-block;
        }
            .inputBox input {
                width:100%;
            }
            .inputBox textarea {
                width:100%;
                height:300px;
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

    <h1>Blog Administration</h1>

    <hr />

    <h2>neuer Eintrag</h2>
    <hr />

    <div class="errorcontainer" <%= string.IsNullOrWhiteSpace(errorLiteral.Text) ? "style=\"display:none\"" : string.Empty %>> 
        <span>
            <asp:Literal id="errorLiteral" runat="server" />
        </span>
    </div>


    <span class="inputLabel">Titel</span>
    <span class="inputBox">
        <asp:TextBox runat="server" ID="titelTextBox" />
    </span>

    <span class="inputLabel">Startseiteneintrag</span>
    <span class="inputBox">
        <asp:CheckBox runat="server" Checked="true" ID="startseiteneintragCheckBox" />
    </span>

    <span class="inputLabel">Bericht</span>
    <span class="inputBox" id="contentContainer" runat="server">
        <asp:TextBox runat="server" ID="contentTextBox" TextMode="MultiLine" />
    <asp:HtmlEditorExtender ID="contentTextBox_HtmlEditorExtender" runat="server"
        Enabled="True" TargetControlID="contentTextBox" EnableSanitization="false"
        DisplaySourceTab="true">
        <Toolbar>
            <asp:Redo />
            <asp:Undo />
            <asp:Bold />
            <asp:Italic />
            <asp:Underline />
            <asp:StrikeThrough />
            <asp:JustifyLeft />
            <asp:JustifyCenter />
            <asp:JustifyRight />
            <asp:JustifyFull />
            <asp:InsertOrderedList />
            <asp:InsertUnorderedList />
            <asp:RemoveFormat />
            <asp:BackgroundColorSelector />
            <asp:ForeColorSelector />
            <asp:FontNameSelector />
            <asp:FontSizeSelector />
            <asp:InsertHorizontalRule />
        </Toolbar>
    </asp:HtmlEditorExtender>
    </span>

    <span class="inputLabel">optionales Bild</span>
    <span class="inputBox">
        <asp:FileUpload ID="bildUpload" runat="server" />
    </span>

    <asp:LinkButton Text="Speichern" runat="server" ID="saveNewEntryButton"
        style="float:right; margin-right:75px; margin-top:20px;" OnClick="saveNewEntryButton_Click" />
   
     <div style="clear:both"></div>


    <h2>ver&ouml;ffentlichte Einträge</h2>
    <hr />

    <asp:Repeater id="eintragRepeater" runat="server">
        <ItemTemplate>
            
            <div style="border-bottom:1px solid #aaa">
                
                <asp:LinkButton Text="Entfernen" runat="server" ID="loeschenButton" 
                        CommandArgument='<%# Eval("Id") %>' OnCommand="loeschenButton_Command"
                        OnClientClick="return confirm('Sind Sie sicher, dass Sie diesen Eintrag löschen möchten?')"
                        style="float:right" />

                <p style="font-weight:bold; text-decoration:underline; font-size:110%"><%# Eval("Titel") %> <%# (bool)Eval("Startseite") ? "(Startseiteneintrag)" : string.Empty %> </p>

                <%# Eval("Content") %>

                <div id="bildDiv" style="text-align:left" runat="server" Visible='<%# !string.IsNullOrEmpty((string)Eval("ImagePath")) %>'>
                    <img src="<%# "/UploadData/Blogbilder/" + Eval("ImagePath") %>" alt='<%# Eval("Titel") %>' style="max-width:500px; max-height:400px;" />
                </div>

                <div style="text-align:right; font-size:80%">

                    Erstellt am <%# ((DateTime)Eval("Datum")).ToString("dd.MM.yyyy HH:mm") %> von <%# Eval("Ersteller") %>

                    
                    <span style="clear:both;"></span>
                </div>

            </div>

        </ItemTemplate>
    </asp:Repeater>

</asp:Content>

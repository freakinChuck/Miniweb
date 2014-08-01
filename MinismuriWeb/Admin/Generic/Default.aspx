<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Admin.Generic.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>

        .invalidInput 
        {
            border:1px solid red;
        }
        input[type=text] {
            width:972px;
            height:30px;
            font-size:130%;
            margin-top:20px;
        }
        textarea {
            width:972px;
            height:600px;
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

    <div class="errorcontainer" <%= string.IsNullOrWhiteSpace(errorLiteral.Text) ? "style=\"display:none\"" : string.Empty %>> 
        <span>
            <asp:Literal id="errorLiteral" runat="server" />
        </span>
    </div>

    <h1>
        <asp:TextBox runat="server" ID="titelTextBox" />
    </h1>

    <hr />

    <asp:TextBox runat="server" ID="contentTextBox" TextMode="MultiLine" />
    <asp:HtmlEditorExtender ID="contentTextBox_HtmlEditorExtender" runat="server"
        Enabled="True" TargetControlID="contentTextBox" EnableSanitization="false"
        DisplaySourceTab="true" OnImageUploadComplete="contentTextBox_HtmlEditorExtender_ImageUploadComplete">
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
            <asp:InsertImage />
        </Toolbar>
    </asp:HtmlEditorExtender>

    <br />

    <asp:LinkButton Text="Speichern" runat="server" style="float:right" Id="speichernButton" OnClick="speichernButton_Click" />

    <div style="clear:both"></div>

</asp:Content>

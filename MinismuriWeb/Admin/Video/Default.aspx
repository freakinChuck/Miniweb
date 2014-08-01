<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Admin.Video.Default" %>
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

    
    <h1>Videoadministration</h1>
    <hr />

    <div <%= videosRepeater.Items.Count <= 0 ? "style=\"display:none\"" : string.Empty %>>
        <h3>Videos</h3>
        <hr />

        <asp:Repeater ID="videosRepeater" runat="server">
            <ItemTemplate>

                <div style="border-bottom:1px dashed #ddd">
                    <span style="width:276px; display:inline-block" title="Dateiname: '<%# Eval("FileName") %>'"><%# Eval("Name") %>
                    </span>
                    <span style="width:280px; font-style:italic; display:inline-block; font-size:80%"><%# (bool)Eval("Freigegeben") ? "Freigabe durch: " + Eval("Freigeber") : string.Empty %></span>
                    <span style="width:100px; display:inline-block">
                        <a href="/Pages/Videos/Default.aspx?VideoUrl=<%# Eval("FileName") %>" >Video &ouml;ffnen</a>
                    </span>
                    <span style="width:120px; display:inline-block">
                        <asp:LinkButton ID="videoFreigabeEntziehenButton" Text="Freig. entziehen" 
                            ForeColor="Orange" runat="server" 
                            CommandArgument='<%# Eval("FileName") %>' 
                            OnCommand="videoFreigabeEntziehenButton_Command"
                            OnClientClick='<%# "return confirm(\"Wollen Sie dem Video \\\"" + Eval("Name") + "\\\" wirklich die Freigabe entziehen?\")" %>' 
                            Visible='<%# (bool)Eval("Freigegeben") %>' />
                        <asp:LinkButton ID="videoFreigebenLinkButton" Text="Video freigeben" 
                            ForeColor="Green" runat="server" 
                            CommandArgument='<%# Eval("FileName") %>' 
                            OnCommand="videoFreigebenLinkButton_Command"
                            OnClientClick='<%# "return confirm(\"Wollen Sie das Video \\\"" + Eval("Name") + "\\\" wirklich freigeben?\")" %>'
                            Visible='<%# !(bool)Eval("Freigegeben") %>' />
                    
                    </span>
                    <span style="width:100px; display:inline-block">
                        <asp:LinkButton ID="videoLoeschenButton" Text="Video löschen" 
                            ForeColor="Red" runat="server" 
                            CommandArgument='<%# Eval("FileName") %>' 
                            OnCommand="videoLoeschenButton_Command"
                            OnClientClick='<%# "return confirm(\"Wollen Sie das Video \\\"" + Eval("Name") + "\\\" wirklich löschen?\")" %>' 
                            Visible='<%# !(bool)Eval("Freigegeben") %>' />
                    </span>

                    <span style="width:30px; display:inline-block; text-align:center">
                        <asp:ImageButton ImageUrl="~/Images/down.png"  runat="server"
                            Id="onceDownImageButton" 
                            CommandArgument='<%# Eval("FileName") %>' 
                            OnCommand="downImageButton_Command"
                            Visible='<%# !(bool)Eval("IsLast") %>'
                            Width="20px" />
                    </span>
                    <span style="width:30px; display:inline-block; text-align:center">
                        <asp:ImageButton ImageUrl="~/Images/up.png"  runat="server"
                            Id="onceUpImageButton" 
                            CommandArgument='<%# Eval("FileName") %>' 
                            OnCommand="upImageButton_Command"
                            Visible='<%# !(bool)Eval("IsFirst") %>'
                            Width="20px" />
                    </span>

                </div>

            </ItemTemplate>
        </asp:Repeater>
    </div>

    <h2>Video erstellen</h2>
    <hr />

    <div class="errorcontainer" <%= string.IsNullOrWhiteSpace(errorLiteral.Text) ? "style=\"display:none\"" : string.Empty %>> 
        <span>
            <asp:Literal id="errorLiteral" runat="server" />
        </span>
    </div>

    <div class="inputContainer">

        <span class="displayLabel">Anzeigename</span>
        <span class="inputBox">
            <asp:TextBox ID="anzeigenameTextBox" runat="server" />
        </span>

        <span class="displayLabel">Dateiname</span>
        <span class="inputBox">
            <asp:TextBox ID="dateinameTextBox" runat="server" />
        </span>

        <asp:LinkButton ID="speichernButton" Text="Speichern" runat="server" 
            style="float:right;" onclick="speichernButton_Click"  />

        <div style="clear:both"> </div>
    </div>    

    <span style="font-style:italic">
        Da die Videos zu gross für einen FileUpload sind, m&uuml;ssen sie manuell in das FTP Verzeichniss "Pages/Videos/VideoLibrary/" kopiert werden.
        Der Name der Datei muss dem Dateinamen im Eintrag entsprechen.
    </span>

</asp:Content>

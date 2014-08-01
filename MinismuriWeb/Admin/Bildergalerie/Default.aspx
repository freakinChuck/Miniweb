<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Admin.Bildergalerie.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function OpenNewBildergalerie() {
            var newGallerieName = $('#neueGalerieNameTextBox').val().trim();
            if (newGallerieName != '') {

                var uploadZiel = '<%= MinismuriWeb.UploadData.UploadType.Bildergalerie.ToString()  %>/' + newGallerieName;
                var handler = window.open('/Upload/FileUpload.aspx?Ziel=' + uploadZiel, newGallerieName + 'ImageUpload', 'toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=800,height=525')
                handler.onbeforeunload = function () {
                    window.location.reload();
                };
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />    

    

    <h1>Bildergalerieadministration</h1>
    <hr />

    <div <%= bestehendeGalerienRepeater.Items.Count <= 0 ? "style=\"display:none\"" : string.Empty %>>
        <h3>Bestehende Galerien</h3>
        <hr />

        <asp:Repeater ID="bestehendeGalerienRepeater" runat="server">
            <ItemTemplate>

                <div style="border-bottom:1px dashed #ddd">
                    <span style="width:276px; display:inline-block"><%# Eval("Name") %>
                    </span>
                    <span style="width:250px; font-style:italic; display:inline-block; font-size:80%">Freigabe durch: <%# Eval("Freigeber") %></span>
                    <span style="width:100px; display:inline-block">
                        <a href="/Pages/Bildergalerie/Default.aspx?Galerie=<%# Eval("Name") %>" >Galerie &ouml;ffnen</a>
                    </span>
                    <span style="width:120px; display:inline-block">
                        <asp:LinkButton ID="galerieFreigabeEntziehenButton" Text="Galerie schliessen" 
                            ForeColor="Orange" runat="server" 
                            CommandArgument='<%# Eval("Name") %>' 
                            OnCommand="galerieFreigabeEntziehenButton_Command"
                            OnClientClick='<%# "return confirm(\"Wollen Sie die Galerie \\\"" + Eval("Name") + "\\\" wirklich schliessen?\")" %>' 
                            Visible='<%# IsFreigeber %>' />
                    </span>
                    <span style="width:110px; display:inline-block">
                        <asp:LinkButton ID="galerieLoeschenButton" Text="Galerie löschen" 
                            ForeColor="Red" runat="server" 
                            CommandArgument='<%# Eval("Name") %>' 
                            OnCommand="galerieLoeschenButton_Command"
                            OnClientClick='<%# "return confirm(\"Wollen Sie die Galerie \\\"" + Eval("Name") + "\\\" wirklich löschen?\")" %>' 
                            Visible="false" />
                    </span>
                    <span style="width:80px; display:inline-block">
                        <a onclick='<%# "window.open(\"EditText.aspx?Galerie=" + Eval("Name")  + "\")" %>'
                            style="cursor:pointer">Begleittext</a>
                    </span>
                </div>

            </ItemTemplate>
        </asp:Repeater>
    </div>
    
    <div <%= freizugebendeGalerienRepeater.Items.Count <= 0 ? "style=\"display:none\"" : string.Empty %>>    
        <h3>nicht ver&ouml;ffentliche Galerie</h3>
        <hr />

        <asp:Repeater ID="freizugebendeGalerienRepeater" runat="server">
            <ItemTemplate>
                <div style="border-bottom:1px dashed #ddd">
                    <span style="width:530px; display:inline-block"><%# Eval("Name") %></span>
                    <span style="width:100px; display:inline-block">
                        <a href="/Pages/Bildergalerie/Default.aspx?Galerie=<%# Eval("Name") %>" >Galerie &ouml;ffnen</a>
                    </span>
                    <span style="width:120px; display:inline-block">
                        <asp:LinkButton ID="galerieFreigebenButton" Text="Galerie freigeben" 
                            ForeColor="Green" runat="server" 
                            CommandArgument='<%# Eval("Name") %>' 
                            OnCommand="galerieFreigebenButton_Command"
                            OnClientClick='<%# "return confirm(\"Wollen Sie die Galerie \\\"" + Eval("Name") + "\\\" wirklich freigeben?\")" %>'
                            Visible='<%# IsFreigeber %>' />
                    </span>
                    <span style="width:110px; display:inline-block">
                        <asp:LinkButton ID="galerieLoeschenButton" Text="Galerie löschen" 
                            ForeColor="Red" runat="server" 
                            CommandArgument='<%# Eval("Name") %>' 
                            OnCommand="galerieLoeschenButton_Command"
                            OnClientClick='<%# "return confirm(\"Wollen Sie die Galerie \\\"" + Eval("Name") + "\\\" wirklich löschen?\")" %>' />
                    </span>
                    <span style="width:80px; display:inline-block">
                        <a onclick='<%# "window.open(\"EditText.aspx?Galerie=" + Eval("Name")  + "\")" %>'
                            style="cursor:pointer">Begleittext</a>
                    </span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        
    </div>

    <fieldset style="width:320px; margin-top:30px;" id="neueGalerieFieldset" runat="server">
        <legend>neue Galerie</legend>

        <span style="width:100px; display:inline-block">Name</span>
        <input type="text" id="neueGalerieNameTextBox"  style="width:200px; display:inline-block" />
        <br />

        <a style="float:right" href="javascript:OpenNewBildergalerie()">Files hinzuf&uuml;gen</a>

        <div style="clear:both"></div>
    </fieldset>
    <span id="keineBerechtigungFreigabeSpan" runat="server" style="display:inline-block; font-style:italic; margin-top:30px;">Sie besitzen keine Berechtigung um Bildergalerien freizugeben.</span>
    <span id="keineBerechtigungUploadSpan" runat="server" style="display:inline-block; font-style:italic; margin-top:30px;">Sie besitzen keine Berechtigung um Bildergalerien zu erstellen.</span>

</asp:Content>

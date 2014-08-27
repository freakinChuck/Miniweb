<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Admin.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <h2>Administration</h2>
    <hr />

    <div style="width:500px; display:inline-block">
        <ul>
            <li><a href="/Admin/Benutzer/Default.aspx">Benutzerverwaltung</a></li>
            <li id="blogLi" runat="server"><a href="/Admin/Blog/Default.aspx">Blog</a></li>
            <li id="bildergalerieLi" runat="server"><a href="/Admin/Bildergalerie/Default.aspx">Bildergalerie</a></li>
            <li id="gaestebuchLi" runat="server"><a href="/Admin/Gaestebuch/Default.aspx">G&auml;stebuch</a></li>
            <li id="linksLi" runat="server"><a href="/Admin/Links/Default.aspx">Links</a></li>
            <li id="termineLi" runat="server"><a href="/Admin/Aktuelles/Default.aspx">Termine</a></li>
            <li id="eventanmeldungLi" runat="server"><a href="/Admin/EventAnmeldung/Default.aspx">Event Anmeldungen</a></li>
            <li id="videoLi" runat="server"><a href="/Admin/Video/Default.aspx">Videos</a></li>
        </ul>
    </div>
    
    <div style="width:300px; display:inline-block" runat="server" visible="false">
        <h4>Statistik</h4>
        <hr />

        <div><%= ZugriffeTag %> Zugriffe vergangene 24 Stunden</div>
        <div><%= ZugriffeWoche %> Zugriffe vergangene Woche</div>
        <div><%= ZugriffeMonat %> Zugriffe vergangenen Monat</div>
        <div><%= ZugriffeGesammt %> Zugriffe gesammt</div>

    </div>

    <div id="seitenadminDiv" runat="server">

        <h2>dynamische Seiten</h2>
        <hr />
        <asp:Repeater ID="genericNavigationRepeater" runat="server">
            <HeaderTemplate>
                <table style="width:980px;">
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="border-bottom: 1px dashed #ddd">
                    <td style='<%# (bool)Eval("IsChild") ? "padding-left:20px;" : string.Empty %>; width:350px'>
                        <a href='<%# Eval("Url") %>'><%# Eval("Title") %></a> <br />
                        <i style="font-size:80%"><%# Eval("User") %>, <%# Eval("Date", "{0:dd.MM.yyyy HH:mm}") %></i>
                    </td>
                    <td style="width:100px">
                        <asp:LinkButton ID="freigabeEntziehenLinkButton" Text="Verbergen" runat="server" 
                            Visible='<%# (bool)Eval("Published") && (bool)Eval("ParentPublished") %>' ForeColor="Red"
                            CommandArgument='<%# Eval("Id") %>' OnCommand="freigabeEntziehenLinkButton_Command" Width="100px"
                            OnClientClick="return confirm('Wollen Sie diese Seite wirklich Verbergen?')" />
                        <asp:LinkButton ID="freigebenLinkButton" Text="Freigeben" runat="server" 
                            Visible='<%# !(bool)Eval("Published") && (bool)Eval("ParentPublished") %>' ForeColor="Green"
                            CommandArgument='<%# Eval("Id") %>' OnCommand="freigebenLinkButton_Command" Width="100px"
                            OnClientClick="return confirm('Wollen Sie diese Seite wirklich freigeben?')" />
                    </td>
                    <td style="width:150px">

                        <asp:LinkButton ID="neuerChildEintragLinkButoon" Text="neue Unterseite" runat="server" 
                            Visible='<%# !(bool)Eval("IsChild") %>'
                            CommandArgument='<%# Eval("Id") %>'
                            OnCommand="neuerChildEintragLinkButoon_Command" />

                    </td>
                    <td style="width:100px">
                        <a href='Generic/Default.aspx?Edit=<%# Eval("Id") %>'>Bearbeiten</a>
                    </td>
                    <td style="width:100px">
                        <asp:LinkButton ID="loeschenLinkButton" Text="Löschen" runat="server" 
                            Visible='<%# !(bool)Eval("Published") %>'
                            CommandArgument='<%# Eval("Id") %>'
                            OnCommand="loeschenLinkButton_Command"
                            OnClientClick="return confirm('Wollen Sie diese Seite wirklich löschen?')" />
                    </td>
                    <td style="width:100px">
                        <span style="width:30px; display:inline-block; text-align:center">
                            <asp:ImageButton ImageUrl="~/Images/down.png"  runat="server"
                                Id="onceDownImageButton" 
                                CommandArgument='<%# Eval("Id") %>' 
                                OnCommand="onceDownImageButton_Command"
                                Visible='<%# !(bool)Eval("IsLast") %>'
                                Width="20px" />
                            &nbsp;
                        </span>
                        <span style="width:30px; display:inline-block; text-align:center">
                            <asp:ImageButton ImageUrl="~/Images/up.png"  runat="server"
                                Id="onceUpImageButton" 
                                CommandArgument='<%# Eval("Id") %>' 
                                OnCommand="onceUpImageButton_Command"
                                Visible='<%# !(bool)Eval("IsFirst") %>'
                                Width="20px" />
                            &nbsp;
                        </span>
                    </td>
                </tr>    
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <a style="float:right; margin-top:20px; margin-right:20px;" href="Generic/Default.aspx">neue Top-Seite</a>
        
        <div style="clear:both"></div>
    
    </div>

</asp:Content>

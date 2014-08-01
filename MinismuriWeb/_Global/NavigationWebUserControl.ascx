<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationWebUserControl.ascx.cs" Inherits="MinismuriWeb._Global.NavigationWebUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<div>
    
    <asp:Table runat="server" CellSpacing="0" CellPadding="0" Width="100%">
        <asp:TableRow ID="mainNavigationTableRow" runat="server">
        </asp:TableRow>
    </asp:Table>

    <asp:PlaceHolder ID="navigationExtraStuffPlaceholder" runat="server" />

<%--    <asp:RoundedCornersExtender ID="leftPanel_RoundedCornersExtender" 
        runat="server" Enabled="True" Radius="5" Corners="Left" TargetControlID="leftPanel">
    </asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="rightPanel_RoundedCornersExtender" 
        runat="server" Enabled="True" Radius="5" Corners="Right" TargetControlID="rightPanel" >
    </asp:RoundedCornersExtender>--%>
    <%--<table cellspacing="0" cellpadding="0">
        <tr>
            <td>    
                <asp:Image ID="Image1" runat="server" ImageUrl="~/_Global/ImagesNavigation/1_Home.png" CssClass="NavigationImage" />
            </td>
            <td>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/_Global/ImagesNavigation/2_Aktuelles.png" CssClass="NavigationImage" />
            </td>
            <td>
                <asp:Image ID="Image3" runat="server" ImageUrl="~/_Global/ImagesNavigation/3_Bilder.png" CssClass="NavigationImage" />
            </td>
            <td>
                <asp:Image ID="Image4" runat="server" ImageUrl="~/_Global/ImagesNavigation/4_Videos.png" CssClass="NavigationImage" />
            </td>
            <td>
                <asp:Image ID="Image5" runat="server" ImageUrl="~/_Global/ImagesNavigation/5_aeltereMinis.png" CssClass="NavigationImage" />
            </td>
            <td>
                <asp:Image ID="Image6" runat="server" ImageUrl="~/_Global/ImagesNavigation/6_Gaestebuch.png" CssClass="NavigationImage" />
            </td>
            <td>
                <asp:Image ID="Image7" runat="server" ImageUrl="~/_Global/ImagesNavigation/7_Kontakt.png" CssClass="NavigationImage" />
            </td>
            <td>
                <asp:Image ID="Image8" runat="server" ImageUrl="~/_Global/ImagesNavigation/8_Links.png" CssClass="NavigationImage" />
            </td>
        </tr>
    </table>--%>
<%--    <img src="/_Global/ImagesNavigation/1_Home.png" class="NavigationImage" />
    <img src="/_Global/ImagesNavigation/2_Aktuelles.png" class="NavigationImage" />
    <img src="/_Global/ImagesNavigation/3_Bilder.png" class="NavigationImage" />
    <img src="/_Global/ImagesNavigation/4_Videos.png" class="NavigationImage" />
    <img src="/_Global/ImagesNavigation/5_aeltereMinis.png" class="NavigationImage" />
    <img src="/_Global/ImagesNavigation/6_Gaestebuch.png" class="NavigationImage" />
    <img src="/_Global/ImagesNavigation/7_Kontakt.png" class="NavigationImage" />
    <img src="/_Global/ImagesNavigation/8_Links.png" class="NavigationImage" />--%>

    <hr />

    <%--<asp:SiteMapPath ID="siteMapPath" runat="server" />

    <hr />--%>

</div>

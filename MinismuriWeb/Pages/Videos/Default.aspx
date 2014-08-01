<%@ Page Title="" Language="C#" MasterPageFile="~/_Global/MinismuriMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MinismuriWeb.Pages.Videos.Deafult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            //<!-- this will install flowplayer inside the A- tag. -->
            flowplayer("player", "/_Content/Video/flowplayer/flowplayer-3.2.16.swf", {
    
                // this will enable pseudostreaming support
                plugins: {
                    pseudo: {
                        url: "/_Content/Video/flowplayer/flowplayer.pseudostreaming-3.2.12.swf",
                        queryString: escape('?target=${start}') //&secretToken=1235oh8qewr5uweynkc
                    }
                },
 
                // clip properties
                clip: {
 
                    // our clip uses pseudostreaming
                    provider: 'pseudo',

                    url: $('#player').attr('href') //'http://pseudo01.hddn.com/vod/demo.flowplayervod/bbb-800.mp4'//
                },

                autoPlay: true,
                autoBuffering: true

                // Tool um die Video Pseudostreamigfähig zu machen hier: http://renaun.com/air/QTIndexSwapper.air (benötigt Adobe Air)

            });
        });
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />    

    <table style="width:100%" cellspacing="10">
        <tr>
            <td class="VideoLinkContainer" style="width:27%">
                <asp:Repeater id="alleFilmeRepeater" runat="server">
                    <ItemTemplate>
                  
                        <a class="VideoLink <%# (string)Eval("Value") == SelectedVideo ? "VideoLinkSelected" : string.Empty %>" href="Default.aspx?VideoUrl=<%# Eval("Value") %>">
                            <%# Eval("Key") %>
                        </a>
                        

                    </ItemTemplate>
                </asp:Repeater>
            </td>
            <td style="vertical-align:top;">
                <a  style="display:<%= string.IsNullOrEmpty(SelectedVideo) ? "none" : "block" %>;width:720px;height:460px"
		            href="<%= string.Format("Videolibrary/{0}", SelectedVideo) %>"
		            id="player"> 
	            </a> 
            </td>
        </tr>
    </table>

    

</asp:Content>

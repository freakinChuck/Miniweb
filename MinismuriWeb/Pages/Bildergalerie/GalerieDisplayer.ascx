<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalerieDisplayer.ascx.cs" Inherits="MinismuriWeb.Pages.Bildergalerie.GalerieDisplayer" %>

<script type="text/C#" runat="server">

    public string GetThumbanailImageHtml()
    {
        string html = string.Empty;

        foreach (var item in ImageLinks)
        {
            html = string.Format(@"{0}
        <li style='display:inline-block;height: 80px;vertical-align:middle'>
          <a href='{1}'>
            <img src='{2}'>
          </a>
        </li>", html, item.Key, item.Value);
        }

        return html;
    }

</script>

<div id="entfernenContainer" runat="server">
    <asp:LinkButton ID="entfernenLinkButton" Text="aktuelles Bild entfernen" runat="server" 
        OnClientClick="return PrepareImgDelete()"
        style="float:right;" ClientIDMode="Static" OnClick="entfernenLinkButton_Click" />

        <asp:HiddenField Id="deleteHiddenField" runat="server" ClientIDMode="Static" />

    <div style="clear:both"></div>
</div>

<div class="ad-gallery">
  <div class="ad-image-wrapper">
  </div>
  <div class="ad-controls">
  </div>
  <div class="ad-nav">
    <div class="ad-thumbs">



      <ul class="ad-thumb-list">
        <%= GetThumbanailImageHtml()%>
      </ul>



    </div>
  </div>
</div>

<div style="font:inherit; width:99%; margin-top:-50px;">
    <%= Begleittext %>
</div>

<script type="text/javascript">

    function PrepareImgDelete() {

        var hash = window.location.hash;
        hash = hash.replace('#ad-image-', '');
        var result = confirm('Soll das aktuelle Bild wirklich gelöscht werden?')
        $('#deleteHiddenField').val(hash);
        return result;
    }

    $(document).ready(function () {
        var galleries = $('.ad-gallery').adGallery(
        {
            effect: 'fade', //effect'slide-vert', 'fade', or 'resize', 'none'
            scroll_jump: 0,
            slideshow: {
                enable: true,
                autostart: false,
                speed: 3000,
                start_label: 'Slideshow starten',
                stop_label: 'Slideshow anhalten',
                stop_on_scroll: true,
                countdown_prefix: '(',
                countdown_sufix: ')',
                onStart: function () {
                    $('.ad-slideshow-start').hide();
                    $('.ad-slideshow-stop').show();
                },
                onStop: function () {
                    $('.ad-slideshow-start').show();
                    $('.ad-slideshow-stop').hide();
                }
            },
            callbacks: {
                init: function () {

                    var totalWidth = 0;
                    $(".ad-thumb-list").children().each(function () {
                        totalWidth = totalWidth + $(this).outerWidth();
                    });

                    $('.ad-thumb-list').width(totalWidth + 'px');

                }
            }
        });

        $('.ad-slideshow-stop').hide();

    });

</script>
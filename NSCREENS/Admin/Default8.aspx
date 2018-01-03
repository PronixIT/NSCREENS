<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default8.aspx.cs" Inherits="Admin_Default8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Website Title</title>
    <!-- You can use Open Graph tags to customize link previews.
    Learn more: https://developers.facebook.com/docs/sharing/webmasters -->
    <meta charset="UTF-8" />
    <meta name="viewport" content="initial-scale=1.0, width=device-width" />

    <meta property="og:url" content="http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=15&userId=10" />
    <meta property="og:type" content="website" />
    <meta property="og:title" content="Watch" />
    <meta property="og:description" content="Your description" />
    <meta property="og:image" content="http://www.nscreens.com/Video_Images/Shortfilm_23.jpg" />
</head>
<body>
    <form id="form1" runat="server">

      

        <div>
            <!-- Load Facebook SDK for JavaScript -->
            <div id="fb-root"></div>
            <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_IN/sdk.js#xfbml=1&version=v2.10&appId=1525516520838247";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>

            <!-- Your share button code -->
            <div class="fb-share-button"
                data-href="http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=15&userId=10"
                data-layout="button_count">
            </div>

        </div>
    </form>
</body>

</html>

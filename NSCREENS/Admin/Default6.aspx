<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default6.aspx.cs" Inherits="Admin_Default6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.12.3.min.js"></script>
    <script>
        $(function () {
            var player = $('iframe');
            var playerOrigin = '*';
            var status = $('.status');

            // Listen for messages from the player
            if (window.addEventListener) {
                window.addEventListener('message', onMessageReceived, false);
            }
            else {
                window.attachEvent('onmessage', onMessageReceived, false);
            }

            // Handle messages received from the player
            function onMessageReceived(event) {
                // Handle messages from the vimeo player only
                if (!(/^https?:\/\/player.vimeo.com/).test(event.origin)) {
                    return false;
                }

                if (playerOrigin === '*') {
                    playerOrigin = event.origin;
                }

                var data = JSON.parse(event.data);

                switch (data.event) {
                    case 'ready':
                        onReady();
                        break;

                    case 'playProgress':
                        onPlayProgress(data.data);
                        break;

                    case 'pause':
                        onPause();
                        break;

                    case 'finish':
                        onFinish();
                        break;
                }
            }

            // Call the API when a button is pressed
            $('button').on('click', function () {
                post($(this).text().toLowerCase());
            });

            // Helper function for sending a message to the player
            function post(action, value) {
                var data = {
                    method: action
                };

                if (value) {
                    data.value = value;
                }

                var message = JSON.stringify(data);
                player[0].contentWindow.postMessage(message, playerOrigin);
            }

            function onReady() {
                status.text('ready');

                post('addEventListener', 'pause');
                post('addEventListener', 'finish');
                post('addEventListener', 'playProgress');
            }

            function onPause() {
                status.text('paused');
            }

            function onFinish() {
             <%--   if(document.getElementById('<%= txtName.ClientID %>').value=='')
                    alert('navya');
                else
                    alert('ok');--%>
            }

            function onPlayProgress(data) {
                status.text(data.seconds + 's played');
            }
        });
    </script>
    <style>
        div {
            margin-top: 3px;
            padding: 0 10px;
        }

        button {
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            cursor: pointer;
            font-weight: 700;
            font-size: 13px;
            padding: 8px 18px 10px;
            line-height: 1;
            color: #fff;
            background: #345;
            border: 0;
            border-radius: 4px;
            margin-left: 0.75em;
        }

        p {
            display: inline-block;
            margin-left: 10px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <script src="https://f.vimeocdn.com/js/froogaloop2.min.js"></script>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>


        <iframe id="player1" runat="server" src="https://player.vimeo.com/video/213195337?background=0&autoplay=1&api=1&player_id=player1" width="630" height="354" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen aria-autocomplete></iframe>
        <%--<div>
                <p>Status: <span class="status">&hellip;</span></p>
            </div>--%>

       

    </form>

</body>
</html>

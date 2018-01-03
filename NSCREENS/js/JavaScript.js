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
        status.text('finished');
    }

    function onPlayProgress(data) {
        status.text(data.seconds + 's played');
    }
});
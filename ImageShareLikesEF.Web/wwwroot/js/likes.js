$(() => {
    setInterval(() => {
        updateLikes();
    }, 500);

    $("#like-button").on('click', function () {
        const id = $("#image-id").val();
        $.post('/home/update', { id }, function () {
            updateLikes();
        });
        $("#like-button").prop('disabled', true);
    });

    function updateLikes() {
        const id = $("#image-id").val();
        $.get('/home/getCurrentLikes', { id }, function (currentLikes) {
            $("#likes-count").text(currentLikes);
        });
      
    }

})
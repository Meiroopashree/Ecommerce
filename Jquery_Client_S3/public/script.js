$(document).ready(function() {
    $('#btnLoadWeather').click(function() {
        $('#weatherContainer').load('data.txt', function(response, status, xhr) {
            console.log("Status:", status);
            console.log("Response:", response);
            console.log("XHR:", xhr);

            if (status === "success" && response.trim() !== "") {
                alert("Weather data loaded successfully!");
                $('#weatherContainer').html(response); // Display the data in the container
            } else if (status === "success" && response.trim() === "") {
                alert("Weather data loaded successfully");
            } else {
                alert("Error loading weather data: " + xhr.status + " " + xhr.statusText);
            }
        });
    });

    $('#btnFadeOddCities').click(function() {
        $('#cityList li:odd').fadeOut();
    });
});

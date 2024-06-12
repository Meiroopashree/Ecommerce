$(document).ready(function() {
    $('#btnLoadUsers').click(function() {
        $('#userContainer').load('userData.txt', function(response, status, xhr) {
            console.log("Status:", status);
            console.log("Response:", response);
            console.log("XHR:", xhr);

            if (status === "success" && response.trim() !== "") {
                alert("User data loaded successfully!");
                $('#userContainer').html(response); // Display the data in the container
            } else if (status === "success" && response.trim() === "") {
                alert("User data loaded successfully");
            } else {
                alert("Error loading user data: " + xhr.status + " " + xhr.statusText);
            }
        });
    });

    $('#btnHighlightAdults').click(function() {
        $('#userList li').each(function() {
            var userInfo = $(this).text().split(','); // Split name and age
            var age = parseInt(userInfo[1].trim()); // Get the age

            if (!isNaN(age) && age > 30) { // Check if age is valid and greater than 30
                $(this).addClass('highlighted');
            }
        });
    });
});

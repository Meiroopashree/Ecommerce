$(document).ready(function() {
    // Ajax call to load data from data.txt
    $('#btnLoadBooks').click(function() {
        $.get('data.txt', function(data) {
            let lines = data.split('\n');
            let bookContainer = $('#bookContainer');
            bookContainer.empty();
            lines.forEach(line => {
                let [title, author] = line.split(' by ');
                if (title && author) {
                    bookContainer.append(`<p><strong>${title}</strong> by ${author}</p>`);
                }
            });
            alert("Books loaded successfully!");
        }).fail(function(xhr, status, error) {
            alert("An error occurred: " + xhr.status + " " + xhr.statusText);
        });
    });

    // Highlight book titles with more than three words
    $('#btnHighlightBooks').click(function() {
        $('#bookList li').each(function() {
            if ($(this).text().split(' ').length > 3) {
                $(this).addClass('highlighted');
            }
        });
    });
});

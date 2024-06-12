$(document).ready(function() {
    // Ajax call to load data from books.json
    $('#btnLoadBooks').click(function() {
        $.get('books.json', function(data) {
            let books = JSON.parse(data);
            let bookContainer = $('#bookContainer');
            bookContainer.empty();
            books.forEach(book => {
                bookContainer.append(`<p><strong>${book.title}</strong> by ${book.author}</p>`);
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

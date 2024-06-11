$(document).ready(function() {
  $('#btnHideSections').click(function() {
      $('section').slideUp();
  });

  $('section').hover(
      function() {
          $(this).css('background-color', 'lightblue');
      },
      function() {
          $(this).css('background-color', 'white');
      }
  );
});

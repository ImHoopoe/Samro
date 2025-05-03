document.querySelector('.mobile-menu-toggle').addEventListener('click', function() {
  const userActions = document.querySelector('.user-actions');
  userActions.classList.toggle('show');
});


window.addEventListener('resize', function() {
  if (window.innerWidth > 768) {
      document.querySelector('.user-actions').classList.remove('show');
  }
});


$(document).ready(function () {
  let recordsTimeout, blogsTimeout;

  $("#Records").hover(
    function () {
      clearTimeout(recordsTimeout);
      $("#RecordsMegaMenu").addClass("d-block");
    },
    function () {
      recordsTimeout = setTimeout(function () {
        $("#RecordsMegaMenu").removeClass("d-block");
      }, 100);
    }
  );

  $("#Blogs").hover(
    function () {
      clearTimeout(blogsTimeout);
      $("#BlogsMegaMenu").addClass("d-block");
    },
    function () {
      blogsTimeout = setTimeout(function () {
        $("#BlogsMegaMenu").removeClass("d-block");
      }, 100);
    }
  );

  $("#BlogsMegaMenu").hover(
    function () {
      clearTimeout(blogsTimeout);
      $("#BlogsMegaMenu").addClass("d-block");
    },
    function () {
      blogsTimeout = setTimeout(function () {
        $("#BlogsMegaMenu").removeClass("d-block");
      }, 100);
    }
  );

  $("#RecordsMegaMenu").hover(
    function () {
      clearTimeout(recordsTimeout);
      $("#RecordsMegaMenu").addClass("d-block");
    },
    function () {
      recordsTimeout = setTimeout(function () {
        $("#RecordsMegaMenu").removeClass("d-block");
      }, 100);
    }
  );
});

//$(window).on('scroll', function() {
//  if ($(window).scrollTop() > 0) {
//    $('.navbar').addClass('navbar-sticky');
//  } else {
//    $('.navbar').removeClass('navbar-sticky');
//  }
//});



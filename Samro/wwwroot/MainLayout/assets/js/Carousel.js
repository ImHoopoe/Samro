$(document).ready(function() {
    const slides  = $('.card-slide');
    const thumbs  = $('.card-thumb');
    const total   = thumbs.length;
    let currentIndex = 0;
  
 
    thumbs.on('click', function() {
        const index = thumbs.index(this);    
        currentIndex = index;
  
        thumbs.removeClass('active');
        $(this).addClass('active');
  
        slides.removeClass('active');
        slides.eq(index).addClass('active');
    });

    const $wrapper = $('.thumbnail-wrapper');
    const scrollAmount = 200; 
  
    $wrapper.on('click', '::before', function() {
      $wrapper.animate({ scrollLeft: '-=' + scrollAmount }, 400);
    });
  
    $wrapper.on('click', '::after', function() {
      $wrapper.animate({ scrollLeft: '+=' + scrollAmount }, 400);
    });

    let autoPlay = setInterval(() => {
        currentIndex = (currentIndex + 1) % total;
        thumbs.eq(currentIndex).trigger('click');
    }, 5000);
  
 
    $('.custom-card-carousel').hover(
        () => clearInterval(autoPlay),
        () => {
          autoPlay = setInterval(() => {
            currentIndex = (currentIndex + 1) % total;
            thumbs.eq(currentIndex).trigger('click');
          }, 5000);
        }
    );

    let carousel = $('#customCarousel');


    $('.thumbs img').on('click', function () {
      let index = $(this).data('bs-slide-to');
      carousel.carousel(index);
    });


    carousel.on('slide.bs.carousel', function (e) {
      let index = $(e.relatedTarget).data('index');
      $('.thumbs img').removeClass('active');
      $('.thumbs img').eq(index).addClass('active');
    });

    $('.thumb-prev').click(() => $wrapper.animate({ scrollLeft: '-=' + scrollAmount }, 400));
$('.thumb-next').click(() => $wrapper.animate({ scrollLeft: '+=' + scrollAmount }, 400));
  });

  document.addEventListener('DOMContentLoaded', function () {
    const swiper = new Swiper('.thumbnail-swiper', {
      slidesPerView: 'auto',
      spaceBetween: 20,
      navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
      },
      breakpoints: {
        768: {
          slidesPerView: 3,
        },
        992: {
          slidesPerView: 4,
        },
        1200: {
          slidesPerView: 5,
        },
      },
    });
  
    // فعال‌سازی تامبنیل‌ها
    const thumbs = document.querySelectorAll('.card-thumb');
    thumbs.forEach((thumb, index) => {
      thumb.addEventListener('click', () => {
        thumbs.forEach(t => t.classList.remove('active'));
        thumb.classList.add('active');
        // اینجا می‌توانید کدی برای نمایش اسلاید مربوطه اضافه کنید
      });
    });
  });
  
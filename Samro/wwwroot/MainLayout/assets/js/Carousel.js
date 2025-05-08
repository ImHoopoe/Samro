$(document).ready(function () {
    const slides = $('.card-slide');
    const thumbs = $('.card-thumb');
    const total = thumbs.length;
    let currentIndex = 0;

    // اضافه کردن کلاس 'active' به اولین اسلاید و thumbnail
    slides.eq(0).addClass('active');
    thumbs.eq(0).addClass('active');

    thumbs.on('click', function () {
        const index = thumbs.index(this);
        currentIndex = index;

        thumbs.removeClass('active');
        $(this).addClass('active');

        slides.removeClass('active');
        slides.eq(index).addClass('active');
    });

    const $wrapper = $('.thumbnail-wrapper');
    const scrollAmount = 200;

    $('.thumb-prev').click(() =>
        $wrapper.animate({ scrollLeft: '-=' + scrollAmount }, 400)
    );
    $('.thumb-next').click(() =>
        $wrapper.animate({ scrollLeft: '+=' + scrollAmount }, 400)
    );

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
});

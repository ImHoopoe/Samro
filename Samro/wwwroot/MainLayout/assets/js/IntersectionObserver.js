document.addEventListener('DOMContentLoaded', () => {
    const counters = document.querySelectorAll('.counter-number');
  
    const options = {
      root: null,           
      rootMargin: '0px',
      threshold: 0.5      
    };
  
    const observer = new IntersectionObserver((entries, obs) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          animateCount(entry.target);    
          obs.unobserve(entry.target);  
        }
      });
    }, options);
  

    counters.forEach(counter => {
      observer.observe(counter);
    });
  });
  document.addEventListener('DOMContentLoaded', () => {
    const sections = document.querySelectorAll('.section');
  
    const options = {
      root: null,
      rootMargin: '0px',
      threshold: 0.2
    };
  
    const observer = new IntersectionObserver((entries, observer) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          entry.target.classList.add('show');
          observer.unobserve(entry.target);
        }
      });
    }, options);
  
    sections.forEach(section => {
      observer.observe(section);
    });
  });
  
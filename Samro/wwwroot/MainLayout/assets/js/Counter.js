function animateCount(counter) {
    const target = +counter.getAttribute('data-target');
    const count = +counter.innerText;
    const speed = 5000;
    const inc = target / speed;
  
    if (count < target) {
      counter.innerText = Math.ceil(count + inc);
      setTimeout(() => animateCount(counter), 10);
    } else {
      counter.innerText = target;
    }
  }
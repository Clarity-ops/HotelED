let counter = 1;
const scrollAmount = 300;
while (document.getElementById('carousel' + counter) != null) {
    const carousel = document.getElementById('carousel' + counter);
    const prevBtn  = document.getElementById('prevBtn' + counter);
    const nextBtn  = document.getElementById('nextBtn' + counter);

    let updateButtons = () => {
        const maxScrollLeft = carousel.scrollWidth - carousel.clientWidth;
        prevBtn.classList.toggle('hidden', carousel.scrollLeft <= 0);
        nextBtn.classList.toggle('hidden', carousel.scrollLeft >= maxScrollLeft);
    }
    prevBtn.addEventListener('click', () => {
        carousel.scrollBy({left: -scrollAmount, behavior: 'smooth'});
    })
    nextBtn.addEventListener('click', () => {
        carousel.scrollBy({left: scrollAmount, behavior: 'smooth'});
    })
    carousel.addEventListener('scroll', updateButtons);
    window.addEventListener('load', updateButtons);
    counter++;
} 
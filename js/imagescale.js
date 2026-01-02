const modal = document.getElementById('imgModal');
const fullImg = document.getElementById('fullImg');
const closeBtn = document.querySelector('.close');
const images = document.querySelectorAll('.theory img');

images.forEach(img => {
    img.addEventListener('click', () => {
        fullImg.src = img.src;
        modal.style.display = 'flex';
        setTimeout(() => {
            modal.classList.add('show');
        }, 10);
    });
});

closeBtn.addEventListener('click', () => {
    modal.classList.remove('show');
    setTimeout(() => {
        modal.style.display = 'none';
    }, 300); 
});

window.addEventListener('click', (e) => {
    if (e.target === modal) {
        modal.classList.remove('show');
        setTimeout(() => {
            modal.style.display = 'none';
        }, 300);
    }
});

window.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') {
        modal.classList.remove('show');
        setTimeout(() => {
            modal.style.display = 'none';
        }, 300);
    }
});
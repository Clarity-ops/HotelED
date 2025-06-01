const openBtn   = document.getElementById('openCreateBtn');
const closeBtn  = document.getElementById('closeCreateBtn');
const cancelBtn = document.getElementById('cancelBtn');
const modal     = document.getElementById('createModal');
const content   = document.getElementById('modalContent');

function showModal() {
    modal.classList.remove('hidden');
    // trigger enter animation
    content.classList.remove('modal-leave-active');
    content.classList.add('modal-enter-active');
}
function hideModal() {
    content.classList.remove('modal-enter-active');
    content.classList.add('modal-leave-active');
    content.addEventListener('transitionend', () => modal.classList.add('hidden'), { once: true });
}

openBtn.addEventListener('click', showModal);
closeBtn.addEventListener('click', hideModal);
cancelBtn.addEventListener('click', hideModal);
modal.addEventListener('click', hideModal);
content.addEventListener('click', e => e.stopPropagation());
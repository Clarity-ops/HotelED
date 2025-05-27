const toast = document.getElementById('toastError');

function closeToast() {
if (toast) toast.remove();
}

// Клік поза повідомленням
document.addEventListener('click', function (e) {
if (toast && !toast.contains(e.target)) {
    closeToast();
}
});
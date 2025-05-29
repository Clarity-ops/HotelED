  const menuBtn = document.getElementById('userMenuButton');
  const menu    = document.getElementById('userMenu');

  menuBtn.addEventListener('click', e => {
    e.stopPropagation();
    // перед показом додаємо класи для анімації
    if (menu.classList.contains('hidden')) {
      menu.classList.remove('hidden', 'opacity-0', 'scale-95');
      menu.classList.add('opacity-100', 'scale-100');
    } else {
      menu.classList.add('hidden');
    }
  });

  document.addEventListener('click', () => {
    if (!menu.classList.contains('hidden')) {
      menu.classList.add('hidden');
    }
  });

  menu.addEventListener('click', e => e.stopPropagation());

  document.getElementById('logoutButton')
          .addEventListener('click', () => window.location.href = '/Auth/Logout');
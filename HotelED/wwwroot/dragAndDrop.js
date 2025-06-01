  const dropZone = document.getElementById('dropZone');
  const photoInput = document.getElementById('photoInput');
  const previewContainer = document.getElementById('previewContainer');
  const browseText = document.getElementById('browseText');

  // DataTransfer для зберігання файлів
  const dataTransfer = new DataTransfer();

  // Клік по тексту чи зоні відкриває файловий діалог
  browseText.addEventListener('click', () => photoInput.click());
  dropZone.addEventListener('click', () => photoInput.click());

  // Dragover / Dragleave для стилізації
  dropZone.addEventListener('dragover', (e) => {
    e.preventDefault();
    dropZone.classList.add('border-blue-400', 'bg-blue-50');
  });
  dropZone.addEventListener('dragleave', () => {
    dropZone.classList.remove('border-blue-400', 'bg-blue-50');
  });

  // Drop: додаємо файли в dataTransfer та в input.files
  dropZone.addEventListener('drop', (e) => {
    e.preventDefault();
    dropZone.classList.remove('border-blue-400', 'bg-blue-50');
    const files = Array.from(e.dataTransfer.files);
    addFiles(files);
  });

  // Коли вибрали файли через input
  photoInput.addEventListener('change', () => {
    const files = Array.from(photoInput.files);
    addFiles(files);
  });

  // Функція обробки файлів: додаємо до dataTransfer + оновлюємо preview
  function addFiles(files) {
    files.forEach((file) => {
      if (!file.type.startsWith('image/')) return;

      // Додаємо файл в DataTransfer
      dataTransfer.items.add(file);
    });

    // Присвоюємо зібраний FileList до input.files
    photoInput.files = dataTransfer.files;

    // Оновлюємо прев’ю (видаляємо попередні і рендеримо нові)
    renderPreviews();
  }

  // Функція рендерингу прев’ю
  function renderPreviews() {
    previewContainer.innerHTML = ''; // очищуємо попередні
    const fragment = document.createDocumentFragment();

    Array.from(photoInput.files).forEach((file, index) => {
      // Контейнер прев’ю
      const previewDiv = document.createElement('div');
      previewDiv.className = 'relative w-full h-24 bg-gray-100 rounded-lg overflow-hidden animate-fade-in';

      // Кнопка видалення конкретного фото
      const removeBtn = document.createElement('button');
      removeBtn.innerHTML = '&times;';
      removeBtn.className = 'absolute top-1 right-1 text-white bg-red-500 bg-opacity-75 rounded-full w-6 h-6 flex items-center justify-center hover:bg-opacity-100 transition';
      removeBtn.addEventListener('click', () => {
        removeFileAtIndex(index);
      });

      // <img> для прев’ю
      const img = document.createElement('img');
      img.className = 'w-full h-full object-cover';
      img.file = file;

      const reader = new FileReader();
      reader.onload = ((aImg) => (e) => {
        aImg.src = e.target.result;
      })(img);
      reader.readAsDataURL(file);

      // Збираємо
      previewDiv.appendChild(img);
      previewDiv.appendChild(removeBtn);
      fragment.appendChild(previewDiv);
    });

    previewContainer.appendChild(fragment);
  }

  // Видалення файлу за індексом (із dataTransfer та input.files)
  function removeFileAtIndex(indexToRemove) {
    // Створюємо новий DataTransfer і додаємо туди всі, крім видаленого
    const newDataTransfer = new DataTransfer();
    Array.from(photoInput.files).forEach((file, idx) => {
      if (idx !== indexToRemove) {
        newDataTransfer.items.add(file);
      }
    });
    // Присвоюємо новий FileList назад у input
    photoInput.files = newDataTransfer.files;
    // Оновлюємо глобальний dataTransfer (щоб drag-n-drop і далі працював)
    dataTransfer.items.clear();
    Array.from(newDataTransfer.files).forEach((file) => dataTransfer.items.add(file));

    // Перерендерюємо прев’ю
    renderPreviews();
  }
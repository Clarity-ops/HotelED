using Microsoft.AspNetCore.Http;

namespace HotelED.Core.Interfaces;

/// <summary>
/// Служба для зберігання файлів (зображень): 
/// реалізація може писати у wwwroot, Azure Blob, S3 і т.д.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Зберегти файл і повернути відносний або повний URL (залежить від реалізації).
    /// </summary>
    /// <param name="file">IFormFile, який треба зберегти</param>
    /// <param name="subfolder">наприклад, "hotels/{hotelId}"</param>
    Task<string> SaveFileAsync(IFormFile file, string subfolder);

    /// <summary>
    /// Видалити файл за вказаною URL-строкою (якщо реалізація підтримує).
    /// </summary>
    /// <param name="fileUrl">URL файлу, який треба видалити</param>
    Task DeleteFileAsync(string fileUrl);
}
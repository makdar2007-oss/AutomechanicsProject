using AutomechanicsProject.Classes;
using System.Collections.Generic;

/// <summary>
/// Класс для десериализации JSON файла импорта
/// </summary>
public class ImportFileData
{
    /// <summary>
    /// Код валюты
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Список товаров для импорта
    /// </summary>
    public List<ImportProductItem> Products { get; set; }
}
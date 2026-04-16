using AutomechanicsProject.Classes;
using System.Collections.Generic;

/// <summary>
/// Класс для десериализации JSON файла импорта
/// </summary>
public class ImportFileData
{
    public string Currency { get; set; }
    public List<ImportProductItem> Products { get; set; }
}
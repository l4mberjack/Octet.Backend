namespace Octet.DTO;

public class FilterDto
{
    /// <summary>
    /// Имя файла
    /// </summary>
    public string? FileName {get; set;}

    /// <summary>
    /// Минимальная дата и время
    /// </summary>
    public DateTime? MinStartTime { get; set; }
    
    /// <summary>
    /// Максимальная дата и время
    /// </summary>
    public DateTime? MaxStartTime { get; set; }

    /// <summary>
    /// Минимальный средний показатель
    /// </summary>
    public double? MinAvgRate { get; set; }
    
    /// <summary>
    /// Максимальный средний показатель
    /// </summary>
    public double? MaxAvgRate { get; set; }

    /// <summary>
    /// Минимальное среднее время выполнения
    /// </summary>
    public double? MinAvgExecutionTime { get; set; }
    
    /// <summary>
    /// Максимальное среднее время выполнения
    /// </summary>
    public double? MaxAvgExecutionTime { get; set; }
}
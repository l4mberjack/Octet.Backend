namespace Octet.DTO;

public class ResultDto
{
    /// <summary>
    /// Имя файла
    /// </summary>
    public string FileName { get; set;}
    
    /// <summary>
    /// Дельта времени в секундах (MaxStartDate - MinStartDate) 
    /// </summary>
    public double DeltaDate { get; set;}
    
    /// <summary>
    /// Минимальное дата и время
    /// </summary>
    public DateTime MinStartTime { get; set; }
    
    /// <summary>
    /// Среднее время выполнения
    /// </summary>
    public double AvgExecutionTime { get; set; }
    
    /// <summary>
    /// Среднее показателей
    /// </summary>
    public double AvgRate { get; set; }
    
    /// <summary>
    /// Медиана показателей
    /// </summary>
    public double MedianRate { get; set; }
    
    /// <summary>
    /// Максимальный показатель
    /// </summary>
    public double MaxRate { get; set; }
    
    /// <summary>
    /// Минимальный показатель
    /// </summary>
    public double MinRate { get; set; }
}
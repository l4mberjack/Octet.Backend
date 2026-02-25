namespace Octet.Domain;

public class Result
{
    /// <summary>
    /// Уникальный идентификатор - PK
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Имя файла
    /// </summary>
    public string FileName {get; set;}
    
    /// <summary>
    /// Дельта времени в секундах (MaxStartDate - MinStartDate) 
    /// </summary>
    public double DeltaDate {get; set;}
    
    /// <summary>
    /// Минимальное дата и время
    /// </summary>
    public DateTime MinStartTime { get; set; }
    
    /// <summary>
    /// Максимальное дата и время
    /// </summary>
    public DateTime MaxStartTime { get; set; }
    
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
    
    /// <summary>
    /// Значения с плавающей точкой
    /// </summary>
    public ICollection<Value> Values { get; set; } = new List<Value>();
}
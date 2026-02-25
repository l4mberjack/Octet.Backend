namespace Octet.Domain;

public class Value
{
    /// <summary>
    /// Уникальный идентификатор - PK
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// FK с таблицей Result
    /// </summary>
    public Guid ResultId { get; set; }
    
    /// <summary>
    /// Время начала
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Время выполнения в секундах 
    /// </summary>
    public double ExecutionTime { get; set; }
    
    /// <summary>
    /// Показатель
    /// </summary>
    public double Rate { get; set; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public Result Result { get; set; }
}
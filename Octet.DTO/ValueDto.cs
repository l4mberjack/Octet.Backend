namespace Octet.DTO;

public class ValueDto
{
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
}
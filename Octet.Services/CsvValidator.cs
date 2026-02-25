using System.Globalization;
using Octet.DTO;

namespace Octet.Services;

public class CsvValidator
{
     public ValueDto ValidateAndParse(string line, int numberLine)
    {
        var parts = line.Split(';');
        if (parts.Length != 3) 
        {
            throw new Exception($"Строка {numberLine}: неверный формат (ожидается 3 значения)");
        }

        var date = ParseDate(parts[0], numberLine);
        var executionTime = ParseExecutionTime(parts[1], numberLine);
        var rate = ParseRate(parts[2], numberLine);

        if (date < new DateTime(2000, 1, 1) || date > DateTime.UtcNow)
        {
            throw new Exception($"Строка {numberLine}: дата вне допустимого диапазона");
        }

        if (executionTime < 0)
        {
            throw new Exception($"Строка {numberLine}: время выполнения не может быть меньше 0");
        }

        if (rate < 0)
        {
            throw new Exception($"Строка {numberLine}: значение показателя не может быть меньше 0");
        }
        
        return new ValueDto
        {
            StartTime = date,
            ExecutionTime = executionTime,
            Rate = rate
        };
    }

    private DateTime ParseDate(string dateString, int numberLine)
    {
        if (!DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH-mm-ss.ffffZ",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            throw new Exception($"Строка {numberLine}: неверный формат даты");
        }
        return date;
    }

    private double ParseExecutionTime(string timeString, int numberLine)
    {
        if (!double.TryParse(timeString, NumberStyles.Any, CultureInfo.InvariantCulture, out var time))
        {
            throw new Exception($"Строка {numberLine}: неверный формат времени выполнения");
        }
        return time;
    }

    private double ParseRate(string rateString, int numberLine)
    {
        if (!double.TryParse(rateString, NumberStyles.Any, CultureInfo.InvariantCulture, out var rate))
        {
            throw new Exception($"Строка {numberLine}: неверный формат показателя");
        }
        return rate;
    }
}
using Microsoft.EntityFrameworkCore;
using Octet.Domain;
using Octet.DTO;

namespace Octet.Services;

public class FileProcessingService : IFileProcessingService
{
    private readonly ApplicationContext context;
    private readonly CsvValidator validator;

    public FileProcessingService(ApplicationContext context, CsvValidator validator)
    {
        this.context = context;
        this.validator = validator;
    }

    public async Task ProcessFileAsync(string fileName, Stream fileStream)
    {
        var lines = new List<string>();
        using (var reader = new StreamReader(fileStream))
        {
            await reader.ReadLineAsync();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                lines.Add(line);
            }

            if (lines.Count is < 1 or > 10000)
            {
                throw new Exception("Invalid lines count");
            }

            var parsedRecords = new List<ValueDto>();
            for (var i = 0; i < lines.Count; i++)
            {
                var record = validator.ValidateAndParse(lines[i], i + 2);
                parsedRecords.Add(record);
            }
            
            var minStartTime = parsedRecords.Min(x => x.StartTime);
            var maxStartTime = parsedRecords.Max(x => x.StartTime);
            var deltaDate = (maxStartTime - minStartTime).TotalSeconds;

            var avgExecutionTime = parsedRecords.Average(x => x.ExecutionTime);
            var avgRate = parsedRecords.Average(x => x.Rate);
            var minRate = parsedRecords.Min(x => x.Rate);
            var maxRate = parsedRecords.Max(x => x.Rate);
            var medianRate = CalculateMedian(parsedRecords);

            var result = new Result()
            {
                FileName = fileName,
                MinStartTime = minStartTime,
                MaxStartTime = maxStartTime,
                DeltaDate = deltaDate,
                AvgExecutionTime = avgExecutionTime,
                AvgRate = avgRate,
                MinRate = minRate,
                MaxRate = maxRate,
                MedianRate = medianRate
            };

            foreach (var dto in parsedRecords)
            {
                result.Values.Add(new Value
                {
                    StartTime = dto.StartTime,
                    ExecutionTime = dto.ExecutionTime,
                    Rate = dto.Rate,
                });
            }
            
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var existing = await context.Results.FirstOrDefaultAsync(x => x.FileName == fileName);
                if (existing != null)
                    context.Results.Remove(existing);
        
                await context.Results.AddAsync(result);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<List<ResultDto>> GetResultsAsync(FilterDto filters)
    {
        var query = context.Results.AsQueryable();

        if (!string.IsNullOrEmpty(filters.FileName))
        {
            query = query.Where(r => r.FileName == filters.FileName);
        }

        if (filters.MinStartTime.HasValue)
        {
            query = query.Where(r => r.MinStartTime >= filters.MinStartTime.Value);
        }
        if (filters.MaxStartTime.HasValue)
        {
            query = query.Where(r => r.MinStartTime <= filters.MaxStartTime.Value);
        }

        if (filters.MinAvgRate.HasValue)
        {
            query = query.Where(r => r.AvgRate >= filters.MinAvgRate.Value);
        }
        if (filters.MaxAvgRate.HasValue)
        {
            query = query.Where(r => r.AvgRate <= filters.MaxAvgRate.Value);
        }

        if (filters.MinAvgExecutionTime.HasValue)
        {
            query = query.Where(r => r.AvgExecutionTime >= filters.MinAvgExecutionTime.Value);
        }
        if (filters.MaxAvgExecutionTime.HasValue)
        {
            query = query.Where(r => r.AvgExecutionTime <= filters.MaxAvgExecutionTime.Value);
        }

        var results = await query.ToListAsync();

        return results.Select(r => new ResultDto
        {
            FileName = r.FileName,
            DeltaDate = r.DeltaDate,
            MinStartTime = r.MinStartTime,
            AvgExecutionTime = r.AvgExecutionTime,
            AvgRate = r.AvgRate,
            MedianRate = r.MedianRate,
            MaxRate = r.MaxRate,
            MinRate = r.MinRate
        }).ToList();
    }

    public async Task<List<ValueDto>> GetLastValuesAsync(string fileName)
    {
        var result = await context.Results
            .FirstOrDefaultAsync(x => x.FileName == fileName);
    
        if (result == null)
        {
            return new List<ValueDto>();
        }
    
        var values = await context.Values
            .Where(v => v.ResultId == result.Id)
            .OrderByDescending(v => v.StartTime) 
            .Take(10)
            .ToListAsync();
    
        values.Reverse();
    
        return values.Select(v => new ValueDto
        {
            StartTime = v.StartTime,
            ExecutionTime = v.ExecutionTime,
            Rate = v.Rate
        }).ToList();
    }

    private double CalculateMedian(List<ValueDto> records)
    {
        var sortedRates = records.Select(x => x.Rate).OrderBy(x => x).ToList();
        var count =  sortedRates.Count;

        if (count % 2 != 0)
        {
            return sortedRates[count / 2];
        }
        else
        {
            var midOdd = sortedRates[count / 2 - 1];
            var midEven = sortedRates[count / 2];
            return (midOdd + midEven) / 2.0;
        }
    }
}
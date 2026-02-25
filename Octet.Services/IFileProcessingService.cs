using Octet.Domain;
using Octet.DTO;

namespace Octet.Services;

public interface IFileProcessingService
{
    Task ProcessFileAsync(string fileName, Stream fileStream);
    
    Task<List<ResultDto>> GetResultsAsync(FilterDto filters);
    
    Task<List<ValueDto>> GetLastValuesAsync(string fileName);
}
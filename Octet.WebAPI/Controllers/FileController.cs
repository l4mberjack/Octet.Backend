using Microsoft.AspNetCore.Mvc;
using Octet.DTO;
using Octet.Services;

namespace Octet.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileProcessingService _service;

    public FileController(IFileProcessingService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
    public async Task<IActionResult> UploadFile(
        [FromForm] string fileName,
        IFormFile file) 
    {
        try
        {
            using var stream = file.OpenReadStream();
            await _service.ProcessFileAsync(fileName, stream);
            return Ok(new { message = "File uploaded successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("results")]
    public async Task<ActionResult<List<ResultDto>>> GetResults([FromQuery] FilterDto filters)
    {
        var results = await _service.GetResultsAsync(filters);
        return Ok(results);
    }

    [HttpGet("last-values")]
    public async Task<ActionResult<List<ValueDto>>> GetLastValues([FromQuery] string fileName)
    {
        var values = await _service.GetLastValuesAsync(fileName);
        return Ok(values);
    }
}
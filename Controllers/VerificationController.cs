using Microsoft.AspNetCore.Mvc;

namespace ThorstenHans.SampleApi.Controllers;

[ApiController]
[Route("")]
public class VerificationController : ControllerBase
{

    private readonly ILogger<VerificationController> _logger;

    public VerificationController(ILogger<VerificationController> logger)
    {
        _logger = logger;
    }

    [HttpGet("verify")]
    public IActionResult Verify()
    {
        _logger.LogTrace("Trace from {HostName}", Environment.MachineName);
        _logger.LogDebug("Debug from {HostName}", Environment.MachineName);
        _logger.LogInformation("Info from {HostName}", Environment.MachineName);
        _logger.LogWarning("Warning from {HostName}", Environment.MachineName);
        _logger.LogCritical("Critical message from {HostName}", Environment.MachineName);
        _logger.LogError("Error from {HostName}", Environment.MachineName);
        return Ok();
    }
}

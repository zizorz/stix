using Microsoft.AspNetCore.Mvc;
using Stix.Filters;

namespace Stix.Controllers;

[ApiController]
[Route("[controller]")]
public class VulnerabilitiesController : ControllerBase
{

    private readonly ILogger<VulnerabilitiesController> _logger;

    public VulnerabilitiesController(ILogger<VulnerabilitiesController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "CreateVulnerability")]
    [ProducesResponseType(typeof(Vulnerability),  StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(VulnerabilityValidationFilter))]
    public IActionResult Create([FromBody] Vulnerability vulnerability)
    {
        return Created($"{Request.Path.Value}/{vulnerability.Id}", vulnerability);
    }
    
    [HttpPost(Name = "GetVulnerability")]
    [ProducesResponseType(typeof(Vulnerability),  StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(VulnerabilityValidationFilter))]
    [Route("{id}")]
    public IActionResult Get(string id)
    {
        return Ok(new Vulnerability("Type", "Id", "qwe", DateTime.Now, DateTime.Today, "qwe", "he", new ExternalReference[]{}));
    }
}
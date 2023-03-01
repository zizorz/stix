using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stix.Filters;

namespace Stix.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "Reader")]
[Produces("application/json")]
public class VulnerabilitiesController : ControllerBase
{

    private readonly ILogger<VulnerabilitiesController> _logger;

    public VulnerabilitiesController(ILogger<VulnerabilitiesController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Vulnerability),  StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(VulnerabilityValidationFilter))]
    [Authorize(Policy = "Writer")]
    public IActionResult Create([FromBody] Vulnerability vulnerability)
    {
        return Created($"{Request.Path.Value}/{vulnerability.Id}", vulnerability);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(Vulnerability),  StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(VulnerabilityValidationFilter))]
    [Authorize(Policy = "Writer")]
    public IActionResult Update(string id, [FromBody] Vulnerability vulnerability)
    {
        return Ok(vulnerability);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(Vulnerability),  StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public IActionResult Get(string id)
    {
        return Ok(new Vulnerability("Type", "Id", "qwe", DateTime.Now, DateTime.Today, "qwe", "he", new ExternalReference[]{}));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(Vulnerability),  StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult List()
    {
        return Ok(new List<Vulnerability> {new("Type", "Id", "qwe", DateTime.Now, DateTime.Today, "qwe", "he", new ExternalReference[]{}) });
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public IActionResult Delete(string id)
    {
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
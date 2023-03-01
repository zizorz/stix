using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stix.Filters;
using Stix.Models;
using Stix.Services;

namespace Stix.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "Reader")]
[Produces("application/json")]
public class VulnerabilitiesController : ControllerBase
{
    private readonly IVulnerabilityService _vulnerabilityService;
    private readonly ILogger<VulnerabilitiesController> _logger;

    public VulnerabilitiesController(IVulnerabilityService vulnerabilityService, ILogger<VulnerabilitiesController> logger)
    {
        _vulnerabilityService = vulnerabilityService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Vulnerability),  StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(VulnerabilityValidationFilter))]
    [Authorize(Policy = "Writer")]
    public async Task<IActionResult> Create([FromBody] Vulnerability vulnerability)
    {
        await _vulnerabilityService.CreateAsync(vulnerability);
        return Created($"{Request.Path.Value}/{vulnerability.Id}", vulnerability);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(VulnerabilityValidationFilter))]
    [Authorize(Policy = "Writer")]
    public async Task<IActionResult> Update(string id, [FromBody] Vulnerability vulnerability)
    {
        await _vulnerabilityService.UpdateAsync(id, vulnerability);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(Vulnerability),  StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _vulnerabilityService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IList<Vulnerability>),  StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
    {
        var result = await _vulnerabilityService.ListAsync(new QueryOptions());
        return Ok(result);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _vulnerabilityService.DeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
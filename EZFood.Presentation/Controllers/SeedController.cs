using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EZFood.Application.Interfaces;
using EZFood.Domain.Entities.Models;


namespace EZFood.Presentation.Controllers;

//[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class SeedController(IServiceManager serviceManager, ILogger<SeedController> logger) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;
    private readonly ILogger<SeedController> _logger = logger;

    [HttpGet("seed-admin")]
    public async Task<IActionResult> SeedAdminUser()
    {

        User? user = await _serviceManager.DataSeedService.SeedAdminUserAsync();
        return Ok(new { user });
    }
}

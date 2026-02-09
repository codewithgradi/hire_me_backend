using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
  private readonly IUserProfileRepo _profileRepo;
  public ProfileController(IUserProfileRepo profileRepo)
  {
    _profileRepo = profileRepo;
  }
  [HttpGet("{id:int}")]
  [Authorize]
  public async Task<IActionResult> GetUser([FromRoute] int id)
  {
    var user = await _profileRepo.GetByidAsync(id);
    if (user == null) return NotFound("User not found");
    return Ok(user);
  }
  [HttpPost]
  [Authorize]
  public async Task<IActionResult> AddUser([FromBody] AddUserProfileDto addUser)
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (string.IsNullOrEmpty(userId)) return Unauthorized();
    addUser.AppUserId = userId;
    addUser.Email = User.FindFirstValue(ClaimTypes.Email);
    var user = await _profileRepo.AddUserProfileAsync(addUser.AppUserId, addUser);
    if (user == null) return BadRequest();
    return Ok(user);
  }
  [HttpPut("{id:int}")]
  [Authorize]
  public async Task<IActionResult> UpdateUserProfile([FromRoute] int id, [FromBody] UpdateUserProfileDto updateUser)
  {
    var user = await _profileRepo.UpdateUserProfileAsync(id, updateUser);
    if (user == null) return BadRequest();
    return Ok(user);
  }
}
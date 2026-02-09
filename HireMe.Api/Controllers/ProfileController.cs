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
  [HttpGet("{email}")]
  [Authorize]
  public async Task<IActionResult> GetUser([FromRoute] string email)
  {
    var user = await _profileRepo.GetByEmailAsync(email);
    if (user == null) return NotFound("User not found");
    return Ok(user.ToUserProfileDto());
  }
  [HttpPost]
  [Authorize]
  public async Task<IActionResult> AddUser(AddUserProfileDto addUser)
  {
    var user = await _profileRepo.AddUserProfileAsync(addUser);
    if (user == null) return BadRequest();
    return Ok(user.ToUserProfileDto());
  }
  [HttpPut("{id:guid}")]
  [Authorize]
  public async Task<IActionResult> UpdateUserProfile(Guid id, UpdateUserProfileDto updateUser)
  {
    var user = await _profileRepo.UpdateUserProfileAsync(id, updateUser);
    if (user == null) return BadRequest();
    return Ok(user.ToUserProfileDto());
  }
}
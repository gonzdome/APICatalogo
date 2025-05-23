namespace APICatalogo.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ITokenService tokenService,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager,
                          IConfiguration configuration,
                          ILogger<AuthController> logger)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    [Route("CreateRole")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (roleExist)
            return ResponseHelper(StatusCodes.Status400BadRequest, false, $"Role already exists.");

        var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

        var logEventId = roleResult.Succeeded ? 1 : 2;
        var code = roleResult.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
        var message = roleResult.Succeeded ? $"Role {roleName} added successfully!" : $"Issue adding the new {roleName} role.";

        _logger.LogInformation(logEventId, message);
        return ResponseHelper(StatusCodes.Status200OK, roleResult.Succeeded, message);
    }

    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return BadRequest(new { error = "Unable to find user" });

        var result = await _userManager.AddToRoleAsync(user, roleName);

        var code = result.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
        var message = result.Succeeded ? $"User {user.Email} added to the {roleName} role." : $"Error: Unable to add user {user.Email} to the {roleName} role.";
        
        _logger.LogInformation(1, message);
        return ResponseHelper(code, result.Succeeded, message);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var user = await _userManager.FindByNameAsync(loginDTO.UserName!);
        var validPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password!);

        if (user is null || !validPassword)
            return Unauthorized();

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));

        var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

        var refreshToken = _tokenService.GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

        user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

        user.RefreshToken = refreshToken;

        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            Expiration = token.ValidTo
        });
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var userExists = await _userManager.FindByNameAsync(registerDTO.Username!);
        if (userExists != null)
            return ResponseHelper(StatusCodes.Status500InternalServerError, false, $"User already exists.");

        ApplicationUser user = new();
        user.Email = registerDTO.Email;
        user.SecurityStamp = Guid.NewGuid().ToString();
        user.UserName = registerDTO.Username;

        var result = await _userManager.CreateAsync(user, registerDTO.Password!);
        if (!result.Succeeded)
            return ResponseHelper(StatusCodes.Status500InternalServerError, result.Succeeded, $"User creation failed: { string.Join(" ", result.Errors.Select(e => e.Description)) }");

        return Ok(new ResponseDTO { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("RefreshToken")]
    public async Task<IActionResult> RefreshToken(TokenDTO tokenDTO)
    {
        if (tokenDTO is null)
            return BadRequest("Invalid client request");

        string? accessToken = tokenDTO.AccessToken ?? throw new ArgumentNullException(nameof(tokenDTO));

        string? refreshToken = tokenDTO.RefreshToken ?? throw new ArgumentException(nameof(tokenDTO));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);
        if (principal == null)
            return BadRequest("Invalid access token/refresh token");

        string username = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(username!);
        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid access token/refresh token");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpPost]
    [Route("Revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) 
            return BadRequest("Invalid user name");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }

    private IActionResult ResponseHelper(int statusCode, bool status, string message)
    {
        return StatusCode(statusCode, new ResponseDTO { Status = status ? "Success" : "Error" , Message = message });
    }
}

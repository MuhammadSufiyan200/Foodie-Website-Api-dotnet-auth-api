using Authentication_System_API.Data;
using Authentication_System_API.DTO;
using Authentication_System_API.model;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplictionDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplictionDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(User user)
        {
            try
            {
                if (_context.Tbl_User.Any(u => u.Email == user.Email))
                {
                    return BadRequest("User already Exists!...");
                }

                if (String.IsNullOrEmpty(user.Role))
                {
                    user.Role = "User";
                }
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                _context.Tbl_User.Add(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("Update_Password")]
        public async Task<IActionResult> Update_Password([FromBody] forupadtePass New_Pass)
        {
            var Existinguser = await _context.Tbl_User.FindAsync(New_Pass.P_id);
            if (Existinguser == null)
            {
                return NotFound("User not found.");
            }
            Existinguser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(New_Pass.P_Password);
            await _context.SaveChangesAsync();

            return Ok("Password Updated successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                if (user == null) return BadRequest("Invalid client request");
                var Existinguser = _context.Tbl_User.FirstOrDefault(u => u.Email == user.Email);

                if (Existinguser == null || !BCrypt.Net.BCrypt.Verify(user.PasswordHash, Existinguser.PasswordHash))
                {
                    var warningLog = new SystemLog
                    {
                        UserId = null,
                        Action = "Failed Login Attempt",
                        LogLevel = "Warning",
                        TimeStamp = DateTime.Now,
                        IPAddress = $"Email: {user.Email}, IP: {user.IpAddress}"
                    };
                    _context.Tbl_SystemLogs.Add(warningLog);
                    await _context.SaveChangesAsync();
                    return Unauthorized("Invaild Cerditials");
                }

                var tokenHolder = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, Existinguser.FullName),
                    new Claim(ClaimTypes.NameIdentifier, Existinguser.Id.ToString()),
                    new Claim(ClaimTypes.Role, Existinguser.Role),
                    new Claim(ClaimTypes.Email, user.Email)
                }),

                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHolder.CreateToken(tokenDescription);
                string Tokenstring = tokenHolder.WriteToken(token);
                var session = new ActiveSession
                {
                    UserId = Existinguser.Id,
                    DeviceInfo = GetShortDeviceInfo(user.DeviceInfo) ?? "Unknown Device",
                    IpAddress = user.IpAddress ?? "N/A",
                    Location = user.Location ?? "Unknown",
                    Token = token.ToString(),
                    LoginTime = DateTime.Now
                };
                _context.Tbl_ActiveSessions.Add(session);
                await _context.SaveChangesAsync();

                var existingLog = await _context.Tbl_SystemLogs
                    .FirstOrDefaultAsync(l => l.UserId == Existinguser.Id && l.Action == "Login" || l.Action == "Logout");

                if (existingLog != null)
                {
                    existingLog.TimeStamp = DateTime.Now;
                    existingLog.LogLevel = "Info";
                    existingLog.IPAddress = $"Device: {GetShortDeviceInfo(session.DeviceInfo)}, IP: {session.IpAddress}, Location: {session.Location}";
                    _context.Tbl_SystemLogs.Update(existingLog);
                }
                else
                {
                    var log = new SystemLog
                    {
                        UserId = Existinguser.Id,
                        Action = "Login",
                        LogLevel = "Info",
                        TimeStamp = DateTime.Now,
                        IPAddress = $"Device: {GetShortDeviceInfo(session.DeviceInfo)}, IP: {session.IpAddress}, Location: {session.Location}"
                    };
                    _context.Tbl_SystemLogs.Add(log);
                }

                await _context.SaveChangesAsync();

                return Ok(new { Token = Tokenstring, UserName = Existinguser.FullName, Roles = Existinguser.Role });
            }
            catch (Exception ex)
            {
                var errorLog = new SystemLog
                {
                    UserId = null,
                    Action = "Login Exception",
                    LogLevel = "Error",
                    TimeStamp = DateTime.Now,
                    IPAddress = ex.Message
                };
                _context.Tbl_SystemLogs.Add(errorLog);
                await _context.SaveChangesAsync();
                return BadRequest(ex);
            }

        }

        [HttpPost("SocialLogin")]
        public async Task<IActionResult> SocialLogin([FromBody] SocialLoginDto model)
        {
            if (model == null) return BadRequest("Invalid client request");

            var Existinguser = _context.Tbl_User
                .FirstOrDefault(u => u.Email == model.Email && u.Provider == model.Provider && u.ProviderUserId == model.ProviderUserId);

            if (Existinguser == null)
            {
                Existinguser = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Provider = model.Provider,
                    ProviderUserId = model.ProviderUserId,
                    DeviceInfo = model.DeviceInfo,
                    IpAddress = model.IpAddress,
                    Location = model.Location,
                    Role = "User",
                    PasswordHash = "SOCIAL_LOGIN"
                };

                _context.Tbl_User.Add(Existinguser);
                await _context.SaveChangesAsync();
            }

            // JWT token generate karo:
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, Existinguser.FullName),
            new Claim(ClaimTypes.NameIdentifier, Existinguser.Id.ToString()),
            new Claim(ClaimTypes.Role, Existinguser.Role),
            new Claim(ClaimTypes.Email, Existinguser.Email)
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string Tokenstring = tokenHandler.WriteToken(token);

            // Save session:
            var session = new ActiveSession
            {
                UserId = Existinguser.Id,
                DeviceInfo = "Google OAuth",
                IpAddress = model.IpAddress ?? "N/A",
                Location = model.Location ?? "Unknown",
                Token = Tokenstring,
                LoginTime = DateTime.Now
            };

            _context.Tbl_ActiveSessions.Add(session);
            await _context.SaveChangesAsync();

            return Ok(new { Token = Tokenstring, UserName = Existinguser.FullName, Roles = Existinguser.Role });
        }

        private string GetShortDeviceInfo(string? userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return "Unknown Device";

            string device = "Unknown";
            string browser = "Unknown";

            if (userAgent.Contains("Windows NT")) device = "Windows";
            else if (userAgent.Contains("Macintosh")) device = "Mac";
            else if (userAgent.Contains("Android")) device = "Android";
            else if (userAgent.Contains("iPhone")) device = "iPhone";

            if (userAgent.Contains("Chrome")) browser = "Chrome";
            else if (userAgent.Contains("Firefox")) browser = "Firefox";
            else if (userAgent.Contains("Safari") && !userAgent.Contains("Chrome")) browser = "Safari";
            else if (userAgent.Contains("Edge")) browser = "Edge";

            return $"{device} - {browser}";
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            var user = await _context.Tbl_User.FindAsync(Convert.ToInt32(userId));

            if (user == null)
                return NotFound();

            var sessions = _context.Tbl_ActiveSessions.Where(s => s.UserId == user.Id).ToList();
            _context.Tbl_ActiveSessions.RemoveRange(sessions);
            await _context.SaveChangesAsync();

            var existingLog = await _context.Tbl_SystemLogs
                   .FirstOrDefaultAsync(l => l.UserId == user.Id && l.Action == "Logout" || l.Action == "Login");
            string ip = sessions.FirstOrDefault()?.IpAddress ?? "N/A";
            string device = sessions.FirstOrDefault()?.DeviceInfo ?? "Unknown Device";
            string location = sessions.FirstOrDefault()?.Location ?? "Unknown";
            if (existingLog != null)
            {
                existingLog.TimeStamp = DateTime.Now;
                existingLog.Action = "Logout";
                existingLog.LogLevel = "Info";
                existingLog.IPAddress = $"Device: {device}, IP: {ip}, Location: {location}";
                _context.Tbl_SystemLogs.Update(existingLog);
            }
            //else
            //{
            //    var log = new SystemLog
            //    {
            //        UserId = user.Id,
            //        Action = "Logout",
            //        LogLevel = "Info",
            //        TimeStamp = DateTime.Now,
            //        IPAddress = $"Device: {device}, IP: {ip}, Location: {location}"
            //    };
            //    _context.Tbl_SystemLogs.Add(log);
            //}

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _context.Tbl_User
                                .Select(u => new { u.Id, u.FullName, u.Email, u.Role })
                                .ToList();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing.");
            }

            Console.WriteLine("Received Token: " + token);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine("User ID from Token: " + userId);

            if (userId == null)

                return Unauthorized();

            var user = _context.Tbl_User
                              .Where(u => u.Id == Convert.ToInt32(userId))
                              .Select(u => new { u.FullName, u.Email, u.Role })
                              .FirstOrDefault();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("GetProfileById/{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var user = _context.Tbl_User
                              .Where(u => u.Id == id)
                              .Select(u => new { u.FullName, u.Email, u.Role })
                              .FirstOrDefault();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpGet("GetActiveSessions")]
        public IActionResult GetActiveSessions()
        {
            var sessions = _context.Tbl_ActiveSessions
                            .Include(s => s.Tuser)
                            .Select(s => new
                            {
                                s.Id,
                                s.UserId,
                                UserName = s.Tuser.FullName,
                                s.DeviceInfo,
                                s.IpAddress,
                                s.Location,
                                s.LoginTime
                            })
                            .ToList();

            return Ok(sessions);
        }

        [HttpGet("Getsystemlogs")]
        public async Task<IActionResult> Getsystemlogs()
        {
            var logs = await _context.Tbl_SystemLogs
                        .Include(l => l.Tuser)
                        .OrderByDescending(l => l.TimeStamp)
                        .Select(l => new
                        {
                            l.Id,
                            l.UserId,
                            UserName = l.Tuser.FullName,
                            l.Action,
                            l.TimeStamp,
                            l.IPAddress,
                            l.LogLevel

                        })
                        .ToListAsync();

            return Ok(logs);
        }
    }

    public class forupadtePass()
    {
        public int P_id { get; set; }
        public string? P_Password { get; set; }
    }
}

using AutoMapper;
using GradBootcamp_Tolgahaninan.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GradBootcamp_Tolgahaninan.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase // Controller For Users
    {

        private IUserRepository _userRepository; // To Create instance of user repository
        private readonly ILogger<UserController> _logger; // To Create instance of Logger
        private readonly IConfiguration _configuration;// To Create instance of Configuration


        public UserController(IUserRepository userRepository, ILogger<UserController> logger, IConfiguration configuration) // Constructor for dependency injection
        {
            _userRepository = userRepository;  
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("SignIn")]
        public string SignIn (string username , string password) // To sign in by user
        {
            _logger.LogInformation("Attempted to sign in");
            password = HashPassword(password);
            var obj = _userRepository.SignIn(username,password); //User repository to reach database and check the given paremeters by user matchs any record in database

            if (obj == true) // If user exists in database
            {
                return GenerateToken(username); // Creating token for user
            }
            return "";
        }
        private string HashPassword(string password) // To hash users password with SHA256 Algorithm
        {
            var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            var stringBuilder = new StringBuilder();
            for (int i = 0;i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("x2"));
            }
          
            return stringBuilder.ToString();
        }
        private string GenerateToken(string username) // To create authentication token which is in JWT Format for user
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // to create token handler
            var key = Encoding.ASCII.GetBytes(_configuration["Application:JWTSymetricKey"]); // To get encryption token which is specified at appsettings.json

            var tokenDescriptor = new SecurityTokenDescriptor // To identify token
            {
                Audience = "ARVATO",
                Issuer = "ARVATO.Issuer.Development",
                Subject = new ClaimsIdentity(new Claim[] // Claims for token
                 {
                            //Add any claim
                            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Role , "Administrator")
                 }),

                Expires = DateTime.UtcNow.AddMinutes(10), // To make token expire in ten minutes
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) 

            };

            var token = tokenHandler.CreateToken(tokenDescriptor); // To create token with given identifiers
            return tokenHandler.WriteToken(token);

        }
    }
}

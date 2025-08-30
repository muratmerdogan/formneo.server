
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using vesa.api.Controllers;
using vesa.core.DTOs;
using vesa.core.Services;

namespace vesa.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;
        //ttess
        public AuthController(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        [HttpGet("getDatabaseName")]
        public IActionResult GetDatabaseName()
        {

            return Ok("");
            //string connectionString = _configuration.GetConnectionString("PostgreSqlConnection");

            //if (string.IsNullOrEmpty(connectionString))
            //    return BadRequest("Connection string not found!");

            //var builder = new SqlConnectionStringBuilder(connectionString);
            //string databaseName = builder.InitialCatalog;

            //return Ok(databaseName);
        }

        //api/auth/
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
             var result = await _authenticationService.CreateTokenAsync(loginDto);

            return CreateActionResult(result);
        }
        [HttpPost("CreateTokenPost")]
        public async Task<TokenDto> CreateTokenPost(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);

            return result.Data;
        }



        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result = _authenticationService.CreateTokenByClient(clientLoginDto);

            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshToken(refreshTokenDto.Token);

            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)

        {
             var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.Token);

            return CreateActionResult(result);
        }
    }
}   
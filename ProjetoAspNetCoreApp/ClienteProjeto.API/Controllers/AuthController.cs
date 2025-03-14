﻿using Azure;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClienteProjeto.API.Controllers;

[Route("api/[controller]")]
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
    [Authorize(Policy ="SuperAdminOnly")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
            {
                _logger.LogInformation(1, "Roles Added");
                return StatusCode(StatusCodes.Status200OK,
                        new ResponseDTO
                        {
                            Status = "Success",
                            Message = $"Role {roleName} added successfully"
                        });
            }
            else
            {
                _logger.LogInformation(2, "Error");
                return StatusCode(StatusCodes.Status400BadRequest,
                   new ResponseDTO
                   {
                       Status = "Error",
                       Message = $"Issue adding the new {roleName} role"
                   });
            }
        }
        return StatusCode(StatusCodes.Status400BadRequest,
          new ResponseDTO { Status = "Error", Message = "Role already exist." });
    }

    [HttpPost]
    [Route("AddUserToRole")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, $"User {user.Email} added to the {roleName} role");
                return StatusCode(StatusCodes.Status200OK,
                       new ResponseDTO
                       {
                           Status = "Success",
                           Message = $"User {user.Email} added to the {roleName} role"
                       });
            }
            else
            {
                _logger.LogInformation(1, $"Error: Unable to add user {user.Email} to the {roleName} role");
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO  
                {
                    Status = "Error",
                    Message = $"Error: Unable to add user {user.Email} to the {roleName} role"
                });
            }
        }
        return BadRequest(new { error = "Unable to find user" });
    }


    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var user = await _userManager.FindByNameAsync(loginDTO.UserName);

        if (user is not null && await _userManager.CheckPasswordAsync(user, loginDTO.Password)) 
        {
            //obtem os perfis do usuario
            var userRoles = await _userManager.GetRolesAsync(user);

            //decleração de autenticação
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("id", user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles) 
            { 
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims,
                                                          _configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();


            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                                            out int refreshTokenValidityInMinutes);

            user.RefreshTokenExpiryTime = 
                            DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }
        return Unauthorized();

    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UsuarioDTO usuarioDTO)
    {
        var userExists = await _userManager.FindByNameAsync(usuarioDTO.Nome!);

        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status409Conflict,
                   new ResponseDTO { Status = "Error", Message = "User already exists!" });
        }

        ApplicationUser user = new()
        {
            Email = usuarioDTO.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = usuarioDTO.Nome
        };

        var result = await _userManager.CreateAsync(user, usuarioDTO.Password!);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                   new ResponseDTO { Status = "Error", Message = "User creation failed." });
        }

        return Ok(new ResponseDTO { Status = "Success", Message = "User created successfully!" });

    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenDTO tokenDTO)
    {

        if (tokenDTO is null)
        {
            return BadRequest("Invalid client request");
        }

        string? accessToken = tokenDTO.AccessToken
                              ?? throw new ArgumentNullException(nameof(tokenDTO));

        string? refreshToken = tokenDTO.RefreshToken
                               ?? throw new ArgumentException(nameof(tokenDTO));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

        if (principal == null)
        {
            return BadRequest("Invalid access token/refresh token");
        }

        string username = principal.Identity!.Name!;

        var user = await _userManager.FindByNameAsync(username!);

        if (user == null || user.RefreshToken != refreshToken
                         || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid access token/refresh token");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(
                                           principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [HttpPost]
    [Route("revoke/{username}")]
    [Authorize(Policy = "ExclusiveOnly")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) return BadRequest("Invalid user name");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}

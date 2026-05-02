using Microsoft.AspNetCore.Mvc;
using MathsAdda.Business.Interfaces;
using MathsAdda.Common.DTOs;
using MathsAdda.Common.Models;

namespace MathsAdda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);
        if (result == null)
        {
            return Unauthorized(ApiResponse<LoginResponseDto>.ErrorResponse("Invalid email or password"));
        }

        return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(result, "Login successful"));
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Register([FromBody] RegisterRequestDto request)
    {
        var result = await _authService.RegisterAsync(request);
        if (result == null)
        {
            return BadRequest(ApiResponse<LoginResponseDto>.ErrorResponse("Registration failed. Email may already exist."));
        }

        return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(result, "Registration successful"));
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<ApiResponse<LoginResponseDto>>> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var result = await _authService.RefreshTokenAsync(request);
        if (result == null)
        {
            return Unauthorized(ApiResponse<LoginResponseDto>.ErrorResponse("Invalid or expired refresh token"));
        }

        return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(result, "Token refreshed successfully"));
    }

    [HttpPost("revoke-token")]
    public async Task<ActionResult<ApiResponse<bool>>> RevokeToken([FromBody] string token)
    {
        var result = await _authService.RevokeTokenAsync(token);
        if (!result)
        {
            return BadRequest(ApiResponse<bool>.ErrorResponse("Failed to revoke token"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Token revoked successfully"));
    }
}
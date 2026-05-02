using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MathsAdda.Business.Interfaces;
using MathsAdda.Common.Models;
using MathsAdda.Data.Entities;

namespace MathsAdda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("profile")]
    public async Task<ActionResult<ApiResponse<Student>>> GetProfile()
    {
        var userId = GetUserId();
        var student = await _studentService.GetStudentByUserIdAsync(userId);

        if (student == null)
        {
            return NotFound(ApiResponse<Student>.ErrorResponse("Student profile not found"));
        }

        return Ok(ApiResponse<Student>.SuccessResponse(student, "Profile retrieved successfully"));
    }

    [HttpPost("enroll/{courseId}")]
    public async Task<ActionResult<ApiResponse<Student>>> EnrollInCourse(int courseId)
    {
        var userId = GetUserId();
        var student = await _studentService.EnrollStudentAsync(userId, courseId);

        return Ok(ApiResponse<Student>.SuccessResponse(student, "Enrolled successfully"));
    }

    [HttpPut("profile")]
    public async Task<ActionResult<ApiResponse<Student>>> UpdateProfile([FromBody] Student student)
    {
        var userId = GetUserId();
        student.UserId = userId;
        var updatedStudent = await _studentService.UpdateStudentAsync(student);

        return Ok(ApiResponse<Student>.SuccessResponse(updatedStudent, "Profile updated successfully"));
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(userIdClaim, out var userId) ? userId : 0;
    }
}
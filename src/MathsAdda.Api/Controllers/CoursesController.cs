using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MathsAdda.Business.Interfaces;
using MathsAdda.Common.Models;
using MathsAdda.Data.Entities;

namespace MathsAdda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<Course>>>> GetAllCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(ApiResponse<IEnumerable<Course>>.SuccessResponse(courses, "Courses retrieved successfully"));
    }

    [HttpGet("class/{classLevel}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<Course>>>> GetCoursesByClass(string classLevel)
    {
        var courses = await _courseService.GetCoursesByClassAsync(classLevel);
        return Ok(ApiResponse<IEnumerable<Course>>.SuccessResponse(courses, "Courses retrieved successfully"));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<Course>>> GetCourse(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound(ApiResponse<Course>.ErrorResponse("Course not found"));
        }

        return Ok(ApiResponse<Course>.SuccessResponse(course, "Course retrieved successfully"));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<ActionResult<ApiResponse<Course>>> CreateCourse([FromBody] Course course)
    {
        var createdCourse = await _courseService.CreateCourseAsync(course);
        return Ok(ApiResponse<Course>.SuccessResponse(createdCourse, "Course created successfully"));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<ActionResult<ApiResponse<Course>>> UpdateCourse(int id, [FromBody] Course course)
    {
        course.Id = id;
        var updatedCourse = await _courseService.UpdateCourseAsync(course);
        return Ok(ApiResponse<Course>.SuccessResponse(updatedCourse, "Course updated successfully"));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteCourse(int id)
    {
        var result = await _courseService.DeleteCourseAsync(id);
        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("Course not found"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Course deleted successfully"));
    }
}
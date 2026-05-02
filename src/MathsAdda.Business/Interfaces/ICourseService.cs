using MathsAdda.Common.Models;
using MathsAdda.Data.Entities;

namespace MathsAdda.Business.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<IEnumerable<Course>> GetCoursesByClassAsync(string classLevel);
    Task<Course?> GetCourseByIdAsync(int id);
    Task<Course> CreateCourseAsync(Course course);
    Task<Course> UpdateCourseAsync(Course course);
    Task<bool> DeleteCourseAsync(int id);
}

public interface IStudentService
{
    Task<Student?> GetStudentByUserIdAsync(int userId);
    Task<Student> EnrollStudentAsync(int userId, int courseId);
    Task<Student> UpdateStudentAsync(Student student);
}

public interface ISubjectService
{
    Task<IEnumerable<Subject>> GetAllSubjectsAsync();
    Task<IEnumerable<Subject>> GetSubjectsByClassAsync(string classLevel);
    Task<Subject?> GetSubjectByIdAsync(int id);
}
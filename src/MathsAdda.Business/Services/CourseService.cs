using MathsAdda.Business.Interfaces;
using MathsAdda.Data.Entities;
using MathsAdda.Data.Repositories;

namespace MathsAdda.Business.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _courseRepository.GetActiveCoursesAsync();
    }

    public async Task<IEnumerable<Course>> GetCoursesByClassAsync(string classLevel)
    {
        return await _courseRepository.GetByClassLevelAsync(classLevel);
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _courseRepository.GetByIdAsync(id);
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        course.CreatedAt = DateTime.UtcNow;
        return await _courseRepository.AddAsync(course);
    }

    public async Task<Course> UpdateCourseAsync(Course course)
    {
        course.UpdatedAt = DateTime.UtcNow;
        await _courseRepository.UpdateAsync(course);
        return course;
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
        {
            return false;
        }

        course.IsActive = false;
        await _courseRepository.UpdateAsync(course);
        return true;
    }
}

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> GetStudentByUserIdAsync(int userId)
    {
        return await _studentRepository.GetByUserIdAsync(userId);
    }

    public async Task<Student> EnrollStudentAsync(int userId, int courseId)
    {
        var student = await _studentRepository.GetByUserIdAsync(userId);
        if (student == null)
        {
            student = new Student
            {
                UserId = userId,
                EnrolledCourseId = courseId,
                EnrollmentDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            await _studentRepository.AddAsync(student);
        }
        else
        {
            student.EnrolledCourseId = courseId;
            student.EnrollmentDate = DateTime.UtcNow;
            await _studentRepository.UpdateAsync(student);
        }

        return student;
    }

    public async Task<Student> UpdateStudentAsync(Student student)
    {
        await _studentRepository.UpdateAsync(student);
        return student;
    }
}

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
    {
        return await _subjectRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByClassAsync(string classLevel)
    {
        return await _subjectRepository.GetByClassLevelAsync(classLevel);
    }

    public async Task<Subject?> GetSubjectByIdAsync(int id)
    {
        return await _subjectRepository.GetByIdAsync(id);
    }
}
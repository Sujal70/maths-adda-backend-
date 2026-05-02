using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MathsAdda.Data.Entities;

public class Student
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    [MaxLength(50)]
    public string Class { get; set; } = string.Empty; // e.g., "9th", "10th"

    [MaxLength(100)]
    public string SchoolName { get; set; } = string.Empty;

    public int? EnrolledCourseId { get; set; }

    [ForeignKey(nameof(EnrolledCourseId))]
    public virtual Course? EnrolledCourse { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(50)]
    public string ClassLevel { get; set; } = string.Empty; // "9th", "10th"

    [MaxLength(50)]
    public string Subject { get; set; } = string.Empty; // "Mathematics", "Science"

    public decimal Price { get; set; }

    [MaxLength(200)]
    public string? ImageUrl { get; set; }

    public string? Curriculum { get; set; } // JSON string of topics

    public bool IsActive { get; set; } = true;

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Student> EnrolledStudents { get; set; } = new List<Student>();
}

public class Subject
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(20)]
    public string ClassLevel { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
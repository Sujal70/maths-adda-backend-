using System.Linq.Expressions;
using MathsAdda.Data.Entities;

namespace MathsAdda.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> SaveChangesAsync();
}

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByEmailWithRoleAsync(string email);
}

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name);
}

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task AddAsync(RefreshToken token);
    Task UpdateAsync(RefreshToken token);
}

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetByUserIdAsync(int userId);
}

public interface ICourseRepository : IRepository<Course>
{
    Task<IEnumerable<Course>> GetByClassLevelAsync(string classLevel);
    Task<IEnumerable<Course>> GetActiveCoursesAsync();
}

public interface ISubjectRepository : IRepository<Subject>
{
    Task<IEnumerable<Subject>> GetByClassLevelAsync(string classLevel);
}
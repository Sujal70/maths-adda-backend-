using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MathsAdda.Data.Context;
using MathsAdda.Data.Entities;

namespace MathsAdda.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MathsAddaDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(MathsAddaDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MathsAddaDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailWithRoleAsync(string email)
    {
        return await _dbSet.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(MathsAddaDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
    }
}

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly MathsAddaDbContext _context;

    public RefreshTokenRepository(MathsAddaDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(r => r.User)
            .ThenInclude(u => u!.Role)
            .FirstOrDefaultAsync(r => r.Token == token);
    }

    public async Task AddAsync(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshToken token)
    {
        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync();
    }
}

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(MathsAddaDbContext context) : base(context)
    {
    }

    public async Task<Student?> GetByUserIdAsync(int userId)
    {
        return await _dbSet.Include(s => s.EnrolledCourse)
            .FirstOrDefaultAsync(s => s.UserId == userId);
    }
}

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(MathsAddaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Course>> GetByClassLevelAsync(string classLevel)
    {
        return await _dbSet.Where(c => c.ClassLevel == classLevel && c.IsActive).ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetActiveCoursesAsync()
    {
        return await _dbSet.Where(c => c.IsActive).ToListAsync();
    }
}

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(MathsAddaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Subject>> GetByClassLevelAsync(string classLevel)
    {
        return await _dbSet.Where(s => s.ClassLevel == classLevel && s.IsActive).ToListAsync();
    }
}
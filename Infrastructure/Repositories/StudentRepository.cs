using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Infrastructure.Abstracts;
using Infrastructure.Context;
using Infrastructure.InfrastructureBases;

namespace Infrastructure.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        #region Fields
        private readonly DbSet<Student> _students;
        #endregion

        #region Constructors
        public StudentRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _students=dBContext.Set<Student>();
        }
        #endregion

        #region Handles Functions
        public async Task<List<Student>> GetStudentsListAsync()
        {
           // return await _students.Include(x => x.Department).ToListAsync();
           return await _students.ToListAsync();
        }
        #endregion


    }
}

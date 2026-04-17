using Core.Wrappers;
using Data.Entities;
using XUnitTest.Wrappers.Interfaces;

namespace XUnitTest.Wrappers.Implementations
{
    public class PaginatedService : IPaginatedService<Student>
    {
        public async Task<PaginatedResult<Student>> ReturnPaginatedResult(IQueryable<Student> source, int pageNumber, int pageSize)
        {
            return await source.ToPaginatedListAsync(pageNumber, pageSize);
        }
    }
}

using MediatR;
using Core.Bases;
using Core.Features.Students.Queries.Results;

namespace Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<GetStudentListResponse>>>
    {
    }
}

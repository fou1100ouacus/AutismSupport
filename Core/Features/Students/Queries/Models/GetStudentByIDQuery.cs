using MediatR;
using Core.Bases;
using Core.Features.Students.Queries.Results;

namespace Core.Features.Students.Queries.Models
{
    public class GetStudentByIDQuery : IRequest<Response<GetSingleStudentResponse>>
    {
        public int Id { get; set; }
        public GetStudentByIDQuery(int id)
        {
            Id=id;
        }
    }
}

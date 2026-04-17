using Core.Features.Students.Commands.Models;
using Data.Entities;

namespace Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void AddStudentCommandMapping()
        {
            CreateMap<AddStudentCommand, Student>()
               .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
               .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr));
        }
    }
}

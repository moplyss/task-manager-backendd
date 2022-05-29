using AutoMapper;
using BLL.DTO;
using DAL.Model;

namespace BLL.Profiles;

public class AssignmentMapperConfiguration : Profile
{
    public AssignmentMapperConfiguration()
    {
        CreateMap<Assignment, AssignmentDTO>()
            .ForMember(x => x.Status, o => o.MapFrom(p => p.Status.ToString()));

        CreateMap<AssignmentDTO, Assignment>()
            .ForMember(x => x.Status, o => o.MapFrom(p => (Status)Enum.Parse(typeof(Status), p.Status)));
    }
}

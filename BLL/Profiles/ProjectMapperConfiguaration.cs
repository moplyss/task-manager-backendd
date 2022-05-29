using AutoMapper;
using BLL.DTO;
using DAL.Model;

namespace BLL.Profiles;

public class ProjectMapperConfiguaration: Profile
{
    public ProjectMapperConfiguaration()
    {
        CreateMap<Project, ProjectDTO>();
        CreateMap<ProjectDTO, Project>();
    }
}

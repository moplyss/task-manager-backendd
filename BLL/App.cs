using AutoMapper;
using BLL.Profiles;

namespace BLL;

internal static class App
{
    public static IMapper Mapper { get; private set; }

    static App()
    {
        var config = new MapperConfiguration(x =>
        {
            x.AddProfile<ProjectMapperConfiguaration>();
            x.AddProfile<AssignmentMapperConfiguration>();
        });

        config.AssertConfigurationIsValid();
        Mapper = config.CreateMapper();
    }
}

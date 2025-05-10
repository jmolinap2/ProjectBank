using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjectBlack.Authorization;

namespace ProjectBlack;

[DependsOn(
    typeof(ProjectBlackCoreModule),
    typeof(AbpAutoMapperModule))]
public class ProjectBlackApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<ProjectBlackAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(ProjectBlackApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}

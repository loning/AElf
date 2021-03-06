using AElf.Management.Interfaces;
using AElf.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace AElf.Management
{
    [DependsOn(typeof(CoreAElfModule))]
    public class ManagementAElfModule : AElfModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<ManagementOptions>(configuration);
            Configure<MonitorDbOptions>(configuration.GetSection("MonitorDb"));
            
            context.Services.AddAssemblyOf<ManagementAElfModule>();
        }
    }
}
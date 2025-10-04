using Autofac;
using formneo.core.Services;
using formneo.service.Services;

namespace formneo.api.Modules
{
    public class GetirModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GetirService>().As<IGetirService>().InstancePerLifetimeScope();
        }
    }
}



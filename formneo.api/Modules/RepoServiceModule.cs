using Autofac;
using System.Reflection;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository.Repositories;
using vesa.repository.UnitOfWorks;
using vesa.repository;
using vesa.service.Mapping;
using vesa.service.Services;
using Module = Autofac.Module;
using NLayer.Service.Services;
using NLayer.Core.Services;

namespace vesa.api.Modules
{
    public class RepoServiceModule : Module

    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ServiceWithDto<,>)).As(typeof(IServiceWithDto<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GlobalServiceWithDto<,>)).As(typeof(IGlobalServiceWithDto<,>)).InstancePerLifetimeScope();



            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();



            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();


            // builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();

        }
    }
}

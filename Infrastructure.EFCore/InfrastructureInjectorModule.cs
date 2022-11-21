using Autofac;
using DAL.Data;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EFCore
{
    public class InfrastructureInjectorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(GenericQuery<>))
                .As(typeof(IAbstractQuery<>))
                .InstancePerDependency();

            builder.RegisterType<LibraryappDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}

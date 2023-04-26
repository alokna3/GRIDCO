using Autofac;
using GRIDCO.Core.Interfaces;
using GRIDCO.Core.Services;

namespace GRIDCO.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ToDoItemSearchService>()
                .As<IToDoItemSearchService>().InstancePerLifetimeScope();
        }
    }
}

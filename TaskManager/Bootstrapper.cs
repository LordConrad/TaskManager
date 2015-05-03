using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Services;
using Unity.Mvc4;

namespace TaskManager
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<ITaskService, TaskService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILogService, LogService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}
using HMS.BAL;
using HMS.BAL.Interface;
using HMS.BAL.Helper;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace HotelManagment
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IHotelManager, HotelManager>();
            container.AddNewExtension<UnityRepositoryHelper>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
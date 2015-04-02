using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using OnlineShop.Logic;
using OnlineShop.Logic.Interface;

namespace OnlineShop.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController) ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IAccess>().To<UserRepository>();
            ninjectKernel.Bind<IAddress>().To<UserRepository>();
            ninjectKernel.Bind<IProduct>().To<UserRepository>();
            ninjectKernel.Bind<IOrder>().To<UserRepository>();
            ninjectKernel.Bind<ICategory>().To<UserRepository>();
            ninjectKernel.Bind<IEmail>().To<Email>();
        }
    }
}
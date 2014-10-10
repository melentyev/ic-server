using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using Ninject; 
using Ninject.Parameters; 
using Ninject.Syntax; 
using Events.Abstract;
using Events.Concrete;

namespace Events.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IEventsRepository>().To<EFEventsRepository>();
            kernel.Bind<ICommentsRepository>().To<EFCommentsRepository>();
            kernel.Bind<ISubscribeRepository>().To<EFSubscriptionRepository>();
        }
    }
}
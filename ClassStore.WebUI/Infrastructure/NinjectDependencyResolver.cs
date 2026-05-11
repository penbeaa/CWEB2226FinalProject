using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using ClassStore.Domain.Abstract;
using ClassStore.Domain.Entities;
using ClassStore.Domain.Concrete;
using System.Configuration;
using ClassStore.WebUI.Infrastructure.Abstract;
using ClassStore.WebUI.Infrastructure.Concrete;

namespace ClassStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel mykernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            mykernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type myserviceType)
        {
            return mykernel.TryGet(myserviceType);
        }

        public IEnumerable<object> GetServices(Type myserviceType)
        {
            return mykernel.GetAll(myserviceType);
        }

        private void AddBindings()
        {
            mykernel.Bind<IClassRepository>().To<EFClassRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse
                (ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
        
        
            mykernel.Bind<IOrderProcessor>()
                        .To<EmailOrderProcessor>()
                        .WithConstructorArgument("settings", emailSettings);

            mykernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }

    }
}
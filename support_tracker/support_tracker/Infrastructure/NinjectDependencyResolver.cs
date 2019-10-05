using System;
using System.Collections.Generic;
using Ninject;
using support_tracker.AbstractRepos;
using support_tracker.Repositories;
using support_tracker.Models;
using System.Data.Entity;
using Ninject.Web.Common;
using support_tracker.DbLayer;

namespace support_tracker.Infrastructure
{
    public class NinjectDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
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
            kernel.Bind<IGenericRepository<Department>>().To<DepartmentsRepository<Department, DbContext>>();
            kernel.Bind<DbContext>().To<DataContext>().InRequestScope();
        }
    }
}
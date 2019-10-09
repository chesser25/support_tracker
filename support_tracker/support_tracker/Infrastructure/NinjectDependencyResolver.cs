using System;
using System.Collections.Generic;
using Ninject;
using support_tracker.Abstracts;
using support_tracker.Repositories;
using support_tracker.Models;
using System.Data.Entity;
using Ninject.Web.Common;
using support_tracker.DbLayer;
using System.Web.Mvc;
using support_tracker.Mailer;

namespace support_tracker.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
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
            kernel.Bind<ITicketsRepository<Ticket>>().To<TicketsRepository<Ticket, DbContext>>();
            kernel.Bind<ITicketsMailer>().To<TicketsMailer>();
            kernel.Bind<DbContext>().To<DataContext>().InSingletonScope();
            kernel.Bind<ITicketStatus<TicketStatus>>().To<TicketStatusRepository<TicketStatus, DbContext>>();
            kernel.Bind<IMessageRepository<Message>>().To<MessageRepository<Message, DbContext>>();
        }
    }
}
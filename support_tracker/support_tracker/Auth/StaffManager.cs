using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using support_tracker.Models;
using support_tracker.DbLayer;
using System.Web.Mvc;
using System.Data.Entity;

namespace support_tracker.Auth
{
    public class StaffManager : UserManager<StaffMember>
    {
        public StaffManager(IUserStore<StaffMember> store)
            : base(store)
        {
        }
        public static StaffManager Create(IdentityFactoryOptions<StaffManager> options,
                                                IOwinContext context)
        {
            DataContext db = DependencyResolver.Current.GetService<DbContext>() as DataContext;
            StaffManager manager = new StaffManager(new UserStore<StaffMember>(db));
            return manager;
        }
    }
}
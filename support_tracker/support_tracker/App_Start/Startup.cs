using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using support_tracker.Auth;
using support_tracker.DbLayer;
using System.Data.Entity;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(support_tracker.Startup))]

namespace support_tracker
{
    public class Startup
    {
        private DbContext dbContext;
        public Startup()
        {
            dbContext = DependencyResolver.Current.GetService<DbContext>();
        }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DataContext>(() => dbContext as DataContext);
            app.CreatePerOwinContext<StaffManager>(StaffManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
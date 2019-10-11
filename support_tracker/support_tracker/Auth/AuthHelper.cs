using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace support_tracker.Auth
{
    public class AuthHelper
    {
        public StaffManager GetStaffManagerFromOwinContext => HttpContext.Current.GetOwinContext().GetUserManager<StaffManager>();
        public IAuthenticationManager GetAuthenticationManagerFromOwinContext => HttpContext.Current.GetOwinContext().Authentication;
    }
}
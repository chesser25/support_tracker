using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace support_tracker.Models
{
    public class StaffMember : IdentityUser
    {
        public int StaffMemberId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }

        [Required]
        public override string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
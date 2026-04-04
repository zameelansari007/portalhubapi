using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PortalHub.Domain.Models.Portal
{
    [Table("UserPasswords", Schema = "Portal")]
    public class UserPassword
    {
        [Key]
        public long UserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PortalHub.Domain.Models.Portal
{
    [Table("UserSocialAccounts", Schema = "Portal")]
    public class UserSocialAccount
    {
        [Key]
        public long SocialId { get; set; }
        public long UserId { get; set; }
        public string Provider { get; set; }
        public string ProviderUserId { get; set; }
        public DateTime LinkedAt { get; set; }

        public User User { get; set; }
    }
}

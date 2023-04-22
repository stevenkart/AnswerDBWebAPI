using System;
using System.Collections.Generic;

namespace AnswerDBWebAPI.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }

        public int UserRoleId { get; set; }
        public string UserRole1 { get; set; } = null!;
        public bool IsUserSelectable { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

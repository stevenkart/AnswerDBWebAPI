using System;
using System.Collections.Generic;

namespace AnswerDBWebAPI.Models
{
    public partial class User
    {
        public User()
        {
            Answers = new HashSet<Answer>();
            Asks = new HashSet<Ask>();
            ChatReceivers = new HashSet<Chat>();
            ChatSenders = new HashSet<Chat>();
            Likes = new HashSet<Like>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string UserPassword { get; set; } = null!;
        public int StrikeCount { get; set; }
        public string BackUpEmail { get; set; } = null!;
        public string? JobDescription { get; set; }
        public int UserStatusId { get; set; }
        public int CountryId { get; set; }
        public int UserRoleId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual UserRole UserRole { get; set; } = null!;
        public virtual UserStatus UserStatus { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Ask> Asks { get; set; }
        public virtual ICollection<Chat> ChatReceivers { get; set; }
        public virtual ICollection<Chat> ChatSenders { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}

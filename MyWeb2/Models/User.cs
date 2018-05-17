using System;
using System.Collections.Generic;

namespace MyWeb2.Models
{
    public partial class User
    {
        public User()
        {
            Communication = new HashSet<Communication>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Password { get; set; }

        public ICollection<Communication> Communication { get; set; }
    }
}

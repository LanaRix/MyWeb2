using System;
using System.Collections.Generic;

namespace MyWeb2.Models
{
    public partial class Communication
    {
        public int CommunicateId { get; set; }
        public int UserId { get; set; }
        public int? BookId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MyWeb2.Models
{
    public partial class Book
    {
        public Book()
        {
            Communication = new HashSet<Communication>();
        }

        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookInformation { get; set; }
        public string Author { get; set; }
        public int? Year { get; set; }

        public ICollection<Communication> Communication { get; set; }
    }
}

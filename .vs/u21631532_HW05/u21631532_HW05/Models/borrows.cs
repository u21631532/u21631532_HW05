using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace u21631532_HW05.Models
{
    public class borrows
    {
        [Key]
        public int borrowId { get; set; }
        public string studentId { get; set; }
        public int bookId { get; set; }
        public DateTime takenDate { get; set; }
        public DateTime broughtDate { get; set; }

        public string borrowName { get; set; }

    }
}
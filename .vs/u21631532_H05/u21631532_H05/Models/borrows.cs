using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace u21631532_H05.Models
{
    public class borrows
    {
        [Key]
        public int borrowId { get; set; }
        public string studentId { get; set; }
        public int bookId { get; set; }
        public DateTime takenDate { get; set; }
        public DateTime broughtDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace u21631532_H05.Models
{
    public class authors
    {
        [Key]
        public int authorId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}
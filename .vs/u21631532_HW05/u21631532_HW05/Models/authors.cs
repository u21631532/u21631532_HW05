using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace u21631532_HW05.Models
{
    public class authors
    {
        [Key]
        public int authorId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

    }
}
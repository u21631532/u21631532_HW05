using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace u21631532_H05.Models
{
    public class students
    {
        [Key]
        public string studentId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string Class { get; set; }
        public int point { get; set; }
    }
}
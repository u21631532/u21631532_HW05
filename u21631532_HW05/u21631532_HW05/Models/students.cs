using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace u21631532_HW05.Models
{
    public class students
    {
        [Key]
        public int studentId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string Class { get; set; }
        public int point { get; set; }
    }
}
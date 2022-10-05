using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace u21631532_HW05.Models
{
    public class books
    {
        [Key]
        public int bookId { get; set; }
        public string name { get; set; }
        public int pagecount { get; set; }
        public int point { get; set; }
        public string authorId { get; set; }
        public string typeId { get; set; }
        public string status { get; set; }
    }
}
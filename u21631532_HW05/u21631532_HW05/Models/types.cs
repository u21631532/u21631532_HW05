using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace u21631532_HW05.Models
{
    public class types
    {
        [Key]
        public int typeId { get; set; }
        public string name { get; set; }
    }
}
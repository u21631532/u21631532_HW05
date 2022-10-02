using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace u21631532_H05.Models
{
    public class types
    {
        [Key]
        public int typeId { get; set; }
        public string name { get; set; }
    }
}
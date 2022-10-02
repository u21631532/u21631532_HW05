using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21631532_H05.Models
{
    public class booksVM
    {
        public List<books> books { get; set; }

        public List<authors> Authors { get; set; }
        public List<students> Students { get; set; }
        public int numberBooks { get; set; }
    }
}
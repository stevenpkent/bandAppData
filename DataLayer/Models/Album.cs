using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public short Rating { get; set; }

        public virtual Band Band { get; set; } //an album will have 1 related band
    }
}
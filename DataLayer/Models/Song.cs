using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Band Band { get; set; } //song is by a band
        public virtual Album Album { get; set; } //song is on an album
    }
}

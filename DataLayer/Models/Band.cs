using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer.Models
{
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Rating { get; set; }

        public virtual ICollection<Album> Albums { get; set; } //band has albums
        public virtual ICollection<Song> Songs { get; set; } //band has songs
    }
}

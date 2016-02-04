using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer
{
    public class Context : DbContext
    {
        public Context()
            : base("name=BandEntities") //use a connection string with the key of BandEntities in the configuration file
        {        
            this.Configuration.LazyLoadingEnabled = false; //loading: https://msdn.microsoft.com/en-us/data/jj574232.aspx
        }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
    }
}

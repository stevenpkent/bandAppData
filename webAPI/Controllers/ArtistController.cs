using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer.Models;
using DataLayer;
using System.Data.Entity;
//EnableCors - not needed here because I set it globally. using System.Web.Http.Cors; 

namespace webAPI.Controllers
{   
    //I set this globally so not needed here. [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/artists")]
    //[Authorize]
    public class ArtistController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get() //GET http://localhost:49677/api/artists
        {
            List<Band> bands = null;

            using (var context = new Context())
            {
                bands = context.Bands.ToList();
            }

            return Ok(bands);          
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id) //GET http://localhost:49677/api/artists/id
        {
            Band band;

            using (var context = new Context()) 
            {
                band = context.Bands.Find(id);  //Where(b => b.Id == id).FirstOrDefault();
                context.Entry(band).Collection(a => a.Albums).Load(); //explicit load. i have disabled lazy loading
            }

            if (band == null)
                return NotFound();
            else
                return Ok(band);
        }

        [Route("")]
        public IHttpActionResult Post(Band band) //POST http://localhost:49677/api/artists , values in body
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                using (var context = new Context())
                {
                    context.Bands.Add(band);
                    context.SaveChanges();
                }

                return Created("api/artists/" + band.Id, band);               
            }
        }

        [Route("{id}")]
        [Authorize]
        public IHttpActionResult Put(int id, [FromBody] Band updateValues) //PUT http://localhost:49677/api/artists/id , update values in body
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                Band band;
                using (var context = new Context())
                {
                    band = context.Bands.Where(b => b.Id == id).FirstOrDefault();                  
                }

                if (band != null)
                {
                    band.Name = updateValues.Name; //does updates in disconnnected state
                    band.Rating = updateValues.Rating;

                    using (var context = new Context()) //open new context, save update
                    {
                        context.Bands.Attach(band);
                        context.Entry(band).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                    return Ok();
                }
                else
                    return NotFound();
            }
        }

        [Route("{id}")]
        [Authorize]
        public IHttpActionResult Delete(int id) //DELETE http://localhost:49677/api/artists/id
        {
            Band band;
            using (var context = new Context())
            {
                band = context.Bands.Where(b => b.Id == id).FirstOrDefault();
                context.Bands.Remove(band);
                context.SaveChanges();
            }

            /*julie lerman showed using a second context and using this code
            using (var context = new BandEntities())
            {
                context.Bands.Attach(band);
                context.Entry(band).State = EntityState.Deleted;
                context.SaveChanges();
            */

            return Ok();
        }
    }
}

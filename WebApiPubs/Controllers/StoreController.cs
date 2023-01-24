using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Xml.Linq;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly pubsContext context;


        public StoreController(pubsContext context)
        {
            this.context = context;
        }

        //GET
        [HttpGet]
        public ActionResult<IEnumerable<Stores>> GetAllStore()
        {
            return context.Stores.ToList();
        }

        //GET BY ID
        [HttpGet("id/{id}")]
        public ActionResult<Stores> GetByIdStore(string id)
        {
            Stores store = (from s in context.Stores
                            where s.StorId== id
                            select s).SingleOrDefault();
            if(store == null)
            {
                return NotFound();
            }
            return store;
        }

        //GET BY NAME
        [HttpGet("name/{name}")]
        public ActionResult<Stores> GetByNameStore(string name)
        {
            Stores store = (from s in context.Stores
                            where s.StorName == name
                            select s).SingleOrDefault();
            if (store == null)
            {
                return NotFound();
            }
            return store;
        }

        //GET BY ZIP
        [HttpGet("zip/{zip}")]
        public ActionResult<Stores> GetByZip(string zip)
        {
            Stores store = (from s in context.Stores
                            where s.Zip == zip
                            select s).SingleOrDefault();
            if (store == null)
            {
                return NotFound();
            }
            return store;
        }

        //GET BY CITYSTATE
        [HttpGet("citystate/{city}/{state}")]
        public ActionResult<IEnumerable<Stores>> GetByCityState(string city, string state)
        {
           List<Stores> store = (from s in context.Stores
                         where s.City == city && s.State == state
                         select s).ToList();
            if(store.Count() == 0)
            {
                return NotFound();
            }

            return store;
        }

        //POST
        [HttpPost]
        public ActionResult PostStore(Stores store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(store);
            }
            context.Stores.Add(store);
            context.SaveChanges();
            return Ok();
        }

        //PUT
        [HttpPut("{id}")]
        public ActionResult PutStore(string id, [FromBody] Stores store)
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }
            context.Entry(store).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<Stores> Delete(string id)
        {
            var store = (from a in context.Stores
                             where a.StorId == id
                             select a).SingleOrDefault();
            if (store == null)
            {
                return NotFound();
            }
            context.Stores.Remove(store);
            context.SaveChanges();
            return store;


        }
    }
}

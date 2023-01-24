using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        [HttpGet("{id}")]
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
        [HttpGet("{name}")]
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
        [HttpGet("{zip}")]
        public ActionResult<IEnumerable<Stores>> GetByZip()
        {
            return context.Stores.ToList();
        }

        //GET BY CITYSTATE
        [HttpGet("{citystate}")]
        public ActionResult<IEnumerable<Stores>> GetByCityState()
        {
            return null;
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
        [HttpPut]
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
        [HttpDelete]
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

using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly pubsContext context;


        public PublisherController(pubsContext context)
        {
            this.context = context;
        }
        //GET api/publisher
        [HttpGet]

        public ActionResult<IEnumerable<Publishers>> Get()
        {
            return context.Publishers.ToList();
        }

        //GET BY ID
        //GET api/publisher/:id
        [HttpGet("{id}")]
        public ActionResult<Publishers> GetById(string id)
        {
            Publishers publisher = (from a in context.Publishers
                                   where a.PubId == id
                                   select a).SingleOrDefault();
            return publisher;
        }

        //POST api/publisher
        [HttpPost]

        public ActionResult Post(Publishers publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(publisher);
            }
            context.Publishers.Add(publisher);
            context.SaveChanges();
            return Ok();
        }

        //PUT api/publisher/:id

        [HttpPut("{id}")]

        public ActionResult Put(string id, [FromBody] Publishers publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }
            context.Entry(publisher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        //DELETE  api/publisher/:id

        [HttpDelete("{id}")]

        public ActionResult<Publishers> Delete(string id)
        {
            var publisher = (from a in context.Publishers
                             where a.PubId == id
                             select a).SingleOrDefault();
            if (publisher == null)
            {
                return NotFound();
            }
            context.Publishers.Remove(publisher);
            context.SaveChanges();
            return publisher;


        }

    }
}
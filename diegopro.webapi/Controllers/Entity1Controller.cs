using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using diegopro.webapi;

namespace diegopro.webapi.Controllers
{
    public class Entity1Controller : ApiController
    {
        private Model1Container db = new Model1Container();

        // GET: api/Entity1
        public IQueryable<Entity1> GetEntity1()
        {
            return db.Entity1;
        }

        // GET: api/Entity1/5
        [ResponseType(typeof(Entity1))]
        public async Task<IHttpActionResult> GetEntity1(int id)
        {
            Entity1 entity1 = await db.Entity1.FindAsync(id);
            if (entity1 == null)
            {
                return NotFound();
            }

            return Ok(entity1);
        }

        // PUT: api/Entity1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEntity1(int id, Entity1 entity1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entity1.Id)
            {
                return BadRequest();
            }

            db.Entry(entity1).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Entity1Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Entity1
        [ResponseType(typeof(Entity1))]
        public async Task<IHttpActionResult> PostEntity1(Entity1 entity1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entity1.Add(entity1);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = entity1.Id }, entity1);
        }

        // DELETE: api/Entity1/5
        [ResponseType(typeof(Entity1))]
        public async Task<IHttpActionResult> DeleteEntity1(int id)
        {
            Entity1 entity1 = await db.Entity1.FindAsync(id);
            if (entity1 == null)
            {
                return NotFound();
            }

            db.Entity1.Remove(entity1);
            await db.SaveChangesAsync();

            return Ok(entity1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Entity1Exists(int id)
        {
            return db.Entity1.Count(e => e.Id == id) > 0;
        }
    }
}
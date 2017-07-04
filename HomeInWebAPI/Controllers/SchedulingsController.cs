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
using HomeInWebAPI.Entities;

namespace Controllers
{
    public class SchedulingsController : BaseController
    {
        private HomeInEntities db = new HomeInEntities();

        // GET: api/Schedulings
        public IQueryable<Scheduling> GetSchedulings()
        {
            return db.Schedulings;
        }

        // GET: api/Schedulings/5
        [ResponseType(typeof(Scheduling))]
        public async Task<IHttpActionResult> GetScheduling(int id)
        {
            Scheduling scheduling = await db.Schedulings.FindAsync(id);
            if (scheduling == null)
            {
                return NotFound();
            }

            return Ok(scheduling);
        }

        // PUT: api/Schedulings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutScheduling(int id, Scheduling scheduling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scheduling.id)
            {
                return BadRequest();
            }

            db.Entry(scheduling).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchedulingExists(id))
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

        // POST: api/Schedulings
        [ResponseType(typeof(Scheduling))]
        public async Task<IHttpActionResult> PostScheduling(Scheduling scheduling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Schedulings.Add(scheduling);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchedulingExists(scheduling.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = scheduling.id }, scheduling);
        }

        // DELETE: api/Schedulings/5
        [ResponseType(typeof(Scheduling))]
        public async Task<IHttpActionResult> DeleteScheduling(int id)
        {
            Scheduling scheduling = await db.Schedulings.FindAsync(id);
            if (scheduling == null)
            {
                return NotFound();
            }

            db.Schedulings.Remove(scheduling);
            await db.SaveChangesAsync();

            return Ok(scheduling);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchedulingExists(int id)
        {
            return db.Schedulings.Count(e => e.id == id) > 0;
        }
    }
}
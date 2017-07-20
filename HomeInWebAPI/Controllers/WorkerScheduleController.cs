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
using Controllers;

namespace HomeInWebAPI.Controllers
{
    public class WorkerScheduleController : BaseController
    {
        private HomeInEntities db = new HomeInEntities();

        // GET: api/WorkerSchedule
        //public IQueryable<WorkerSchedule> GetWorkerSchedules()
        //{
        //    return db.WorkerSchedules;
        //}

        // GET: api/WorkerSchedule/5
        [ResponseType(typeof(WorkerSchedule))]
        public async Task<IHttpActionResult> GetWorkerSchedule(int id)
        {
            WorkerSchedule workerSchedule = await db.WorkerSchedules.FindAsync(id);
            if (workerSchedule == null)
            {
                return NotFound();
            }

            return Ok(workerSchedule);
        }

        // PUT: api/WorkerSchedule/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWorkerSchedule(int id, WorkerSchedule workerSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workerSchedule.id)
            {
                return BadRequest();
            }

            db.Entry(workerSchedule).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerScheduleExists(id))
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

        // POST: api/WorkerSchedule
        [ResponseType(typeof(WorkerSchedule))]
        public async Task<IHttpActionResult> PostWorkerSchedule(WorkerSchedule workerSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkerSchedules.Add(workerSchedule);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = workerSchedule.id }, workerSchedule);
        }

        // DELETE: api/WorkerSchedule/5
        [ResponseType(typeof(WorkerSchedule))]
        public async Task<IHttpActionResult> DeleteWorkerSchedule(int id)
        {
            WorkerSchedule workerSchedule = await db.WorkerSchedules.FindAsync(id);
            if (workerSchedule == null)
            {
                return NotFound();
            }

            db.WorkerSchedules.Remove(workerSchedule);
            await db.SaveChangesAsync();

            return Ok(workerSchedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkerScheduleExists(int id)
        {
            return db.WorkerSchedules.Count(e => e.id == id) > 0;
        }
    }
}
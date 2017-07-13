using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HomeInWebAPI;
using HomeInWebAPI.Entities;

namespace Controllers
{
    [RoutePrefix("api/people")]
    public class PeopleController : BaseController
    {
        private HomeInEntities db = new HomeInEntities();

        // GET: api/People
        public IQueryable<Person> GetPeople()
        {
            return db.People;
        }

        // GET: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(int id)
        {
            Person person = db.People.Find(id);
            
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        //Get worker schedule
        [Route("schedule")]
        [HttpGet]
        public IHttpActionResult GetPersonSchedule(int id)
        {
            var schedule = (from p in db.People
                        join sch in db.WorkerSchedules on p.id equals sch.worker_id
                        join r in db.PersonRoles on p.id equals r.person_id
                        join role in db.Roles on r.role_id equals role.id
                        where p.id == id
                        select new
                        {
                            Id = p.id,
                            Name = p.name,
                            RoleId = r.role_id,
                            RoleName = role.name,
                            RoleDescription = role.description,
                            StartTime = sch.startdate,
                            EndTiem = sch.enddate
                        }).ToList();

            return Ok(schedule);
        }

        //[Route("worker")]
        //[HttpGet]
        //public IHttpActionResult GetWorker(int Workerid)
        //{
        //    var skills = (from p in db.People
        //                    join sw in db.SkillsWorkers on p.id equals sw.person_id
        //                    join s in db.Skills on sw.skill_id equals s.id
        //                    where p.id == Workerid
        //                    select new
        //                    {
        //                        Skill = s.name,
        //                        Rating = sw.averageRating
                                
        //                    }).ToList();

        //    var lastHired = (from p in db.People
        //                     join lh in db.LastHired on p.id equals lh.workerId
        //                     join user in db.People on lh.userId equals user.id
        //                     where p.id == Workerid
        //                     select new
        //                     {
        //                         Worker = 
        //                         Name 
        //                     })

        //    return Ok(schedule);
        //}

        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson(int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.id)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = person.id }, person);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(int id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            db.SaveChanges();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.People.Count(e => e.id == id) > 0;
        }
    }
}
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
    [RoutePrefix("api/person")]
    public class PeopleController : BaseController
    {
        private HomeInEntities db = new HomeInEntities();

        // GET: api/People
        public IQueryable<Person> GetPeople()
        {
            return db.Person;
        }

        // GET: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(int id)
        {
            Person person = db.Person.Find(id);
            
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
            var schedule = (from p in db.Person
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

        /// <summary>
        /// Get worker information based on their id
        /// </summary>
        /// <param name="Workerid"></param>
        /// <returns></returns>
        [Route("workerRating")]
        [HttpGet]
        public IHttpActionResult GetWorker(int Workerid)
        {
            var skills = (from p in db.Person
                          join sw in db.WorkerSkills on p.id equals sw.person_id
                          join s in db.Skills on sw.skill_id equals s.id
                          where p.id == Workerid && sw.averageRating > 2 //get avg rating better than 3
                          select new
                          {
                              Name = p.name,
                              Skill = s.name,
                              Rating = sw.averageRating

                          }).ToList();

            var workerInfo = (from p in db.Person
                                 where p.id == Workerid
                                 select new
                                 {
                                     Name = p.name,
                                     WorkerPicture = p.picture

                                 }).FirstOrDefault();

            var lastHired = (from p in db.Person
                             join lh in db.Employers on p.id equals lh.worker_id
                             join user in db.Person on lh.user_id equals user.id
                             where p.id == Workerid
                             select new
                             {
                                 User = user.name,
                                 UserPicture = user.picture
                             }).ToList();
            var Worker = new
                                {
                                    id = Workerid,
                                    Name = workerInfo.Name,
                                    Picture = workerInfo.WorkerPicture,
                                    Skills = skills,
                                    Employers = lastHired
                                };
         

            return Ok(Worker);
        }

        /// <summary>
        /// Get worker information based on their id
        /// </summary>
        /// <param name="Workerid"></param>
        /// <returns></returns>
        [Route("WorkerPreferences")]
        [HttpGet]
        public IHttpActionResult GetWorkerPreferences(int workerId)
        {
            var skills = (from p in db.Person
                          join sw in db.WorkerSkills on p.id equals sw.person_id
                          join s in db.Skills on sw.skill_id equals s.id
                          where p.id == workerId
                          select new
                          {
                              Name = p.name,
                              Skill = s.name,
                              Rating = sw.averageRating

                          }).ToList();
            var addressList =  (from p in db.Person
                                join a in db.Addresses on p.id equals a.person_id
                                where p.id == workerId
                                select new
                                          {

                                           Street = a.street,
                                           State = a.state,
                                           Zipcode = a.zipcode,
                                           Address = a.street + "," + a.state + "," + a.zipcode

                                           }).ToList();

            var workerInfo = (from p in db.Person
                              where p.id == workerId
                              select new
                              {
                                  Name = p.name,
                                  Gender = p.gender,
                                  Email = p.email,
                                  Phone = p.phone,
                              }).FirstOrDefault();

            var lastHired = (from p in db.Person
                             join lh in db.LastHiredBies on p.id equals lh.worker_Id
                             where p.id == workerId
                             select new
                             {
                                 Name = lh.user_name,
                                 Email = lh.user_email,
                                 Phone = lh.user_phone
                             }).FirstOrDefault();

            var paymentInfo = (from p in db.Person
                               join pay in db.PaymentProfiles on p.id equals pay.person_id
                               where p.id == workerId
                               select new
                               {
                                   PaymentMode = pay.type
                                   
                               }).FirstOrDefault();

            var availability = (from p in db.Person
                                       join a in db.WorkerAvailabilities on p.id equals a.worker_Id
                                       where p.id == workerId
                                       select new
                                       {
                                           MilesWillingToDrive = a.MilesWantToDrive,
                                           AvailableDays = a.DaysAvailable

                                       }).FirstOrDefault();
            var Worker = new
            {
                id = workerId,
                Name = workerInfo.Name,
                Skills = skills,
                Address = addressList,
                Payment = paymentInfo,
                Availability = availability,
                Employers = lastHired,
            };


            return Ok(Worker);
        }
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

            db.Person.Add(person);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = person.id }, person);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(int id)
        {
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            db.Person.Remove(person);
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
            return db.Person.Count(e => e.id == id) > 0;
        }
    }
}
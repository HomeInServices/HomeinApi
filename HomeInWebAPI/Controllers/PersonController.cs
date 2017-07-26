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
using HomeInWebAPI.Models.Worker;

namespace Controllers
{
    [RoutePrefix("api/person")]
    public class PeopleController : BaseController
    {
        private HomeInEntities db = new HomeInEntities();

        // GET: api/People
        //public IQueryable<Person> GetPeople()
        //{
        //    return db.People;
        //}

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

        /*Worker Schedule*/
        /// <summary>
        /// Get Schedule of a worker
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("workerSchedule")]
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

        /*User Screen - */
        /// <summary>
        /// Get worker information based on their skills and last Employer 
        /// </summary>
        /// <param name="Workerid"></param>
        /// <returns></returns>
        [Route("workerRating")]
        [HttpGet]
        public IHttpActionResult GetWorker(int Workerid)
        {
            var skills = (from p in db.People
                          join sw in db.WorkerSkills on p.id equals sw.person_id
                          join s in db.Skills on sw.skill_id equals s.id
                          where p.id == Workerid && sw.averageRating > 2 //get avg rating better than 3
                          select new
                          {
                              SkillId = s.id,
                              Name = p.name,
                              Skill = s.name,
                              Rating = sw.averageRating

                          }).ToList();

            var workerInfo = (from p in db.People
                              where p.id == Workerid
                                 select new
                                 {
                                     Name = p.name,
                                     WorkerPicture = p.picture

                                 }).FirstOrDefault();

            var lastHired = (from p in db.People
                             join lh in db.Employers on p.id equals lh.worker_id
                             join user in db.People on lh.user_id equals user.id
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
        /// Get worker information for prefilling preferences screen
        /// </summary>
        /// <param name="Workerid"></param>
        /// <returns></returns>
        [Route("workerPreferences")]
        [HttpGet]
        public IHttpActionResult GetWorkerPreferences(int workerId)
        {
            if(workerId >= 0) { 

                    var skills = (from p in db.People
                                  join sw in db.WorkerSkills on p.id equals sw.person_id
                                  join s in db.Skills on sw.skill_id equals s.id
                                  where p.id == workerId
                                  select new
                                  {
                                      Name = p.name,
                                      Skill = s.name,
                                      Rating = sw.averageRating

                                  }).ToList();
                    var addressList =  (from p in db.People
                                        join a in db.Addresses on p.id equals a.person_id
                                        where p.id == workerId
                                        select new
                                                  {

                                                   Street = a.street,
                                                   State = a.state,
                                                   Zipcode = a.zipcode,
                                                   Address = a.street + "," + a.state + "," + a.zipcode

                                                   }).ToList();

                    var workerInfo = (from p in db.People
                                      where p.id == workerId
                                      select new
                                      {
                                          Name = p.name,
                                          Gender = p.gender,
                                          Email = p.email,
                                          Phone = p.phone,
                                      }).FirstOrDefault();

                    var lastHired = (from p in db.People
                                     join lh in db.LastHiredBies on p.id equals lh.worker_Id
                                     where p.id == workerId
                                     select new
                                     {
                                         Name = lh.user_name,
                                         Email = lh.user_email,
                                         Phone = lh.user_phone
                                     }).FirstOrDefault();

                    var paymentInfo = (from p in db.People
                                       join pay in db.PaymentProfiles on p.id equals pay.person_id
                                       where p.id == workerId
                                       select new
                                       {
                                           PaymentMode = pay.type
                                   
                                       }).FirstOrDefault();

                    var availability = (from p in db.People
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
                else
                {
                    return BadRequest("Worker is invalid");
                }
        }

        [Route("workerBasicInformation")]
        [HttpPost]
        public IHttpActionResult PostBasicInformation(BasicInformation bi)
        {
            if(bi != null) { 
                using (var dbv = new HomeInEntities())
                {
                    
                    var resultPerson = dbv.People.FirstOrDefault(x => x.facebook_id == bi.facebookid);

                    if (resultPerson != null)
                    { 
                        var resultAddress = dbv.Addresses.FirstOrDefault(x => x.person_id == resultPerson.id);
                        var resultMWD = dbv.WorkerAvailabilities.FirstOrDefault(x => x.worker_Id == resultPerson.id);

                        if (resultAddress != null && resultMWD != null)
                        {
                            resultPerson.phone = bi.phone;

                            resultAddress.street = bi.street;
                            resultAddress.city = bi.city;
                            resultAddress.state = bi.state;
                            resultAddress.country = bi.country;
                            resultAddress.zipcode = bi.zipcode;

                            resultMWD.MilesWantToDrive = bi.MilesWantToDrive;
                            try
                            { 
                                dbv.SaveChanges();
                                return Ok("Basic Information updated ");
                            }
                            catch(Exception e)
                            {
                                return BadRequest("Error: oops! Something went wrong: " + e.Message);
                            }

                        }
                        else
                        {
                            Address ad = new Address()
                            {
                                person_id = resultPerson.id,
                                type_id = 1,
                                street = bi.street,
                                city = bi.city,
                                state = bi.state,
                                country = bi.country,
                                zipcode = bi.zipcode
                            };
                            dbv.Addresses.Add(ad);

                            WorkerAvailability wa = new WorkerAvailability()
                            {
                                worker_Id = resultPerson.id,
                                MilesWantToDrive = bi.MilesWantToDrive,
                                DaysAvailable = ""
                            };
                            dbv.WorkerAvailabilities.Add(wa);

                            resultPerson.phone = bi.phone;
                        }
                        try
                        {
                            dbv.SaveChanges();
                            return Ok("Basic Information updated ");
                        }
                        catch (Exception e)
                        {
                            return BadRequest("Error: oops! Something went wrong: " + e.Message);
                        }
                    }
                    else
                    {
                        return BadRequest("Error: Request cannot be completed at this time. Please register with the application");
                    }
                 }
            }
            else
            {
                BadRequest("Error: Basic information is invalid, please review.");
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("workerSkillsPaymentOptions")]
        [HttpPost]
        public IHttpActionResult SkillsPaymentOptions(SkillsPaymentOptions spo)
        {
            if (spo != null)
            {
                using (var dbv = new HomeInEntities())
                {
                    var resultPerson = dbv.People.SingleOrDefault(x => x.facebook_id == spo.facebookid);

                    if (resultPerson != null)
                    {
                        var resultPayment = dbv.PaymentProfiles.FirstOrDefault(x => x.person_id == resultPerson.id);

                        if (WorkerSkillsDelete(spo))
                        {
                            foreach (var skillId in spo.SkillIds)
                            {
                                WorkerSkill ws = new WorkerSkill()
                                {
                                    skill_id = skillId,
                                    person_id = resultPerson.id,
                                    averageRating = null
                                };
                                dbv.WorkerSkills.Add(ws);
                            }

                        }
                        if (resultPayment != null)
                        {
                            resultPayment.type = spo.paymentType; //need to create a table with person id and payment type
                            resultPayment.person_id = resultPerson.id;
                            resultPayment.billing_address = spo.billingAddress;
                        }
                        else
                        {
                            PaymentProfile pp = new PaymentProfile()
                            {
                                type = spo.paymentType,
                                person_id = resultPerson.id,
                                billing_address = spo.billingAddress
                            };
                            dbv.PaymentProfiles.Add(pp);
                        }
                        try
                        {
                            dbv.SaveChanges();
                            return Ok("Skills Payment Options updated ");
                        }
                        catch (Exception e)
                        {
                            return BadRequest("Error: oops! Something went wrong: " + e.Message);
                        }
                    }
                    else
                    {
                        return BadRequest("Error: oops! User not registered ");
                    }
                }
            }
            else
            {
                return BadRequest("Error: oops! parameters not valid ");
            }

        }

        public bool WorkerSkillsDelete(SkillsPaymentOptions spo)
        {
            using (var dbv = new HomeInEntities()) {

                var resultPerson = dbv.People.SingleOrDefault(x => x.facebook_id == spo.facebookid);

                if (resultPerson != null) { 
                    var userSkills = dbv.WorkerSkills.Where(x => x.person_id == resultPerson.id).ToList();

                    if (userSkills != null) {

                        foreach (var us in userSkills)
                        {
                            dbv.WorkerSkills.Remove(us);
                        }

                        dbv.SaveChanges();
                        return true;
                    }
                    else
                    {
                        //user skills are null so we need to add them.
                        return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// api/People/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
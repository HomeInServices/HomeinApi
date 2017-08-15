using Controllers;
using HomeInWebAPI.Entities;
using HomeInWebAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HomeInWebAPI.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        
        /*Worker Schedule*//*User Screen - */
        /// <summary>
        /// Get Schedule of a worker
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("workerSchedule")]
        [HttpGet]
        public IHttpActionResult GetPersonSchedule(int id)
        {
            using (var db = new HomeInEntities())
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
            
        }

        /*User Screen - */
        /// <summary>
        /// Get worker information based on their skills and last Employer 
        /// </summary>
        /// <param name="Workerid"></param>
        /// <returns></returns>
        [Route("workerRating")]
        [HttpGet]
        public IHttpActionResult GetWorker(int workerid)
        {
            using (var db = new HomeInEntities())
            {
                var skills = (from p in db.People
                              join sw in db.WorkerSkills on p.id equals sw.person_id
                              join s in db.Skills on sw.skill_id equals s.id
                              where p.id == workerid && sw.averageRating > 2 //get avg rating better than 3
                              select new
                              {
                                  SkillId = s.id,
                                  Name = p.name,
                                  Skill = s.name,
                                  Rating = sw.averageRating

                              }).ToList();

                var workerInfo = (from p in db.People
                                  where p.id == workerid
                                  select new
                                  {
                                      Name = p.name,
                                      WorkerPicture = p.picture

                                  }).FirstOrDefault();

                var lastHired = (from p in db.People
                                 join lh in db.Employers on p.id equals lh.worker_id
                                 join user in db.People on lh.user_id equals user.id
                                 where p.id == workerid
                                 select new
                                 {
                                     User = user.name,
                                     UserPicture = user.picture
                                 }).ToList();
                var Worker = new
                {
                    id = workerid,
                    Name = workerInfo.Name,
                    Picture = workerInfo.WorkerPicture,
                    Skills = skills,
                    Employers = lastHired
                };


                return Ok(Worker);
            }
        }

        /*Worker Screen - */
        /// <summary>
        /// Get worker information for prefilling preferences screen
        /// </summary>
        /// <param name="Workerid"></param>
        /// <returns></returns>
        [Route("userPreferences")]
        [HttpGet]
        public IHttpActionResult GetUserPreferences(int userId)
        {
            if (userId >= 0)
            {
                using (var db = new HomeInEntities())
                {
                    
                    var addressList = (from p in db.People
                                       join a in db.Addresses on p.id equals a.person_id
                                       where p.id == userId
                                       select new
                                       {

                                           Street = a.street,
                                           State = a.state,
                                           Zipcode = a.zipcode,
                                           Address = a.street + "," + a.state + "," + a.zipcode

                                       }).ToList();

                    var AddressInfo = (from p in db.People
                                       join ai in db.AddressInformations on p.id equals ai.person_id
                                        where p.id == userId
                                        select new
                                        {
                                            person_id = p.id,
                                            LivingRooms = ai.LivingRooms,
                                            SquareFootSize = ai.SquareFootSize,
                                            BedRooms = ai.BedRooms,
                                            Kitchen = ai.Kitchen
                                        }).FirstOrDefault();

                    var userInfo = (from p in db.People
                                      where p.id == userId
                                      select new
                                      {
                                          Name = p.name,
                                          Gender = p.gender,
                                          Email = p.email,
                                          Phone = p.phone,
                                      }).FirstOrDefault();

                    var paymentInfo = (from p in db.People
                                       join pay in db.PaymentProfiles on p.id equals pay.person_id
                                       where p.id == userId
                                       select new
                                       {
                                           PaymentMode = pay.type

                                       }).FirstOrDefault();

                    
                    var User = new
                    {
                        id = userId,
                        Name = userInfo.Name,
                        Address = addressList,
                        AddressInformation = AddressInfo,
                        Payment = paymentInfo,
                    };


                    return Ok(User);
                }
            }
            else
            {
                return BadRequest("User is invalid");
            }
        }

        /*User Screen - */
        /// <summary>
        /// Basic Information of User
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        [Route("userBasicInformation")]
        [HttpPost]
        public IHttpActionResult PostBasicInformation(BasicInformation bi)
        {
            if (bi != null)
            {
                using (var dbv = new HomeInEntities())
                {

                    var resultPerson = dbv.People.FirstOrDefault(x => x.facebook_id == bi.facebookid);

                    if (resultPerson != null)
                    {
                        var resultAddress = dbv.Addresses.FirstOrDefault(x => x.person_id == resultPerson.id);

                        if (resultAddress != null)
                        {
                            resultPerson.phone = bi.phone;

                            resultAddress.street = bi.street;
                            resultAddress.city = bi.city;
                            resultAddress.state = bi.state;
                            resultAddress.country = bi.country;
                            resultAddress.zipcode = bi.zipcode;
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

        /*User Screen - */
        /// <summary>
        /// Payment options for user
        /// </summary>
        /// <param name="po"></param>
        /// <returns></returns>
        [Route("userPaymentOptions")]
        [HttpPost]
        public IHttpActionResult PostPaymentOptions(PaymentOption po)
        {
            if (po != null)
            {
                using (var dbv = new HomeInEntities())
                {
                    var resultPerson = dbv.People.SingleOrDefault(x => x.facebook_id == po.facebookid);

                    if (resultPerson != null)
                    {
                        var resultPayment = dbv.PaymentProfiles.FirstOrDefault(x => x.person_id == resultPerson.id);

                        
                        if (resultPayment != null)
                        {
                            resultPayment.type = po.paymentType; //need to create a table with person id and payment type
                            resultPayment.person_id = resultPerson.id;
                            resultPayment.billing_address = po.billingAddress;
                        }
                        else
                        {
                            PaymentProfile pp = new PaymentProfile()
                            {
                                type = po.paymentType,
                                person_id = resultPerson.id,
                                billing_address = po.billingAddress
                            };
                            dbv.PaymentProfiles.Add(pp);
                        }
                        try
                        {
                            dbv.SaveChanges();
                            return Ok("Payment Options updated ");
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

        /*User Screen - */
        /// <summary>
        /// House Information for user
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        [Route("userHouseInformation")]
        [HttpPost]
        public IHttpActionResult PostHouseInformation(HouseInformation hi)
        {
            if (hi != null)
            {
                using (var dbv = new HomeInEntities())
                {
                    var resultPerson = dbv.People.SingleOrDefault(x => x.facebook_id == hi.facebookid);

                    if (resultPerson != null)
                    {
                        var resultHouseInformation = dbv.AddressInformations.FirstOrDefault(x => x.person_id == resultPerson.id);


                        if (resultHouseInformation != null)
                        {
                            resultHouseInformation.Kitchen = hi.Kitchen;
                            resultHouseInformation.LivingRooms = hi.LivingRooms;
                            resultHouseInformation.SquareFootSize = hi.SquareFootSize;
                            resultHouseInformation.BedRooms = hi.BedRooms;
                        }
                        else
                        {
                            AddressInformation ai = new AddressInformation()
                            {
                                
                                person_id = resultPerson.id,
                                LivingRooms = hi.LivingRooms,
                                SquareFootSize = hi.SquareFootSize,
                                BedRooms = hi.BedRooms,
                                Kitchen = hi.Kitchen
                            };
                            dbv.AddressInformations.Add(ai);
                        }
                        try
                        {
                            dbv.SaveChanges();
                            return Ok("House Information updated ");
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

    }
}

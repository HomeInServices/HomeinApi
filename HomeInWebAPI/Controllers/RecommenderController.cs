using Controllers;
using HomeInWebAPI.Entities;
using HomeInWebAPI.Models.Recommendation;
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
    [RoutePrefix("api/recommendar")]
    public class RecommenderController : BaseController
    {
        /*Recommendation Screen - */
        /// <summary>
        /// House Information for user
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        [Route("recommander")]
        [HttpPost]
        public IHttpActionResult PostRecommenderInfo(EmployerReferral er)
        {
            if (er != null)
            {
                using (var dbv = new HomeInEntities())
                {
                    var resultPerson = dbv.People.SingleOrDefault(x => x.facebook_id == er.facebookid);

                    if (resultPerson != null)
                    {
                        var EmployeeReferral = (from item in dbv.EmployeeReferrals
                                                where item.user_id == resultPerson.id && item.worker_id == er.worker_id
                                                select new
                                                {
                                                    EmployeeReferral = item.employerReferrel,
                                                    RateCharged = item.ratecharged,
                                                    UserId = item.user_id,
                                                    WorkerId = item.worker_id
                                                    
                                                }).ToList();
                        var Ratings = (from item in dbv.Ratings
                                       where item.user_id == resultPerson.id && item.worker_id == er.worker_id
                                       select new {

                                           SkillId = item.skill_id,
                                           Rating = item.rating1,
                                           UserId = item.user_id,
                                           WorkerId = item.worker_id
                                       }).ToList();

                        if (EmployeeReferral.Count() > 0)
                        {
                            return Ok("Referral already submitted!");
                        }

                        if (Ratings.Count() > 0)
                        {
                            return Ok("Ratings already submitted!");
                        }
                        
                        if(EmployeeReferral.Count() <= 0 && Ratings.Count() <=0)
                        {
                            EmployeeReferral erworker = new EmployeeReferral()
                            {
                                user_id = resultPerson.id,
                                worker_id = er.worker_id,
                                employerReferrel = er.employerReferral,
                                ratecharged = er.avgPriceChargedWithEmployer,
                                workExperience = er.workExperienceWithEmployer,
                                recommendationDate = DateTime.Now

                            };
                            dbv.EmployeeReferrals.Add(erworker);
                            
                            var a = er.SkillIds.Keys.ToList();

                            foreach(var key in a)
                            {
                                Rating r = new Rating()
                                {
                                    skill_id = key,
                                    rating1 = er.SkillIds[key],
                                    ratedOn = DateTime.Now,
                                    worker_id = er.worker_id,
                                    user_id = resultPerson.id
                                };

                                dbv.Ratings.Add(r);
                            }
                        }
                        try
                        {
                            dbv.SaveChanges();
                            return Ok("Employee Referral and Ratings updated ");
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

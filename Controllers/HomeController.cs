using JopOffere.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db =new ApplicationDbContext();
        public ActionResult Index()
        {
            var list = db.Categories.ToList();
            return View(list);
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jops.Find(JobId);
            if(job==null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }


        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Apply(string message)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobId"];

            var check = db.ApplyForJobs.Where(a => a.JobId == jobId && a.userId == userId).ToList();

                if (check.Count < 1)
            {

                var job = new ApplyForJob();

                job.userId = userId;
                job.JobId = jobId;
                job.Message = message;
                job.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();

                ViewBag.Result = "Sent";
            }
            else
            {
                ViewBag.Result = " Sorry, you Apply for this job befor";
            }


            return View();
        }


        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.userId == UserId);
            return View(jobs.ToList());

       }

        [HttpGet]
        public ActionResult jobDetails(int Id)
        {
            var job = db.ApplyForJobs.Find(Id);
            if (job == null)
            {
                return HttpNotFound();
            }
       
            return View(job);

        }

        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {

                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;


                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }

            return View(job);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {

            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            try
            {
                var myJob = db.ApplyForJobs.Find(job.Id);
                db.ApplyForJobs.Remove(myJob);
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            catch
            {
                return View(job);
            }
        }


        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var userId = User.Identity.GetUserId();
            var jobs = from app in db.ApplyForJobs
                       join job in db.Jops
                       on app.JobId equals job.Id
                       where job.User.Id == userId
                       select app;

            var grouped = from j in jobs
                          group j by j.job.JopTitle
                        into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
           

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("alaa011203113a@gmail.com", "011203113a");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("alaa011203113a@gmail.com"));
            mail.Subject = contact.supject;
            mail.IsBodyHtml = true;
            string body = "sender: " + contact.Name + "<br>" +
                "email:" + contact.Email + "<br>" +
                "header: " + contact.supject + "<br>" +
                "message: " + contact.message + "<br>";


            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            

            return RedirectToAction("index");
        }

        public ActionResult Search()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jops.Where(a => a.JopTitle.Contains(searchName)
                  || a.JopDescription.Contains(searchName)
                  || a.Category.CategoryName.Contains(searchName)
                  || a.Category.CategoryDescription.Contains(searchName)).ToList();

            return View(result);
        }

        
    }
}
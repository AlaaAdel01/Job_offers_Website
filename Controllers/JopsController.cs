using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JopOffere.Models;
using WebApplication4.Models;
using Microsoft.AspNet.Identity;

namespace JopOffere.Controllers
{
    [Authorize]
    public class JopsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jops
        public async Task<ActionResult> Index()
        {
            var jops = db.Jops.Include(j => j.Category);
            return View(await jops.ToListAsync());
        }

        // GET: Jops/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jop jop = await db.Jops.FindAsync(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            return View(jop);
        }

        // GET: Jops/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Jops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,JopTitle,JopDescription,CategoryId")] Jop jop)
        {
            if (ModelState.IsValid)
            {
                db.Jops.Add(jop);
                jop.UserId = User.Identity.GetUserId();
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", jop.CategoryId);
            return View(jop);
        }

        // GET: Jops/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jop jop = await db.Jops.FindAsync(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", jop.CategoryId);
            return View(jop);
        }

        // POST: Jops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,JopTitle,JopDescription,CategoryId")] Jop jop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jop).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", jop.CategoryId);
            return View(jop);
        }

        // GET: Jops/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jop jop = await db.Jops.FindAsync(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            return View(jop);
        }

        // POST: Jops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Jop jop = await db.Jops.FindAsync(id);
            db.Jops.Remove(jop);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

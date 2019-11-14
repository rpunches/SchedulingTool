using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;

namespace SAT.UI.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: Students
        public ActionResult Index()
        {
            //return list of current students that are enrolled in a class
            //var enrolled = (from e in db.Enrollments
            //                join s in db.Enrollments on e.StudentId equals s.Student.StudentId
            //                select new { e.StudentId, s.Student, }).ToList();

            //ViewBag.Enrolled = enrolled;

            //var enrolled = db.Enrollments.Where(i => i.Student.StudentId == Enrollmen;
            //var enrolled = db.Enrollments.Where(id => id.StudentId));
            //ViewBag.Enrolled = enrolled;

            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {

                #region File Upload
                string imgName = "person-placeholder.jpg";
                if (Photo != null)
                {
                    imgName = Photo.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf("."));
                    string[] goodExts = new string[] { ".jpeg", ".jpg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //imgName = Guid.NewGuid() + ext;

                        Photo.SaveAs(Server.MapPath("~/Content/images/" + imgName));
                    }
                    else
                    {
                        imgName = "person-placeholder.jpg";
                    }
                }

                student.PhotoUrl = imgName;
                #endregion



                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    string imgName = Photo.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf("."));
                    string[] goodExts = new string[] { ".jpeg", ".jpg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //imgName = Guid.NewGuid() + ext;
                        Photo.SaveAs(Server.MapPath("~/Content/images/" + imgName));
                        string currentFile = Request.Params["PhotoUrl"];
                        if (currentFile != null && currentFile != "person-placeholder.jpg" && currentFile.Length != 0)
                        {
                            //uncomment when deployed to test delete
                            System.IO.File.Delete(Server.MapPath("~/Content/images/" + currentFile));
                        }                       
                    }
                    student.PhotoUrl = imgName;
                }


                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            Enrollment enrollment = db.Enrollments.Find(id);
            if (student.PhotoUrl != null && student.PhotoUrl != "person-placeholder.jpg" && student.PhotoUrl.Length != 0)
            {
                System.IO.File.Delete(Server.MapPath("~/Content/images/" + "currentFile"));
            }

            if (student.SSID != 3)
            {
                student.SSID = 3;
            }
            else if (student.SSID == 3)
            {
                student.SSID = 2;
            }
            else
            {
                return RedirectToAction("Edit/" + student.StudentId);
            }
            //db.Students.Remove(student);
            db.SaveChanges();
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

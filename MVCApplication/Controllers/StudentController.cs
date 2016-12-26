using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApplication.ViewModels;
using Microsoft.Reporting.WebForms;


namespace MVCApplication.Controllers
{

    public class StudentController : Controller
    {
        MVCTESTEntities dbContext = new MVCTESTEntities();
        //
        // GET: /Student/
        public ActionResult Index()
        {


            //IEnumerable<StudentInfoViewModel> model = null;

            //model = (from S in dbContext.t_Student
            //         join CA in dbContext.t_ClassAcademicYear on S.Student_ClassAcademicYear_ID equals CA.ClassAcademicYear_ID
            //         join C in dbContext.t_Classes on S.Student_Classes_ID equals C.Classes_ID
            //         join CS in dbContext.t_ClassSection on S.Student_ClassSection_ID equals CS.ClassSection_ID

            //         select new StudentInfoViewModel
            //         {
            //             StudentID = S.Student_ID,
            //             StudentName = S.Student_StudentName,
            //             AcademicYear = CA.ClassAcademicYear_Session,
            //             ClassName = C.Classes_ClassName,
            //             SectionName = CS.ClassSection_SectionName

            //         }).ToList();

            return View();

            // return View();
        }

        public ActionResult StudentList()
        {
            IEnumerable<StudentInfoViewModel> model = null;

            model = (from S in dbContext.t_Student
                     join CA in dbContext.t_ClassAcademicYear on S.Student_ClassAcademicYear_ID equals CA.ClassAcademicYear_ID
                     join C in dbContext.t_Classes on S.Student_Classes_ID equals C.Classes_ID
                     join CS in dbContext.t_ClassSection on S.Student_ClassSection_ID equals CS.ClassSection_ID

                     select new StudentInfoViewModel
                     {
                         StudentID = S.Student_ID,
                         StudentName = S.Student_StudentName,
                         AcademicYear = CA.ClassAcademicYear_Session,
                         ClassName = C.Classes_ClassName,
                         SectionName = CS.ClassSection_SectionName

                     }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Create()
        {
            ViewBag.ClassName = new SelectList(dbContext.t_Classes, "Classes_ID", "Classes_ClassName");
            ViewBag.AplicantGender = new SelectList(dbContext.t_Gender, "Gender_ID", "Gender_GenderName");
            ViewBag.AcademicYear = new SelectList(dbContext.t_ClassAcademicYear, "ClassAcademicYear_ID", "ClassAcademicYear_Session");
            return View();
        }
        public ActionResult Edit(int id = 0)
        {
            // ActionResult return id.ToString();
            t_Student StudentEdit = dbContext.t_Student.Find(id);
            if (StudentEdit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassName = new SelectList(dbContext.t_Classes, "Classes_ID", "Classes_ClassName");
            ViewBag.SectionName = new SelectList(dbContext.t_ClassSection.Where(x => x.ClassSection_ClassID == StudentEdit.Student_Classes_ID), "ClassSection_ID", "ClassSection_SectionName", StudentEdit.Student_ClassSection_ID);

            ViewBag.AplicantGender = new SelectList(dbContext.t_Gender, "Gender_ID", "Gender_GenderName");
            ViewBag.AcademicYear = new SelectList(dbContext.t_ClassAcademicYear, "ClassAcademicYear_ID", "ClassAcademicYear_Session");
            return View(StudentEdit);
            //  return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(t_Student student)
        {
            bool saveFailed;
            try
            {

                if (ModelState.IsValid)
                {
                    dbContext.Entry(student).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                saveFailed = true;

                // Update original values from the database 
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                //catch
                //{
                return View(student);
                //}
            }
        }
        public ActionResult Delete(int id = 0)
        {
            t_Student StudentEdit = dbContext.t_Student.Find(id);
            if (StudentEdit == null)
            {
                return HttpNotFound();
            }
            dbContext.t_Student.Remove(StudentEdit);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id = 0)
        {
            t_Student StudentEdit = dbContext.t_Student.Find(id);
            if (StudentEdit == null)
            {
                return HttpNotFound();
            }



            return View(StudentEdit); //RedirectToAction("Detals");
        }
        //Action result for ajax call
        public ActionResult GetSectionByClassId(int ClassID)
        {
            #region [ Get Section By ClassID]
            List<t_ClassSection> objClassSection = dbContext.t_ClassSection.Where(m => m.ClassSection_ClassID == ClassID).ToList();
            return Json(objClassSection, JsonRequestBehavior.AllowGet);
            #endregion

        }

        [HttpPost]
        public ActionResult Create(t_Student student)
        {
            try
            {
                dbContext.t_Student.Add(student);

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(student);
            }


        }

    }
}
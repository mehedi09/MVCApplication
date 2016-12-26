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


namespace MVCApplication.Controllers
{

    public class StudentController : Controller
    {
        MVCTESTEntities dbContext = new MVCTESTEntities();
        //
        // GET: /Student/
        public ActionResult Index()
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

                     });

            return View(model);

            // return View();
        }
        public ActionResult Create()
        {
            ViewBag.ClassName = new SelectList(dbContext.t_Classes, "Classes_ID", "Classes_ClassName");
            ViewBag.AplicantGender = new SelectList(dbContext.t_Gender, "Gender_ID", "Gender_GenderName");
            ViewBag.AcademicYear = new SelectList(dbContext.t_ClassAcademicYear, "ClassAcademicYear_ID", "ClassAcademicYear_Session");
            return View();
        }

        //Action result for ajax call
        public ActionResult GetSectionByClassId(int ClassID)
        {
            #region [ Get Section By ClassID]
            List<t_ClassSection> objClassSection = dbContext.t_ClassSection.Where(m => m.ClassSection_ClassID == ClassID).ToList();
            // SelectList obgClassSection = new SelectList(objClassSection, "ClassSection_ID", "ClassSection_SectionName", 0);


            if (ClassID == 1)
            {


            }




            return Json(objClassSection, JsonRequestBehavior.AllowGet);
            #endregion

        }
        [HttpPost]
        public ActionResult Create(t_Student student)
        {
            try
            {
                dbContext.t_Student.Add(student);
               // dbContext.t_Classes.Add()
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(student);
            }

            // return View();
        }
       //  [HttpGet]
        //public ActionResult Edit(string id)
        //{

        //}
    }
}
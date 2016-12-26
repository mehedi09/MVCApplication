using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCApplication.ViewModels
{
    public class StudentInfoViewModel
    {
        [DisplayName("Student ID")]
        public int StudentID { get; set; }
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        [DisplayName("Academic Year")]
        public string AcademicYear { get; set; }
        [DisplayName("Class Name")]
        public string ClassName { get; set; }
        [DisplayName("Section Name")]
        public string SectionName { get; set; }
    }
}
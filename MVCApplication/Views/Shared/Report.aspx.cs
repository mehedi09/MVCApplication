using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;


namespace MVCApplication.Views.Shared
{
    public partial class Report : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MVCTESTConnectionString"].ConnectionString);

        MVCTESTEntities dbContext = new MVCTESTEntities();
        static string fileName;
        static string PdfLocation;

        protected void Page_Load(object sender, EventArgs e)
        {
            DeletePDFfiles();
            showReport();
        }
        private void DeletePDFfiles()
        {
            // throw new NotImplementedException();
            string rootFolderPath = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")) + "PDF\\";
            //string PDFPath = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")) + "PDF\\";
            // string rootFolderPath = @"../PDF/";
            string filesToDelete = @"*Student" + "*.pdf";   // Only delete DOC files containing "BankBook" in their filenames
            string[] fileList = System.IO.Directory.GetFiles(rootFolderPath, filesToDelete);
            foreach (string file in fileList)
            {
                //    System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                System.IO.File.Delete(file);
            }
        }
        private void showReport()
        {
            string PDFPath = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")) + "PDF\\";
            string studentID = "";
            string FilePath;
            string Sign;
            int examid = 0;
            //if (!string.IsNullOrEmpty(hdID.Value))
            //{
            studentID = "1,2";
            //}

            ReportViewer1.Reset();
            DataTable dt = getdata(studentID);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rds);

            this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Report/") + "rpt_StudentDetails.rdlc";
            //   ReportViewer1.LocalReport.SetParameters(parameter);


            ReportViewer1.LocalReport.Refresh();



            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            string[] streamids = null;
            String mimeType = null;
            String encoding = null;
            String extension = null;
            Byte[] bytes = null;

            fileName = "StudentDetails" + DateTime.Now.ToFileTime() + ".pdf";

            bytes = ReportViewer1.LocalReport.Render("PDF", "", out mimeType, out encoding, out extension, out streamids, out warnings);


            FileStream fs = new FileStream(PDFPath + fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] data = new byte[fs.Length];
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            PdfLocation = PDFPath + fileName;

            report.Attributes.Add("src", "../PDF/" + fileName);
        }
        public void PopUp(string sMsg)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + sMsg + "');", true);
        }
        private DataTable getdata(string StudentID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("RPT_StudentInformation", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = StudentID;
     
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }
    }
}
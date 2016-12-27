using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MVCApplication.Report
{
    public partial class AppReportViewer : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MVCTESTConnectionString"].ConnectionString);
        static string fileName;
        static string PdfLocation;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DeletePDFfiles();
                ShowReport();
            }

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
        public void PopUp(string sMsg)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + sMsg + "');", true);
        }
        private void ShowReport()
        {
            //AppReportViewer.Reset();
            //AppReportViewer.LocalReport.EnableExternalImages = true;
            //AppReportViewer.LocalReport.ReportPath = Server.MapPath("~/Report/rpt_StudentDetails.rdlc");
            //DataTable dtReportData = new DataTable();
            string PDFPath = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")) + "PDF\\";
            string studentID = "1";
            string FilePath;

            AppReport.Reset();
            DataTable dt = getdata(studentID);
            ReportDataSource rds = new ReportDataSource("DataSet", dt);
            AppReport.LocalReport.DataSources.Add(rds);

            this.AppReport.ProcessingMode = ProcessingMode.Local;
            AppReport.LocalReport.ReportPath = Server.MapPath("../Report/") + "rpt_StudentDetails.rdlc";
            //   ReportViewer1.LocalReport.SetParameters(parameter);

            AppReport.LocalReport.Refresh();



            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            string[] streamids = null;
            String mimeType = null;
            String encoding = null;
            String extension = null;
            Byte[] bytes = null;

            fileName = "StudentDetails" + DateTime.Now.ToFileTime() + ".pdf";

            bytes = AppReport.LocalReport.Render("PDF", "", out mimeType, out encoding, out extension, out streamids, out warnings);


            FileStream fs = new FileStream(PDFPath + fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] data = new byte[fs.Length];
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            PdfLocation = PDFPath + fileName;

            report.Attributes.Add("src", "../PDF/" + fileName);


        }

    }
}
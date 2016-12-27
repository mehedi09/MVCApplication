<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppReportViewer.aspx.cs" Inherits="MVCApplication.Report.AppReportViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="AppReport" runat="server" AsyncRendering="false" SizeToReportContent="true" Visible="false"></rsweb:ReportViewer>
        <embed runat="server" id="report" style="width: 98%;" height="550" name="StudentInformation" type='application/pdf' />
    
    </div>
    </form>
</body>
</html>



function GetDateTimePicker(htmlElement) {
    //$("#Student_AdmissionDate").datepicker(
    htmlElement.datepicker(
        {
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',


        }
    );
};
//$('#Student_Classes_ID').on('change',  $('#DDL_SectionID').value
function LoadSectionsByClassChange() {
  //  alert("got call");
    var value = $('#Student_Classes_ID').val();
    //  $('#Student_Classes_ID')
    $.get('/Student/GetSectionByClassId/', { ClassID: value }, function (data) {
       //   alert("Hello");
          $('#Student_ClassSection_ID').empty();
          $('#Student_ClassSection_ID').append("<option>-- Select --</option>");
        for (i = 0; i < data.length; i++) {
            $('#Student_ClassSection_ID').append("<option value=" + data[i].ClassSection_ID + ">" + data[i].ClassSection_SectionName + "</option>");
        }
    });
};

function LoadStudentList() {
    $.get('../Student/StudentList/', function (data) {
        //var result = "<div class='table-responsive'><table class='table'><tr>";
        //result += "<th style='display:none;'> Student ID </th>";
        //result += "<th>@(MVCApplication.Resources.ModelResource.Student_StudentName)</th>";
        //result += "<th>Academic Year </th>";
        //result += "<th>Class Name</th>";
        //result += "<th>Section Name</th>";
        //result += "<th>Edit/View/Delete</th>";
        result = "";
        for (i = 0; i < data.length; i++) {
            result += "<tr><td style='display:none;'>" + data[i].StudentID + "</td>" +
                "<td> " + data[i].StudentName + "</td>" +
                "<td> " + data[i].AcademicYear + "</td>" +
                "<td> " + data[i].ClassName + "</td>" +
                "<td> " + data[i].SectionName + "</td>" +
                "<td> <a href=../Student/Edit/" + data[i].StudentID + ">Edit </a><a href=../Student/Details/" + data[i].StudentID + "> View</a><a href=../Student/Delete/" + data[i].StudentID + "> Delete</a></td></tr>";
        }
      //  result += "</tbody>";
        //studentListTable
        $('#studentListTable').empty();
        $('#studentListTable').append(result);

    }

)
};
//)

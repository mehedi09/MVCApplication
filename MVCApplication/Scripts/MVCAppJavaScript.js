

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
    var value = $('#Student_Classes_ID').val();
    //  $('#Student_Classes_ID')
    $.get('../Student/GetSectionByClassId/', { ClassID: value }, function (data) {
        //  alert("Hello");
        $('#DDL_SectionID').empty();
        $('#DDL_SectionID').append("<option>-- Select --</option>");

        for (i = 0; i < data.length; i++) {
            $('#DDL_SectionID').append("<option value=" + data[i].ClassSection_ID + ">" + data[i].ClassSection_SectionName + "</option>");
        }
    });
};
//)

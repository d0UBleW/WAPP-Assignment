const toggleStudentDiv = (show) => {
    if (show === true) {
        $("#student-div").show()
        $("#student-div :input").prop('required', true)
        $("#student-div :input").prop('disabled', false)
    }
    else {
        $("#student-div").hide()
        $("#student-div :input").prop('required', false)
        $("#student-div :input").prop('disabled', true)
    }
}

$(function () {
    const userType = $("input[name$='UserTypeRadio']:checked").val()
    if (userType === "admin") {
        toggleStudentDiv(false);
    }
    else if (userType === "student") {
        toggleStudentDiv(true);
    }
})

$(document).ready(function () {
    $("input[name$='UserTypeRadio']").each(function () {
        $(this).on("change", function () {
            if ($(this).val() == "student" && $(this).is(":checked")) {
                toggleStudentDiv(true)
            }
            else if ($(this).val() == "admin" && $(this).is(":checked")) {
                toggleStudentDiv(false)
            }
        })
    })
})


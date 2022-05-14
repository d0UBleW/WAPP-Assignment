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
      checkUsername()
    })
  })
})

const checkUsername = () => {
  const $usernameTxtBox = $("input[name$='UsernameTxtBox']")
  $.ajax({
    type: "POST",
    url: "MyService.asmx/IsUsernameDuplicate",
    data: {
      table: $("input[name$='UserTypeRadio']:checked").val(),
      username: $usernameTxtBox.val()
    },
    success: function (response) {
      $("#username_feedback").hide()
      const isDup = $(response).find("boolean").text()
      if (isDup == "true") {
        $usernameTxtBox.removeClass("is-valid")
        $("#username_feedback").removeClass("valid-feedback")
        $usernameTxtBox.addClass("is-invalid")
        $("#username_feedback").addClass("invalid-feedback")
        $("#username_feedback").text("Username is already taken")
        $("#username_feedback").show()
      }
      else {
        $usernameTxtBox.removeClass("is-invalid")
        $("#username_feedback").removeClass("invalid-feedback")
        $usernameTxtBox.addClass("is-valid")
        $("#username_feedback").addClass("valid-feedback")
        $("#username_feedback").hide()
      }
    }
  })
}

$("[id$='UsernameTxtBox']").on('change', checkUsername)

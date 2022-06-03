$("[data-toggle='passwordToggler']").on('click', function () {
  const $passwordTxtBox = $($.find($(this).data("toggleTarget")))
  const type = $passwordTxtBox.prop("type")
  if (type === "password") {
    $passwordTxtBox.prop("type", "text")
  }
  else {
    $passwordTxtBox.prop("type", "password")
  }
  const classToggle = $(this).data("toggleClass")
  if (classToggle != null) {
    $(this).find("i").toggleClass(classToggle)
  }
})

$("[data-toggle='passwordToggler']").on('click', function () {
  const $passwordTxtBox = $("[data-toggle='password']")
  const type = $passwordTxtBox.prop("type")
  if (type === "password") {
    $passwordTxtBox.prop("type", "text")
  }
  else {
    $passwordTxtBox.prop("type", "password")
  }
  $(this).toggleClass("bi-eye")
})

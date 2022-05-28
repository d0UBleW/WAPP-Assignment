$("[data-toggle='passwordToggler']").on('click', function () {
  const $passwordTxtBox = $(this).closest("div").find("[data-toggle='password']")
  const type = $passwordTxtBox.prop("type")
  if (type === "password") {
    $passwordTxtBox.prop("type", "text")
  }
  else {
    $passwordTxtBox.prop("type", "password")
  }
  $(this).find("i").toggleClass("bi-eye")
})

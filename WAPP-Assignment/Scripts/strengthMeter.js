const base = {
  "upper": 1,
  "lower": 1,
  "numeric": 1,
  "special": 1,
  "length": 10,
}

const enumerateString = (str) => {
  let count = {
    "upper": 0,
    "lower": 0,
    "numeric": 0,
    "special": 0,
    "length": 0,
  }
  count.length = str.length;
  for (let i = 0; i < str.length; i++) {
    let char = str[i]
    if (char >= 'A' && char <= 'Z') {
      count.upper++;
    }
    else if (char >= 'a' && char <= 'z') {
      count.lower++;
    }
    else if (char >= '0' && char <= '9') {
      count.numeric++;
    }
    else {
      count.special++;
    }
  }
  return count;
}

const isValidPassword = (passwd) => {
  const rg = new RegExp("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{10,}$")
  let matches = passwd.match(rg)
  if (matches != null) {
    return true
  }
  return false
}

const calculateStrength = (count, base, valid) => {
  let strength = 0

  if (!valid) {
    if (count.upper > base.upper) {
      strength += base.upper / base.upper * 8
    }
    else {
      strength += count.upper / base.upper * 8
    }

    if (count.lower > base.lower) {
      strength += base.lower / base.lower * 8
    }
    else {
      strength += count.lower / base.lower * 8
    }

    if (count.numeric > base.numeric) {
      strength += base.numeric / base.numeric * 8
    }
    else {
      strength += count.numeric / base.numeric * 8
    }

    if (count.special > base.special) {
      strength += base.special / base.special * 8
    }
    else {
      strength += count.special / base.special * 8
    }

    if (count.length > base.length) {
      strength += base.length / base.length * 18
    }
    else {
      strength += count.length / base.length * 18
    }
  }
  else {
    strength = 50 + (count.length - base.length) * 5
  }

  return Math.min(Math.floor(strength), 100)
}

const updateHelp = ($bar, base) => {
  const $passwordTxtBox = $($.find($bar.data("passwordMeterTarget")))
  const $helpLbl = $($.find($bar.data("passwordMeterHelp")))
  const passwd = $passwordTxtBox.val()
  const count = enumerateString(passwd)
  let helpLabel = "Require "
  const lenLeft = base.length - count.length
  const upperLeft = base.upper - count.upper
  const lowerLeft = base.lower - count.lower
  const numericLeft = base.numeric - count.numeric
  const specialLeft = base.special - count.special
  if (lenLeft > 0) {
    helpLabel += `${lenLeft} more character(s) `
  }

  if (upperLeft > 0) {
    helpLabel += `${upperLeft} more uppercase character(s) `
  }

  if (lowerLeft > 0) {
    helpLabel += `${lowerLeft} more lowercase character(s) `
  }

  if (numericLeft > 0) {
    helpLabel += `${numericLeft} more numeric character(s) `
  }

  if (specialLeft > 0) {
    helpLabel += `${specialLeft} more special character(s)`
  }

  if (helpLabel === "Require ") {
    $helpLbl.hide()
  }
  else {
    $helpLbl.show()
    $helpLbl.text(helpLabel)
  }

}

const updateMeter = ($bar) => {
  const $passwordTxtBox = $($.find($bar.data("passwordMeterTarget")))
  const passwd = $passwordTxtBox.val()
  const count = enumerateString(passwd)
  const valid = isValidPassword(passwd)
  if (!valid && passwd.length > 0) {
    $passwordTxtBox.addClass("is-invalid")
  }
  else {
    $passwordTxtBox.removeClass("is-invalid")
  }
  const strength = calculateStrength(count, base, valid)
  let indicator
  let bgColor

  if (strength >= 85) {
    indicator = "Excellent"
    bgColor = "#0d6efd"
  }
  else if (strength >= 65) {
    indicator = "Strong"
    bgColor = "#198754"
  }
  else if (strength >= 50) {
    indicator = "Average"
    bgColor = "#0dcaf0"
  }
  else if (strength >= 25) {
    indicator = "Weak"
    bgColor = "#ffc107"
  }
  else {
    indicator = "Poor"
    bgColor = "#dc3545"
  }

  $bar.css({"width": `${strength}%`, "background-color": bgColor})
  $bar.text(indicator)
  $bar.prop("aria-valuenow", strength)
}

$(document).ready(function () {
  const $bars = $("[data-password-meter='true']")
  $bars.each(function () {
    const $bar = $(this)
    const $passwordTxtBox = $($.find($bar.data("passwordMeterTarget")))
    $passwordTxtBox.on('keyup', function () {
      updateMeter($bar)
      updateHelp($bar, base)
    })
    updateHelp($bar, base)
  })

})

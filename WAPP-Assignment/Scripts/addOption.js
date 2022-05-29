const createOption = ($list, seedIdx, value) => {
  const newIdx = seedIdx+1
  const listId = $list.prop("id")
  const newId = `${listId}_${newIdx}`
  const newName = `${listId}\$${newIdx}`

  const $newItem = $(`<li class="list-group-item d-flex align-items-center">`)
  const $delBtn = $(`<button type="button" class="btn btn-outline-danger btn-sm rounded-circle me-3"><i class="bi bi-x-lg"></i></button>`)
  $delBtn.on('click', function () {
    this.closest("li").remove()
  })
  $newItem.append($delBtn)

  const $newDiv = $(`<div class="form-check">`)
  const $newCheckBox = $(`<input type="checkbox" id="${newId}" name="${newName}" value="${value}" class="form-check-input me-2" data-idx="${newIdx}" />`)
  const $newLabel = $(`<label for="${newId}">${value}</label>`)
  const $newHidden = $(`<input type="hidden" id="${newId}_hidden" name="${newName}_unchecked" value="${value}" />`)
  $newDiv.append($newCheckBox)
  $newDiv.append($newLabel)
  $newDiv.append($newHidden)

  $newItem.append($newDiv)

  $newCheckBox.on('change', function () {
    if ($(this).is(":checked")) {
      const checked = newName +"_checked"
      $newHidden.prop("name", checked)
      $newItem.addClass("list-group-item-success")
    } else {
      const unchecked = newName + "_unchecked"
      $newHidden.prop("name", unchecked)
      $newItem.removeClass("list-group-item-success")
    }
  })
    return $newItem
}

$("[id$='AddOptBtn']").on('click', function () {
  const $optList = $("#OptList")
  const opt = $("[id$='OptTxtBox']").val()
  $("[id$='OptTxtBox']").val("")
  if (opt === "") return
  const $lastItem = $optList.find("li:last")
  let seedIdx = 0
  if ($lastItem.length != 0) {
    seedIdx = parseInt($lastItem.find("input[type='checkbox']").data("idx"))
  }
  const $newItem = createOption($optList, seedIdx, opt)
  $optList.append($newItem)
})

const CheckOption = () => {
  const $optList = $("#OptList")
  let checked = false
  $("[id$='OptStatus']").hide()
  $optList.find("input[type='checkbox']").each(function () {
      if ($(this).is(":checked")) {
          checked = true
      }
  })
  if (!checked) {
    $("[id$='OptStatus']").show()
  }
  else {
    $("[id$='OptStatus']").hide()
  }
  return checked
}
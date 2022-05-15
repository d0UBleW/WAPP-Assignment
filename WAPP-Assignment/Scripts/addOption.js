const createOption = (tableId, seedIdx, value) => {
    const $newOpt = $("<tr>")
    $newOpt.append($("<td>"))
    const $valColumn = $newOpt.find("td:last")
    const newIdx = seedIdx+1
    const newId = `${tableId}_${newIdx}`
    const newName = `${tableId}\$${newIdx}`
    const $inp = $(`<input id="${newId}" type="checkbox" name="${newName}" value="${value}" />`)
    const $hid = $(`<input id="${newId}_hidden" type="hidden" name="${newName}_unchecked" value="${value}" />`)
    $inp.on("change", function () {
        if (!$inp.is(":checked")) {
            const unchecked = newName + "_unchecked"
            $hid.prop("name", unchecked)
            // $inp.prop("name", unchecked)
        }
        else {
            const checked = newName + "_checked"
            $hid.prop("name", checked)
            // $inp.prop("name", newName)
        }
    })
    $valColumn.append($inp)
    $valColumn.append($hid)
    $valColumn.append($(`<label for="${newId}">${value}</label>`))

    $newOpt.append($("<td>"))
    const $btnColumn = $newOpt.find("td:last")
    const $btn = $(`<button type="button">Delete</button>`)
    $btn.on('click', function () {
        this.closest("tr").remove()
    })
    //$btnColumn.append($(`<button type="button" id="btn${tableId}_${newIdx}">Delete</button>`))
    $btnColumn.append($btn)
    return $newOpt
}

$(document).ready(function () {
    $("[id$='AddOptBtn']").on('click', function () {
        const opt = $("[id$='OptTxtBox']").val()
        $("[id$='OptTxtBox']").val("")
        if (opt === "") return
        const $tbody = $("[id$='OptTable']").find("tbody")
        const $lastRow = $tbody.find("tr:last")
        if ($lastRow.length == 0) {
            const $newRow = createOption("OptTable", 0, opt)
            $tbody.append($newRow)
            return
        }
        const seedIdx = parseInt($lastRow.find("input").prop("id").match(/OptTable_(\d+)/)[1])
        const $newRow = createOption("OptTable", seedIdx, opt)
        $tbody.append($newRow)
    })
})

const CheckOption = () => {
  let checked = false
  $("[id$='OptStatus']").hide()
  $("input[type='checkbox']").each(function () {
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
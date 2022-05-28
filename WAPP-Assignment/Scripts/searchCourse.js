const $SearchCatTxtBox = $("input[id$='SearchCatTxtBox']")
const $SearchCatLbl = $SearchCatTxtBox.siblings("label")
const $searchCatDiv = $("div[id$='searchCat']")
const $SearchTitleTxtBox = $("input[id$='SearchTitleTxtBox']")
const $SearchTitleLbl = $SearchTitleTxtBox.siblings("label")
const $searchTitleDiv = $("div[id$='searchTitle']")
const $CoursePanel = $("div[id$='CoursePanel']")
const $FilterList = $("select[id$='FilterList']")

$(document).ready(function () {
  $searchCatDiv.hide()
  $SearchCatTxtBox.prop("disabled", true)
})

const escapeRegExp = (string) => {
  return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
}

const resetView = () => {
  $CoursePanel.find(".course-container").show()
}

const search = () => {
  const by = $FilterList.find("option:selected").val()
  const $courseContainer = $CoursePanel.find(".course-container")
  if (by == "title") {
    const $ele = $courseContainer.find(".course-title")
    const keyword = $SearchTitleTxtBox.val()
    if (keyword != "") {
      $courseContainer.each(function () {
        $(this).hide()
      })
    }
    else {
      resetView()
    }
    const pattern = `.*${escapeRegExp(keyword.toLowerCase())}.*`
    const rg = new RegExp(pattern)
    $ele.each(function () {
      if ($(this).text().toLowerCase().match(rg) != null) {
        $(this).closest(".course-container").show()
      }
      else {
        $(this).closest(".course-container").hide()
      }
    })
  }
  else {
    const $ele = $courseContainer.find(".course-category-item")
    const keyword = $SearchCatTxtBox.val()
    if (keyword != "") {
      $courseContainer.each(function () {
        $(this).hide()
      })
    }
    else {
      resetView()
    }
    const pattern = `.*${escapeRegExp(keyword.toLowerCase())}.*`
    const rg = new RegExp(pattern)
    $ele.each(function () {
      if ($(this).text().toLowerCase().match(rg) != null) {
        $(this).closest(".course-container").show()
        return false // breaks out
      }
      else {
        $(this).closest(".course-container").hide()
      }
    })
  }
}

$FilterList.on('change', function () {
  $searchTitleDiv.hide()
  $SearchTitleTxtBox.prop("disabled", true)

  $searchCatDiv.hide()
  $SearchCatTxtBox.prop("disabled", true)
  if (this.value == "title") {
    const value = $SearchCatTxtBox.val()
    $searchTitleDiv.show()
    $SearchTitleTxtBox.val(value)
    $SearchTitleTxtBox.prop("disabled", false)
  }
  else {
    const value = $SearchTitleTxtBox.val()
    $searchCatDiv.show()
    $SearchCatTxtBox.val(value)
    $SearchCatTxtBox.prop("disabled", false)
  }
  search()
})

$("button[name='searchBtn']").on('click', search)

$SearchCatTxtBox.keyup(search)
$SearchTitleTxtBox.keyup(search)

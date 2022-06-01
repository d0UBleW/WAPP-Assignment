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
  $CoursePanel.find(".course-container").addClass("d-flex")
}

const search = () => {
  const by = $FilterList.find("option:selected").val()
  console.log(by)
  const unenrolled = $("#enrollmentChk").is(":checked")
  const $courseContainer = $CoursePanel.find(".course-container")
  resetView()
  $courseContainer.each(function () {
    if (by == "title") {
      const keyword = $SearchTitleTxtBox.val()
      const pattern = `.*${escapeRegExp(keyword.toLowerCase())}.*`
      const rg = new RegExp(pattern)
      const title = $($(this).find(".course-title")).text().toLowerCase()
      if (title.match(rg) == null || (unenrolled && $(this).data("enrolled"))) {
        $(this).removeClass("d-flex")
        $(this).hide()
      }
    }
    else {
      const keyword = $SearchCatTxtBox.val()
      if (keyword == "") {
        if (unenrolled && $(this).data("enrolled")) {
          $(this).removeClass("d-flex")
          $(this).hide()
        }
        return
      }
      const pattern = `.*${escapeRegExp(keyword.toLowerCase())}.*`
      const rg = new RegExp(pattern)
      const $ele = $(this).find(".course-category-item")
      if ($ele.length == 0) {
        $(this).removeClass("d-flex")
        $(this).hide()
        return
      }
      const categoryName = Array.from($ele.map(function () {
        return $(this).text().toLowerCase()
      }))
      const matches = new Set(categoryName.map((str) => {
        return str.match(rg)
      }))
      if ((matches.size == 1 && matches.has(null)) || (unenrolled && $(this).data("enrolled"))) {
        $(this).removeClass("d-flex")
        $(this).hide()
      }
    }
  })
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
$("#enrollmentChk").on('change', function () {
  console.log('a')
  search()
})

$SearchCatTxtBox.keyup(search)
$SearchTitleTxtBox.keyup(search)

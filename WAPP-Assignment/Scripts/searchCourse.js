const $SearchCatTxtBox = $("input[id$='SearchCatTxtBox']")
const $SearchTitleTxtBox = $("input[id$='SearchTitleTxtBox']")
const $CoursePanel = $("div[id$='CoursePanel']")
const $FilterList = $("select[id$='FilterList']")

$(document).ready(function () {
    $SearchCatTxtBox.hide()
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
    $SearchTitleTxtBox.hide()
    $SearchTitleTxtBox.prop("disabled", true)

    $SearchCatTxtBox.hide()
    $SearchCatTxtBox.prop("disabled", true)
    if (this.value == "title") {
        const value = $SearchCatTxtBox.val()
        $SearchTitleTxtBox.val(value)
        $SearchTitleTxtBox.show()
        $SearchTitleTxtBox.prop("disabled", false)
    }
    else {
        const value = $SearchTitleTxtBox.val()
        $SearchCatTxtBox.val(value)
        $SearchCatTxtBox.show()
        $SearchCatTxtBox.prop("disabled", false)
    }
    search()
})

$("#searchBtn").on('click', search)

$SearchCatTxtBox.keyup(search)
$SearchTitleTxtBox.keyup(search)

$(document).ready(function () {
    $("#SearchCatLbl").hide()
    $("#SearchCatTxtBox").hide()
    $("#SearchCatTxtBox").prop("disabled", true)
})

const escapeRegExp = (string) => {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
}

const resetView = () => {
    console.log("reset")
    $("#CoursePanel .course-container").show()
}

const search = () => {
    const by = $("#FilterList option:selected").val()
    console.log(`by ${by}`)
    const $courseContainer = $("#CoursePanel .course-container")
    if (by == "title") {
        const $ele = $courseContainer.find(".course-title")
        const keyword = $("#SearchTitleTxtBox").val();
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
            if (this.innerText.match(rg) != null) {
                $(this).closest(".course-container").show()
            }
            else {
                $(this).closest(".course-container").hide()
            }
        })
    }
    else {
        const $ele = $courseContainer.find(".course-category")
        const keyword = $("#SearchCatTxtBox").val();
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
            const $sp = $(this).find("span")
            $sp.each(function () {
                if (this.innerText.match(rg) != null) {
                    $(this).closest(".course-container").show()
                    return false // breaks out
                }
                else {
                    $(this).closest(".course-container").hide()
                }
            })
        })
    }
}

$("#FilterList").on('change', function () {
    $("#SearchTitleLbl").hide()
    $("#SearchTitleTxtBox").hide()
    $("#SearchTitleTxtBox").prop("disabled", true)

    $("#SearchCatLbl").hide()
    $("#SearchCatTxtBox").hide()
    $("#SearchCatTxtBox").prop("disabled", true)
    if (this.value == "title") {
        const value = $("#SearchCatTxtBox").val()
        $("#SearchTitleTxtBox").val(value)
        $("#SearchTitleLbl").show()
        $("#SearchTitleTxtBox").show()
        $("#SearchTitleTxtBox").prop("disabled", false)
    }
    else {
        const value = $("#SearchTitleTxtBox").val()
        $("#SearchCatTxtBox").val(value)
        $("#SearchCatLbl").show()
        $("#SearchCatTxtBox").show()
        $("#SearchCatTxtBox").prop("disabled", false)
    }
    search()
})

$("#searchBtn").on('click', search)

$("#SearchCatTxtBox").keyup(search)
$("#SearchTitleTxtBox").keyup(search)

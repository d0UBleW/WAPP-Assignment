const $CatField = $("input[id$='CatField']")
const $CatList= $("[id$='CatList']")
const $CatAddBtn = $("[id$='CatAddBtn']")
const $CatDelBtn = $("[id$='CatDelBtn']")

const updateCat = () => {
    $CatField.val([].concat([...$CatList.find("option")].map(el => {
        return el.value
    })).join("~|~"))
}

$CatAddBtn.click(function (event) {
    const inputBox = $("input[name$='CatTxtBox']")
    const category = inputBox.val()
    inputBox.val("")
    if (category === "") return
    const currentCategory = [].concat([...$CatList.find("option")].map(el => {
        return el.value
    }))
    let exist = currentCategory.includes(category)
    if (exist) return
    const opt = $("<option />").val(category).html(category)
    $CatList.append(opt)
    updateCat()
})

$CatDelBtn.click(function (event) {
    $CatList.find("option:selected").remove()
    updateCat()
})
const updateCat = () => {
    $("#CatField").val([].concat([...$("#CatList > option")].map(el => {
        return el.value
    })).join("<|>"))
}

$("#CatAddBtn").click(function (event) {
    const inputBox = $("input[name='CatTxtBox']")
    const category = inputBox.val()
    inputBox.val("")
    if (category === "") return
    const currentCategory = [].concat([...$("#CatList > option")].map(el => {
        return el.value
    }))
    let exist = currentCategory.includes(category)
    if (exist) return
    const opt = $("<option />").val(category).html(category)
    $("#CatList").append(opt)
    updateCat()
})

$("#CatDelBtn").click(function (event) {
    $("#CatList option:selected").remove()
    updateCat()
})
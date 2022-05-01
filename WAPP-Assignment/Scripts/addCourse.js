const updateCat = () => {
    $("#CatField").val([].concat([...$("#CatList > option")].map(el => {
        return el.value
    })).join("<|>"))
}

$("#CatAddBtn").click(function (event) {
    const inputBox = $("input[name='CatTxtBox']")
    const category = inputBox.val()
    if (category === "") return
    const opt = $("<option />").val(category).html(category)
    $("#CatList").append(opt)
    inputBox.val("")
    updateCat()
})

$("#CatDelBtn").click(function (event) {
    $("#CatList option:selected").remove()
    updateCat()
})
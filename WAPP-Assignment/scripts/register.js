
function changeForm() {
    const userTypeRadio = document.getElementById(userTypeRadioID)
    const userTypeList = userTypeRadio.getElementsByTagName("input")
    const userType = [...userTypeList].filter(item => item.checked ? item : null)
    const studentDiv = document.getElementById('student-div')
    const studentInput = studentDiv.getElementsByTagName('input')
    for (let box of studentInput) {
        if (userType == "student") {
            box.style.visibility = "visible"
        }
        else {
            box.style.visibility = "hidden"
        }
    }
}

window.onload = changeForm()

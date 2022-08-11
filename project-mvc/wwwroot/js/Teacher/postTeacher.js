const postTeacher = (teacher) => {
    teacher.TeachersSubjects = []

    const checkedValues = $('input[class*=checkbox-subject-]')
    for (let i = 0; checkedValues[i]; ++i) {
        if (checkedValues[i].checked) {
            let tSubject = {
                "TeacherId": teacher.Id.toString(),
                "SubjectId": checkedValues[i].value.toString()
            }
            teacher.TeachersSubjects.push(tSubject)
        }
    }

    const form = $('#__AjaxAntiForgeryForm')
    const token = $('input[name="__RequestVerificationToken"]', form).val()
    const subjectSearch = $('input[name="subjectSearch"]', form).val()

    $.ajax({
        type: "POST",
        url: `https://localhost:7051/Teachers/Subjects/?subjectSearch=${subjectSearch}`,
        headers: { "RequestVerificationToken": token },
        data: JSON.stringify(teacher),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (response) => {
            window.location.href = response.redirectToUrl
        }
    })
}

const handleCheckBoxes = (subjectId) => {
    const isBoxChecked = $(`.checkbox-subject-${subjectId}`).prop("checked")

    if (isBoxChecked)
        $(`.checkbox-subject-${subjectId}`).prop("checked", false)
    else
        $(`.checkbox-subject-${subjectId}`).prop("checked", true)
}
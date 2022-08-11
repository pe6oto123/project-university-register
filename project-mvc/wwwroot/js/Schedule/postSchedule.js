const postSchedule = (id, courseId, year) => {
    let schedule = {
        "Id": id.toString(),
        "CourseId": courseId.toString(),
        "Year": year.toString(),
        "SchedulesSubjects": []
    }

    const checkedValues = $('input[class*=checkbox-subject-]')
    for (let i = 0; checkedValues[i]; ++i) {
        if (checkedValues[i].checked) {
            let jSubject = {
                "ScheduleId": id.toString(),
                "SubjectId": checkedValues[i].value.toString()
            }
            schedule.SchedulesSubjects.push(jSubject)
        }
    }

    const form = $('#__AjaxAntiForgeryForm')
    const token = $('input[name="__RequestVerificationToken"]', form).val()
    const subjectSearch = $('input[name="subjectSearch"]', form).val()

    $.ajax({
        type: "POST",
        url: `https://localhost:7051/Courses/Schedule/?subjectSearch=${subjectSearch}`,
        headers: { "RequestVerificationToken": token },
        data: JSON.stringify(schedule),
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
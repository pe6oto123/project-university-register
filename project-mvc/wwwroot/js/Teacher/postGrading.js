const selectListCache = (subjectId, dropDown) => {
	if (localStorage.grading == null) {
		localStorage.setItem("grading", "[]")
	}
	const studentId = dropDown.replace('student-grade-', '')
	const gradeId = $(`#${dropDown} :selected`).val()
	subjectId = subjectId.toString()

	let grading = JSON.parse(localStorage.getItem("grading"))
	let student = grading.find((item) => {
		return item.StudentId == studentId && item.SubjectId == subjectId
	})

	if (student === undefined) {
		grading.push({
			"StudentId": studentId,
			"SubjectId": subjectId,
			"GradeId": gradeId
		})
	}
	else {
		grading.find((item) => {
			if (item.StudentId == studentId && item.SubjectId == subjectId)

				item.GradeId = gradeId
		})
	}

	localStorage.setItem("grading", JSON.stringify(grading))
	$(`#student-grade-${studentId}`).css('color', 'red')
}

const postGrading = () => {
	const form = $('#__AjaxAntiForgeryForm')
	const token = $('input[name="__RequestVerificationToken"]', form).val()
	const subjectId = $('input[name=subjectId]').val()
	const grading = localStorage.getItem("grading")	

	$.ajax({
		type: "POST",
		url: `https://localhost:7051/TeacherUsers/Students?subjectId=${subjectId}`,
		headers: { "RequestVerificationToken": token },
		data: grading,
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: (response) => {
			localStorage.removeItem("grading")
			window.location.href = response.redirectToUrl
		}
	})
}

window.onload = () => {
	const subjectId = $('input[name=subjectId]').val()
	const grading = JSON.parse(localStorage.getItem("grading"))
	grading.forEach(item => {
		if (item.SubjectId == subjectId) {
			$(`#student-grade-${item.StudentId}`).val(item.GradeId)
			$(`#student-grade-${item.StudentId}`).css('color', 'red')
		}
	})
}

const resetSelection = () => {
	localStorage.removeItem("grading")
	const url = window.location.href.split('?')[0]
		+ "?subjectId="
		+ $('input[name=subjectId]').val()

	window.location.replace(url)
}
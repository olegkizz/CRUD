window.onload = function () {
    $('.accordion-item .btn-success').click((e) => {
        if (e.target.tagName == "SPAN")
            e.target = e.target.parentElement;
        let accordionItem = e.target.parentElement.parentElement.parentElement;
        let lessonId = accordionItem.querySelector("#lesson_id").value;
        let formData = new FormData();
        let url = "Lessons/Edit";
        let inputFile = accordionItem.querySelector("input[type=File]");
        let buttons = accordionItem.querySelectorAll(".btn");
        if (inputFile)
            formData.append('File', inputFile.files[0]);
        formData.append('Id', lessonId);
        formData.append('Name', accordionItem.querySelector("#Name").value);
        formData.append('Theme', accordionItem.querySelector("#Theme").value);
        formData.append('CourseId', document.querySelector("#CourseId").value);

        buttonToLoading(buttons);
       
        sendAJAXRequestEditLesson(formData, url, (data) => {
            if (!lessonId && data.statusCode == 200)
                location.reload();
            console.log(data);
            buttonFromLoadingToSuccess(buttons);
            if (data.statusCode != 200) {
                return;
            }
            if (inputFile)
                if (inputFile.files[0]) {
                    inputFile.remove();
                    if (!accordionItem.querySelector(".file-href"));
                    accordionItem.querySelector("label[for=File]")
                        .insertAdjacentHTML("afterEnd", '<a class="file-href" href="' + data.file.path +
                            '" target="_blank">' + data.file.name +
                            '</a><button type="button" class="btn btn-warning btn-close">' +
                            '<span class= "action_text"></span></button>');
                    accordionItem.querySelector(".btn-close").onclick = () => eventDeletingFile(accordionItem);
                }
        },
            (fail) => {
                buttonFromLoadingToSuccess(accordionItem, buttons);
                console.log(fail)
            });
    });
    $('.accordion-item .btn-danger').click((e) => {
        if (e.target.tagName == "SPAN")
            e.target = e.target.parentElement;
        let accordionItem = e.target.parentElement.parentElement.parentElement;
        let formData = new FormData();
        formData.append('Id', accordionItem.querySelector("#lesson_id").value);
        buttonToLoading(accordionItem.querySelectorAll(".btn"));
        sendAJAXRequestEditLesson(formData, "Lessons/Delete", (data) => {
            if (data.statusCode == 200) {
                accordionItem.remove();
            }
        }, (fail) => {
            buttonFromLoadingToSuccess(accordionItem.querySelectorAll(".btn"));
            console.log(fail);
        })
    });

    if ($('.accordion-item .btn-close'))
        $('.accordion-item .btn-close').click((e) => {
            let accordionItem = e.target.parentElement.parentElement.parentElement.parentElement.parentElement;
            eventDeletingFile(accordionItem);
        });

    function sendAJAXRequestEditLesson(formData, url, returnHandler, failHandler) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: window.location.origin + "/" + url,
            data: formData,
            processData: false,
            contentType: false,
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() }
        }).done(returnHandler).fail(failHandler)
    }

    function buttonFromLoadingToSuccess(button) {
        if (button.length)
            button.forEach((btn) => removeLoading(btn));
        else removeLoading(button);
        function removeLoading(btn) {
            if (btn.querySelector(".spinner-border"))
                btn.querySelector(".spinner-border").setAttribute("Style", "display: none");
            btn.querySelector(".action_text").removeAttribute("style");
            btn.classList.remove("btn-light");
            btn.removeAttribute("disabled");
        }
    }

    function buttonToLoading(button) {
        if (button.length)
            button.forEach((btn) => setLoading(btn));
        else setLoading(button);
        function setLoading(btn) {
            btn.querySelector(".spinner-border").setAttribute("style", "inline-block");
            btn.querySelector(".action_text").setAttribute("style", "display:none");
            btn.classList.add("btn-light");
            btn.setAttribute("disabled", "disabled");
        }
    }
    function eventDeletingFile(accordionItem) {
        let url = "Lessons/DeleteFile";
        let formData = new FormData();
        formData.append('lessonId', accordionItem.querySelector("#lesson_id").value);
        sendAJAXRequestEditLesson(formData, url, (data) => {
            if (data.statusCode == 200) {
                accordionItem.querySelector(".file-href").remove();
                accordionItem.querySelector(".btn-close").remove();
                if (!accordionItem.querySelector("input[type=File]"))
                    accordionItem.querySelector("label[for=File]")
                        .insertAdjacentHTML("afterEnd", '<input type="File" accept=".doc,.docx,.xml,.pdf,.csv" />');
            }
            console.log(data)
        },
            (fail) => console.log(fail));
    }
}
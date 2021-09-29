window.onload = function () {
    $('.accordion-item .btn-success').click((e) => {
        if (e.target.tagName == "SPAN")
            e.target = e.target.parentElement;
        let accordionItem = e.target.parentElement.parentElement.parentElement;
        let formData = new FormData(accordionItem.querySelector('#lesson_form'));
        let url = "StudentMarks/Edit";
        let buttons = accordionItem.querySelector(".btn-success");
        let spanErrorHeader = accordionItem.querySelector('.span-header-error');
        let spanSuccessHeader = accordionItem.querySelector('.span-header-success');
      
        formData.append('groupId', document.querySelector('#group_id').value);

        buttonToLoading(buttons);
       
        sendAJAXRequestEditLesson(formData, url, (data) => {
            console.log(data);
            buttonFromLoadingToSuccess(buttons);
            if (data.statusCode != 200) {
                if (data.statusCode == 400) {
                    spanErrorHeader.textContent = data.message;
                    return;
                }
            } else {
                if (spanErrorHeader.textContent.trim() != '')
                    spanErrorHeader.textContent = '';
                spanSuccessHeader.textContent = data.message;
                setTimeout(() => { spanSuccessHeader.textContent = '' }, 2500);
                return;
            }
        },
            (fail) => {
                buttonFromLoadingToSuccess(accordionItem, buttons);
                console.log(fail)
            });
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
}
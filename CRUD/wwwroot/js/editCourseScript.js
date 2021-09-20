﻿window.onload = function () {
    var selectedGroup = document.querySelector("#SelectedGroup");

    selectedGroup.onchange = function updateButtons() {
        let selectedIndex = selectedGroup.options.selectedIndex;
        let editGroupHref = document.querySelector("#editGroupButton").href;
        document.querySelector("#editGroupButton").href = editGroupHref.replace(/\/\d+/g, "/" + selectedGroup.options[selectedIndex].value);
        console.log(selectedIndex, document.querySelector("#editGroupButton").formAction);
    };
};

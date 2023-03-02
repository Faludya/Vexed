function setStatusColor(statusElement) {
    var statusValue = statusElement.innerText;
    if (statusValue === statusManager.Submitted) {
        statusElement.style.color = "var(--bs-warning)";
    } else if (statusValue === statusManager.SuperiorApproval)  {
        statusElement.style.color = "var(--bs-info)";
    } else if (statusValue === statusManager.HRApproval) {
        statusElement.style.color = "var(--bs-success)";
    } else if (statusValue === statusManager.Cancelled) {
        statusElement.style.color = "var(--bs-danger)";
    }
}

var statusElements = document.querySelectorAll("[id^='status-']");
statusElements.forEach(function (statusElement) {
    setStatusColor(statusElement);
});
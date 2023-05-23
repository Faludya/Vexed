$(document).ready(function () {
    var successMessage = $('#successMessage').data('value');

    if (successMessage && successMessage.trim() !== '') {
        // Display the success message using toastr
        toastr.success(successMessage, "Success", {
            timeOut: 5000,
            closeButton: true,
            debug: false,
            newestOnTop: true,
            progressBar: true,
            positionClass: "toast-top-right",
            preventDuplicates: true,
            onclick: null,
            showDuration: "300",
            hideDuration: "1000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut",
            tapToDismiss: false
        });
    }
});

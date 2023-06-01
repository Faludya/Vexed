$(document).ready(function () {
    function createTask() {
        var taskText = $('#taskText').val();

        $.ajax({
            url: '/Home/CreateTask',
            type: 'POST',
            data: { taskText: taskText },
            success: function (response) {
                // Handle the successful response
                console.log(response);
                // Optionally, update the UI or perform any additional actions

                // Clear the input field after successful task creation
                $('#taskText').val('');

                // Add the new task to the task list on the page
                var newTaskElement = $('<li>')
                    .attr('data-task-id', response.id)
                    .append($('<label>')
                        .append($('<input>').attr('type', 'checkbox'))
                        .append('<i></i>')
                        .append($('<span>').text(response.text))
                        .append($('<a>').attr('href', '#').addClass('ti-trash').attr('data-task-id', response.id)));

                $('#todo_list').append(newTaskElement);

                // Adjust the position of the input field
                adjustInputPosition();
            },
            error: function (error) {
                // Handle the error response
                console.log(error);
                // Optionally, display an error message to the user
            }
        });


        updateCompletedTasksUI(('#todo_list'), totalTasks);
    }

    function deleteTask(taskId) {
        $.ajax({
            url: '/Home/DeleteTask',
            type: 'POST',
            data: { taskId: taskId },
            success: function (response) {
                // Handle the successful response
                console.log(response);
                // Optionally, update the UI or perform any additional actions

                // Remove the task element from the list
                $('[data-task-id="' + taskId + '"]').remove();

            },
            error: function (error) {
                // Handle the error response
                console.log(error);
                // Optionally, display an error message to the user
            }
        });

        updateCompletedTasksUI($('#todo_list'), $('#todo_list'));
    }

    function updateTaskStatus(taskId, completed) {
        $.ajax({
            url: '/Home/UpdateTask',
            type: 'POST',
            data: { taskId: taskId, completed: completed },
            success: function (response) {
                // Handle the successful response
                console.log(response);
                // Optionally, update the UI or perform any additional actions
            },
            error: function (error) {
                // Handle the error response
                console.log(error);
                // Optionally, display an error message to the user
            }
        });
    }

    function adjustInputPosition() {
        var taskCount = $('#todo_list li').length;
        var inputField = $('#taskText');
        var inputContainer = $('#taskForm');

        if (taskCount === 0) {
            inputContainer.removeClass('input-adjust');
        } else {
            inputContainer.addClass('input-adjust');
        }
    }

    $('#taskText').on('keydown', function (event) {
        if (event.key === 'Enter') {
            event.preventDefault();
            createTask();
        }
    });

    $(document).on('click', '.ti-trash', function (event) {
        event.preventDefault(); // Prevent the default behavior of the anchor tag

        var taskId = $(this).data('task-id');
        console.log(taskId);
        deleteTask(taskId);
    });

    $(document).on('change', '.task-checkbox', function (event) {
        var taskId = $(this).closest('li').data('task-id');
        var completed = $(this).is(':checked');
        updateTaskStatus(taskId, completed);
    });

});

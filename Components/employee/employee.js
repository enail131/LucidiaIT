lucidia.components.employee = (function ($) {
    var constants = {
        ajax: {            
            createEmployeeUrl: '/Employees/Create',
            editEmployeeUrl: '/Employees/Edit',
            submitButton: '#create-employee-btn',
            editButton: "#edit-employee-btn",
            imagesForm: '#employee-images-form',
            employeeForm: '#employee-info-form',
            initialImage: '#InitialImage',
            hoverImage: '#HoverImage',
            token: "input[name='__RequestVerificationToken']"
        },
        selectors: {
            employeeImage: ".people-img",
            employeeContainer: "#create-employee-container"
        }
    },
        properties = {

        },
        methods = (function (c, p) {
            var clickCreateButtonHandler = function () {
                $(c.ajax.submitButton).click(function () {
                    createEmployee();
                });
            },
                clickEditButtonHandler = function () {
                    $(c.ajax.editButton).click(function () {
                        editEmployee();
                    });
                },
                createEmployee = function () {
                    var formData = new FormData(),
                        token = $(c.ajax.token).val();
                    getEmployeeInfo(formData);
                    getImages(formData);

                    $.ajax({
                        method: 'post',
                        processData: false,
                        contentType: false,
                        cache: false,
                        data: formData,
                        headers: {
                            RequestVerificationToken: token
                        },
                        enctype: 'multipart/form-data',
                        url: c.ajax.createEmployeeUrl,
                        success: createEmployeeSuccess
                    });
                },
                editEmployee = function () {
                    var formData = new FormData(),
                        token = $(c.ajax.token).val();
                    getEmployeeID(formData);
                    getEmployeeInfo(formData);
                    getImages(formData);

                    $.ajax({
                        method: 'post',
                        processData: false,
                        contentType: false,
                        cache: false,
                        data: formData,
                        headers: {
                            RequestVerificationToken: token
                        },
                        enctype: 'multipart/form-data',
                        url: c.ajax.editEmployeeUrl,
                        success: editEmployeeSuccess
                    });
                },
                createEmployeeSuccess = function (data) {
                    $(c.selectors.employeeContainer).html(data);
                },
                editEmployeeSuccess = function (data) {
                    window.location.href = data.url;
                },
                getEmployeeID = function (formData) {
                    var id = $(c.ajax.employeeForm + " #ID").attr("value");
                    formData.append("id", id);
                },
                getEmployeeInfo = function (formData) {
                    $(c.ajax.employeeForm + " input[type='text']").each(function () {
                        formData.append($(this).attr("name"), $(this).val());
                    });

                    $(c.ajax.imagesForm + " input[type='file']").each(function () {
                        var files = $(this).get(0).files;
                        if (files[0] != null) {
                            formData.append($(this).attr("name"), files[0].name);
                        }
                    });
                },
                getImages = function (formData) {
                    $(c.ajax.imagesForm + " input[type='file']").each(function () {
                        var files = $(this).get(0).files;
                        if (files[0] != null) {
                            formData.append('files', files[0]);
                        }
                    });
                },
                eventHandlers = function () {
                    clickCreateButtonHandler();
                    clickEditButtonHandler();
                },
                init = function () {
                    eventHandlers();
                };
            return {
                init: init
            };
        }(constants, properties));
    return methods;
}(jQuery));
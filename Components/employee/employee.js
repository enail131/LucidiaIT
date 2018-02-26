lucidia.components.employee = (function ($) {
    var constants = {
        ajax: {
            uploadUrl: '/Employees/UploadImages',
            createEmployeeUrl: '/Employees/Create',
            submitButton: '#create-employee-btn',
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
                createEmployeeSuccess = function (data) {
                    $(c.selectors.employeeContainer).html(data);
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
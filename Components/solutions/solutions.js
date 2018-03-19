lucidia.components.solutions = (function ($) {
    var constants = {
        ajax: {
            infoForm: '#solution-info-form',
            imageForm: '#solution-image-form',
            editButton: '#edit-solution-btn',
            createButton: '#create-solution-btn',
            token: "input[name='__RequestVerificationToken']",
            createUrl: '/Solutions/Create',
            editUrl: '/Solutions/Edit'
        }
    },
        properties = {

        },
        methods = (function (c, p) {
            var clickCreateButtonHandler = function () {
                $(c.ajax.createButton).click(function () {
                    createSolution();
                });
            },
                createSolution = function () {
                    var formData = new FormData(),
                        token = $(c.ajax.token).val();
                    getSolutionInfo(formData);
                    getSolutionImage(formData);

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
                        url: c.ajax.createUrl,
                        success: getSuccessful
                    });
                },
                editSolution = function () {
                    var formData = new FormData(),
                        token = $(c.ajax.token);
                    getSolutionInfo(formData);
                    getSolutionImage(formData);

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
                        url: c.ajax.editUrl,
                        success: getSuccessful
                    });
                },
                getSuccessful = function (data) {
                    $('#solution-container').html(data);
                },
                getSolutionInfo = function (formData) {
                    $(c.ajax.infoForm + ' input[type="text"]').each(function () {
                        formData.append($(this).attr("name"), $(this).val());
                    });

                    $(c.ajax.imageForm + " input[type='file']").each(function () {
                        var files = $(this).get(0).files;
                        if (files[0] != null) {
                            formData.append($(this).attr("name"), files[0].name);
                        }
                    });
                },
                getSolutionImage = function (formData) {
                    $(c.ajax.imageForm + " input[type='file']").each(function () {
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
    return methods;s
}(jQuery));
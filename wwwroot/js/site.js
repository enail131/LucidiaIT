var lucidia = {
    keyCodes: {
        backspace: 8,
        tab: 9,
        enter: 13,
        shift: 16,
        ctrl: 17,
        alt: 18,
        pause: 19,
        capsLock: 20,
        escape: 27,
        space: 32,
        pageUp: 33,
        pageDown: 34,
        end: 35,
        home: 36,
        leftArrow: 37,
        upArrow: 38,
        rightArrow: 39,
        downArrow: 40,
        insert: 45,
        "delete": 46
    },
    utilities: {},
    components: {},
    log: {
        info: [],
        debug: [],
        error: []
    }
};

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
lucidia.components.contactUs = (function ($) {
    var constants = {
        selectors: {
            sendButton: '#send-email-btn',
            contactForm: '#contact-us-form',
            contactFormContainer: '#contact-form-container'
        },
        ajax: {
            url: '/ContactUs/SendEmail',
            dataType: 'HTML'
        }
    },
        properties = {

        },
        methods = (function (c, p) {
            var clickSendButton = function () {
                $(c.selectors.sendButton).click(function () {
                    sendEmail();
                });                
            },
                sendEmail = function (formData) {
                    if ($(c.selectors.contactForm).valid()) {
                        $.post(c.ajax.url, $(c.selectors.contactForm).serialize(), sendEmailCallback, c.ajax.dataType);
                    }
                },
                sendEmailCallback = function (data) {
                    $(c.selectors.contactFormContainer).html(data);
                },
                eventHandlers = function () {
                    clickSendButton();
                },
                init = function () {
                    eventHandlers();
                };
            return {
                init: init
            }
        }(constants, properties));
    return methods;
}(jQuery));


lucidia.components.navigation = (function ($) {
    var constants = {

    },
        properties = {

        },
        methods = (function (c, p) {
            var testFunction = function () {
                console.log("this is a test function");
            },
                init = function () {
                };
            return {
                init: init
            };
        }(constants, properties));
    return methods;
}(jQuery));
lucidia.components.partners = (function ($) {
    var constants = {
        selectors: {
            leftArrow: '#slider__left-arrow-container',
            rightArrow: '#slider__right-arrow-container',
            sliderArrow: '.slider-arrow',
            sliderContainer: '#partners #partners-container',
            partner: '.partner-container',
            createPartnerContainer: '#create-partner-container'
        },
        ajax: {
            createPartnerButton: '#create-partner-btn',
            editPartnerButton: '#edit-partner-btn',
            partnerImageForm: "#partner-image-form",
            partnerInfoForm: '#partner-info-form',
            createPartnerUrl: '/Partners/Create',
            editPartnerUrl: '/Partners/Edit',
            token: "input[name='__RequestVerificationToken']"
        }
    },
        properties = {

        },
        methods = (function (c, p) {
            var clickArrow = function () {
                $(c.selectors.sliderArrow).click(function (e) {
                    var direction = $(this).data('arrow-direction'),
                        partnerWidth = $(c.selectors.partner).outerWidth(),
                        sliderWidth = $('#partners .partner-container').outerWidth() + 5,
                        sliderPosition = $(c.selectors.sliderContainer).position();

                    if (direction === 'left') {
                        $(c.selectors.sliderContainer).animate({
                            left: (sliderPosition.left + sliderWidth)
                        });
                    } else if (direction === 'right') {
                        $(c.selectors.sliderContainer).animate({
                            left: (sliderPosition.left - sliderWidth)
                        })
                    }
                });
            },
                clickCreateButtonHandler = function () {
                    $(c.ajax.createPartnerButton).click(function () {
                        createPartner();
                    });
                },
                clickEditButtonHandler = function () {
                    $(c.ajax.editPartnerButton).click(function () {
                        editPartner();
                    });
                },
                createPartner = function () {
                    var formData = new FormData(),
                        token = $(c.ajax.token).val();
                    getPartnerInfo(formData);
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
                        url: c.ajax.createPartnerUrl,
                        success: createPartnerSucces
                    });
                },
                editPartner = function () {
                    var formData = new FormData(),
                        token = $(c.ajax.token).val();
                    getPartnerId(formData);
                    getPartnerInfo(formData);
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
                        url: c.ajax.editPartnerUrl,
                        success: editPartnerSuccess
                    });
                },
                createPartnerSucces = function (data) {
                    $(c.selectors.createPartnerContainer).html(data);
                },
                editPartnerSuccess = function (data) {
                    window.location.href = data.url;
                }
                getPartnerId = function (formData) {
                    var id = $(c.ajax.partnerInfoForm + " #ID").attr("value");
                    formData.append("id", id);
                },
                getPartnerInfo = function (formData) {
                    $(c.ajax.partnerInfoForm + " input[type='text']").each(function () {
                        formData.append($(this).attr("name"), $(this).val());
                    });

                    $(c.ajax.partnerImageForm + " input[type='file']").each(function () {
                        var files = $(this).get(0).files;
                        if (files[0] != null) {
                            formData.append($(this).attr("name"), files[0].name);
                        }
                    });
                },
                getImages = function (formData) {
                    $(c.ajax.partnerImageForm + " input[type='file']").each(function () {
                        var files = $(this).get(0).files;
                        if (files[0] != null) {
                            formData.append('files', files[0]);
                        }
                    });
                },
                eventHandlers = function () {
                    clickArrow();
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
lucidia.init = function () {
    lucidia.components.contactUs.init();
    lucidia.components.employee.init();
    lucidia.components.navigation.init();
    lucidia.components.partners.init();
    lucidia.components.solutions.init();
};
jQuery(document).ready(lucidia.init);
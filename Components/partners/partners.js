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
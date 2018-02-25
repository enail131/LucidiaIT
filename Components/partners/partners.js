lucidia.components.partners = (function ($) {
    var constants = {
        selectors: {
            leftArrow: '#slider__left-arrow-container',
            rightArrow: '#slider__right-arrow-container',
            sliderArrow: '.slider-arrow',
            sliderContainer: '#partners #partners-container',
            partner: '.partner-container'
        },
        ajax: {
            createPartnerButton: '#create-partner-btn',
            partnerImageForm: "#partner-image-form",
            partnerInfoForm: '#partner-info-form',
            createPartnerUrl: '/Partners/Create',
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
                        success: function (data) {
                            //console.log(data);
                        }
                    });
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
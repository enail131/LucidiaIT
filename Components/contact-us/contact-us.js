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
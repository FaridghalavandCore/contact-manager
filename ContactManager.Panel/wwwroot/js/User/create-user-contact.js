(function ($, window, document, undefined) {
    function CreateUserContact(element, options) {
        "use strict";

        this.$element = $(element);

        // OPTIONS
        this.apiUrl = options.ApiUrl;
        this.mobileRegex = options.MobileRegex;


        // FORM
        this.$create_form = this.$element.find('form.create-user');

        // FORM INPUTS
        this.$first_name = $(this.$element.find('input[name="FirstName"]'));
        this.$last_name = $(this.$element.find('input[name="LastName"]'));
        this.$mobile = $(this.$element.find('input[name="PhoneNumber"]'));
        this.$email = $(this.$element.find('input[name="Email"]'));
        this.$password = $(this.$element.find('input[name="Password"]'));
        this.$create_user_button = $(this.$element.find('button.create-user'));

        // FORM SPAN        
        this.$first_name_message = this.$element.find('span.first-name');
        this.$last_name_message = this.$element.find('span.last-name');
        this.$mobile_message = this.$element.find('span.phoneNumber');
        this.$email_message = this.$element.find('span.email');
        this.$password_message = this.$element.find('span.password');


        this.initialize();
    }

    CreateUserContact.prototype = {

        initialize: function () {
            let self = this;

            self.$create_user_button.on('click', function () {
                self.createUser();
            }),

                self.$mobile.keyup(function () {
                    self.validateMobile(self.$mobile_message, self.$mobile.val(), self.mobileRegex)
                })
        },

        createUser: function () {
            let self = this;
            self.disableForm();

            let obj = {
                "firstName": self.$first_name.val(),
                "lastName": self.$last_name.val(),
                "phoneNumber": self.$mobile.val(),
                "email": self.$email.val(),
                "password": self.$password.val(),
            };

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: '/user/create',
                data: JSON.stringify(obj),
                success: function (res) {
                    self.enableForm()
                    self.clearForm()
                    alert(res.title)
                },
                error: function (xhr, errorType, errorName) {
                    self.enableForm()
                }
            });
        
        },

        validateMobile: function (box, mobile, regex) {
            let self = this;

            self.hideMessage(box);

            if (mobile === '') {
                self.showMessage(box, 'error', "مویایل را وارد کنید");
                return false;
            }


            if (!(mobile.match(regex))) {
                self.showMessage(box, 'error', "موبایل را به درستی وارد کنید");
                return false;
            }

            return true;
        },

        disableForm: function () {
            $(this.$element.find('input')).attr('disabled', 'disabled');
            $(this.$element.find('button')).attr('disabled', 'disabled');
        },

        enableForm: function () {
            $(this.$element.find('input')).removeAttr('disabled');
            $(this.$element.find('button')).removeAttr('disabled');
        },

        clearForm: function () {
            $(this.$element.find('input')).val("");
        },

        showMessage: function (holder, type, text) {
            let $holder = $(holder);
            let html = '';

            switch (type.toLocaleString()) {
                case 'error':
                    html = '<div class="alert alert-danger">' + text + '</div>';
                    break;
                case 'warning':
                    html = '<div class="alert alert-warning">' + text + '</div>';
                    break;
                case 'success':
                    html = '<div class="alert alert-success">' + text + '</div>';
                    break;
                case 'info':
                    html = '<div class="alert alert-info">' + text + '</div>';
                    break;
            }

            $holder.html(html);
            $holder.css('display', 'block');
        },

        hideMessage: function (holder) {
            let $holder = $(holder);
            $holder.html('');
            $holder.css('display', 'none');
        }
    };

    $.fn.createUserContact = function (options) {
        if (typeof options === 'string') {
            let plugin = this.data('createUserContact');
            if (plugin) {
                let r = plugin[options].apply(plugin, Array.prototype.slice.call(arguments, 1));
                if (r) return r;
            }
            return this;
        }

        options = $.extend({}, $.fn.createUserContact.defaults, options);

        return this.each(function () {
            let plugin = $.data(this, 'createUserContact');
            if (!plugin) {
                plugin = new CreateUserContact(this, options);
                $.data(this, 'createUserContact', plugin);
            }
        });
    };

    $.fn.createUserContact.defaults = {
        ApiUrl: '',
        MobileRegex: /(09)([ ]|,|-|[()]){0,1}[0-9]([ ]|,|-|[()]){0,2}(?:[0-9]([ ]|,|-|[()]){0,2}){8}/
    };

})(jQuery, window, document);
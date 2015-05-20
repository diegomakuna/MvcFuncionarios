$(document).ready(function () {
    $('.phone_with_ddd').mask('(00) 0000-00000');
    validator = ["#dpCreate","#dpEdit","#fCreate"];
    jQuery.each(validator, function (i, validator) {
        $(validator).validate({
            debug: true,
            highlight: function (element) {
                $(element).closest('.form-group').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.form-group').removeClass('has-error');
            },
            errorPlacement: function (error, element) {
                return true;
            },

            submitHandler: function (form) {
                form.submit();
            }

        });
    });

})
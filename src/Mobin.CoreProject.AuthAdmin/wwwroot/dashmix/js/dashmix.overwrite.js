function showBeautyMessage(res) {
    res = res || {};

    if (res.Message === undefined || res.Succeed === undefined) {
        return false;
    }

    var message = res.Succeed
        ? 'عملیات با موفقیت انجام شد'
        : 'عملیات با خطا مواجه شد';

    if (!res.Succeed && res.Message)
        message = res.Message

    swal({
        title: (res.Succeed ? 'موفق' : 'خطا'),
        text: message,
        type: res.Succeed ? "success" : "error",
        confirmButtonText: "تایید",
        html: false
    });
}

$(function() {
    $('[data-role="delete"]').on('click',
        function(e) {
            $('.tooltip').remove();

            var shouldContinue = confirm('آیا از حذف مطمئن هستید؟');

            if (!shouldContinue) {
                return false;
            }

            var $elem = $(this);
            var $row = $elem.closest('tr');
            var url = $elem.attr('href');
            $.get(url, { isAjax: true })
                .then(function(response) {
                    if (response.Succeed) {
                        $row.css({
                            'pointer-events': 'none',
                            'opacity': 0.3
                        });
                        $elem.tooltip('disable');
                    }

                    showBeautyMessage(response);
                })
                .fail(function(response) {
                    showBeautyMessage(response);
                });

            return false;
        });
});

$(function() {
    $('form').one('submit', function (e) {
        var $form = $(this);
        setTimeout(function () {
            $form.find(':submit').prop('disabled', true);
        }, 0);
    });
});


$(function () {
    $("body").on("input", ":input", function (e) {
        setTimeout(function () {
            var newString = $(e.target).val()
                    .replace(/ي/g, 'ی')
                    .replace(/ك/g, 'ک')
                    .replace(/<script/g, '')
                ;

            $(e.target).val(newString);
        }, 0);
    });
});

$(function() {
    $('body').on('submit', 'form[data-ajax]', function (e) {
        e.preventDefault();
        var $this = $(this);
        var data = $this.serialize() + '&isAjax=true';
        var action = $this.attr('action');
        var req = $.post(action, data);
        var $button = $this.find(':submit');
        $button.prop('disabled', true);
        $button.append('<i class="fa fa-spinner fa-spin"/>');

        
        req.then(function (res) {
            if (typeof res != typeof {}) {
                $this.removeAttr('data-ajax');
                $this.submit();
            } else {
                if (!res.Succeed) {
                    $this.removeAttr('data-ajax');
                    $this.submit();
                } else {
                    showBeautyMessage(res);
                    $button.prop('disabled', false);
                    $button.find('.fa.fa-spin.fa-spinner').remove();
                }
            }
        });
        
    });
});
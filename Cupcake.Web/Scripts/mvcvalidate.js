(function ($) {


    $.fn.MvcValidate = function (setting) {
        var defaults = {
            color: '#428BCA',
            direction: 2,//tips默认方向
            blurTime: 3000,//错误提示时间
            focusTime: 3000,//输入提示时间
            tipsMore: false,//允许同时显示多个
            showFocusTips:true,//是否开启输入规则提示
            form: function () {

            }
        }
        function SetError() {
            $(this).css("border", "solid 1px rgb(247, 23, 23)");
            //$(this).focus();
        }
        function ClearError() {
            $(this).css("border", "solid 1px #ccc");
        }
        defaults = $.extend(defaults, setting);
        return this.each(function (index, data) {
            var require = $(this).hasClass("input-validation-error");
            var isSelect = this.tagName.toString().toLowerCase() == "select";
            if (require) {
                var error = $(this).next("span[class='field-validation-error']").text();
                if (error||isSelect) {
                    SetError.call(this);
                    if (defaults.tipsMore) {
                        layer.tips(error, $(this), { time: defaults.blurTime, tips: [defaults.direction, defaults.color], tipsMore: true });
                    }
                    $(this).attr("placeholder", error);
                }
            }

            //输入出错的时候提示输入规则
            $(this).bind("blur", function () {

                var isSelect = this.tagName.toString().toLowerCase() == "select";
                var data_val_required = $(this).attr("data-val-required");
                if (!isSelect) {
                    //最大长度验证
                    var data_val_length_max = $(this).attr("data-val-length-max");
                    var data_val_length = $(this).attr("data-val-length");
                    if (data_val_length && $(this).val().length > parseInt(data_val_length_max)) {

                        SetError.call(this);
                        layer.tips(data_val_length, $(this), { time: defaults.blurTime, tips: [defaults.direction, defaults.color] });
                        return;
                    }

                    //必填验证
                  
                    if (data_val_required && $(this).val() == "") {
                        SetError.call(this);
                        layer.tips(data_val_required, $(this), { time: defaults.blurTime, tips: [defaults.direction, defaults.color] });
                        return;
                    }
                }
                else {
                    //下拉列表处理
                    
                    if (data_val_required && $(this).find("option:selected").index()==0) {
                        SetError.call(this);
                        layer.tips(data_val_required, $(this), { time: defaults.blurTime, tips: [defaults.direction, defaults.color] });
                        return;
                    }
                }
                ClearError.call(this);

            });


           

            //获取焦点则显示输入规则
            if (defaults.showFocusTips) {
                $(this).bind("focus", function () {
                    var value = $(this).attr("data-val-regex") || $(this).attr("data-val-length") || $(this).attr("data-val-range") || $(this).attr("data-val-required");
                    if (value) {
                        layer.tips(value, $(this), { time: defaults.focusTime, tips: [defaults.direction, defaults.color] });
                    }
                });
            }
            
        });
    }
})($);
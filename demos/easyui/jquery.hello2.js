(function ($) {
    function init(target) {
        $(target).addClass('hello2');

        return $(target);
    }

    //easyui插件函数
    $.fn.hello2 = function (options, param) {
        //如果options为string，则是方法调用，如$('#divMyPlugin').hello('sayHello');
        if (typeof options == 'string') {
            var method = $.fn.hello2.methods[options];
            if (method) { //尝试调用hello2的方法，没有找到就去找hello的方法
                return method(this, param);
            } else {
                return this.hello(options, param); //调用继承的hello的方法
            }
        }

        //否则是插件初始化函数，如$('#divMyPlugin').hello();
        options = options || {};
        return this.each(function () {
            var state = $.data(this, 'hello');
            if (state) {
                $.extend(state.options, options);
            } else {
                //easyui的parser会依次计算options、initedObj
                state = $.data(this, 'hello2', {
                    options: $.extend({}, $.fn.hello2.defaults, $.fn.hello2.parseOptions(this), options),
                });

                init(this);
            }

            $(this).hello(state.options); //调用继承的hello的构造方法

            var $input = $("<input />");
            var current = this;
            $input.width(state.options.inputWidth).val(state.options.to).change(function () {
                var val = $(this).val();
                $.data(current, 'hello').options.to = val;
                $.data(current, 'hello2').options.to = val;
            });
            $(this).append($input);

            $(this).css('color', state.options.myColor);
        });
    };

    //【注意】这里的methods没有采用$.extend
    $.fn.hello2.methods = {
        options: function (jq) {
            var copts = jq.hello('options'); //获取hello继承的options
            return $.extend($.data(jq[0], 'hello2').options, {});
        }
    };

    //设置参数转换方法（使用$.extend从继承的hello那里拓展）
    $.fn.hello2.parseOptions = function (target) {
        var opts = $.extend({}, $.fn.hello.parseOptions(target), $.parser.parseOptions(target, [{ inputWidth: 'number' }]));//这里可以指定参数的类型
        return opts;
    };

    //设置hello插件的一些默认值（使用$.extend从继承的hello那里拓展）
    $.fn.hello2.defaults = $.extend({}, $.fn.hello.defaults, {
        inputWidth: 100
    });

    //注册插件hello2
    $.parser.plugins.push("hello2");
})(jQuery);

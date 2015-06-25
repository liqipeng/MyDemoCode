(function($){
    function init(target) {
        //注：此处还不能获取options

        //所以这里可以进行一些如设置样式、绑定时间的事情
        $(target).css('cursor', 'pointer');

        $(target).bind('click', function (e, preventBubble) {
            $.fn.hello.methods.sayHello($(e.target));
            return false;
        });

		return $(target);
	}

    //easyui插件函数
    $.fn.hello = function (options, param) {
        //如果options为string，则是方法调用，如$('#divMyPlugin').hello('sayHello');
		if (typeof options == 'string'){
		    var method = $.fn.hello.methods[options];
			if (method){
				return method(this, param);
			}
		}
		
        //否则是插件初始化函数，如$('#divMyPlugin').hello();
		options = options || {};
		return this.each(function(){
		    var state = $.data(this, 'hello');
			if (state){
				$.extend(state.options, options);
			} else {
                //easyui的parser会依次计算options、initedObj
			    state = $.data(this, 'hello', {
			        options: $.extend({}, $.fn.hello.defaults, $.fn.hello.parseOptions(this), options),
			        initedObj: init(this) //这里的initedObj名称随便取的
				});
			}

			$(this).css('color', state.options.myColor);
		});
	};
	
    //设置hello插件的一些方法的默认实现
    //注：第一个参数为当前元素对应的jQuery对象
	$.fn.hello.methods = {
		options: function(jq){
		    return $.data(jq[0], 'hello').options;
		},
		sayHello: function (jq) {
		    var opts = $.data(jq[0], 'hello').options; //获取配置参数
		    for (var i = 0; i < opts.repeatTimes; i++) {
		        opts.howToSay(opts.to);
		    }
		}
	};
	
    //设置参数转换方法
	$.fn.hello.parseOptions = function (target) {
	    var opts = $.extend({}, $.parser.parseOptions(target, ['to', 'myColor', { repeatTimes: 'number' }]));//这里可以指定参数的类型
	    return opts;
	};
	
    //设置hello插件的一些默认值
	$.fn.hello.defaults = {
	    to: 'world',
	    repeatTimes: 1,
	    myColor: null,
	    howToSay: function (to) {
	        alert('Hello, ' + to + "!");
	    }
	};
    
    //注册自定义easyui插件hello
    $.parser.plugins.push("hello");
})(jQuery);

(function ($) {
    
    $.extend($.Util || ($.Util = {}), {
        //todo: ajax方法（结合后台消息）

        //todo: 拓展easyui的Dialog，可弹出带iframe的弹框
        //提交型Dialog\查看型Dialog
        openDialog: function (config) {
            var id = $.Util.newGuid();
            $('body').append('<div id="' + id + '"></div>');

            var defaultConfig = {
                id: id,
                title: 'New Dialog',
                width: $(window).width()/2,
                height: $(window).height() / 2,
                closed: false,
                cache: false,
                //content: 'Good',
                modal: true,
                resizable: true,
                onClose: function () {
                    $('#' + id).dialog('destroy');
                }
            };
            
            if (config && $.isFunction(config.onClose)) {
                defaultConfig.onClose = function () {
                    config.onClose();
                    defaultConfig.onClose();
                };
            }

            config = $.extend(defaultConfig, config);
            if (config.iframeUrl && !config.content) {
                config.content = '<iframe style="width:100%; height:100%; border:0" src="' + config.iframeUrl + '"></iframe>';
                $('#' + id).css('overflow', 'hidden');
            }

            $('#' + id).dialog(config);

            return id;
        },

        openViewDialog: function (config) {
            var defaultConfig = {
                buttons: [{
                    text: 'Close',
                    handler: function () {
                        setTimeout(function () {
                            $('#' + id).dialog('close');
                        }, 10);
                    }
                }]
            };
            config = $.extend(defaultConfig, config);

            var id = $.Util.openDialog(config);

            $('#' + id).next('.dialog-button').css('text-align', 'center');
        },

        openSubmitDialog: function (config) {
            var closeDlg = function () {
                setTimeout(function () {
                    $('#' + id).dialog('close');
                }, 10);
            };
            var leftButtonClick;
            if (config && $.isFunction(config.leftButtonClick)) {
                leftButtonClick = function () {
                    config.leftButtonClick(closeDlg);
                }; 
            } else {
                closeDlg();
            }

            var defaultConfig = {
                buttons: [{
                    text: 'OK',
                    handler: leftButtonClick
                },{
                    text: 'Cancel',
                    handler: function () {
                        setTimeout(function () {
                            $('#' + id).dialog('close');
                        }, 10);
                    }
                }]
            };
            config = $.extend(defaultConfig, config);

            var id = $.Util.openDialog(config);
        },

        //guid生成器
        newGuid: function () {
            var s4 = function () {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            }
            //return s4() + s4() + s4() + s4() + s4() + s4() + s4() + s4();
            return s4() + s4() + s4() + s4() + s4() + s4() + s4() + s4();
        },

        //安静消息提示
        //todo: 按不同颜色显示
        //todo: 回调支持
        quietMessage: function (msg, showTime) {
            showTime = showTime || 1000;

            var containerWidth = 200,
                containerHeight = 30,
                className = 'quietMessageBox-Container';
            var $div = $('<div></div>');
            $div.height(containerHeight)
                .width(containerWidth)
                .addClass(className)
                .css('position', 'fixed')
                .css('z-index', 99999)
                .css('background-color', 'Green')
                .css('text-align', 'center')
                .css('font-weight', 'bold')
                .css('line-height', containerHeight + 'px')
                .css('color', 'White')
                .css('top', $.quietMessageBoxCount * containerHeight).css('left', ($(window).width() - containerWidth) / 2)
                .html(msg);

            $('body').append($div);
            $.quietMessageBoxCount++;

            setTimeout(function () {
                $.quietMessageBoxCount--;
                $div.fadeOut("slow", function () {
                    $div.remove();
                });
            }, showTime);
        }

        //todo: 简单增删改查datagrid便捷（结合后台）
    });

    $.quietMessageBoxCount = 0;

    $(window).resize(function () {
        //保持居中显示
        var containerWidth = 200;
        $('.quietMessageBox-Container').css('left', ($(window).width() - containerWidth) / 2);
    });

})(jQuery);
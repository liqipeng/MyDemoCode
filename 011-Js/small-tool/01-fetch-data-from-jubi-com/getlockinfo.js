var requestVersion = new Date().getTime();
var newestUrl = 'http://www.jubi.com/coin/allcoin?t=0.0' + requestVersion;
var showDebug = true;

getLockInfo();

function getLockInfo() {
    $.getJSON(newestUrl, function(allData){
        var arrInfo = [];
        for(var key in allData){
            arrInfo.push({
                key: key,
                cnName: allData[key][0]
            });
        }

        debugLog(arrInfo);

        //type: sell 卖出; buy 买入; 0买卖
        getTradeInfo(arrInfo, '0', function (arrInfo) {
            debugLog(arrInfo);
            showTradeInfo(arrInfo);
        });
    });
}

function showTradeInfo(arrInfo) {
    $('#div-custom-data').remove();

    var result = [];
    var sumBuy = 0;
    var sumSell = 0;
    for (var i = 0; i < arrInfo.length; i++) {
        for (var j = 0; j < arrInfo[i].tradeData.length; j++) {
            if (arrInfo[i].tradeData[j].status == "2") { //未成交
                var record = 'name=' + arrInfo[i].cnName
                    //+ ', key=' + arrInfo[i].key
                    + ', status=' + arrInfo[i].tradeData[j].status
                    + ', type=' + arrInfo[i].tradeData[j].type
                    + ', ' + arrInfo[i].tradeData[j].num_total + ' x '
                    + arrInfo[i].tradeData[j].price + '='
                    + (parseFloat(arrInfo[i].tradeData[j].num_total) * parseFloat(arrInfo[i].tradeData[j].price)).toFixed(3);
                result.push(record);
                if (arrInfo[i].tradeData[j].type == 'sell') {
                    sumSell += parseFloat(arrInfo[i].tradeData[j].num_total) * parseFloat(arrInfo[i].tradeData[j].price);
                } else if (arrInfo[i].tradeData[j].type == 'buy') {
                    sumBuy += parseFloat(arrInfo[i].tradeData[j].num_total) * parseFloat(arrInfo[i].tradeData[j].price);
                }

                //debugLog(arrInfo[i].tradeData[j]);
            }
        }
    }

    result.push('sumSell=' + sumSell.toFixed(3));
    result.push('sumBuy=' + sumBuy.toFixed(3));

    //alert(result.join('\n'));
    $('<div></div>').prependTo($(document.body)).attr('style', 'position:fixed;bottom:10px;left:10px;background-color:#205081;color:white;z-index:999999;width:600px;').attr('id', 'div-custom-data').html(result.join('<br />'));
}

//type: sell 卖出; buy 买入; 0买卖
function getTradeInfo(arrInfo, type, endCallback) {
    var index = 0;
    var func = function (info) {
        debugLog(index + '-' + info.key);
        $.getJSON(getTradeUrl(info.key, type), function (recordInfo) {
            info.tradeData = recordInfo.data.datas;

            ++index;
            if (index <= arrInfo.length - 1) {
                setTimeout(function () {
                    func(arrInfo[index]);
                }, 500);
            } else {
                debugLog('finish.');
                if ($.isFunction(endCallback)) {
                    endCallback(arrInfo);
                }
            }
        });
    };
    func(arrInfo[index]);
}

function debugLog(obj) {
    if (showDebug) {
        console.log(obj);
    }
}

//type: sell 卖出; buy 买入; 0买卖
function getTradeUrl(key, type) {
    var url = 'http://www.jubi.com/ajax/trade/list/coin/' + key + '/type/' + type + '/status/0?p=1';
    debugLog(url);
    return url;
}
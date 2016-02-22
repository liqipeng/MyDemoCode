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
        getTradeInfo(arrInfo, 'sell', function (arrInfo) {
            getTradeInfo(arrInfo, 'buy', function (arrInfo) {
                debugLog(arrInfo);
                showTradeInfo(arrInfo);
            });
        });
    });
}

function showTradeInfo(arrInfo) {
    $('#div-custom-data').remove();

    var result = [];
    var completed = [];
    var sumBuy = 0;
    var sumSell = 0;
    for (var i = 0; i < arrInfo.length; i++) {
        for (var j = 0; j < arrInfo[i].tradeData.length; j++) {
            if (arrInfo[i].tradeData[j].status == "2") { //未成交
                var record = showCnName(arrInfo[i].cnName)
                    //+ ', key=' + arrInfo[i].key
                    + ', status=' + arrInfo[i].tradeData[j].status
                    + ', type=' + arrInfo[i].tradeData[j].type
                    + ', (' + parseFloat(arrInfo[i].tradeData[j].num_total).toFixed(3) + '-' + (parseFloat(arrInfo[i].tradeData[j].num_total)-parseFloat(arrInfo[i].tradeData[j].num_over)).toFixed(3) + ') × '
                    + arrInfo[i].tradeData[j].price + '='
                    + (parseFloat(arrInfo[i].tradeData[j].num_over) * parseFloat(arrInfo[i].tradeData[j].price)).toFixed(3);
                result.push(record);
                if (arrInfo[i].tradeData[j].type == 'sell') {
                    sumSell += parseFloat(arrInfo[i].tradeData[j].num_over) * parseFloat(arrInfo[i].tradeData[j].price);
                } else if (arrInfo[i].tradeData[j].type == 'buy') {
                    sumBuy += parseFloat(arrInfo[i].tradeData[j].num_over) * parseFloat(arrInfo[i].tradeData[j].price);
                }

                //debugLog(arrInfo[i].tradeData[j]);
            } else if (arrInfo[i].tradeData[j].status == "4") { //已成交
            }
        }
    }

    result.push('sumSell=' + sumSell.toFixed(3));
    result.push('sumBuy=' + sumBuy.toFixed(3));

    debugLog(arrInfo.chengjiaoInfo);
    var newest = sortObjArr(arrInfo.chengjiaoInfo, function (a, b) { return parseInt(a.c) < parseInt(b.c); }).slice(0, 28);
    debugLog(newest);
    
    for (var i = 0; i < newest.length; i++) {
        //{"id":"31376","p":0.211201,"n":112.11,"s":23.67774411,"c":"1455652485","f":"0.168165 GOOC","t":"\u4e70\u5165"}
        console.log(newest[i]);
        var item = fncDT(newest[i].c) + '    ' + newest[i].t + '  ' + newest[i].p.toFixed(7)+ '  ' + showCnName(newest[i].cnName) + '    ￥' + parseFloat(newest[i].s).toFixed(3);
        result.push(item);
    }

    //alert(result.join('\n'));
    $('<div></div>').prependTo($(document.body)).attr('style', 'position:fixed;bottom:10px;left:10px;background-color:#205081;color:white;z-index:999999;width:600px;max-height: 80%;overflow: scroll;').attr('id', 'div-custom-data').html(result.join('<br />'));
}

function showCnName(n) {
    var arr = ['　', '　', '　', '　', '　'];
    if (n.length < 5) {
        return arr.splice(0,5-n.length).join('') + n;
    } else {
        return n;
    }
}

//type: sell 卖出; buy 买入; 0买卖
function getTradeInfo(arrInfo, type, endCallback) {
    if (!arrInfo.chengjiaoInfo) {
        arrInfo.chengjiaoInfo = [];
    }

    var index = 0;
    var func = function (info) {
        debugLog(index + '-' + info.key);
        $.getJSON(getTradeUrl(info.key, type), function (recordInfo) {
            if (!info.tradeData) {
                info.tradeData = recordInfo.data.datas;
            } else {
                info.tradeData = info.tradeData.concat(recordInfo.data.datas);
            }

            setTimeout(function () {
                $.getJSON(getChengjiaoUrl(info.key, type), function (chengjiaoInfo) {
                    $(chengjiaoInfo.data.datas).each(function (i, item) {
                        item.cnName = info.cnName;
                    });
                    arrInfo.chengjiaoInfo = arrInfo.chengjiaoInfo.concat(chengjiaoInfo.data.datas);

                    ++index;
                    if (index <= arrInfo.length - 1) {
                        setTimeout(function () {
                            func(arrInfo[index]);
                        }, 200);
                    } else {
                        debugLog('finish.');
                        if ($.isFunction(endCallback)) {
                            endCallback(arrInfo);
                        }
                    }

                });
            }, 200);

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

function getChengjiaoUrl(key, type){
	var types = { sell:1, buy:2 };
    var url = 'http://www.jubi.com/ajax/trade/order/coin/' + key + '/type/' + (types[type]||3) + '?p=1';
    debugLog(url);
    return url;
}

function fncDT(d, nt) {
    if(!d) return '刚刚';
    var time = new Date((parseInt(d)+28800)*1000);
    var ymd = time.getUTCFullYear() + "/" + lessThan10((time.getUTCMonth() + 1)) + "/" + lessThan10(time.getUTCDate()) + ' ';
    if(nt) return ymd;
    return ymd + lessThan10(time.getUTCHours()) + ":" + (time.getUTCMinutes() < 10 ? '0' : '') + time.getUTCMinutes() + ':' + lessThan10(time.getUTCSeconds());
}

function lessThan10(n) {
    if (n < 10) {
        return '0' + n;
    } else {
        return n;
    }
}

function sortObjArr(objArr, func) {
    for (var i = 0; i < objArr.length; i++) {
        for (var j = i+1; j < objArr.length; j++) {
            if (func(objArr[i], objArr[j])) {
                var tmp = objArr[i];
                objArr[i] = objArr[j];
                objArr[j] = tmp;
            }
        }
    }
    return objArr;
}
var requestVersion = new Date().getTime();
var newestUrl = 'http://www.jubi.com/coin/allcoin?t=0.0' + requestVersion;
var financeUrl = 'http://www.jubi.com/ajax/user/finance?t=0.0' + requestVersion;
var showDebug = false;
var filterList = [
    ''
];

analyze(getNewest);

function analyze(getNewestCall) {
    $.getJSON(financeUrl, function (jsonData) {
        if ($.isFunction(getNewestCall)) {
            getNewestCall(jsonData);
        }
    });
}

function getNewest(financeData) {
    $.getJSON(newestUrl, function (jsonData) {
        for (var key in jsonData) {
            var balanceField = key + '_balance';
			var lockField = key + '_lock';
            if (((financeData.data[balanceField] > 0  || financeData.data[lockField] > 0) == false)) {
                delete jsonData[key];
            } else if ((financeData.data[balanceField] > 0) && (filterList.join().indexOf(key) > -1)) {
                jsonData[key] = null;
            } else {
                jsonData[key].balance = financeData.data[balanceField] + financeData.data[lockField];
            }
        }
        debugLog(jsonData);

        var keys = [];
        for (var key in jsonData) {
            if (jsonData[key]) {
                keys.push(key);
            }
        }

        ajaxQueue(keys, jsonData, function () {
			jsonData['cny_rmb']={
				cnName:'RMB',
				currentPrice:1,
				buyPrice:1,
				balance:financeData.data.cny_balance
			};
			jsonData['cny_lock']={
				cnName:'冻结',
				currentPrice:1,
				buyPrice:1,
				balance:financeData.data.cny_lock
			};
			jsonData['cny']={
				cnName:'Total',
				currentPrice:1,
				buyPrice:1,
				balance:financeData.data.cny_total
			};
			
            showData(jsonData);
        });

    });
}

function debugLog(obj) {
    if (showDebug) {
        console.log(obj);
    }
}

function showData(jsonData) {

    $('#table-custom-data').remove();

    var $tb = $('<table></table>');
    $tb.attr('id', 'table-custom-data');
    $tb.attr('style', 'border: 1px solid #ccc;border-collapse: collapse;');
    var $tr = $('<tr></tr>');
    $('<td>名称</tb>').attr('style', 'border: 1px solid green;color:white;').appendTo($tr);
    $('<td></tb>').attr('style', 'border: 1px solid green;color:white;').appendTo($tr);
    $('<td></tb>').attr('style', 'border: 1px solid green;color:white;').appendTo($tr);
    $('<td>买入价</tb>').attr('style', 'border: 1px solid green;color:white;').appendTo($tr);
    $('<td>当前价</tb>').attr('style', 'border: 1px solid green;color:white;').appendTo($tr);
    $('<td>数量</tb>').attr('style', 'border: 1px solid green;color:white;').appendTo($tr);
    $('<td>当前价值</tb>').attr('style', 'border: 1px solid green;color:white;').appendTo($tr);
    $tr.appendTo($tb);

    for (var key in jsonData) {
        if (jsonData[key] && (jsonData[key].currentPrice * jsonData[key].balance)>0.01) {
            var $tr = $('<tr></tr>');
            $('<td>' + jsonData[key].cnName + '</tb>').attr('style', 'border: 1px solid green;color:white;').appendTo($tr);
            $('<td>' + getUpDown(jsonData[key].buyPrice, jsonData[key].currentPrice) + '</tb>').attr('style', 'border: 1px solid green;color:white;').appendTo($tr);
            $('<td>' + ((jsonData[key].currentPrice / jsonData[key].buyPrice - 1) * 100).toFixed(3) + '%</tb>').attr('style', 'text-align:right;border: 1px solid green;color:white;').appendTo($tr);
            $('<td>' + jsonData[key].buyPrice.toFixed(7) + '</tb>').attr('style', 'text-align:right;border: 1px solid green;color:white;').appendTo($tr);
            $('<td>' + jsonData[key].currentPrice.toFixed(7) + '</tb>').attr('style', 'text-align:right;border: 1px solid green;color:white;').appendTo($tr);
            $('<td>' + jsonData[key].balance.toFixed(4) + '</tb>').attr('style', 'text-align:right;border: 1px solid green;color:white;').appendTo($tr);
            $('<td>' + (jsonData[key].currentPrice * jsonData[key].balance).toFixed(3) + '</tb>').attr('style', 'text-align:right;border: 1px solid green;color:white;').appendTo($tr);
            $tr.appendTo($tb);

            console.log('name=' + jsonData[key].cnName
            + ', buyPrice=' + jsonData[key].buyPrice
            + ', currentPrice=' + jsonData[key].currentPrice
            + ', balance=' + jsonData[key].balance);
        }
    }
    $tb.prependTo($(document.body)).attr('style', 'position:fixed;bottom:10px;right:10px;background-color:#205081;color:white;z-index:999999;width:500px;');
}

function getUpDown(buyPrice, currentPrice) {
    if (buyPrice > currentPrice) {
        return '↓';
    }else if(buyPrice == currentPrice){
		return '|';
	} else {
        return '↑';
    }
}

function ajaxQueue(keys, jsonData, endCallback) {
    var index = 0;
    var func = function (key) {
        debugLog(index + '-' + key);
        $.getJSON(getBuyInfoUrl(key), function (recordInfo) {
            jsonData[key].buyPrice = recordInfo.data.datas[0].p;
            jsonData[key].cnName = jsonData[key][0];
            jsonData[key].currentPrice = jsonData[key][1];

            ++index;
            if (index <= keys.length - 1) {
                setTimeout(function () {
                    func(keys[index]);
                }, 100);
            } else {
                debugLog('finish.');
                endCallback();
            }
        });
    };
    func(keys[index]);
}

function getBuyInfoUrl(key) {
    return 'http://www.jubi.com/ajax/trade/order/coin/' + key + '/type/3?p=1';
}
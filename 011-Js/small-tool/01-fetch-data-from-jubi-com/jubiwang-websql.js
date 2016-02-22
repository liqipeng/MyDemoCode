//----------common begin---------------------------------------
pageCfg = {
    writeLog: true
};

String.prototype.paddingLeft = function (length, char) {
    if (!char) {
        char = ' ';
    }

    if (length && length > 100) {
        throw Error('not supported length:' + length + ', maxlength=' + 100);
    }
    var arr = [];
    for (var i = 0; i < 100; i++) {
        arr.push(char);
    }

    if (this.length < length) {
        return arr.splice(0, length - this.length).join('') + this;
    } else {
        return this;
    }
};

Number.prototype.paddingLeft = function (length, char) {
    if (!char) {
        char = '0';
    }
    return this.toString().paddingLeft(length, char);
};

Number.prototype.paddingBoth = function (left, right, char) {
    if (!char) {
        char = '0';
    }
    if (!right || right <= 0) {
        return parseInt(this).toString().paddingLeft(left, '0');
    }
    var str = parseFloat(this).toFixed(right);
    console.log(str);
    var strLeft = str.substring(0, str.indexOf('.')).paddingLeft(left, char, '0');
    console.log(strLeft);
    return str.replace(/\d+(\.\d*)/, strLeft + '$1');
};

utils = {
    log: function (info) {
        if (pageCfg.writeLog) {
            var msgs = [];
            for (var i in arguments) {
                if (typeof arguments[i] == 'object') {
                    msgs.push(JSON.stringify(arguments[i]));
                } else {
                    msgs.push(arguments[i]);
                }
            }
            console.log(msgs.join(' '));
        }
    },
    msg: function (msg) {
        if (pageCfg.writeLog) {
            console.log(msg);
        }
    },
    showDate: function (dateInt, notShowTime) {
        if (!dateInt) return '刚刚';
        var time = new Date((parseInt(dateInt) + 28800) * 1000);
        var ymd = time.getUTCFullYear() + "-"
            + (time.getUTCMonth() + 1).paddingLeft(2) + "-"
            + time.getUTCDate().paddingLeft(2);
        if (notShowTime) return ymd;
        return ymd + ' ' + time.getUTCHours().paddingLeft(2) + ":" + time.getUTCMinutes().paddingLeft(2) + ':' + time.getUTCSeconds().paddingLeft(2);
    },
    sortObjArr: function (objArr, func) {
        for (var i = 0; i < objArr.length; i++) {
            for (var j = i + 1; j < objArr.length; j++) {
                if (func(objArr[i], objArr[j])) {
                    var tmp = objArr[i];
                    objArr[i] = objArr[j];
                    objArr[j] = tmp;
                }
            }
        }
        return objArr;
    },
    ajaxQueue: function (cfg) {
        //todo:校验参数
        if (!$.isFunction(cfg.debugLog)) {
            cfg.debugLog = utils.log;
        }
        var index = 0;
        var next = function (end) {
            if (!end) {
                ++index;
                if (index <= cfg.items.length - 1) {
                    setTimeout(function () {
                        func(cfg.items[index]);
                    }, cfg.interval || 800);
                } else {
                    if ($.isFunction(cfg.endCallback)) {
                        cfg.endCallback();
                    }
                    cfg.debugLog('finish.');
                }
            } else {
                console.log('end=false, process is suspended.');
            }
        };
        var func = function (item) {
            cfg.debugLog('no. ' + index, item);
            var url = cfg.getUrl(item, index);
            cfg.debugLog(url);

            var ajaxCfg = {
                url: url,
                type: cfg.type || 'GET',
                dataType: 'json',
                success: function (data, textStatus, jqXHR) {
                    var result = cfg.dataIsOK(data, item, index);
                    if (typeof result == 'boolean' && result === true) { //dataIsOK如果返回true，则next
                        next();
                    } else if ($.isFunction(result)) {
                        result(next);
                    } else {
                        utils.log('suspended!');
                    }
                },
                error: function (err) {
                    if (cfg.continueWhenError) {
                        next();
                    }
                    cfg.debugLog('ajax error:', err);
                }
            };
            if ($.isFunction(cfg.setAjaxData)) { //设置ajax的请求参数
                ajaxCfg.data = setAjaxData(item, index);
            }

            $.ajax(ajaxCfg);
        };
        func(cfg.items[index]);
    },
    batchProcess: function (cfg) {
        if (!$.isFunction(cfg.debugLog)) {
            cfg.debugLog = utils.log;
        }
        var index = 0;
        var func = function (item) {
            cfg.process(item, index, function () {
                utils.log('开始itemIsOK' + index);
                ++index;
                if (index <= cfg.items.length - 1) {
                    utils.log('1秒后执行下一个' + index);
                    setTimeout(function () {
                        utils.log('开始执行下一个' + index);
                        func(cfg.items[index]);
                    }, cfg.interval || 1);
                }
                else {
                    if ($.isFunction(cfg.endCallback)) {
                        cfg.endCallback();
                    }
                    cfg.debugLog('finish.');
                }
            });
        };
        func(cfg.items[index]);
    }
};

//----------common end---------------------------------------

//----------DBTool begin---------------------------------------
dbTool = {
    openDB: function (dbInfo) {
        var db = window.openDatabase(dbInfo.dbName, dbInfo.dbVersion,
        dbInfo.dbDisplayName, dbInfo.dbEstimatedSize);
        return db;
    },
    executeSql: function (db, sql, params, success) {
        db.transaction(function (tx) {
            tx.executeSql(
               sql, params, success,
                function (tx, err) {
                    utils.msg(err);
                }
            );
        });
    },
    executeBatchSql: function (db, arrSqlAndParams, allSuccess, success) {
        db.transaction(function (tx) {
            var arrInsertId = [];
            utils.log('begin executeBatchSql');
            for (var i in arrSqlAndParams) {
                tx.executeSql(
                   arrSqlAndParams[i].sql,
                   arrSqlAndParams[i].params,
                   function (tx, result) {
                       if ($.isFunction(success)) {
                           success(tx, result);
                       }
                       arrInsertId.push(result);
                       if (arrInsertId.length == arrSqlAndParams.length) {
                           if ($.isFunction(allSuccess)) {
                               allSuccess(arrInsertId);
                           }
                       }
                   },
                    function (tx, err) {
                        utils.msg('ERROR: ' + err.code + ', ' + err.message);
                    }
                );
                utils.log(i + ' - ' + arrSqlAndParams[i].sql + ' - ' + JSON.stringify(arrSqlAndParams[i].params));
            }
            utils.log('end executeBatchSql');
        });
    }
};

//----------DBTool end---------------------------------------
urls = {
    getAllCoinUrl: function () {
        return 'http://www.jubi.com/coin/allcoin?t=0.0' + new Date().getTime();
    },
    getFinanceUrl: function () {
        return 'http://www.jubi.com/ajax/user/finance?t=0.0' + new Date().getTime();
    },
    getTradeUrl: function (key, type, page) {
        var types = { buy: 1, sell: 2 };
        //type: 买=1 卖=2 买卖=3
        var url = 'http://www.jubi.com/ajax/trade/order/coin/' + key + '/type/' + (types[type] || 3) + '?p=' + (page || 1);
        utils.log(url);
        return url;
    },
    getDelegateUrl: function (key, type, page) {
        //type: sell 卖出; buy 买入; 0买卖
        //status: 未提交=2
        var url = 'http://www.jubi.com/ajax/trade/list/coin/' + key + '/type/' + type + '/status/0?p=' + (page || 1);
        utils.log(url);
        return url;
    }
};

if (!openDatabase) {
    utils.msg('web sql is not supported');
} else {
    utils.msg('web sql is supported');
}

var dbInfo = {
    dbName: "CoinDB",  // 名称
    dbVersion: "0.01", // 版本
    dbDisplayName: "CoinDB Version 0.01", // 显示名称
    dbEstimatedSize: 10 * 1024 * 1024  // 大小 (byte) 
};

var db = window.openDatabase(dbInfo.dbName, dbInfo.dbVersion,
        dbInfo.dbDisplayName, dbInfo.dbEstimatedSize);

coinUtil = {
    notLogin: function () {
        alert('可能未登陆。');
    },
    initTable: function (then) {
        var arrSqlAndParams = [
            {
                sql: "CREATE TABLE IF NOT EXISTS coin (id INTEGER PRIMARY KEY AUTOINCREMENT, key TEXT, cnName TEXT, price REAL, buyPrice REAL)",
                params: []
            },
            {
                sql: "CREATE TABLE IF NOT EXISTS finance (id INTEGER PRIMARY KEY AUTOINCREMENT, key STRING, balance REAL, lock REAL, rate REAL, total REAL)",
                params: []
            },
            {
                sql: "CREATE TABLE IF NOT EXISTS trade (id INTEGER PRIMARY KEY, key STRING, price REAL, count REAL, amount REAL)",
                params: []
            }
        ];
        dbTool.executeBatchSql(db, arrSqlAndParams, function () {
            utils.log('coin database created table successfully!');
            if ($.isFunction(then)) {
                then();
            }
        });
    },
    initAllCoin: function (then) {
        $.getJSON(urls.getAllCoinUrl(), function (allCoinData) {
            var insertSql = "INSERT INTO coin(key, cnName, price) SELECT ?, ?, ? WHERE NOT EXISTS(SELECT 1 FROM coin WHERE key=?)";
            var sqls = [];
            for (var key in allCoinData) {
                sqls.push({
                    sql: insertSql,
                    params: [key, allCoinData[key][0], allCoinData[key][1], key]
                });
            }
            sqls.push({
                sql: insertSql,
                params: ['cny', '人民币', 1, 'cny']
            });
            utils.log(sqls);

            dbTool.executeSql(db, 'DELETE FROM coin', [], function () {
                utils.log('old coin data is deleted.');

                dbTool.executeBatchSql(db, sqls,
                    function (arrNewIds) {
                        utils.log('all coin insert success. count=' + arrNewIds.length);

                        if ($.isFunction(then)) {
                            then();
                        }
                        //return;
                        //utils.log('begin select');
                        //dbTool.executeSql(db, 'select * from coin', [], function (tx, result) {
                        //    utils.log('select count=' + result.rows.length);
                        //    utils.log(result.rows);
                        //});
                    });
            });

        });
    },
    initFinance: function (then) {
        $.getJSON(urls.getFinanceUrl(), function (financeData) {
            if (financeData.status != 1) {
                coinUtil.notLogin();
                return;
            }

            var insertSql = "INSERT INTO finance(key, balance, lock, rate, total) SELECT ?, ?, ?, ?,? WHERE NOT EXISTS(SELECT 1 FROM finance WHERE key=?)";
            var sqls = [];
            for (var key in financeData.msg) {
                var balanceField = key + '_balance';
                var lockField = key + '_lock';
                var rateField = key + '_rate';
                var totalField = key + '_total';
                sqls.push({
                    sql: insertSql,
                    params: [key
                        , financeData.data[balanceField] || null
                        , financeData.data[lockField] || null
                        , financeData.data[rateField] || null
                        , financeData.data[totalField] || null
                        , key
                    ]
                });
            }
            sqls.push({
                sql: insertSql,
                params: ['cny'
                    , financeData.data['cny'] || null
                    , financeData.data['cny_lock'] || null
                    , 0
                    , financeData.data['cny_total'] || null
                    , 'cny'
                ]
            });

            utils.log(sqls);

            dbTool.executeSql(db, 'DELETE FROM finance', [], function () {
                utils.log('old finance data is deleted.');
                dbTool.executeBatchSql(db, sqls,
                    function (arrNewIds) {
                        utils.log('all finance insert success. count=' + arrNewIds.length);

                        if ($.isFunction(then)) {
                            then();
                        }
                    });
            })

        });
    },
    initTrade: function (then) {
        dbTool.executeSql(db, "SELECT * FROM finance WHERE key<>? and key<>? and key <>? and key <> ? ", ['cny', 'qec', 'pts', 'mec'], function (tx, result) {
            utils.log('finance count=' + result.rows.length);

            var ajaxCfg = [];

            utils.ajaxQueue({
                items: result.rows,
                getUrl: function (item, index) {
                    return urls.getTradeUrl(item.key, null, 1);
                },
                dataIsOK: function (data, item, idx) {
                    if (data.status != 1) {
                        coinUtil.notLogin();
                        return false;
                    } else {
                        //ajaxCfg.push({
                        //    key: item.key,
                        //    pagemax: data.data.page.pagemax
                        //});

                        return function (next) {
                            next();

                            //(id INTEGER PRIMARY KEY, key STRING, price REAL, count REAL, amount REAL)
                            var insertSql = 'INSERT INTO trade(id, key, price, count, amount) SELECT ?, ?, ?, ?,? WHERE NOT EXISTS(SELECT 1 FROM trade WHERE id=?)';
                            var sqls = [];
                            for (var idx in data.data.datas) {
                                sqls.push({
                                    sql: insertSql,
                                    params: [data.data.datas[idx].id
                                        , item.key
                                        , data.data.datas[idx].p || null
                                        , data.data.datas[idx].n || null
                                        , data.data.datas[idx].s || null
                                        , data.data.datas[idx].id
                                    ]
                                });
                            }

                            dbTool.executeBatchSql(db, sqls,
                                function (arrNewIds) {
                                    utils.log('all trade insert success. count=' + arrNewIds.length);

                                    if ($.isFunction(then)) {
                                        //then();
                                    }
                            });

                            //dbTool.executeSql(db, "SELECT MAX(id) FROM trade WHERE key=? ", [item.key], function (tx2, result2) {
                            //    next();
                            //        console.log('---------------------------');
                            //        console.log(result2);
                            //});
                        };
                    }
                },
                endCallback: function () {
                    console.log('trade buy data complete......');
                },
                interval: 100
            });
        });
    },
    initDelegate: function (then) {

    }
};

coinUtil.initTable(function () {
    coinUtil.initAllCoin(function () {
        coinUtil.initFinance(function () {
            coinUtil.initTrade(function () {
                utils.log('finished................');
            });
        });
    });
});
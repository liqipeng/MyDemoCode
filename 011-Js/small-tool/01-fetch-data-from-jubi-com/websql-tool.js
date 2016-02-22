dbExecutor = {
    createUI: function () {
        var id = 'divExecutor'; //parseInt(Math.random() * 10000000);
        $('#' + id).remove();
        var $div = $('<div id=' + id + '><div>');
        var idDb = id + '-' + 'txtDb';
        var idSql = id + '-' + 'txtSql';
        var idVer = id + '-' + 'txtVer';
        var idResult = id + '-' + 'txtResult';
        var idClose = id + '-' + 'btnClose';
        var idClose2 = id + '-' + 'btnClose2';
        var html = 'db:<input type="text" id="' + idDb + '" style="width:480px;" value="CoinDB" /><br />'
            + 'ver:<input type="text" id="' + idVer + '" style="width:480px;" value="0.1" /><br />'
            + 'sql:<textarea id="' + idSql + '" style="width:480px;height:100px;"></textarea><br />'
            + '<input type="button" value="exe" onclick="dbExecutor.exe(\'#'
            + idDb + '\', \'#' + idVer + '\',\'#' + idSql + '\',\'#' + idResult + '\')" /><br />'
            + 'res:<textarea id="' + idResult + '" style="width:480px;height:100px;"></textarea><br />'
            + '<input id="' + idClose + '" type="button" value="x" onclick="dbExecutor.close(\'#'
            + id + '\', \'#' + idClose2 + '\')" />';
        console.log(html);
        $div.html(html);
        $div.prependTo($(document.body)).attr('style', 'position:fixed;left:0;right:0;top:3px;margin:auto;background-color:white;color:black;z-index:999999;width:600px;min-height: 300px; max-height: 80%;border:3px solid #555;padding:5px');
        $('<input id="' + idClose2 + '" type="button" value="sql console" onclick="dbExecutor.show(\'#'
            + id + '\', \'#' + idClose2 + '\')" style="display:none;position:fixed;left:3px;right:3px;z-index:999999;" />').prependTo($(document.body));
    },
    exe: function (idDb, idVer, idSql, idResult) {
        var dbName = $(idDb).val();
        var ver = $(idVer).val();
        var sql = $(idSql).val();

        var db = window.openDatabase(dbName, ver,
                dbName + ver, 10 * 1024 * 1024);
        db.transaction(function (tx) {
            tx.executeSql(
               sql, [], function (tx, result) {
                   $(idResult).val(JSON.stringify(result.rows) + '\nrowsAffected:' + result.rowsAffected);
                   console.log(result);
               }, function (tx, err) {
                   $(idResult).val('code:' + err.code + '\n\nmessage:'
                       + err.message);
                   console.log(err);
               });
        });
    },
    close: function (id, idClose2) {
        $(idClose2).show();
        $(id).hide();
    },
    show: function (id, idClose2) {
        $(idClose2).hide();
        $(id).show();
    },
};

dbExecutor.createUI();

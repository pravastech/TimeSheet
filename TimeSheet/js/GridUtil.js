GridUtil = {
    editRow: function (rowid) {
        var rowData = gridData[rowid];
        if (rowData) {
            if (typeof GridUtil.beforeEdit != "undefined") {
                GridUtil.beforeEdit(rowData);
            }
            for (var x in columnJS) {
                var colName = columnJS[x].name;
                var colType = columnJS[x].type;
                if (colType == "Boolean") {
                    if (rowData[colName]) {
                        $("#" + colName).attr("checked", "checked");
                    } else {
                        $("#" + colName).removeAttr("checked");
                    }
                } else {
                    $('#' + colName).val(rowData[colName]);
                }
            }
            $('#guidfield').val(rowData.guidfield);
            $('#' + GridUtil.UIFormName()).dialog({ title: 'Edit ' + GridUtil.Title(), width: 600,
                open: GridUtil.dOpen()
            });
        } else {
            alert("Table row not found");
        }
    },

    deleteRow: function (rowid) {
        var rowData = gridData[rowid];
        if (rowData) {
            $('#deleteKey').val(rowData.guidfield);
            $('#submitKey').val(genRandTxt());
            $("#form").submit();
        }
    },

    validateRow: function () {
        var result = "";
        for (var x in columnJS) {
            var colName = columnJS[x].name;
            var colReq = columnJS[x].required;
            if (colReq) {
                var val = $('#' + colName).val();
                if ((typeof val != 'undefined') && val.length == 0) {
                    $('#' + colName).addClass("error");
                    result += $('#' + colName).parent().find("label").html() + ' is required.\n';
                } else {
                    $('#' + colName).removeClass("error");
                }
            }
        }
        if (result.length > 0) {
            alert(result);
        }
        return (result.length == 0)
    },

    newRow: function () {
        if (typeof GridUtil.beforeNew != "undefined") {
            GridUtil.beforeNew();
        }
        for (var x in columnJS) {
            var colName = columnJS[x].name;
            var colType = columnJS[x].type;
            if (colType == "Boolean") {
                $("#" + colName).removeAttr("checked");
            } else {
                $('#' + colName).val("");
            }
        }
        $('#guidfield').val('');
        $('#' + GridUtil.UIFormName()).dialog({ title: 'New ' + GridUtil.Title(), width: 600,
            open: GridUtil.dOpen()
        });
    },

    dOpen : function(){
        if(typeof GridUtil.dialogOpen != "undefined"){
            GridUtil.dialogOpen();
        }
    },

    saveRow: function () {
        if(typeof GridUtil.beforeSave != "undefined"){
            GridUtil.beforeSave();
        }
        if (GridUtil.validateRow()) {
            $('#submitKey').val(SiteUtil.genRandTxt());
            $("#form").submit();
        }
    }
}
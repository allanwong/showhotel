$(function () {
    var dt = $('#content .data-table').dataTable({
        oLanguage: {
            sUrl: "/scripts/app/plugins/jquery.dataTables.lang.cn.js"
        },
        sDom: '<""l>t<"F"pi>',
        sAjaxSource: "roles/list",
        aoColumns: [
            {
                sTitle: "角色名称",
                mData: "Name",
                sName: "Name"
            },
            {
                sTitle: "权限",
                mData: "Modules",
                sName: "Modules",
                bSortable: false,
                mRender: function (data, type, full) {
                    if ($.isArray(data)) {
                        return _.map(data, function (item) {
                            return item.Name;
                        }).join("，");
                    }
                }
            }
        ]
    });

    dt.on("rowClicked", function (e, row, tbl) {
        var nodes = tbl.fngetSelectedNodes();

        if (nodes.length == 1) {
            $(".btnEdit").removeAttr("disabled");
        }
        else {
            $(".btnEdit").attr("disabled", "disabled");
        }

        if (nodes.length > 0) {
            $(".btnDel").removeAttr("disabled");
        }
        else {
            $(".btnDel").attr("disabled", "disabled");
        }
    });

    $(".btnRefresh").click(function () {
        dt.fnDraw()
    });

    $(".btnSelAll").click(function () {
        dt.fntoggleSelectAll()
    });

    $(".btnAdd").click(function () {
        loadContentByVName("role_addedit");
    });

    $(".btnEdit").click(function () {
        var node = dt.fngetSelectedNodes()[0],
            row = dt.fnGetData(node);

        loadContentByVName("role_addedit",
            {
                data:
                    {
                        Id: row.Id
                    }
            });
    });
});
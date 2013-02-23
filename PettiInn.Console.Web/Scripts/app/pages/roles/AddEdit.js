$(function () {
    var container = $("#content"),
        tree = $('.module-tree')

    tree.tree({
        url: '/Module/Tree',
        onlyLeafCheck: tree,
        checkbox: true,
        animate: true,
        lines: true,
        lines: true,
        onBeforeLoad: function(node, params)
        {
            $(".divModulesTree").block({ message: " " });
        },
        onLoadSuccess: function (node, data) {
            var id = container.find("input[name='Id']").val();
            if (id) {
                load();
            }
            $(".divModulesTree").unblock();
        }
    });

    function load() {
        var ids = container.find(".mids").val();

        if (ids && ids.length) {
            var idArr = JSON.parse(ids);
            _.each(idArr, function (id) {
                var node = tree.tree("find", id).target;
                tree.tree("check", node);
            });
        }
    }

    container.find(".tree-cbAll").click(function () {
        var nodes = tree.tree('getChecked', 'unchecked');
        _.each(nodes, function (node) {
            tree.tree("check", node.target);
        });
    });

    container.find(".tree-cbNone").click(function () {
        var nodes = tree.tree('getChecked', 'checked');
        _.each(nodes, function (node) {
            tree.tree("uncheck", node.target);
        });
    });

    $("#formRoleAddedit").validate({
        rules: {
            Name: {
                required: true
            }
        },
        messages:
            {
                Name:
                    {
                        required: "请输入角色名称"
                    }
            },
        errorClass: "help-inline",
        errorElement: "span",
        highlight: function (element, errorClass, validClass) {
            $(element).parents('.control-group').addClass('error');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents('.control-group').removeClass('error');
            $(element).parents('.control-group').addClass('success');
        }
    });
});
Ext.define("app.modules.Admin",
{
    requires: [
        'app.models.AdministratorModel',
        'app.models.RoleModel',
        'Ext.ux.touch.grid.List',
        'Ext.ux.touch.grid.feature.Feature',
        'Ext.ux.touch.grid.feature.Sorter',
        "app.ux.field.MultiSelect"
    ],
    constructor: function (args) {
        this.callParent(arguments);

        var store = Ext.create('Ext.data.Store', {
            model: 'app.models.AdministratorModel',
            autoLoad: true,
            proxy: {
                type: 'ajax',
                url: '/app/GetAdministrators',
                actionMethods: { create: "POST", read: "POST", update: "POST", destroy: "POST" },
                reader: {
                    type: 'json'
                }
            }
        });

        var grid = Ext.create('Ext.ux.touch.grid.List', {
            itemId: "admins_grid",
            store: store,

            features: [
                {
                    ftype: 'Ext.ux.touch.grid.feature.Sorter',
                    launchFn: 'initialize'
                }
            ],
            columns: [
                {
                    header: '姓名',
                    dataIndex: 'Name',
                    style: 'padding-left: 1em;',
                    width: '20%',
                    filter: { type: 'string' }
                },
                {
                    header: '帐号',
                    dataIndex: 'UserName',
                    style: 'padding-left: 1em;',
                    width: '20%',
                    filter: { type: 'string' }
                },
                {
                    header: '超管',
                    dataIndex: 'IsSuper',
                    style: 'text-align: center;',
                    cls: 'centered-cell redgreen-cell',
                    width: '15%',
                    renderer: function (value) {
                        var cls = (value) ? 'red' : '',
                            text = value ? "是" : "否";

                        return '<span class="' + cls + '">' + text + '</span>';
                    }
                },
                {
                    header: '手机',
                    dataIndex: 'Mobile',
                    style: 'text-align: center;',
                    width: '20%',
                    filter: { type: 'string' }
                },
                {
                    header: '上次登录',
                    dataIndex: 'LastLoggedIn',
                    style: 'text-align: center;',
                    width: '25%',
                    filter: { type: 'MSDate' },
                    renderer: function (value) {
                        if (Ext.isDate(value)) {
                            return Ext.Date.format(value, "Y-m-d  H:i");
                        }
                        else {
                            return "从未登录";
                        }
                    }
                }
            ],
            listeners:
            {
                selectionchange: function (view, records, eOpts) {
                    var btn_edit = Ext.Viewport.down("#t_top").down("#btn_edit");
                    btn_edit.setDisabled(!records.length);

                    var btn_password = Ext.Viewport.down("#t_top").down("#btn_password");
                    btn_password.setDisabled(!records.length);
                }
            }
        });

        Ext.Viewport.add(grid);
        this.buildToolbar();
    },

    btn_addHandler: function (btn) {
        var admin_add = Ext.Viewport.down("#admin_add");

        if (!admin_add) {
            var overlay = Ext.Viewport.add({
                xtype: 'app.modules.Admin.Add',
                itemId: "admin_add"
            });
        }
    },

    buildToolbar: function () {
        var btn_add =
        {
            iconCls: "add",
            handler: this.btn_addHandler
        };

        var btn_edit =
        {
            iconCls: "compose",
            itemId: "btn_edit",
            disabled: true,
            handler: function (btn) {
                var selections = Ext.Viewport.down("#admins_grid").getSelection(),
                    admin_add = Ext.Viewport.down("#admin_add"),
                    id = selections[0].get("Id");

                if (!admin_add) {
                    var overlay = Ext.Viewport.add({
                        xtype: 'app.modules.Admin.Add',
                        itemId: "admin_add",
                        eId: id
                    });
                }
            }
        };

        var btn_refresh =
        {
            iconCls: "refresh",
            handler: function (btn) {
                var grid = Ext.Viewport.down("#admins_grid");
                grid.getStore().load();
            }
        };

        var btn_password =
        {
            iconCls: "lock_closed",
            itemId: "btn_password",
            disabled: true,
            handler: function (btn) {
                var selections = Ext.Viewport.down("#admins_grid").getSelection(),
                    admin_password = Ext.Viewport.down("#admin_password"),
                    id = selections[0].get("Id");

                if (!admin_password) {
                    var overlay = Ext.Viewport.add({
                        xtype: 'app.modules.Admin.Password',
                        itemId: "admin_password",
                        eId: id
                    });
                }
            }
        };

        Ext.Viewport.down("#t_top").insert(0, btn_refresh);
        Ext.Viewport.down("#t_top").insert(0, btn_password);
        Ext.Viewport.down("#t_top").insert(0, btn_edit);
        Ext.Viewport.down("#t_top").insert(0, btn_add);
    },
    destroy: function () {
        var admin_add = Ext.Viewport.down("#admin_add");

        if (admin_add) {
            admin_add.destroy();
        }

        var admin_password = Ext.Viewport.down("#admin_password");

        if (admin_password) {
            admin_password.destroy();
        }
    }
});

Ext.define("app.modules.Admin.Add",
{
    extend: "Ext.Panel",
    id: "pnlAdminAdd",
    xtype: "app.modules.Admin.Add",
    config: {
        modal: true,
        centered: true,
        hideOnMaskTap: true,
        layout: "fit",
        width: Ext.os.deviceType == 'Phone' ? "100%" : 400,
        height: Ext.os.deviceType == 'Phone' ? "100%" : 400,
        scrollable: true,

        items: [
            {
                xtype: "formpanel",
                listeners:
                {
                    painted: function (comp, opts) {
                        var panel = Ext.getCmp("pnlAdminAdd"),
                            form = panel.down("formpanel");

                        if (panel.config.eId) {
                            form.setMasked({
                                xtype: 'loadmask',
                                message: '请稍候...'
                            });
                            Ext.Ajax.request({
                                url: '/app/GetAdministrator',
                                jsonData: {
                                    adminId: panel.config.eId
                                },
                                scope: this,
                                success: function (response) {
                                    var data = Ext.decode(response.responseText);
                                    form.setValues(data);

                                    if (Ext.isArray(data.Roles)) {
                                        var roles = Ext.Array.map(data.Roles, function (item) {
                                            return item.Id;
                                        });

                                        form.down("#roles").setValue(roles);
                                    }

                                    form.setMasked(false);
                                }
                            });

                            form.setUrl("/app/UpdateAdministrator");
                        }
                        else {
                            var fieldset = form.down("fieldset");
                            fieldset.insert(2, {
                                id: "Password",
                                name: 'Password',
                                xtype: "passwordfield",
                                label: '密码'
                            });

                            fieldset.insert(3, {
                                id: "ConfirmPassword",
                                xtype: "passwordfield",
                                name: 'ConfirmPassword',
                                label: '确认密码'
                            });

                            form.setUrl("/app/CreateAdministrator");
                        }
                    }
                },
                items: [
                    {
                        xtype: "fieldset",
                        defaultType: "textfield",
                        defaults: {
                            required: true,
                            labelAlign: 'left',
                            autoComplete: false,
                            labelWidth: '40%'
                        },
                        title: "添加管理员",
                        items: [
                            {
                                id: "Name",
                                name: 'Name',
                                label: '姓名'
                            },
                            {
                                id: "UserName",
                                name: 'UserName',
                                label: '登录名'
                            },
                            {
                                id: "Mobile",
                                name: 'Mobile',
                                required: false,
                                label: '手机'
                            },
                            {
                                id: "IsSuper",
                                name: 'IsSuper',
                                required: false,
                                xtype: 'togglefield',
                                label: '超管'
                            },
                            {
                                xtype: "multiselectfield",
                                itemId: "roles",
                                usePicker: false,
                                displayField: 'Name',
                                valueField: 'Id',
                                label: '角色',
                                store: {
                                    autoLoad: true,
                                    proxy: {
                                        type: 'ajax',
                                        model: "app.models.RoleModel",
                                        url: '/app/Roles',
                                        actionMethods: { create: "POST", read: "POST", update: "POST", destroy: "POST" },
                                        reader: {
                                            type: 'json'
                                        }
                                    }
                                }
                            }
                        ]
                    }
                ]
            },
            {
                docked: 'bottom',
                xtype: 'toolbar',
                defaultTypes: "button",
                layout: {
                    pack: 'center',
                    align: 'center'
                },
                items: [
                    {
                        text: "提交",
                        ui: "confirm",
                        handler: function (btn) {
                            var form = btn.up("panel").down("formpanel"),
                                eId = btn.up("panel").config.eId,
                                values = form.getValues(),
                                model = Ext.create('app.models.AdministratorModel', Ext.apply(values, { Id: eId })),
                                roles = form.down("#roles").getValue(),
                                roleIds = [],
                                errors = model.validate(),
                                msg = '';

                            if (roles && roles.length) {
                                roleIds = Ext.Array.map(roles, function (item) {
                                    return { Id: parseInt(item) };
                                });
                            }

                            if (!errors.isValid()) {
                                util.err("错误", errors);
                            }
                            else {
                                form.setMasked({
                                    xtype: 'loadmask',
                                    message: '请稍候...'
                                });

                                Ext.apply(values, { Id: eId, Roles: roleIds });

                                Ext.Ajax.request(
                                {
                                    url: form.getUrl(),
                                    method: "post",
                                    jsonData: values,
                                    success: function (response, opts) {
                                        var obj = Ext.decode(response.responseText);
                                        form.setMasked(false);
                                        if (obj.success) {
                                            var admins_grid = Ext.Viewport.down("#admins_grid");

                                            if (admins_grid) {
                                                admins_grid.getStore().load();
                                            }

                                            btn.up("panel").destroy();
                                        }
                                        else {
                                            util.err("错误", obj.error);
                                        }
                                    }
                                });
                            };
                        }
                    },
                    {
                        text: "重置",
                        handler: function (btn) {
                            btn.up("panel").down("formpanel").reset();
                        }
                    },
                    {
                        text: "取消",
                        ui: "decline",
                        handler: function (btn) {
                            btn.up("panel").destroy();
                        }
                    }
                ]
            }
        ]
    }
});

Ext.define("app.modules.Admin.Password",
{
    extend: "Ext.Panel",
    xtype: "app.modules.Admin.Password",
    config:
    {
        modal: true,
        centered: true,
        hideOnMaskTap: true,
        layout: "fit",
        width: Ext.os.deviceType == 'Phone' ? '90%' : 400,
        height: Ext.os.deviceType == 'Phone' ? '100%' : 250,
        scrollable: true,
        items: [
            {
                xtype: "formpanel",
                url: "/app/UpdatePassword",
                items: [
                    {
                        xtype: "fieldset",
                        defaultType: "passwordfield",
                        defaults: {
                            required: true,
                            labelAlign: 'left',
                            autoComplete: false,
                            labelWidth: '40%'
                        },
                        title: "更改密码",
                        items: [
                            {
                                id: "Password",
                                name: 'Password',
                                label: '新密码'
                            },
                            {
                                id: "ConfirmPassword",
                                name: 'ConfirmPassword',
                                label: '确认密码'
                            }
                        ]
                    }
                ]
            },
            {
                docked: 'bottom',
                xtype: 'toolbar',
                defaultTypes: "button",
                layout: {
                    pack: 'center',
                    align: 'center'
                },
                items: [
                    {
                        text: "提交",
                        ui: "confirm",
                        handler: function (btn) {
                            var form = btn.up("panel").down("formpanel"),
                                eId = btn.up("panel").config.eId,
                                values = form.getValues(),
                                model = Ext.create('app.models.PasswordModel', values),
                                errors = model.validate(),
                                msg = '';

                            if (!errors.isValid()) {
                                util.err("错误", errors);
                            }
                            else {
                                form.setMasked({
                                    xtype: 'loadmask',
                                    message: '请稍候...'
                                });

                                form.submit(
                                {
                                    params:
                                    {
                                        Id: eId
                                    },
                                    success: function (form, result) {
                                        btn.up("panel").destroy();
                                    },
                                    failure: function (form, result) {
                                        util.err("错误", result.error);
                                    }
                                }, null, null, false, "请稍候...");
                            };
                        }
                    },
                    {
                        text: "重置",
                        handler: function (btn) {
                            btn.up("panel").down("formpanel").reset();
                        }
                    },
                    {
                        text: "取消",
                        ui: "decline",
                        handler: function (btn) {
                            btn.up("panel").destroy();
                        }
                    }
                ]
            }
        ]
    }
});
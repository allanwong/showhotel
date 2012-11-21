Ext.define("app.modules.MyPassword",
{
    extend: "Ext.Panel",
    xtype: "app.modules.MyPassword",
    requires: [
        'app.models.MyPasswordModel'
    ],
    config:
    {
        modal: true,
        centered: true,
        hideOnMaskTap: true,
        layout: "fit",
        width: Ext.os.deviceType == 'Phone' ? '90%' : 400,
        height: Ext.os.deviceType == 'Phone' ? '100%' : 300,
        scrollable: true,
        items: [
            {
                xtype: "formpanel",
                url: "/app/UpdateMyPassword",
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
                        title: "修改我的密码",
                        items: [
                            {
                                id: "OldPassword",
                                name: 'OldPassword',
                                label: '当前密码'
                            },
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
                                values = form.getValues(),
                                model = Ext.create('app.models.MyPasswordModel', values),
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
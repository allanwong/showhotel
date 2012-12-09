Ext.Loader.setConfig({
    enabled: true,
    paths: {
        'app': '/scripts/app'
    }
});

Ext.application({
    // Setup the icons and startup screens for phones and tablets.
    phoneStartupScreen: '/content/img/loading/Homescreen.jpg',
    tabletStartupScreen: '/content/img/loading/Homescreen~ipad.jpg',

    glossOnIcon: false,
    icon: {
        57: '/content/img/icon.png',
        72: '/content/img/icon@72.png',
        114: '/content/img/icon@2x.png',
        144: '/content/img/icon@114.png'
    },

    requires: [
        'Ext.form.*',
        'Ext.field.*',
        'Ext.Button',
        'Ext.data.Store',
        'app.ux.field.ToggleBool'
    ],

    launch: function () {
        var items = this.getFormItems(),
            config, form;

        config = {
            xtype: 'formpanel',
            submitOnAction: true,
            listeners:
            {
                show: function (comp, opts) {
                    var state = util.getAppState(),
                        field = comp.down("#UserName"),
                        toggle = comp.down("#RememberMe");

                    field.focus();

                    if (state.login) {
                        toggle.setValue(state.login.RememberMe);

                        if (state.login.RememberMe) {
                            field.setValue(state.login.UserName);
                            comp.down("#Password").focus();
                        }
                    }
                }
            },
            url: "/home/login",
            items: items
        };

        if (Ext.os.deviceType == 'Phone') {
            form = Ext.Viewport.add(config);
        } else {
            Ext.apply(config, {
                modal: true,
                height: 505,
                width: 480,
                centered: true,

                hideOnMaskTap: false
            });

            form = Ext.Viewport.add(config);
            form.show();
        }

        this.form = form;
    },

    getFormItems: function () {
        return [
            {
                xtype: 'fieldset',
                title: '尚豪系列酒店管理控制台',
                defaults: {
                    required: true,
                    labelAlign: 'left',
                    labelWidth: '40%'
                },
                items: [
                    {
                        xtype: 'textfield',
                        itemId: "UserName",
                        name: 'UserName',
                        autoComplete: false,
                        label: '用户名'
                    },
                    {
                        xtype: 'passwordfield',
                        itemId: "Password",
                        name: 'Password',
                        autoComplete: false,
                        label: '密码'
                    },
                    {
                        xtype: 'togglefield',
                        itemId: "RememberMe",
                        required: false,
                        name: "RememberMe",
                        label: '记住我'
                    }
                ]
            },
            {
                xtype: 'container',
                margin: "0 0 5 0",
                layout: "hbox",
                items: [
                    { xtype: "img", id: "captcha", src: capctha, width: 108, height: 38, margin: "0 8 0 0" },
                    { xtype: "button", text: "看不清，换一张", handler: function (btn) {
                        var captcha = Ext.getCmp("captcha"),
                            newSrc = captcha.getSrc() + "?";

                        captcha.setSrc(newSrc);
                    }
                    }
                ]
            },
            {
                xtype: 'fieldset',
                instructions: '请输入登录信息',
                defaults: {
                    xtype: 'textfield',
                    required: true,
                    labelAlign: 'left',
                    labelWidth: '40%'
                },
                items: [
                    { name: 'Captcha', label: '验证码', autoComplete: false }
                ]
            },
            {
                xtype: 'toolbar',
                docked: 'bottom',
                layout: {
                    pack: 'center',
                    align: 'center'
                },
                items: [
                {
                    text: '重置',
                    scope: this,
                    handler: function () {
                        this.form.reset();
                    }
                },
                {
                    text: '登录',
                    ui: 'confirm',
                    scope: this,
                    handler: function () {
                        var form = this.form,
                            values = form.getValues(),
                            model = Ext.create('app.models.LoginModel', values),
                            errors = model.validate(),
                            msg = '';

                        if (!errors.isValid()) {
                            util.err("错误", errors);
                        }
                        else {
                            form.setMasked({
                                xtype: 'loadmask',
                                message: '登录中...'
                            });

                            var state = util.getAppState();
                            state.login =
                            {
                                RememberMe: model.get("RememberMe"),
                                UserName: model.get("RememberMe") ? model.get("UserName") : null
                            };

                            util.setAppState(state);

                            form.submit(
                            {
                                success: function (form, result) {
                                    window.location = result.returnUrl;
                                },
                                failure: function (form, result) {
                                    util.err("错误", result.error);
                                }
                            }, null, null, false, "登录中...");
                        };
                    }
                }]
            }
        ];
    }
});


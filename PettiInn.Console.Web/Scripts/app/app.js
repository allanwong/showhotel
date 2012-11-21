Ext.Loader.setConfig({
    enabled: true,
    paths: {
         'app': '/scripts/app',
        'Ext.ux.touch.grid': '/scripts/app/ux/grid'
    }
});

Ext.application({
    glossOnIcon: false,

    icon: {
        57: '/content/img/icon.png',
        72: '/content/img/icon@72.png',
        114: '/content/img/icon@2x.png',
        144: '/content/img/icon@114.png'
    },

    phoneStartupScreen: '/content/img/loading/Homescreen.jpg',
    tabletStartupScreen: '/content/img/loading/Homescreen~ipad.jpg',

    requires: [
        'app.models.ModuleModel',
        'app.modules.MyPassword',
        'Ext.tab.Panel',
        'Ext.Toolbar',
        'app.ux.field.ToggleBool'
    ],

    launch: function () {
        Ext.Viewport.add([
            {
                xtype: 'toolbar',
                itemId: "t_bottom",
                docked: 'bottom',
                layout: {
                    pack: 'center',
                    align: 'center'
                },
                defaults: {
                    iconMask: true
                },

                scrollable: {
                    direction: 'horizontal',
                    indicators: false
                }
            },

            {
                xtype: 'toolbar',
                itemId: "t_top",
                docked: 'top',

                layout: {
                    pack: 'center',
                    align: 'center'
                },
                scrollable: {
                    direction: 'horizontal',
                    indicators: false
                },
                defaults: {
                    iconMask: true,
                    ui: 'plain'
                },
                items: [
                        {
                            itemId: "opGroup",
                            xtype: "segmentedbutton",
                            items: [
                                {
                                    text: "更改密码",
                                    handler: function (btn) {
                                        btn.up("segmentedbutton").setPressedButtons([]);
                                        var myPassword = Ext.Viewport.down("#myPassword");

                                        if (!myPassword) {
                                            var overlay = Ext.Viewport.add({
                                                xtype: 'app.modules.MyPassword',
                                                itemId: "myPassword"
                                            });
                                        }
                                    }
                                },
                                {
                                    text: "关于",
                                    handler: function (btn) {
                                        btn.up("segmentedbutton").setPressedButtons([]);
                                    }
                                },
                                {
                                    text: "退出",
                                    ui: "decline",
                                    handler: function (btn) {
                                        btn.up("segmentedbutton").setPressedButtons([]);
                                        util.ask("退出", "您确定要退出系统吗？", function () {
                                            util.logout();
                                        });
                                    }
                                }
                            ]
                        }
                    ]
            }
            ]);

        this.loadModules();
    },
    loadModules: function () {
        var myStore = Ext.create('Ext.data.Store', {
            model: 'app.models.ModuleModel',
            proxy: {
                type: 'ajax',
                url: '/app/GetRootModules',
                actionMethods: { read: "POST" },
                reader: {
                    type: 'json'
                }
            },
            autoLoad: true,
            listeners:
            {
                beforeload: function (store, operation, eOpts) {
                    Ext.Viewport.setMasked({
                        xtype: 'loadmask',
                        message: '初始化...'
                    });
                },
                load:
                {
                    scope: this,
                    fn: function (store, records, successful, operation, eOpts) {
                        var bToolBar = Ext.Viewport.down("#t_bottom");

                        Ext.Viewport.setMasked(false);
                        var btns = Ext.create("Ext.SegmentedButton",
                        {
                            allowDepress: false,
                            defaults:
                            {
                                minWidth: 100
                            }
                        });
                        Ext.each(records, function (record, index) {
                            btns.add(
                            {
                                //iconCls: record.get("CSS"),
                                text: record.get("Name"),
                                itemId: util.moduleId(record.get("Id")) + "_btn",
                                scope: this,
                                handler: function (btn) {
                                    Ext.Viewport.removeInnerAt(0);

                                    if (this.module) {
                                        this.module.destroy();
                                    }

                                    var btnsToRemove = Ext.Array.filter(Ext.Viewport.down("#t_top").getItems().items, function (b) {
                                        return b.getItemId() !== "opGroup";
                                    });
                                    Ext.Array.each(btnsToRemove, function (b) {
                                        Ext.Viewport.down("#t_top").remove(b, true);
                                    });

                                    this.module = Ext.create(record.get("Class"));
                                }
                            });
                        }, this);
                        bToolBar.add(btns);
                    }
                }
            }
        });
    }
});

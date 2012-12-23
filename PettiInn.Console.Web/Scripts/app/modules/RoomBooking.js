Ext.define("app.modules.RoomBooking",
    {
        requires: [
            'app.models.HotelModel'
        ],
        constructor: function (args) {
            this.callParent(arguments);

            var overlay = Ext.Viewport.add({
                xtype: 'app.modules.RoomBooking.Add',
                itemId: "booking_add"
            });
        }
    });

Ext.define("app.modules.RoomBooking.Add",
    {
        extend: "Ext.Panel",
        id: "pnlBookingAdd",
        xtype: "app.modules.RoomBooking.Add",
        config: {
            modal: true,
            centered: true,
            hideOnMaskTap: false,
            layout: "fit",
            width: Ext.os.deviceType == 'Phone' ? "100%" : 400,
            height: Ext.os.deviceType == 'Phone' ? "100%" : 400,
            scrollable: true,

            items: [
                {
                    xtype: "formpanel",
                    //listeners:
                    //{
                    //    painted: function (comp, opts) {
                    //        var panel = Ext.getCmp("pnlBookingAdd"),
                    //            form = panel.down("formpanel");

                    //        if (panel.config.eId) {
                    //            form.setMasked({
                    //                xtype: 'loadmask',
                    //                message: '请稍候...'
                    //            });
                    //            Ext.Ajax.request({
                    //                url: '/app/GetAgent',
                    //                jsonData: {
                    //                    id: panel.config.eId
                    //                },
                    //                scope: this,
                    //                success: function (response) {
                    //                    var data = Ext.decode(response.responseText);
                    //                    form.data = data;
                    //                    form.setValues(data);
                    //                    form.setMasked(false);
                    //                }
                    //            });
                    //            form.setUrl("/app/UpdateAgent");
                    //        }
                    //        else {
                    //            form.setUrl("/app/CreateAgent");
                    //        }
                    //    }
                    //},
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
                            title: "预订房间",
                            items: [
                                {
                                    xtype: "selectfield",
                                    itemId: "hotels",
                                    name: "HotelId",
                                    displayField: 'Name',
                                    valueField: 'Id',
                                    label: '酒店',
                                    store: {
                                        autoLoad: true,
                                        proxy: {
                                            type: 'ajax',
                                            model: "app.models.HotelModel",
                                            url: '/app/HotelsSimple',
                                            actionMethods: { create: "POST", read: "POST", update: "POST", destroy: "POST" },
                                            reader: {
                                                type: 'json'
                                            }
                                        }
                                    }
                                },
                                {
                                    id: "Priority",
                                    name: 'Priority',
                                    xtype: "numberfield",
                                    value: 10,
                                    minValue: 0,
                                    label: '优先级'
                                },
                                {
                                    name: "Address",
                                    xtype: "textareafield",
                                    required: false,
                                    label: "办公地址"
                                },
                                {
                                    name: "Comment",
                                    xtype: "textareafield",
                                    required: false,
                                    label: "备注"
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
                                    model = Ext.create('app.models.AgentModel', Ext.apply(values, { Id: eId })),
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

                                    Ext.apply(values, { Id: eId });

                                    Ext.Ajax.request(
                                    {
                                        url: form.getUrl(),
                                        method: "post",
                                        jsonData: values,
                                        success: function (response, opts) {
                                            var obj = Ext.decode(response.responseText);
                                            form.setMasked(false);
                                            if (obj.success) {
                                                var rooms_grid = Ext.Viewport.down("#agents_grid");

                                                if (rooms_grid) {
                                                    rooms_grid.getStore().load();
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
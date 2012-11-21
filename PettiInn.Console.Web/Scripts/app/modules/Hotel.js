Ext.define("app.modules.Hotel",
{
    requires: [
        'app.models.HotelModel',
        'app.models.RoomTypeModel',
        "app.ux.field.MultiSelect",
        "app.ux.field.BaiduMap",
        'Ext.ux.touch.grid.List',
        'Ext.ux.touch.grid.feature.Feature',
        'Ext.ux.touch.grid.feature.Sorter'
    ],
    constructor: function (args) {
        this.callParent(arguments);

        var store = Ext.create('Ext.data.Store', {
            model: 'app.models.HotelModel',
            autoLoad: true,
            proxy: {
                type: 'ajax',
                url: '/app/Hotels',
                actionMethods: { create: "POST", read: "POST", update: "POST", destroy: "POST" },
                reader: {
                    type: 'json'
                }
            }
        });

        var grid = Ext.create('Ext.ux.touch.grid.List', {
            itemId: "hotels_grid",
            store: store,

            features: [
                {
                    ftype: 'Ext.ux.touch.grid.feature.Sorter',
                    launchFn: 'initialize'
                }
            ],
            columns: [
                {
                    header: '名称',
                    dataIndex: 'Name',
                    width: "60%",
                    style: 'padding-left: 1em;',
                    filter: { type: 'string' }
                },
                {
                    header: '排序',
                    dataIndex: 'Sort',
                    width: "40%",
                    style: 'padding-left: 1em;',
                    filter: { type: 'int' }
                }
            ],
            listeners:
            {
                selectionchange: function (view, records, eOpts) {
                    var btn_edit = Ext.Viewport.down("#t_top").down("#btn_edit");
                    btn_edit.setDisabled(!records.length);

                    var btn_delete = Ext.Viewport.down("#t_top").down("#btn_delete");
                    btn_delete.setDisabled(!records.length);
                }
            }
        });

        Ext.Viewport.add(grid);
        this.buildToolbar();
    },

    btn_addHandler: function (btn) {
        var role_add = Ext.Viewport.down("#hotel_add");

        if (!role_add) {
            var overlay = Ext.Viewport.add({
                xtype: 'app.modules.Hotel.Add',
                itemId: "hotel_add"
            });
        }
    },

    btn_editHandler: function (btn) {
        var selections = Ext.Viewport.down("#hotels_grid").getSelection(),
                    role_add = Ext.Viewport.down("#hotel_add"),
                    id = selections[0].get("Id");

        if (!role_add) {
            var overlay = Ext.Viewport.add({
                xtype: 'app.modules.Hotel.Add',
                itemId: "hotel_add",
                eId: id
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
            handler: this.btn_editHandler
        };

        var btn_delete =
        {
            itemId: "btn_delete",
            iconCls: "delete",
            disabled: true,
            handler: function (btn) {
                util.ask("删除", "您确定要删除该酒店吗？<div style=\"color:red\">警告：删除酒店会将属于该酒店的所有信息和预订全部删除！</div>", function () {
                    var grid = Ext.Viewport.down("#hotels_grid"),
                        selections = grid.getSelection(),
                        id = selections[0].get("Id");

                    grid.setMasked({
                        xtype: 'loadmask',
                        message: '请稍候...'
                    });

                    Ext.Ajax.request({
                        url: '/app/DeleteHotel',
                        jsonData: {
                            Id: id
                        },
                        scope: this,
                        success: function (response) {
                            var data = Ext.decode(response.responseText);

                            if (data.success) {
                                grid.getStore().load();
                            }
                            else {
                                util.err("错误", data.error);
                            }

                            grid.setMasked(false);
                        }
                    });
                });
            }
        };

        var btn_refresh =
        {
            iconCls: "refresh",
            handler: function (btn) {
                var grid = Ext.Viewport.down("#hotels_grid");
                grid.getStore().load();
            }
        };

        Ext.Viewport.down("#t_top").insert(0, btn_delete);
        Ext.Viewport.down("#t_top").insert(0, btn_refresh);
        Ext.Viewport.down("#t_top").insert(0, btn_edit);
        Ext.Viewport.down("#t_top").insert(0, btn_add);
    },
    destroy: function () {
        var hotel_add = Ext.Viewport.down("#hotel_add");

        if (hotel_add) {
            hotel_add.destroy();
        }
    }
});

Ext.define("app.modules.Hotel.Add",
{
    extend: "Ext.Panel",
    id: "pnlHotelAdd",
    xtype: "app.modules.Hotel.Add",
    config: {
        modal: true,
        centered: true,
        hideOnMaskTap: true,
        eId: null,
        layout: "fit",
        width: Ext.os.deviceType == 'Phone' ? '100%' : 650,
        height: Ext.os.deviceType == 'Phone' ? '100%' : 450,
        scrollable: true,

        items: [
            {
                xtype: "formpanel",
                padding: 0,
                listeners:
                {
                    painted: function (comp, opts) {
                        var panel = Ext.getCmp("pnlHotelAdd"),
                            form = panel.down("formpanel");
                        if (panel.getEId()) {
                            form.setMasked({
                                xtype: 'loadmask',
                                message: '请稍候...'
                            });
                            Ext.Ajax.request({
                                url: '/app/GetHotel',
                                jsonData: {
                                    hotelId: panel.config.eId
                                },
                                scope: this,
                                success: function (response) {
                                    var data = Ext.decode(response.responseText);
                                    form.setValues(data);

                                    if (Ext.isArray(data.RoomTypes)) {
                                        var roomtypes = Ext.Array.map(data.RoomTypes, function (item) {
                                            return item.Id;
                                        });

                                        form.down("#roomtypes").setValue(roomtypes);
                                    }

                                    if (!Ext.isEmpty(data.Location)) {
                                        var loc = Ext.decode(data.Location);

                                        form.down("#mapVal").setValue(data.Location);
                                        form.down("#Loc").setValue(loc.search);
                                        form.down("#Desc").setValue(loc.info);
                                    }

                                    form.setMasked(false);
                                }
                            });

                            form.setUrl("/app/UpdateHotel");
                        }
                        else {
                            form.setUrl("/app/CreateHotel");
                        }
                    }
                },
                items: [
                    {
                        xtype: "tabpanel",
                        height: 450,
                        items: [
                            {
                                title: "基本设置",
                                items: [
                                    {
                                        xtype: "fieldset",
                                        defaultType: "textfield",
                                        defaults: {
                                            labelAlign: 'left',
                                            autoComplete: false,
                                            labelWidth: '40%'
                                        },
                                        title: "基本信息",
                                        items: [
                                        {
                                            itemId: "mapVal",
                                            xtype: 'hiddenfield'
                                        },
                                        {
                                            name: 'Name',
                                            required: true,
                                            label: '酒店名称'
                                        },
                                        {
                                            name: "Sort",
                                            xtype: "numberfield",
                                            value: 10,
                                            minValue: 0,
                                            label: "排序"
                                        },
                                        {
                                            name: "Address",
                                            xtype: "textareafield",
                                            label: "详细地址"
                                        }]
                                    },
                                    {
                                        xtype: "fieldset",
                                        defaults: {
                                            labelAlign: 'left',
                                            labelWidth: '40%'
                                        },
                                        title: "酒店房型",
                                        items: [
                                            {
                                                xtype: "multiselectfield",
                                                itemId: "roomtypes",
                                                usePicker: false,
                                                displayField: 'Name',
                                                valueField: 'Id',
                                                label: '房型',
                                                store: {
                                                    autoLoad: true,
                                                    proxy: {
                                                        type: 'ajax',
                                                        model: "app.models.RoomTypeModel",
                                                        url: '/app/Roomtypes',
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
                                title: "地图",
                                layout:
                                {
                                    type: "hbox",
                                    align: "stretch"
                                },
                                items: [
                                    {
                                        xtype: "container",
                                        width: 200,
                                        layout: "vbox",
                                        padding: "0 0 58 0",
                                        items: [
                                            {
                                                itemId: "Loc",
                                                name: "Loc",
                                                xtype: "textareafield",
                                                flex: 1,
                                                label: "地址"
                                            },
                                            {
                                                itemId: "Desc",
                                                name: "Desc",
                                                flex: 2,
                                                xtype: "textareafield",
                                                label: "描述"
                                            },
                                            {
                                                xtype: 'toolbar',
                                                layout: {
                                                    pack: 'center',
                                                    align: 'center'
                                                },
                                                items: [
                                                    {
                                                        xtype: "button",
                                                        text: "搜索",
                                                        handler: function (btn) {
                                                            var pnl = btn.up("formpanel"),
                                                                search = pnl.down("#Loc").getValue(),
                                                                info = pnl.down("#Desc").getValue(),
                                                                map = pnl.down("baidumap");

                                                            if (!Ext.isEmpty(search)) {
                                                                map.updateGeo(
                                                                {
                                                                    search: search,
                                                                    info: info
                                                                });
                                                            }
                                                        }
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        xtype: "baidumap",
                                        flex: 1,
                                        listeners:
                                        {
                                            loaded: function (comp, map) {
                                                var pnl = comp.up("#hotel_add"),
                                                    eId = pnl.getEId();

                                                if (eId) {
                                                    var map = pnl.down("baidumap"),
                                                        mapVal = pnl.down("#mapVal").getValue();

                                                    if (!Ext.isEmpty(mapVal)) {
                                                        var v = Ext.decode(mapVal);
                                                        map.updateGeo(
                                                        {
                                                            search: v.search,
                                                            info: v.info,
                                                            zoom: v.zoom
                                                        });
                                                    }
                                                }
                                            }
                                        },
                                        mapOptions:
                                        {
                                            enableHighResolution: true
                                        }
                                    }
                                ]
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
                                eId = btn.up("panel").getEId(),
                                values = form.getValues(),
                                model = Ext.create('app.models.HotelModel', Ext.apply(values, { Id: eId })),
                                roomtypes = form.down("#roomtypes").getValue(),
                                roomtypesIds = [],
                                errors = model.validate(),
                                map = form.down("baidumap"),
                                mapVal = map.getMapOptions(),
                                location = Ext.encode({
                                    search: values.Loc,
                                    info: values.Desc,
                                    zoom: mapVal.zoom
                                });

                            if (roomtypes && roomtypes.length) {
                                roomtypesIds = Ext.Array.map(roomtypes, function (item) {
                                    return { Id: item };
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

                                Ext.apply(values, { Id: eId, RoomTypes: roomtypesIds, Location: location });
                                Ext.Ajax.request(
                                {
                                    url: form.getUrl(),
                                    method: "post",
                                    jsonData: values,
                                    success: function (response, opts) {
                                        var obj = Ext.decode(response.responseText);
                                        form.setMasked(false);
                                        if (obj.success) {
                                            var hotels_grid = Ext.Viewport.down("#hotels_grid");

                                            if (hotels_grid) {
                                                hotels_grid.getStore().load();
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
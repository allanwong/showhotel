Ext.define('app.ux.field.BaiduMap', {
    extend: 'Ext.Component',
    xtype: 'baidumap',
    requires: ['Ext.util.Geolocation'],

    isMap: true,

    config: {
        baseCls: Ext.baseCSSPrefix + 'map',
        useCurrentLocation: false,
        api: "http://api.map.baidu.com/api?v=1.3&callback=initialize",
        map: null,
        geo: null,
        nav: null,
        location:
        {
            local: "香港",
            search: "尖沙咀",
            info: "",
            zoom: 16
        },
        mapOptions:
        {
            nav: true
        }
    },

    constructor: function () {
        this.callParent(arguments);
        this.element.setVisibilityMode(Ext.Element.OFFSETS);
    },

    initialize: function () {
        this.callParent();
        this.on({
            painted: 'onPainted',
            scope: this
        });
        this.element.on('touchstart', 'onTouchStart', this);
    },

    onTouchStart: function (e) {
        e.makeUnpreventable();
    },

    getMapOptions: function () {
        return Ext.merge({}, this.options || this.getInitialConfig('mapOptions'));
    },

    getValue: function () {
        var val = {
            zoom: this.getMap().getZoom()
        };

        return val;
    },

    updateGeo: function (location) {
        var geo = this.getGeo(),
            map = this.getMap(),
            location = Ext.apply(this.getLocation(), location),
            infoWindow;

        geo.getPoint(location.search, function (point) {
            if (point) {
                map.clearOverlays();
                map.centerAndZoom(point, location.zoom);
                var marker = new BMap.Marker(point);

                map.addOverlay(marker);

                if (!Ext.isEmpty(location.info)) {
                    infoWindow = new BMap.InfoWindow(location.info);
                    marker.openInfoWindow(infoWindow);
                }
                else {
                    infoWindow = null;
                }

                marker.addEventListener("click", function () {
                    if (infoWindow) {
                        this.openInfoWindow(infoWindow);
                    }
                });
            }
        }, location.local);
    },

    onPainted: function () {
        var me = this;
        if (!window.BMap) {
            this.getParent().setMasked({
                xtype: 'loadmask',
                message: '请稍候...'
            });

            Ext.Loader.injectScriptElement(this.getApi(), function () {
                this.getParent().setMasked(false);

                var gm = window.BMap,
                    map = this.getMap();

                setTimeout(function () {
                    me.renderMap();
                }, 100);
            }, function () {
                this.getParent().setMasked(false);
                util.err("读取地图失败", "无法读取百度地图");
            }, this);
        }
        else {
            setTimeout(function () {
                me.renderMap();
            }, 100);
        }
    },

    // @private
    renderMap: function () {
        var me = this,
            gm = window.BMap,
            element = me.element,
            mapOptions = me.getMapOptions(),
            map = me.getMap(),
            event;

        if (gm) {
            me.setMap(new gm.Map(element.dom, mapOptions));
            me.setGeo(new gm.Geocoder());
            map = me.getMap();

            var loc = this.getLocation(),
                opts = this.getMapOptions();

            if (opts.nav) {
                me.setNav(new BMap.NavigationControl());
                map.addControl(me.getNav());
            }
            
            map.addEventListener('zoomend', Ext.bind(me.onZoomChange, me));

            me.fireEvent('loaded', this, map);
        }
    },

    // @private
    onZoomChange: function () {
        var mapOptions = this.getMapOptions(),
            map = this.getMap(),
            zoom;

        zoom = (map && map.getZoom) ? map.getZoom() : mapOptions.zoom || 16;

        this.options = Ext.apply(mapOptions, {
            zoom: zoom
        });

        this.fireEvent('zoomchange', this, map, zoom);
    },

    // @private
    destroy: function () {
        Ext.destroy(this.getGeo());
        Ext.destroy(this.getMap());

        this.callParent();
    }
});

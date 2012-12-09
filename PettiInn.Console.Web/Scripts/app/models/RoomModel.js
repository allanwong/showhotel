Ext.define('app.models.RoomModel', {
    extend: 'app.models.ModelBase',

    config: {
        idProperty: "Id",
        fields: [
            { name: 'Id' },
            { name: 'Name', type: 'string' },
            { name: 'Sort', type: 'int' },
            { name: 'Size', type: 'int' },
            { name: 'HasWindow', type: 'bool' },
            { name: 'RoomType', type: 'string' },
            { name: 'Hotel', type: 'string' },
            { name: 'RoomTypeId', type: 'int' },
            { name: 'HotelId', type: 'int' }
        ],
        associations: [{ type: 'hasOne', model: 'app.models.HotelModel' }, { type: 'hasOne', model: 'app.models.RoomTypeModel' }],
        validations: [
            { type: 'presence', field: 'Name', message: "请输入房间名称" },
            { type: 'presence', field: 'HotelId', message: "请选择房间所属的酒店" },
            { type: 'presence', field: 'RoomTypeId', message: "请选择房间类型" }
        ]
    }
});
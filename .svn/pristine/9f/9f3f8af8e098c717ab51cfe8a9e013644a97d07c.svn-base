Ext.define('app.models.HotelModel', {
    extend: 'app.models.ModelBase',

    config: {
        idProperty: "Id",
        fields: [
            { name: 'Id' },
            { name: 'Name', type: 'string' },
            { name: 'Sort', type: 'int' },
            { name: "Location" },
            { name: "Address" }
        ],
        validations: [
            { type: 'presence', field: 'Name', message: "请输入酒店名称" }
        ]
    }
});
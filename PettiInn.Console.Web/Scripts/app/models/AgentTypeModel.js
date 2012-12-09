Ext.define('app.models.AgentTypeModel', {
    extend: 'app.models.ModelBase',

    config: {
        idProperty: "Id",
        fields: [
            { name: 'Id' },
            { name: 'Name', type: 'string' },
            { name: 'Sort', type: 'int' }
        ],
        validations: [
            { type: 'presence', field: 'Name', message: "请输入中介类型名称" }
        ]
    }
});
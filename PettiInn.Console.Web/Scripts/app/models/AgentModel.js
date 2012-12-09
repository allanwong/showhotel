Ext.define('app.models.AgentModel', {
    extend: 'app.models.ModelBase',

    config: {
        idProperty: "Id",
        fields: [
            { name: 'Id' },
            { name: 'Name', type: 'string' },
            { name: 'Priority', type: 'int' },
            { name: 'TypeId', type: 'int' },
            { name: "AgentType", type: 'string' },
            { name: "Comment", type: 'string' },
            { name: "Address", type: 'string' },
            { name: "CreatedOn", type: 'MSDate' }
        ],
        validations: [
            { type: 'presence', field: 'Name', message: "请输入中介名称" },
            { type: 'presence', field: 'TypeId', message: "请选择中介类型" }
        ]
    }
});
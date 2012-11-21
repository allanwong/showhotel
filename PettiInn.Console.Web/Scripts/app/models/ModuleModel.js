Ext.define('app.models.ModuleModel', {
    extend: 'Ext.data.Model',

    config: {
        idProperty: "Id",
        fields: [
            { name: 'Id' },
            { name: 'Name', type: 'string' },
            { name: 'CSS', type: 'string' },
            { name: 'Class', type: 'string' },
            { name: 'Sort', type: 'int' }
        ]
    }
});
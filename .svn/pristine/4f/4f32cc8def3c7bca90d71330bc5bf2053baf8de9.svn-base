Ext.define('app.models.CurrencyUnitModel', {
    extend: 'app.models.ModelBase',

    config: {
        idProperty: "Id",
        fields: [
            { name: 'Id' },
            { name: 'Name', type: 'string' },
            { name: 'Symbol', type: 'string' },
            { name: 'EName', type: 'string' }
        ],
        validations: [
            { type: 'presence', field: 'Name', message: "请输入货币名称" },
            { type: 'presence', field: 'EName', message: "请输入货币英文代号，如'RMB'" },
            { type: 'presence', field: 'Symbol', message: "请输入货币符号，如'$'" }
        ]
    }
});
Ext.define('app.models.AdministratorModel', {
    extend: 'app.models.ModelBase',

    config: {
        idProperty: "Id",
        fields: [
            { name: 'Id' },
            { name: 'Name', type: 'string' },
            { name: 'UserName', type: 'string' },
            { name: 'Password', type: 'string' },
            { name: 'ConfirmPassword', type: 'string' },
            { name: 'Mobile', type: 'string' },
            { name: 'CreatedOn', type: 'date' },
            { name: 'LastLoggedIn', type: 'MSDate' },
            { name: 'IsSuper', type: 'bool' }
        ],
        validations: [
            { type: 'presence', field: 'Name', message: "请输入用户姓名" },
            { type: 'presence', field: 'UserName', message: "请输入用户名" },
            { type: 'custom', field: 'Password', message: "注册密码长度必须在2-6位之间", fn: function (value) {
                var id = this.get("Id");
                if (id && Ext.isNumber(id)) {
                    return true;
                }
                
                var valid = Ext.data.validations.presence({ field: "Password" }, value) &&
                            Ext.data.validations.length({ field: "Password", min: 6 }, value);

                return valid;
            }
            },
            { type: 'custom', field: 'ConfirmPassword', message: "请输入确认密码，并确保与原密码一致", fn: function (value) {
                var id = this.get("Id");
                if (id && Ext.isNumber(id)) {
                    return true;
                }
                var valid = Ext.data.validations.presence({ field: "ConfirmPassword" }, value) &&
                            value === this.get("Password");

                return valid;
            }
            }
        ]
    }
});
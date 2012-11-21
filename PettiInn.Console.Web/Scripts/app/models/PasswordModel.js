Ext.define('app.models.PasswordModel', {
    extend: 'app.models.ModelBase',

    config: {
        fields: [
            { name: 'Password', type: 'string' },
            { name: 'ConfirmPassword', type: 'string' }
        ],
        validations: [
            { type: 'custom', field: 'Password', message: "注册密码长度必须在2-6位之间", fn: function (value) {
                var valid = Ext.data.validations.presence({ field: "Password" }, value) &&
                            Ext.data.validations.length({ field: "Password", min: 6 }, value);

                return valid;
            }
            },
            { type: 'custom', field: 'ConfirmPassword', message: "请输入确认密码，并确保与原密码一致", fn: function (value) {
                var valid = Ext.data.validations.presence({ field: "ConfirmPassword" }, value) &&
                            value === this.get("Password");

                return valid;
            }
            }
        ]
    }
});
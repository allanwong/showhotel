Ext.define('app.models.LoginModel', {
    extend: 'Ext.data.Model',

    config: {
        fields: [
            { name: 'UserName', type: 'string' },
            { name: 'Password', type: 'string' },
            { name: 'Captcha', type: 'string' },
            { name: 'RememberMe', type: 'bool' }
        ],
        validations: [
            { type: 'presence', field: 'UserName', message: "请输入用户名" },
            { type: 'presence', field: 'Password', message: "请输入登录密码" },
            { type: 'presence', field: 'Captcha', message: "请输入验证码" }
        ]
    }
});
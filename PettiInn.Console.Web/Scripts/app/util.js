function ToJSDate(msdate) {
    var t = msdate.replace("/Date(", "").replace(")/", "");
    if (msdate && msdate.length && t.length > 1) {
        // expects MS JSON dates of the form \/Date(1069689066000)\/ or \/Date(-1069689066000)\/

        var date = new Date(parseInt(t));
        return date;
    }

    return null;
}

Ext.define("app.util",
{
    extend: 'Ext.Sheet',

    logout: function () {
        window.location = '/app/logout';
    },
    api: function(url)
    {
        var api = apiBase + url;

        return api;
    },
    err: function (title, messages) {
        var message = messages;
        if (Ext.isArray(messages)) {
            message = messages.join("<br/>");
        }
        else if (Ext.getClassName(messages) == "Ext.data.Errors") {
            message = '';
            messages.each(function (err) {
                message += err.getMessage() + '<br/>';
            });
        }

        Ext.Msg.show(
        {
            title: title,
            message: message,
            icon: Ext.MessageBox.ERROR
        });
    },
    ask: function (title, message, okFunc, cancelFunc) {
        Ext.Msg.show(
        {
            title: title,
            message: message,
            buttons: Ext.MessageBox.YESNO,
            icon: Ext.MessageBox.QUESTION,
            fn: function (btnId) {
                if (btnId == "yes" && Ext.isFunction(okFunc)) {
                    okFunc();
                }
                else if (btnId == "no" && Ext.isFunction(cancelFunc)) {
                    cancelFunc();
                }
            }
        });
    },
    createCookie: function (name, value, days) {
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else var expires = "";

        if (!Ext.isString(value)) {
            value = Ext.JSON.encode(value);
            value = encodeURIComponent(value);
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    },
    getCookie: function (name) {
        var result;
        var pairs = document.cookie.split('; ');
        for (var i = 0, pair; pair = pairs[i] && pairs[i].split('='); i++) {
            if (pair[0] === name)
                result = pair[1] || '';
        }
        result = decodeURIComponent(result);

        try {
            result = Ext.JSON.decode(result);
        }
        catch (err) {
            console.log(err);
        }
        return result;
    },
    removeCookie: function (name) {
        this.createCookie(name, "", -1);
    },
    getAppState: function () {
        var state = this.getCookie("pettiinn.state") || {};

        return state;
    },
    setAppState: function (state) {
        this.createCookie("pettiinn.state", state);
    },
    moduleId: function (id) {
        return "module_" + id;
    }
}, function (Util) {
    Ext.onSetup(function () {
        util = new Util;
    });
});
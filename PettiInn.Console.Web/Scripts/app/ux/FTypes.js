Ext.data.Types.MSDATE = {
    convert: function (v) {
        if (!v) {
            return null;
        }

        if (Ext.isDate(v)) {
            return v;
        }

        if (Ext.isString(v) && v.length) {
            // expects MS JSON dates of the form \/Date(1069689066000)\/
            var date = ToJSDate(v);
            return date;
        }
    },
    sortType: function (v) {
        return v;
    },
    type: 'MSDate'
}
if (Ext.data) {
    Ext.data.validations.custom = function (config, value) {
        if (config && Ext.isFunction(config.fn)) {
            //this should be the model
            if (config.self) {
                return config.fn.call(config.self, value);
            } else {
                return config.fn(value);
            }
        }
        else {
            return false;
        }
    };
    Ext.data.validations.customMessage = "Error";
}
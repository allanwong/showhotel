Ext.define('app.models.ModelBase', {
    extend: 'Ext.data.Model',

    init: function () {
        var i, len;
        if (this.config.validations) {
            for (i = 0, len = this.config.validations.length; i < len; i++) {
                this.config.validations[i].self = this;
            }
        }
    }
});
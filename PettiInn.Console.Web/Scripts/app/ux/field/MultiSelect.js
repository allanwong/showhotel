Ext.define('app.ux.field.Multiselect', {
    extend: 'Ext.field.Select',
    alias: 'widget.multiselectfield',
    xtype: 'multiselectfield',
    usePicker: false,  //force list panel, not picker
    getTabletPicker: function () {  //override with modified function
        var config = this.getDefaultTabletPickerConfig();
        if (!(this.listPanel)) {
            this.listPanel = Ext.create('Ext.Panel', Ext.apply({
                left: 0,
                top: 0,
                modal: true,
                cls: Ext.baseCSSPrefix + 'select-overlay',
                layout: 'fit',
                hideOnMaskTap: false,
                width: Ext.os.is.Phone ? '14em' : '18em',
                height: Ext.os.is.Phone ? '10em' : '15em',
                items: {
                    xtype: 'list',
                    mode: 'MULTI',
                    store: this.getStore(),
                    itemTpl: '<span class="x-list-label">{' + this.getDisplayField() + ':htmlEncode}</span>',
                    listeners: {
                        select: this.onListSelect,
                        itemtap: this.onListTap,
                        hide: this.onListHide,
                        scope: this
                    },
                    items: {
                        xtype: 'button',
                        text: '确定',
                        ui: 'action',
                        height: '20px',
                        width: '50%',
                        docked: 'bottom',
                        style: 'margin-top: 10px; margin-bottom: 10px; margin-left: auto; margin-right: auto;',
                        listeners: {
                            tap: this.onButtonTap,
                            scope: this
                        }
                    }
                }
            }, config));
        }
        return this.listPanel;
    },


    applyValue: function (value) {  //override with modified function
        this.getOptions();
        if (!(value instanceof Ext.data.Model)) {
            var values = value,
                text = [];

            if (Ext.isString(values)) {
                values = values.split(",");
            }

            if (values) {
                for (var i = 0; i < values.length; i++) {
                    var index = this.getStore().find(this.getValueField(), values[i], null, null, null, true),
                    r = this.getStore().getAt(index);

                    if (r) {
                        text.push(r.get(this.getDisplayField()));
                    }
                }
            }

            this.element.dom.lastChild.firstChild.firstChild.value = text.join(","); //display csv string in field when value applied
        }
        return value;
    },


    updateValue: function (newValue, oldValue) {  //override with modified function
        this.previousRecord = oldValue;
        this.record = newValue;
        // String does not have methods //this.callParent([newValue ? newValue.get(this.getDisplayField()) : '']);
        this.fireEvent('change', this, newValue, oldValue);
    },


    getValue: function () {  //override with modified function
        var record = this.record,
            values = [];

        if (Ext.isString(record) && !Ext.isEmpty(record)) {
            values = record.split(",");
            return values;
        }
        return record;
    },


    showPicker: function () {  //override with modified function
        //check if the store is empty, if it is, return
        if (this.getStore().getCount() === 0) {
            return;
        }
        if (this.getReadOnly()) {
            return;
        }
        this.isFocused = true;
        //hide the keyboard
        //the causes https://sencha.jira.com/browse/TOUCH-1679
        // Ext.Viewport.hideKeyboard();
        if (this.getUsePicker()) {
            var picker = this.getPhonePicker(),
            name = this.getName(),
            value = {};
            value[name] = this.record.get(this.getValueField());
            picker.setValue(value);
            if (!picker.getParent()) {
                Ext.Viewport.add(picker);
            }
            picker.show();
        } else { //reworked code to split csv string into array and select correct list items
            var listPanel = this.getTabletPicker(),
            list = listPanel.down('list'),
            store = list.getStore(),
            itemStringArray = new Array(),
            values = this.getValue(),
            v = 0,
            vNum = values ? values.length : 0;
            if (!listPanel.getParent()) {
                Ext.Viewport.add(listPanel);
            }
            for (v = 0; v < vNum; v++) {
                itemStringArray.push(values[v]);
            }
            v = 0;
            for (v = 0; v < vNum; v++) {
                var record = store.findRecord(this.getValueField(), itemStringArray[v], 0, true, false, false);
                list.select(record, true, false);
            }
            listPanel.showBy(this.getComponent());
            listPanel.down('list').show();
        }
    },


    onListSelect: function (item, record) {  //override with empty function
    },


    onListTap: function () {  //override with empty function
    },


    onButtonTap: function () {
        this.setValue('');
        this.listPanel.down('list').hide(); //force list hide event
        this.listPanel.hide({
            type: 'fade',
            out: true,
            scope: this
        });
    },


    onListHide: function (cmp, opts) {
        var me = this,
        recordArray = this.listPanel.down('list').selected.items,
        valueStringArray = new Array(),
        v = 0,
        vNum = recordArray.length;
        for (v = 0; v < vNum; v++) {
            if (recordArray[v].data[this.getDisplayField()]) {
                valueStringArray.push(recordArray[v].data[this.getValueField()]);
            }
        }
        if (valueStringArray.length > 0) {
            me.setValue(valueStringArray.join(','));
            this.listPanel.down('list').deselectAll();
        } else {
            me.setValue(null);
        }
    }
});  
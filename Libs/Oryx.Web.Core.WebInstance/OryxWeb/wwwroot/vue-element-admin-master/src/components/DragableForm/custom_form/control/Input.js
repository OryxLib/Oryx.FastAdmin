export default (_self, h) => {
  return [
    h("el-input", {
      props: {
        value: _self.obj.value || ""
      },
      attrs: {
        maxlength: parseInt(_self.obj.maxlength) || 20,
        placeholder: _self.obj.placeholder || "这是一个输入框",
      },
      on: {
        "change": function(val) {
          //   if (!_self.obj.name) {
          //     return false;
          //   }
          _self.obj.value = event.currentTarget.value;
          _self.$emit('handleChangeVal', event.currentTarget.value)
        }
      }
    })
  ];
};


export let inputConf = {
  // 对应数据库内类型
  type: 'input',
  // 是否可配置
  config: true,
  // 控件左侧label内容
  label: '输入框',
  name: '',
  placeholder: '',
  // 最大长度
  maxlength: 20,
  value: '',
}

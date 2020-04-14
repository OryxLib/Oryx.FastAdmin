export default (_self, h) => {
  let icons = [];
  // 配置按钮
  if (!!_self.obj.config) {
    icons.push(h('i', {
      class:{
        "el-icon-setting":true
      },
      props: {
        type: 'gear-a',
      },
      on:{
        click() {
          console.log('emit click')
          _self.$emit('handleConfEle', _self.index);
        }
      },
      nativeOn: {
        click() {
          console.log('emit click')
          _self.$emit('handleConfEle', _self.index);
        }
      }
    }));
  }
  // 删除按钮
  icons.push(h('i', {
    class:{
      "el-icon-minus":true
    },
    props: {
      // type: 'el-icon-minus'
    },
    on:{
      click() {
        console.log('emit click')
        _self.$emit('handleRemoveEle', _self.index);
      }
    },
    nativeOn: {
      click() {
        _self.$emit('handleRemoveEle', _self.index);
      }
    }
  }));
  const item_icon = h('div', {
    class: {
      'item-icon': true
    }
  }, icons);
  return item_icon;
}

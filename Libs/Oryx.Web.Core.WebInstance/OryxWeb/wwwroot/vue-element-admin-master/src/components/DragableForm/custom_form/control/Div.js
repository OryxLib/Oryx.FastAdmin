export default (_self, h) => {
  return [
    h('div', {
      style: {
        'margin-bottom': _self.obj.marginBottom + 'px',
        'margin-top': _self.obj.marginTop + 'px',
        'color': _self.obj.color || "#000",
        'height':_self.obj.height,
        'width':_self.obj.width,
        'border':'1px solid black'
      },
      domProps: {
        innerHTML: _self.obj.label || "盒"
      }
    })
  ]
}

export const divConf = {
  config: true,
  label: '盒',
  color: '#000',
  marginTop: 0,
  marginBottom: 24,
  height:50,
  width:100
}
  
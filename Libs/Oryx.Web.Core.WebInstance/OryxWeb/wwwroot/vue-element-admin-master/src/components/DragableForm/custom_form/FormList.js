// import { inputConf } from "./control/Input";
// import { radioConf } from "./control/Radio";
// import { checkBoxConf } from "./control/CheckBox";

// const formList = {
//   input: inputConf,
//   radio: radioConf,
//   checkBox: checkBoxConf
// };
import { inputConf } from "./control/Input";
import { checkBoxConf } from "./control/CheckBox";
import { radioConf } from "./control/Radio";
import { hrConf } from "./control/Hr";
import { pConf } from "./control/P";

import { selectConf } from "./control/Select";
import { cascaderConf } from "./control/Cascader";
import { textConf } from "./control/Text";
import { titleConf } from "./control/Title";
import { uploadsConf } from './control/Uploads';
import { datePickerConf } from './control/DatePicker'
import { addressConf } from './control/Address';
import { divConf } from './control/Div';

const formList = {
  title: titleConf,
  hr: hrConf,
  p: pConf,
  input: inputConf,
  radio: radioConf,
  checkbox: checkBoxConf,

  select: selectConf, 
  datepicker: datePickerConf,
  cascader: cascaderConf,
  address: addressConf,
  uploads: uploadsConf,
  text: textConf,

  div:divConf
};
console.log('start formlist')
let list_arr = [];
for (let i in formList) {
  list_arr.push({
    ele: i,
    obj: formList[i]
  });
}
export default list_arr;

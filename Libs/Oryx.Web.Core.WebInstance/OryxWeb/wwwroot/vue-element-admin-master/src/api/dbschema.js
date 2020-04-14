import request from '@/utils/requestTrue'

export function loadTable(tableName) { 
    return request({
      url: '/db/DbTableSchema',
      method: 'get'
    }) 
}

export function deleteTable(id){
  return request({
    url:"/delete/DbTableSchema/"+id,
    method:'post'
  })
}

export function loadTableSchem(tableName){
  return request({
    url: `/db/schema/${tableName}`,
    method: 'get'
  })
}

export function loadTableFormMapping(tn1){ 
  return request({
    url: `/db/all/${tn1}`,
    method: 'post',
    data: 
      {
        table:'DbTableFormMapping',
        where :[
          ["Table","=",tn1]
        ]
      } 
  })
}

export function loadTableFormMapping1(tn1,tn2){ 
  return request({
    url: `/db/all/${tn1}`,
    method: 'post',
    data:[
      {
        table:'DbTableSchema'
      },
      {
        table:'DbTableFormMapping',
        where :[
          ["Table","=",tn2]
        ]
      }
    ]
  })
}

export function loadLableJson(id){
  if (id){
    return request({
      url:`/static/label.${id}.json`,
      method:'get'
    }) 
  }
  else
  {
    return request({
      url:`/static/label.json`,
      method:'get'
    }) 
  }
}

export function insertTableForm(data){
    var dataList = []
    data.forEach(item=>{
      dataList.push({
        "Table":item.bindTable,
        "Form": item.bindColumn,
        "FormConfig": JSON.stringify({
          ele:item.ele,
          obj: item.obj
        })
      })
    })
    return request({
      url:`/db/insert/DbTableFormMapping`,
      method:'post',
      data:dataList
    })
}
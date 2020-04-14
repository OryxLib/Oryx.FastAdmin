import request from '@/utils/requestTrue'
 
export function loadRoute() {
  return request({
    url: '/db/Route',
    method: 'get',
  })
}

export function postRoute(data) {
  return request({
    url: '/db/insert/Route',
    method:'post',
    data
  })
}

export function putRoute(data) {
  return request({
    url: '/db/update/Route',
    method: 'post',
    data:data
  })
}

export function deleteRoute(id){
  return request ( {
    url: '/db/delete/Route',
    method: 'post',
    data: {
      where:`id=${id}`
    }
  })
}
import request from '@/utils/requestTrue'
 
export function loadPage() {
  return request({
    url: '/db/Page',
    method: 'get'
  })
}
export function postPage(data) {
  return request({
    url: '/db/insert/Page',
    method: 'post',
    data
  })
}

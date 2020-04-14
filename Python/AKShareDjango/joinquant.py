# from jqdatasdk import *
# auth('15601222365','Linengneng123#')

# coding=utf-8
import requests,json
url="https://dataapi.joinquant.com/apis"
#获取调用凭证
body={       
    "method": "get_token",
    "mob": "15601222365",  #mob是申请JQData时所填写的手机号
    "pwd": "Linengneng123",  #Password为聚宽官网登录密码，新申请用户默认为手机号后6位
}
response = requests.post(url, data = json.dumps(body))
token=response.text
print (token)
#调用get_security_info获取单个标的信息
body={
    "method": "get_security_info",
    "token": token,
    "code": "502050.XSHG",
}
response = requests.post(url, data = json.dumps(body))
print (response.text)

 
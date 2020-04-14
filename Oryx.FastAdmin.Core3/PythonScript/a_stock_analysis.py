import sqlite3

db_conn = sqlite3.connect('stock_data.db')
 
from akshare.stock import zh_stock_a_sina as a_stock
#1.
#获取热门概念
gainianList = a_stock.get_zh_a_hot_gainian()
top1_hot_gainian = gainianList[0]
# print('top1hot_gainian type')
# print (top1_hot_gainian) 
 
#获取热门概念下的股票列表
gainian_stock_list = a_stock.get_zh_a_stock_byQueryList([top1_hot_gainian])

# print('top 1 stock item')
# print(gainian_stock_list[0])

#获取过去一年的股票数据
stockDataHistory = a_stock.stock_zh_a_daily(gainian_stock_list[0]['symbol'])
# print('历史数据')
# print(stockDataHistory)

#通过数学模型分析走势拐点

#2.
#输入指定股票代码

#获取过去一年的股票数据

#获取相关财报及新闻数据

#通过数学模型分析走势拐点

#匹配走势拐点与当期新闻及财报公告

#3.
#获取所有的股票代码

#获取过去一年的股票数据

#分析股票数据并活动拐点


#4. 
#数据库模型建立

#选用sqlite数据

#数据模型

#table stock 
#股票代码 股票概念 股票地区 

#table stock history data


#5. BP 神经网络实现
def testBPNN():
    from DataMiner.BPNN import bpnn

    nn = bpnn.BPNeuralNetwork()
    #nn.train()

    def test():
        cases = [
                [0, 0],
                [0, 1],
                [1, 0],
                [1, 1],
            ]
        labels = [[0], [0.5], [0.5], [0]]
        nn.setup(8, 5, 2)
        nn.train(cases, labels, 10000, 0.05, 0.1)
        for case in cases:
            print(nn.predict(case))
    test()        


    import matplotlib.pyplot as plt
    #X轴，Y轴数据
    x=[0,0.3,0.7,1]
    y=[0.03015244858425173,
    0.9640072675811764,
    0.9660646638773009,
    0.03636092800912747]

    plt.figure(figsize=(8,4)) #创建绘图对象
    plt.plot(x,y,linewidth=1)   #在当前绘图对象绘图（X轴，Y轴，蓝色虚线，线宽度）
    plt.xlabel("Time(s)") #X轴标签
    plt.ylabel("Volt")  #Y轴标签
    plt.title("Line plot") #图标题
    plt.show()  #显示图
testBPNN()
    
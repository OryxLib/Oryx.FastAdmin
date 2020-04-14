from numpy import *

class NaiveBayesClassifier(object):
    
    def __init__(self):
        self.dataMat = list()
        self.labelMat = list()
        self.pLabel1 = 0
        self.p0Vec = list()
        self.p1Vec = list()

    def loadDataSet(self,filename):
        fr = open(filename)
        for line in fr.readlines():
            lineArr = line.strip().split()
            dataLine = list()
            for i in lineArr:
                dataLine.append(float(i))
            label = dataLine.pop() # pop the last column referring to  label
            self.dataMat.append(dataLine)
            self.labelMat.append(int(label))


    def train(self):
        dataNum = len(self.dataMat)
        featureNum = len(self.dataMat[0])
        self.pLabel1 = sum(self.labelMat)/float(dataNum)
        p0Num = zeros(featureNum)
        p1Num = zeros(featureNum)
        p0Denom = 1.0
        p1Denom = 1.0
        for i in range(dataNum):
            if self.labelMat[i] == 1:
                p1Num += self.dataMat[i]
                p1Denom += sum(self.dataMat[i])
            else:
                p0Num += self.dataMat[i]
                p0Denom += sum(self.dataMat[i])
        self.p0Vec = p0Num/p0Denom
        self.p1Vec = p1Num/p1Denom

    def predict(self, data):
        p1 = reduce(lambda x, y: x * y, data * self.p1Vec) * self.pLabel1
        p0 = reduce(lambda x, y: x * y, data * self.p0Vec) * (1.0 - self.pLabel1)
        if p1 > p0:
            return 1
        else: 
            return 0

    def test(self):
        self.loadDataSet('testNB.txt')
        self.train()
        print(self.predict([1, 2]))

if __name__ == '__main__':
    NB =  NaiveBayesClassifier()
    NB.test()

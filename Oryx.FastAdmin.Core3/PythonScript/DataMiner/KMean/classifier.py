import KMean
from numpy import *


class KMeanCluster:
    def __init__(self):
        self.centers = []
        self.dataMat = []

    def loadDataSet(self, filename):
        dataMat = []
        fr = open(filename)
        for line in fr.readlines():
            curLine = line.strip().split()
            fltLine = map(float, curLine)
            dataMat.append(fltLine)
        self.dataMat = mat(dataMat)
        # print(self.dataMat)
        return self.dataMat

    def train(self, k=4):
        centers, assess = KMean.binKMeans(self.dataMat, k)
        self.centers = centers

    def predict(self, sample):
        return KMean.getBestClass(self.centers, sample)

    def test(self):
        dataMat = self.loadDataSet('testSet.txt')
        self.train()
        print("centers:")
        print(self.centers)
        bestCenter, center = KMean.getBestClass(self.centers, [1, 1])
        print("best class:")
        print(bestCenter)
        print(center)


if __name__ == '__main__':
    kmean = KMeanCluster()
    kmean.test()

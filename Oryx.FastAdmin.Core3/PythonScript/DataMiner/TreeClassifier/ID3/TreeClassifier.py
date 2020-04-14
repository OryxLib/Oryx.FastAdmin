import id3

class TreeClassifier(object): # ID3 Decision Tree  
    def __init__(self):
        self.dataSet = list()
        self.labels = list()
        self.tree = dict()

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
        if not (self.dataSet and self.labels):
            return dict()
        self.tree = id3.createTree(self.dataSet,self.labels)

    def predict(self,data):
        if not (self.dataSet and self.labels):
            return None
        return id3.classify(self.tree, self.labels, data)

    def test(self):
        self.dataSet = [[1, 1, 'yes'],
                   [1, 1, 'yes'],
                   [1, 0, 'no'],
                   [0, 1, 'no'],
                   [0, 1, 'no']]
        self.labels = ['no surfacing','flippers']
        self.train()
        print(self.tree)
        print( self.predict([1,1]) )         


if __name__ == '__main__':
    TC = TreeClassifier()
    TC.test()
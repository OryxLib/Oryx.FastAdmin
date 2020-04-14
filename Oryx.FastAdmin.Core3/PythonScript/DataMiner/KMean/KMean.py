from numpy import *


def distEclud(vecA, vecB):
    return sqrt(sum(power(vecA - vecB, 2)))


def randCent(dataSet, k):
    n = shape(dataSet)[1]
    centers = mat(zeros((k, n)))
    for j in range(n):
        minJ = min(dataSet[:, j])
        rangeJ = float(max(dataSet[:, j]) - minJ)
        centers[:, j] = mat(minJ + rangeJ * random.rand(k, 1))
    return centers


def kMeans(dataSet, k, distMethod=distEclud, createCent=randCent):
    m = shape(dataSet)[0]
    clusterAssess = mat(zeros((m, 2)))
    centers = createCent(dataSet, k)
    clusterChanged = True
    while clusterChanged:
        clusterChanged = False
        for i in range(m):  # for each sample
            # get closest center
            minDist = inf
            minIndex = -1
            for j in range(k):  # for each class
                dist = distMethod(centers[j, :], dataSet[i, :])
                if dist < minDist:
                    minDist = dist
                    minIndex = j
            if clusterAssess[i, 0] != minIndex:
                clusterChanged = True
            clusterAssess[i, :] = minIndex, minDist ** 2
        # update center
        for cent in range(k):
            ptsInClust = dataSet[nonzero(clusterAssess[:, 0].A == cent)[0]]
            centers[cent, :] = mean(ptsInClust, axis=0)
    return centers, clusterAssess


def binKMeans(dataSet, k, distMethod=distEclud):
    m = shape(dataSet)[0]
    clusterAssess = mat(zeros((m, 2)))
    originCenters = mean(dataSet, axis=0).tolist()[0]
    centers = [originCenters]
    # get origin error
    for j in range(m):
        clusterAssess[j, 1] = distMethod(mat(originCenters), dataSet[j, :]) ** 2
    # try to cluster
    while (len(centers) < k):
        # get best spilt
        minError = inf
        for i in range(len(centers)):
            ptsInCurrCluster = dataSet[nonzero(clusterAssess[:, 0].A == i)[0], :]
            splitCenter, splitAssess = kMeans(ptsInCurrCluster, 2, distMethod)
            spiltError = sum(splitAssess[:, 1])
            formerError = sum(clusterAssess[nonzero(clusterAssess[:, 0].A != i)[0], 1])
            if (spiltError + formerError) < minError:
                bestCentToSplit = i
                bestNewCents = splitCenter
                bestClustAss = splitAssess.copy()
                minError = spiltError + formerError
        # update assessment
        bestClustAss[nonzero(bestClustAss[:, 0].A == 1)[0], 0] = len(centers)
        bestClustAss[nonzero(bestClustAss[:, 0].A == 0)[0], 0] = bestCentToSplit
        # update global centers and assessment
        centers[bestCentToSplit] = bestNewCents[0, :].tolist()[0]
        centers.append(bestNewCents[1, :].tolist()[0])
        clusterAssess[nonzero(clusterAssess[:, 0].A == bestCentToSplit)[0], :] = bestClustAss
    return centers, clusterAssess


def getBestClass(centers, sample, distMethod=distEclud):
    centers = mat(centers)
    m = shape(centers)[0]
    sample = mat(sample)
    bestCenter = -1
    minDist = inf
    for i in range(m):
        dist = distMethod(centers[i, :], sample)
        if dist < minDist:
            minDist = dist
            bestCenter = i
    return bestCenter, centers[bestCenter]


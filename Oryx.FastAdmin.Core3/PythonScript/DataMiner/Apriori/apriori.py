from numpy import *


def createUnit(dataSet):  # create cell with one element
    universe = []
    for cell in dataSet:
        for item in cell:
            if not [item] in universe:
                universe.append([item])
    return map(frozenset, universe)


def filterCandidates(dataSet, candidates, limit):
    cellCount = {}
    for cell in dataSet:
        for candidate in candidates:
            if candidate.issubset(cell):
                if not candidate in cellCount:
                    cellCount[candidate] = 1
                else:
                    cellCount[candidate] += 1
    cellNum = len(dataSet)
    selected = []
    supports = {}
    for cell in cellCount:
        support = float(cellCount[cell]) / cellNum
        if support >= limit:
            selected.insert(0, cell)
        supports[cell] = support
    return selected, supports


def createKCell(origins, k):
    cells = []
    originCount = len(origins)
    for i in range(originCount):
        for j in range(i + 1, originCount):
            list1 = list(origins[i])[:k - 2]
            list2 = list(origins[j])[:k - 2]
            list1.sort()
            list2.sort()
            if list1 == list2:  # if first k-2 elements are equal
                cells.append(origins[i] | origins[j])  # set union
    return cells


def apriori(dataMat, limit=0.5):
    units = createUnit(dataMat)
    dataSet = map(set, dataMat)
    origin, supports = filterCandidates(dataSet, units, limit)
    candidates = [origin]
    k = 2
    while (len(candidates[k - 2]) > 0):
        cellK = createKCell(candidates[k - 2], k)
        cellK, supportK = filterCandidates(dataSet, cellK, limit)
        supports.update(supportK)
        candidates.append(cellK)
        k += 1
    return candidates, supports


def generateRules(cells, supports, limit=0.7):
    bigRuleList = []
    for i in range(1, len(cells)):
        for cell in cells[i]:
            consequences = [frozenset([item]) for item in cell]
            if i > 1:
                rulesFromConseq(cell, consequences, supports, bigRuleList, limit)
            else:
                filterRules(cell, consequences, supports, bigRuleList, limit)
    return bigRuleList


def filterRules(cells, consequences, supports, bigRuleList, limit=0.7):
    prunedConsequences = []
    for consequence in consequences:
        confidence = supports[cells] / supports[cells - consequence]  # calc confidence
        if confidence >= limit:
            rule = (cells - consequence, consequence, confidence)
            bigRuleList.append(rule)
            prunedConsequences.append(consequence)
    return prunedConsequences


def rulesFromConseq(cells, consequences, supports, bigRuleList, limit=0.7):
    m = len(consequences[0])
    if len(cells) > (m + 1):  # try further merging
        new_consequences = createKCell(consequences, m + 1)  # create Hm+1 new candidates
        new_consequences = filterRules(cells, new_consequences, supports, bigRuleList, limit)
        if len(new_consequences) > 1:  # need at least two sets to merge
            rulesFromConseq(cells, new_consequences, supports, bigRuleList, limit)


def test():
    dataSet = [[1, 3, 4], [2, 3, 5], [1, 2, 3, 5], [2, 5]]
    cells, supports = apriori(dataSet, 0.5)
    # print(cells)
    rules = generateRules(cells, supports)
    print(rules)
    # units = createUnit(dataSet)
    # print(units)
    # cells, supports = filterCandidates(dataSet, units, 0.5)
    # print(cells, supports)
    # cells = createKCell(selected, 2)
    # print(cells)

if __name__ == '__main__':
    test()

﻿# 聚落建筑设计文档

聚落中的建筑是通用的，但是可能会随等级和聚落拥有资源解锁

聚落所有者可以看到聚落建筑所有相关信息，非所有者只能看到是否有该建筑

## 建筑种类

农田，粮仓

### 农田

每个聚落都可以拥有农田，但是会根据聚落所处地形地貌的区别设置不同的农田上限，农田直接决定了每个聚落的粮食产量。

### 粮仓

每个聚落都可以修建粮仓，粮仓决定了聚落可以储存多少粮食。

每日8时结算粮食，先计算增加的再计算消耗的，最后更新粮仓库存。如果是被围城期间，则粮食不会增加。

粮食增加量=农田数量* 每块农田产出

粮食消耗量=聚落人口数量* 每个人的粮食消耗

如果每日结算粮食时发现粮食不够，就进行饥荒判定，如果饥荒率在0.4以下，则暂无影响，如果饥荒率在0.4以上，则按比例减少人口，最多在饥荒率到1的时候减少百分之十的人口

饥荒率=粮食缺额/需要的总粮食

粮食数量，粮食储量，农田数量以及各自对应上限只能聚落所有者看到
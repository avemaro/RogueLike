using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker {


    public static Enemy PopEnemy(Floor floor, Cell cell) {
        var rand = Random.Range(0, 256);
        var accum = 0;
        foreach (var (ID, prob) in EnemyData.GetProbs(floor.NumberOfStairs)) {
            accum += prob;
            if (rand < accum) return Create(floor, cell, ID);
        }
        throw new System.Exception();
    }

    public static Enemy Create(Floor floor, Cell cell, char ID) {
        var data = EnemyData.GetData(ID);
        return new Enemy(floor, cell, data.Name, ID, data.Spec);
    }
}

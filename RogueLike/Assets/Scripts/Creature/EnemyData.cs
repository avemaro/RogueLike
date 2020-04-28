using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData {
    public static EnemyData GetData(char ID) {
        var found = data.Find(d => d.ID == ID);
        return new EnemyData(found.name, found.spec);
    }

    public string Name { get; private set; }
    public int[] Spec;

    private EnemyData(string name, params int[] spec) {
        Name = name;
        Spec = spec;
    }

    static readonly List<(char ID, int prob)[]> probs =
        new List<(char ID, int prob)[]>();
    static List<(char ID, string name, int[] spec)> data = new List<(char ID, string name, int[] spec)>();

    public static void AddData(char ID, string name, params int[] spec) {
        data.Add((ID, name, spec));
    }

    public static void AddDistribution(params int[] prob) {
        var list = new List<(char, int)>();

        for (var i = 0; i < prob.Length; i++)
            list.Add((data[i].ID, prob[i]));

        probs.Add(list.ToArray());
    }

    public static (char, int)[] GetProbs(int numberOfStairs) {
        return probs[numberOfStairs - 1];
    }
}

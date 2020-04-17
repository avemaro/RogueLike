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

    public static readonly (char ID, int prob)[] probs = {
        ('マ', 85), ('小', 86), ('豆', 85)
    };

    static List<(char ID, string name, int[] spec)> data = new List<(char ID, string name, int[] spec)>();

    public static void InitData() {
        AddData('マ', "", 5, 2, 2, 1);
        AddData('小', "", 6, 3, 3, 4);
        AddData('豆', "", 6, 5, 4, 1);
    }

    public static void AddData(char ID, string name, params int[] spec) {
        data.Add((ID, name, spec));
    }
}

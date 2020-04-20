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

    public static readonly List<(char ID, int prob)[]> probs = new List<(char ID, int prob)[]>();
    //    = {
    //    ('マ', 85), ('小', 86), ('豆', 85)
    //};

    static List<(char ID, string name, int[] spec)> data = new List<(char ID, string name, int[] spec)>();

    public static void InitData() {
        AddData('マ', "", 5, 2, 2, 1);
        AddData('小', "", 6, 3, 3, 4);
        AddData('豆', "", 6, 5, 4, 1);
        AddData('ボ', "", 7, 4, 4, 10);
        AddData('お', "", 8, 8, 5, 10);
        AddData('に', "", 10, 10, 7, 4);
        AddData('ガ', "", 10, 5, 0, 7);
        AddData('畠', "", 12, 12, 8, 11);
        AddData('ハ', "", 11, 10, 10, 9);
        AddData('鬼', "", 10, 15, 7, 17);
        AddData('ぴ', "", 5, 7, 2, 14);
        AddData('死', "", 25, 30, 15, 8);
        AddData('ガ', "", 23, 15, 14, 11);
        AddData('コ', "", 25, 40, 17, 9);
        AddData('デ', "", 25, 23, 9, 9);
        AddData('ぬ', "", 35, 20, 0, 6);
        AddData('兵', "", 18, 10, 23, 12);
        AddData('や', "", 4, 15, 9, 32);

        AddProb(('マ', 85), ('小', 86), ('豆', 85));
        AddProb(('ボ', 59), ('小', 58), ('豆', 59), ('お', 59), ('に', 14), ('ガ', 7));
        AddProb(('畠', 70), ('ハ', 71), ('鬼', 71), ('に', 18), ('ガ', 9), ('ぴ', 17));
        AddProb(('死', 52), ('ガ', 52), ('コ', 52), ('デ', 53), ('ガ', 7), ('ぴ', 13),
            ('ぬ', 13), ('兵', 7), ('や', 7));
    }

    public static void AddData(char ID, string name, params int[] spec) {
        data.Add((ID, name, spec));
    }

    public static void AddProb(params (char ID, int prob)[] prob) {
        probs.Add(prob);
    }

    public static (char ID, int prob)[] GetProbs(int numberOfStairs) {
        if (numberOfStairs >= 7) return probs[3];
        if (numberOfStairs >= 5) return probs[2];
        if (numberOfStairs >= 3) return probs[1];
        return probs[0];
    }
}

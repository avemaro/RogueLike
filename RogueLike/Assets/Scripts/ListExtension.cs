using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtension {
    public static T GetAtRandom<T>(this List<T> list) {
        if (list.Count == 0) {
            Debug.LogError("リストが空です！");
        }

        return list[Random.Range(0, list.Count)];
    }
}

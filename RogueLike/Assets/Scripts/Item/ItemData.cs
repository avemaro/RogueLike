using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData {
    public ItemType Type { get; private set; }
    public int[] Spec;
    public static ItemData GetData(string name) {
        throw new NotImplementedException();
    }
}

public enum ItemType {
    weapon, shield, arrow, riceBall, bracelet, scroll, wand, drag, pot
}
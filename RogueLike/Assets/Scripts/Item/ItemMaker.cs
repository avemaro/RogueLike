using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemMaker {
    public static Item Create(string name) {
        return new Drag();
    }
}
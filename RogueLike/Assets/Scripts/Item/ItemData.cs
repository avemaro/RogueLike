using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData {
    static public List<(string name, int prob)> weapons = new List<(string name, int prob)>();
    static public List<(string name, int prob)> shields = new List<(string name, int prob)>();
    static public List<(string name, int prob)> arrows = new List<(string name, int prob)>();
    static public List<(string name, int prob)> bracelets = new List<(string name, int prob)>();
    static public List<(string name, int prob)> drags = new List<(string name, int prob)>();
    static public List<(string name, int prob)> foods = new List<(string name, int prob)>();
    static public List<(string name, int prob)> scrolls = new List<(string name, int prob)>();
    static public List<(string name, int prob)> wands = new List<(string name, int prob)>();
    static public List<(string name, int prob)> pots = new List<(string name, int prob)>();

    public ItemType Type { get; private set; }
    public string Name { get; private set; }
    public int[] Spec;

    private ItemData(ItemType type, string name, params int[] spec) {
        Type = type;
        Name = name;
        Spec = spec;
    }

    static List<(ItemType type, string name, int[] spec)> data = new List<(ItemType type, string name, int[] spec)>();

    public static ItemData GetData(string name) {
        var found = data.Find(d => d.name == name);
        //Debug.Log(found);
        return new ItemData(found.type, found.name, found.spec);
    }

    public static void AddData(ItemType type, string name, params int[] spec) {
        data.Add((type, name, spec));
        if (type == ItemType.weapon) weapons.Add((name, spec[4]));
        if (type == ItemType.shield) shields.Add((name, spec[4]));
        if (type == ItemType.arrow) arrows.Add((name, spec[4]));
        if (type == ItemType.bracelet) bracelets.Add((name, spec[4]));
        if (type == ItemType.drag) drags.Add((name, spec[6]));
        if (type == ItemType.food) foods.Add((name, spec[6]));
        if (type == ItemType.wand) wands.Add((name, spec[0]));
        if (type == ItemType.scroll) scrolls.Add((name, spec[0]));
        if (type == ItemType.pot) pots.Add((name, spec[0]));
    }
}

public enum ItemType {
    none, weapon, shield, bracelet, arrow, food, scroll, wand, drag, pot
}
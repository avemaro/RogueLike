﻿using System;
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

    static List<(ItemType type, string name, string description, int[] spec)> data = new List<(ItemType, string, string, int[])>();

    public static ItemData GetData(string name) {
        var found = data.Find(d => d.name == name);
        return new ItemData(found.type, found.name, found.spec);
    }

    public static void AddData(string typeName, string name, string description, params int[] spec) {
        var type = GetType(typeName);
        data.Add((type, name, description, spec));
        if (type == ItemType.weapon) weapons.Add((name, spec[9]));
        if (type == ItemType.shield) shields.Add((name, spec[9]));
        if (type == ItemType.arrow) arrows.Add((name, spec[9]));
        if (type == ItemType.bracelet) bracelets.Add((name, spec[9]));
        if (type == ItemType.drag) drags.Add((name, spec[9]));
        if (type == ItemType.food) foods.Add((name, spec[9]));
        if (type == ItemType.wand) wands.Add((name, spec[9]));
        if (type == ItemType.scroll) scrolls.Add((name, spec[9]));
        if (type == ItemType.pot) pots.Add((name, spec[9]));
    }

    public static string GetDescription(Item item) {
        var (type, name, description, spec) = data.Find(d => d.name == item.Name);
        return description;
    }

    public static ItemType GetType(string type) {
        switch (type) {
            case "weapon": return ItemType.weapon;
            case "shield": return ItemType.shield;
            case "bracelet": return ItemType.bracelet;
            case "arrow": return ItemType.arrow;
            case "food": return ItemType.food;
            case "scroll": return ItemType.scroll;
            case "wand": return ItemType.wand;
            case "drag": return ItemType.drag;
            case "pot": return ItemType.pot;
            default: return ItemType.none;
        }
    }
}

public enum ItemType {
    none, weapon, shield, bracelet, arrow, food, scroll, wand, drag, pot
}